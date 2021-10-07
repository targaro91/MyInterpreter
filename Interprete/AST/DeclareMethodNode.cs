using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class DeclareMethodNode : NodeAST
    {
        Token tokenNombre;

        List<DeclareVarNode> paramsVar;
        SegmentNode segment;

        public DeclareMethodNode(Token token, Token tokenNombre) : base(token)
        {
            this.tokenNombre = tokenNombre;
            this.paramsVar = null;
            this.segment = null;
        }

        public Token TokenNombre { get => tokenNombre; }
        public List<DeclareVarNode> ParamsVar { get => paramsVar; set => paramsVar = value; }
        public SegmentNode Segment { get => segment; set => segment = value; }

        public override object Accept(Visitor v)
        {
            return v.VisitDeclareMethod(this);
        }
    }
}
