using System;

namespace MyInterpreter.Interprete.Semantic
{
    enum MsgType { MSG_TYPE, MSG_VAR_DECLARE, MSG_METHOD_DECLARE, MSG_COUNT_PARAM }
    class ExceptionSemantic : Exception
    {
        public static string MSG_TYPE="Incompatibilidad de tipos.";
        public static string MSG_VAR_DECLARE = "Variable no declarada.";
        private Token token;

        public ExceptionSemantic(MsgType msgType,Token token):base("Error Semántico: ")
        {   
            this.token = token;

            switch (msgType)    
            {
                case MsgType:

                    break;
            }
        }
    }
}
