using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State
{
    /// <summary>
    /// State that proceeds to the next one if all the given keywords are matched, otherwise it throws an exception.
    /// </summary>
    public class GreedyMatchKeywordState : AbstractState
    {
        private int currentIndex = 1;
        private string[] keywords;
        private Func<QueryInfo, AbstractState> nextStateTransition;

        public GreedyMatchKeywordState(QueryInfo queryInfo, string[] keywords,
                Func<QueryInfo, AbstractState> nextStateTransition) : base(queryInfo)
        {

            this.keywords = keywords;
            this.nextStateTransition = nextStateTransition;
        }

        public GreedyMatchKeywordState(QueryInfo queryInfo, string[] keywords,
                Func<QueryInfo, AbstractState> nextStateTransition, int currentIndex) : base(queryInfo)
        {
            this.keywords = keywords;
            this.nextStateTransition = nextStateTransition;
            this.currentIndex = currentIndex;
        }

        public override AbstractState TransitionToNextState(string token)
        {
            if (token.EqualsIgnoreCase(keywords[currentIndex]))
            {
                currentIndex++;
                if (currentIndex == keywords.Length)
                {
                    return nextStateTransition?.Invoke(queryInfo);
                }
                return this;
            }
            throw new RamengoException(keywords[currentIndex], token);
        }

    }
}
