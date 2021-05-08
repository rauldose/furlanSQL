using FurlanSql.Engine.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.State
{
	/// <summary>
	/// Base class for any state used by the FurlanSql parser.
	/// </summary>
	public abstract class AbstractState
	{

		protected QueryInfo queryInfo;

		public AbstractState(QueryInfo queryInfo)
		{
			this.queryInfo = queryInfo;
		}

		/// <summary>
		/// Moves from this state to the next one, according to the token read.
		/// </summary>
		/// <param name="token">the token in the query currently being parsed</param>
		/// <returns>the next state</returns>
		public abstract AbstractState TransitionToNextState(string token);

		/// <summary>
		/// Returns true if the query can end with this state (e.g. if the query can end with a given token).
		/// </summary>
		/// <returns>true if the query can end in this state</returns>
		public virtual bool IsFinalState()
		{
			return false;
		}

		public QueryInfo GetQueryInfo()
		{
			return queryInfo;
		}

	}
}
