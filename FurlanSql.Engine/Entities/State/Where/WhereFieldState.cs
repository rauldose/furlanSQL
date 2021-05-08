using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State.Where
{
    public class WhereFieldState : AbstractState
    {
        public WhereFieldState(QueryInfo queryInfo) : base(queryInfo)
        {

        }

        public override AbstractState TransitionToNextState(String token)
        {
            try
            {
                return new WhereOperatorState(queryInfo, new WhereCondition(token));
            }

            catch (Exception e)
            {
                throw new RamengoException(e);
            }
        }

    }
}
