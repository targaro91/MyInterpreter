using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class IfElseNode : NodeAST
    {
        public IfElseNode(Token token) : base(token)
        {
        }

        public override object Accept(Visitor v)
        {
            throw new NotImplementedException();
        }
    }
}
