using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Entities.Model
{
    public class WhereCondition
    {

        private string _field;
        private string _value;
        private string _operator;

        public WhereCondition(string field)
        {
            this._field = field;
        }

        public string GetField()
        {
            return _field;
        }

        public string GetValue()
        {
            return _value;
        }

        public void SetValue(string value)
        {
            this._value = value;
        }

        public string GetOperator()
        {
            return _operator;
        }

        public void SetOperator(string sqlOperator)
        {
            this._operator = sqlOperator;
        }

    }
}
