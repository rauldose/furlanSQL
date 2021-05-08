using System;
using System.Collections.Generic;
using System.Text;

namespace FurlanSql.Engine.Constants
{
    public static class Keywords
    {
        public static string[] SELECT_KEYWORDS = { "cjape", "su" };
        public static string[] UPDATE_KEYWORDS = { "fa", "e", "disfa" };
        public static string[] INSERT_KEYWORDS = { "pare", "dentri" };
        public static string[] DELETE_KEYWORDS = { "are", "vie" };
        public static string[] JOIN_KEYWORDS = { "di", "rif", "o", "di", "raf"};
        public static string[] FROM_KEYWORDS = { "dal", "cjossul" };
        public static string ASTERISK_KEYWORD = "dut";
        public static string WHERE_KEYWORD = "dula";
        public static string[] BEGIN_TRANSACTION_KEYWORDS = { "tache", "bande" };
        public static string[] COMMIT_KEYWORDS = { "daur", "man" };
        public static string ROLLBACK_KEYWORD = "taconiti";
        public static string AND_KEYWORD = "e";
        public static string OR_KEYWORD = "o";
        public static string NULL_KEYWORD = "nie";
        public static string[] IS_KEYWORDS = { "al", "e" };
        public static string VALUES_KEYWORD = "chist";
        public static string[] IS_NOT_KEYWORDS = { "nol", "e" };
        public static string SET_KEYWORD = "cussi";
        public static List<string> WHERE_OPERATORS = new List<string>{">", "<", "=", "!=", "<>", ">=", "<=",
                IS_KEYWORDS[0], IS_NOT_KEYWORDS[0]};
        public const string SET_EQUAL_KEYWORD = "come";
    }
}
