using FurlanSql.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using Xunit.Sdk;

namespace FurlanSql.Test
{
    [TestClass]
    public class GrammarTest
    {
        [TestMethod]
        public void TestSelectWithJoin()
        {
            var res = FurlanSqlInterpreter.ToSqlQuery("cjape su sustui, corobulis dal cjossul TABLE di rif o di raf PLUTO dula COLUMN = CJOSSUL");
            Debug.WriteLine(res);
            Debug.Assert(res == "SELECT sustui, corobulis FROM TABLE INNER JOIN PLUTO WHERE COLUMN = CJOSSUL");
        }


    }
}
