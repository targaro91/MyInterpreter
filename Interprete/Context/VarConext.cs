using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.Context
{
    enum TypeVar { NUM, TEXT, BOOL}
    class VarConext
    {
        private string value;
        private string nombre;
        private TypeVar typeVar;

        public VarConext(string nombre, TypeVar typeVal)
        {
            this.nombre = nombre;
            this.typeVar = typeVal;
            value = null;
        }

        public string Value { get => value; set => this.value = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public TypeVar TypeVar { get => typeVar; set => typeVar = value; }

        public static TypeVar GetTypeVar(TokenType tokenType) 
        {
            TypeVar typeVar = TypeVar.NUM;

            switch (tokenType)
            {
                case TokenType.TYPE_TEXT:
                    typeVar = TypeVar.TEXT;
                    break;
                case TokenType.TYPE_BOOL:
                    typeVar = TypeVar.BOOL;
                    break;
                default:
                    break;
            }

            return typeVar;
        }


    }
}
