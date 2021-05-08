using FurlanSql.Engine.Constants;
using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State.Where
{
    public class WhereValueState : AbstractState
    {
        private WhereCondition condition;

        public WhereValueState(QueryInfo queryInfo, WhereCondition condition) : base(queryInfo)
        {
            this.condition = condition;
        }


        public override AbstractState TransitionToNextState(string token)
        {
            try
            {
                if (token.EqualsIgnoreCase(Keywords.NULL_KEYWORD))
                {
                    condition.SetValue("NULL");
                }
                else
                {
                    condition.SetValue(token);
                }
                queryInfo.AddWhereCondition(condition);
                return new WhereJoinState(queryInfo);
            }
            catch (Exception e)
            {
                throw new RamengoException(e);
            }
        }

    }
}
