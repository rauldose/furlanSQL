using FurlanSql.Engine.Constants;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State.Query
{
    public class SelectColumnsState : AbstractState
    {
		public SelectColumnsState(QueryInfo queryInfo): base(queryInfo)
		{
	
		}

        public override AbstractState TransitionToNextState(string token)
        {
			if (token.EqualsIgnoreCase(Keywords.ASTERISK_KEYWORD))
			{
				// Token is "*" (all columns). We proceed to from keyword
				queryInfo.AddColumnName("*");
				return new GreedyMatchKeywordState(queryInfo, Keywords.FROM_KEYWORDS,
						q2 => new AnyTokenConsumerState(q2, q2.SetTableName, (q) => new OptionalWhereState(q)), 0);
				//return new GreedyMatchKeywordState(queryInfo, new string[] { Keywords.ASTERISK_KEYWORD },
				//		q=> new GreedyMatchKeywordState(q, Keywords.FROM_KEYWORDS,
				//				q2 => new AnyTokenConsumerState(q2, q2.SetTableName, (q) => new OptionalWhereState(q)), 0));
			}
			else
			{
				// Token is a column name, we continue until there are none
				queryInfo.AddColumnName(token);
				return new CommaSeparedValuesState(queryInfo, queryInfo.GetColumnNames(), Keywords.FROM_KEYWORDS[0],
						"%COLUMN_NAME%", q => new GreedyMatchKeywordState(queryInfo, Keywords.FROM_KEYWORDS,
								q2 => new AnyTokenConsumerState(q2, q2.SetTableName, (q) => new OptionalWhereState(q))));
			}
		}
    }
}
