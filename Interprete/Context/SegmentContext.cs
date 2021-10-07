using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.Context;
using MyInterpreter.Interprete.Execute;

namespace MyInterpreter.Interprete.Context
{
    class SegmentContex
    {
        private Dictionary<string, VarConext> variables;
        private Dictionary<string, MethodContext> metodos;

        public SegmentContex() 
        {
            this.variables = new Dictionary<string, VarConext>();
            this.metodos = new Dictionary<string, MethodContext>();
        }

        public Dictionary<string, VarConext> Variables { get => variables; }
        public Dictionary<string, MethodContext> Metodos { get => metodos; }
    }
}
