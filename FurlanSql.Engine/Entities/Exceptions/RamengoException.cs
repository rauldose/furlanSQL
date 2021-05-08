using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.Exceptions
{
    /// <summary>
    ///  Exception thrown by FurlanSql when an error occurs during query parsing/execution.
    /// </summary>
    public class RamengoException : Exception
    {
        public RamengoException(List<string> expectedTokens, string actualToken) : base($"Expected one of {string.Join(",",expectedTokens)} but got [{actualToken}]") 
        { 
        
        }

        public RamengoException(string expectedToken, string token) : base($"Expected one of {expectedToken} but got [{token}]") 
        { 
        
        }

        public RamengoException(string message): base(message)
        {
       
        }

        public RamengoException(Exception e) : base(e.Message, e)
        {

        }

    }
}
