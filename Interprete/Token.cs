using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete
{
    enum TokenType
    {
        TYPE_NUM, TYPE_TEXT, TYPE_BOOL, IF, FOR, WHILE, VAR, METHOD, RESERVED_METHOD,
        EQUAL, BIN_OPERATOR_DIV, BIN_OPERATOR_SUM, BIN_OPERATOR_MULT, BIN_OPERATOR_MIN, 
        BIN_OPERATOR_LESS_THAN, BIN_OPERATOR_GREATER_THAN, BIN_OPERATOR_EQUAL_EQUAL, BIN_OPERATOR_NOT_EQUAL, BIN_OPERATOR_AND, BIN_OPERATOR_OR,
        CONSTANT_NUM,CONSTANT_TEXT,CONSTANT_BOOL,
        PARENT_CLOSE, PARENT_OPEN,KEY_CLOSE, KEY_OPEN, SEMI_COLON, COLON, END
    }
    class Token
    {
        private TokenType tokenType;
        private string[] values;
        private int line;

        public TokenType TokenType { get => tokenType; set => tokenType = value; }
        public string[] Values { get => values; set => values = value; }
        public int Line { get => line; set => line = value; }

        public Token(TokenType tokenType, string[] values, int line)
        {
            this.tokenType = tokenType;
            this.values = values;
            this.line = line;
        }

       public  Token(TokenType tokenType)
        {
            this.tokenType = tokenType;
            this.values = new string[] { };
            this.line = 0;
        }

    }
}
