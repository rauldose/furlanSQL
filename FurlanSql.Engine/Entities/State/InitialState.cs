using FurlanSql.Engine.Constants;
using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Entities.State.Query;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static FurlanSql.Engine.Entities.Model.QueryInfo;

namespace FurlanSql.Engine.Entities.State
{
    public class InitialState : AbstractState
    {
        public InitialState() : base(new QueryInfo())
        {

        }

        public override AbstractState TransitionToNextState(string token)
        {
            if (Keywords.SELECT_KEYWORDS.ToList().Contains(token))
            {
                queryInfo.SetType(QueryType.SELECT);
                return new GreedyMatchKeywordState(queryInfo, Keywords.SELECT_KEYWORDS, (q) => new SelectColumnsState(q));
                //return new SelectColumnsState(queryInfo);
            }
            if (Keywords.UPDATE_KEYWORDS.Contains(token))
            {
                queryInfo.SetType(QueryType.UPDATE);
                return new GreedyMatchKeywordState(queryInfo, Keywords.UPDATE_KEYWORDS,
                     q => new AnyTokenConsumerState(queryInfo, queryInfo.SetTableName,
                        q1 => new SingleTokenMatchState(q1, Keywords.SET_KEYWORD,
                                q2 => new AnyTokenConsumerState(q2, q2.AddColumnName,
                                        q3 => new SingleTokenMatchState(q3, Keywords.SET_EQUAL_KEYWORD,
                                                q4 => new AnyTokenConsumerState(q4, q4.AddValue, (q4) => new UpdateSetState(q4)))))));
            }
            if (token.EqualsIgnoreCase(Keywords.DELETE_KEYWORDS[0]))
            {
                queryInfo.SetType(QueryType.DELETE);
                return new GreedyMatchKeywordState(queryInfo, Keywords.DELETE_KEYWORDS,
                        q => new GreedyMatchKeywordState(q, Keywords.FROM_KEYWORDS,
                                q2 => new AnyTokenConsumerState(q2, q2.SetTableName, (q2) => new OptionalWhereState(q2)), 0));
            }
            if (token.EqualsIgnoreCase(Keywords.INSERT_KEYWORDS[0]))
            {
                queryInfo.SetType(QueryType.INSERT);
                return new GreedyMatchKeywordState(queryInfo, Keywords.INSERT_KEYWORDS,
                        q => new AnyTokenConsumerState(q, q.SetTableName,
                                q2 => new CommaSeparedValuesState(q2, q2.GetColumnNames(), Keywords.VALUES_KEYWORD,
                                        "%COLUMN_NAME%", true, false, q3 => new CommaSeparedValuesState(q3, q3.GetValues(),
                                                null, "%VALUE%", true, true, (q3) => new FinalState(q3)))));
            }
            if (token.EqualsIgnoreCase(Keywords.COMMIT_KEYWORDS[0]))
            {
                queryInfo.SetType(QueryType.COMMIT);
                return new GreedyMatchKeywordState(queryInfo, Keywords.COMMIT_KEYWORDS, (q) => new FinalState(q));
            }
            if (token.EqualsIgnoreCase(Keywords.ROLLBACK_KEYWORD))
            {
                queryInfo.SetType(QueryType.ROLLBACK);
                return new FinalState(queryInfo);
            }
            if (token.EqualsIgnoreCase(Keywords.BEGIN_TRANSACTION_KEYWORDS[0]))
            {
                queryInfo.SetType(QueryType.BEGIN_TRANSACTION);
                return new GreedyMatchKeywordState(queryInfo, Keywords.BEGIN_TRANSACTION_KEYWORDS, (q) => new FinalState(q));
            }
            throw new RamengoException(new List<string>{Keywords.SELECT_KEYWORDS[0], Keywords.UPDATE_KEYWORDS[0],
                    Keywords.INSERT_KEYWORDS[0], Keywords.DELETE_KEYWORDS[0], Keywords.BEGIN_TRANSACTION_KEYWORDS[0],
                    Keywords.COMMIT_KEYWORDS[0], Keywords.ROLLBACK_KEYWORD}, token);
        }
    }
}
