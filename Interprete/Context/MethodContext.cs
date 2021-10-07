using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.AST;
namespace MyInterpreter.Interprete.Context
{
    class MethodContext
    {
        
        private string nombre;
        private TypeVar typeVar;
        private DeclareMethodNode declareMethodNode;


        public MethodContext(string nombre, TypeVar typeVal, DeclareMethodNode declareMethodNode)
        {
            this.nombre = nombre;
            this.typeVar = typeVal;
            this.declareMethodNode = declareMethodNode;
        }

        
       
        public string Nombre { get => nombre;  }
        public TypeVar TypeVar { get => typeVar;  }
        public DeclareMethodNode DeclareMethodNode { get => declareMethodNode;  }
    }
}
