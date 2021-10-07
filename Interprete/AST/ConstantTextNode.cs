using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class ConstantTextNode : NodeAST
    {
        public ConstantTextNode(Token token) : base(token)
        {
        }

        public override object Accept(Visitor v)
        {
            return v.VisitConstantText(this);
        }
    }
}
