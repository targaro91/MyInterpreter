using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.Context;

namespace MyInterpreter.Interprete.AST
{
    class ContextNode : NodeAST
    {
        


        public ContextNode() : base()
        { 
        }

        

        public override object Accept(Visitor v)
        {
            return v.VisitContext(this);
        }
    }
}
