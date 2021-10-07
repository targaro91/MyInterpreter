using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete
{
    class ExceptionParser : Exception
    {
        private int line;

        public ExceptionParser(Token token) 
        {
            string message = "Token no esperado: " + token.TokenType.ToString();
            this.line = token.Line;
        }
    }
}
