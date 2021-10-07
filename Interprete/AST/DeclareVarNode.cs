using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class DeclareVarNode : NodeAST
    {
        Token tokenNombre;
        public DeclareVarNode(Token token, Token tokenNombre) : base(token)
        {
            this.tokenNombre = tokenNombre;
        }

        internal Token TokenNombre { get => tokenNombre; }

        public override object Accept(Visitor v)
        {
            return v.VisitDeclareVar(this);
        }
    }
}
