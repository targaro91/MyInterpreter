using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class ConstantBoolNode : NodeAST
    {
        public ConstantBoolNode(Token token) : base(token)
        {
        }

        public override object Accept(Visitor v)
        {
            return v.VisitConstantBool(this);
        }
    }
}
