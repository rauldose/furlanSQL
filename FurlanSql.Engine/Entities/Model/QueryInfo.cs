using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.Model
{
	public class QueryInfo
	{

		public enum QueryType
		{
			SELECT, UPDATE, DELETE, INSERT, COMMIT, BEGIN_TRANSACTION, ROLLBACK
		}

		private QueryType type;

		private string tableName;

		private List<string> columnNames = new List<string>();

		private List<string> values = new List<string>();

		private List<WhereCondition> whereConditions = new List<WhereCondition>();

		private List<string> joinedTables = new List<string>();

		private List<string> whereConditionsJoinOperators = new List<string>();

		public QueryType GetQueryType()
		{
			return type;
		}

		public void SetType(QueryType type)
		{
			this.type = type;
		}

		public string GetTableName()
		{
			return tableName;
		}

		public void SetTableName(string tableName)
		{
			this.tableName = tableName;
		}

		public List<string> GetColumnNames()
		{
			return columnNames;
		}

		public void AddColumnName(string columnName)
		{
			this.columnNames.Add(columnName);
		}

		public List<string> GetValues()
		{
			return values;
		}

		public void AddValue(string value)
		{
			this.values.Add(value);
		}

		public List<WhereCondition> GetWhereConditions()
		{
			return whereConditions;
		}

		public void AddWhereCondition(WhereCondition whereCondition)
		{
			this.whereConditions.Add(whereCondition);
		}

		public List<string> GetJoinedTables()
		{
			return joinedTables;
		}

		public void AddJoinedTable(string joinedTable)
		{
			this.joinedTables.Add(joinedTable);
		}

		public List<string> GetWhereConditionsJoinOperators()
		{
			return whereConditionsJoinOperators;
		}

		public void AddWhereConditionsJoinOperator(string whereConditionsJoinOperator)
		{
			this.whereConditionsJoinOperators.Add(whereConditionsJoinOperator);
		}
	}
}