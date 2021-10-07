using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.Execute
{
    class ExceptionExecute : Exception
    {
        private Token token;

        public ExceptionExecute(string message, Token token):base("Error en Tiempo de Ejecución: "+message)
        {   
            this.token = token;
        }
    }
}
