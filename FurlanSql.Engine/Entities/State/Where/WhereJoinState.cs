using FurlanSql.Engine.Constants;
using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State.Where
{
    public class WhereJoinState : AbstractState
    {
        public WhereJoinState(QueryInfo queryInfo) : base(queryInfo)
        {

        }
        public override AbstractState TransitionToNextState(string token)
        {
            if (token.EqualsIgnoreCase(Keywords.AND_KEYWORD))
            {
                queryInfo.AddWhereConditionsJoinOperator("AND");
                return new WhereFieldState(queryInfo);
            }
            if (token.EqualsIgnoreCase(Keywords.OR_KEYWORD))
            {
                queryInfo.AddWhereConditionsJoinOperator("OR");
                return new WhereFieldState(queryInfo);
            }
            throw new RamengoException(new List<string> { Keywords.AND_KEYWORD, Keywords.OR_KEYWORD }, token);
        }

        public override bool IsFinalState()
        {
            return true;
        }
    }
}
