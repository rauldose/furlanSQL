using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Entities.State;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static FurlanSql.Engine.Entities.Model.QueryInfo;

namespace FurlanSql.Engine
{
    public class FurlanSqlInterpreter
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public FurlanSqlInterpreter(IDbConnection connection)
        {
            _connection = connection;
            _transaction = null;
        }

        public static string ToSqlQuery(string furlanQuery)
        {
            QueryInfo info = ParseQuery(furlanQuery);
            return BuildSqlQuery(info);
        }

        private static QueryInfo ParseQuery(string query)
        {
            AbstractState currentState = new InitialState();
            // TODO: bug: whitespaces inside quotes should be ignored
            List<string> tokens = query.SplitAndKeep(" ,".ToCharArray()).ToList();
            foreach (var token in tokens)
            {
                if (token.Trim().IsEmpty()) continue;

                currentState = currentState.TransitionToNextState(token.Trim());
            }

            if (!currentState.IsFinalState())
            {
                throw new RamengoException("Unexpected end of query");
            }

            return currentState.GetQueryInfo();
        }

        private static string BuildSqlQuery(QueryInfo queryInfo)
        {
            switch (queryInfo.GetQueryType())
            {
                case QueryType.SELECT:
                    return BuildSelectQuery(queryInfo);
                case QueryType.INSERT:
                    return BuildInsertQuery(queryInfo);
                case QueryType.UPDATE:
                    return BuildUpdateQuery(queryInfo);
                case QueryType.DELETE:
                    return BuildDeleteQuery(queryInfo);
                case QueryType.BEGIN_TRANSACTION:
                    return "START_TRANSACTION";
                case QueryType.COMMIT:
                    return "COMMIT";
                case QueryType.ROLLBACK:
                    return "ROLLBACK";
            }
            throw new RamengoException("Unrecognised query");
        }

        private static string BuildDeleteQuery(QueryInfo queryInfo)
        {
            return "DELETE FROM " + queryInfo.GetTableName() + BuildWhereClause(queryInfo);
        }

        private static string BuildWhereClause(QueryInfo queryInfo)
        {
            List<WhereCondition> whereConditions = queryInfo.GetWhereConditions();
            List<string> whereConditionsJoinOperators = queryInfo.GetWhereConditionsJoinOperators();
            if (whereConditions.Count() == 0)
            {
                return "";
            }
            StringBuilder whereClause = new StringBuilder(" WHERE ");
            for (int i = 0; i < whereConditions.Count(); i++)
            {
                WhereCondition whereCondition = whereConditions[i];
                whereClause.Append(whereCondition.GetField()).Append(" ").Append(whereCondition.GetOperator()).Append(" ").Append(whereCondition.GetValue());
                if (whereConditionsJoinOperators.Count() >= i + 1)
                {
                    whereClause.Append(" ").Append(whereConditionsJoinOperators[i]).Append(" ");
                }
            }
            return whereClause.ToString();
        }

        private static string BuildUpdateQuery(QueryInfo queryInfo)
        {
            StringBuilder query = new StringBuilder("UPDATE ").Append(queryInfo.GetTableName()).Append(" SET ");
            for (int i = 0; i < queryInfo.GetColumnNames().Count(); i++)
            {
                query.Append(queryInfo.GetColumnNames()[i]).Append(" = ").Append(queryInfo.GetValues()[i])
                        .Append(", ");
            }
            query.Remove(query.Length - 2, 2);
            return query.ToString() + BuildWhereClause(queryInfo);
        }


        private static string BuildInsertQuery(QueryInfo queryInfo)
        {
            StringBuilder query = new StringBuilder("INSERT INTO ").Append(queryInfo.GetTableName());
            if (queryInfo.GetColumnNames().Count != 0)
            {
                query.Append(" ( ").Append(string.Join(", ", queryInfo.GetColumnNames().ToList()))
                        .Append(" )");
            }
            query.Append(" VALUES ( ").Append(string.Join(", ", queryInfo.GetValues().ToList()))
                    .Append(" )");
            return query.ToString();
        }

        private static string BuildSelectQuery(QueryInfo queryInfo)
        {
            string query = "SELECT ";

            // Column names
            query += string.Join(", ", queryInfo.GetColumnNames().ToList());

            // Table name
            query += " FROM " + queryInfo.GetTableName();

            // Joins
            List<String> joinedTables = queryInfo.GetJoinedTables();
            if (joinedTables.Count() != 0)
            {
                query += " INNER JOIN " + string.Join(" INNER JOIN ", joinedTables.ToList());
            }
            // Where
            query += BuildWhereClause(queryInfo);

            return query;
        }

        public FurlanSqlResult Execute(string furlanSqlQuery)
        {
            QueryInfo queryInfo = ParseQuery(furlanSqlQuery);
            string sqlQuery = BuildSqlQuery(queryInfo);
            FurlanSqlResult result = new FurlanSqlResult();

            // Executes the query
            try
            {
                IDbCommand statement = _connection.CreateCommand();
                switch (queryInfo.GetQueryType())
                {
                    case QueryType.SELECT:
                        statement.CommandText = sqlQuery;
                        IDataReader resultSet = statement.ExecuteReader();
                        result.SetResultSet(resultSet);
                        break;
                    case QueryType.BEGIN_TRANSACTION:
                        _transaction = _connection.BeginTransaction();
                        break;
                    case QueryType.COMMIT:
                        _transaction?.Commit();
                        _transaction?.Dispose();
                        break;
                    case QueryType.DELETE:
                    case QueryType.UPDATE:
                    case QueryType.INSERT:
                        statement.CommandText = sqlQuery;
                        int updatedRows = statement.ExecuteNonQuery();
                        result.SetAffectedRows(updatedRows);
                        break;
                    case QueryType.ROLLBACK:
                        _transaction?.Rollback();
                        _transaction?.Dispose();
                        break;
                }
            }
            catch (SqlException e)
            {
                throw new RamengoException(e);
            }
            return result;
        }

    }
}
