using FurlanSql.Engine;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace FurlanSql.Test
{
    [TestClass]
    public class DbClientTest
    {
        [TestMethod]
        public void TestSqLite()
        {
            using (var ts = new TransactionScope())
            {
                var conn = new SqliteConnection("Data Source=:memory:");

                conn.Open();
                //create dummy table
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"CREATE TABLE tocai(
                                   bim TEXT,
                                   bam TEXT);";
                cmd.ExecuteNonQuery();

                var intepreter = new FurlanSqlInterpreter(conn);
                var res = intepreter.Execute("tache bande");
                res = intepreter.Execute("pare dentri tocai bim, bam chist 'a', 'b'");
                Debug.Assert(res.GetAffectedRows() == 1);
                res = intepreter.Execute("daur man");

                res = intepreter.Execute("cjape su bim, bam dal cjossul tocai");
                Debug.Assert(res.GetResultSet().Read());

                res = intepreter.Execute("tache bande");
                res = intepreter.Execute("fa e disfa tocai cussi bim come 'c' dula bam = 'b'");
                Debug.Assert(res.GetAffectedRows() == 1);
                res = intepreter.Execute("daur man");

                res = intepreter.Execute("cjape su bim, bam dal cjossul tocai");
                Debug.Assert(res.GetResultSet().Read() && res.GetResultSet()["bam"] as string == "b");

                res = intepreter.Execute("tache bande");
                res = intepreter.Execute("are vie dal cjossul tocai");
                Debug.Assert(res.GetAffectedRows() == 1);
                res = intepreter.Execute("taconiti");


                res = intepreter.Execute("cjape su bim, bam dal cjossul tocai");
                Debug.Assert(res.GetResultSet().Read() && res.GetResultSet()["bam"] as string == "b");

                res = intepreter.Execute("tache bande");
                res = intepreter.Execute("are vie dal cjossul tocai");
                Debug.Assert(res.GetAffectedRows() == 1);
                res = intepreter.Execute("daur man");

                res = intepreter.Execute("cjape su bim, bam dal cjossul tocai");
                Debug.Assert(!res.GetResultSet().Read());

                conn.Close();
                Debug.WriteLine(res);
                ts.Complete();
            }

            //Debug.Assert(res == "SELECT sustui, corobulis FROM TABLE INNER JOIN PLUTO WHERE COLUMN = CJOSSUL");
        }
    }
}
