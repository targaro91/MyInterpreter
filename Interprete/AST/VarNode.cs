using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.Context;
namespace MyInterpreter.Interprete.AST
{
    //Esta clase se usa para solo representarse en el AST y ademas para visitar
    class VarNode : NodeAST
    {
        public VarNode(Token token) : base(token)
        {
           
        }

        public override object Accept(Visitor v)
        {
            return v.VisitVar(this);
        }
    }
}
