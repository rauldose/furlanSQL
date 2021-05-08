using FurlanSql.Engine.Entities.Exceptions;
using FurlanSql.Engine.Entities.Model;
using FurlanSql.Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State
{
    public class CommaSeparedValuesState : AbstractState
    {
		private bool lastWasComma = false;
		private List<string> collector;
		private string nextToken;
		private Func<QueryInfo, AbstractState> transitionFunction;
		private string expectedToken;
		private bool canBeFinalState = false;
		private bool optionalValues = false;

		public CommaSeparedValuesState(QueryInfo queryInfo, List<String> collector, String nextToken, String expectedToken,
				Func<QueryInfo, AbstractState> transitionFunction): base(queryInfo)
		{
			this.collector = collector;
			this.nextToken = nextToken;
			this.transitionFunction = transitionFunction;
			this.expectedToken = expectedToken;
		}

		public CommaSeparedValuesState(QueryInfo queryInfo, List<String> collector, String nextToken, String expectedToken,
				bool lastWasComma, bool canBeFinalState, Func<QueryInfo, AbstractState> transitionFunction): this(queryInfo, collector, nextToken, expectedToken, transitionFunction)
		{
			// Used when the first token is not consumed by the previous state.
			this.lastWasComma = lastWasComma;
			this.canBeFinalState = canBeFinalState;
			this.optionalValues = true;
		}

		public override AbstractState TransitionToNextState(string token)
        {
			if (token.Equals(","))
			{
				if (lastWasComma)
				{
					// Case ", ,"
					throw new RamengoException(expectedToken, token);
				}
				else
				{
					// Case "%expectedToken% ,"
					lastWasComma = true;
					return this;
				}
			}

			// Case ", %expectedToken%"
			if (lastWasComma)
			{
				if (optionalValues && token.EqualsIgnoreCase(nextToken))
				{
					return transitionFunction?.Invoke(queryInfo);
				}
				else
				{
					optionalValues = false;
				}
				collector.Add(token);
				lastWasComma = false;
				return this;
			}

			// Case "%expectedToken% %nextToken%"
			if (token.EqualsIgnoreCase(nextToken))
			{
				return transitionFunction?.Invoke(queryInfo);
			}

			throw new RamengoException(new List<string> { ",", nextToken }, token);
        }

		public override bool IsFinalState()
		{
			return canBeFinalState;
		}
	}
}
