using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class WhileNode : NodeAST
    {
        public WhileNode(Token token) : base(token)
        {
        }

        public override object Accept(Visitor v)
        {
            throw new NotImplementedException();
        }
    }
}
