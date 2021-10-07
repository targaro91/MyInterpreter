using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.Context;

namespace MyInterpreter.Interprete.AST
{
    class SegmentNode : NodeAST
    {
        private List<DeclareVarNode> param;


        public SegmentNode(Token token) : base(token)
        {
            param = new List<DeclareVarNode>();
        }
        public SegmentNode() : base()
        {
            param = new List<DeclareVarNode>();
        }

        public List<DeclareVarNode> Param { get => param; set => param = value; }

        public object Accept(Visitor v, string[] paramValues)
        {
            return v.VisitSegment(this, paramValues);
        }

        public override object Accept(Visitor v)
        {
            return v.VisitSegment(this);
        }
    }
}
