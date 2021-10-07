using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum BinNumOpType { SUM, MIN, DIV, MULT, LESS_THAN, GREATER_THAN , EQUAL_EQUAL, NOT_EQUAL, AND, OR }

namespace MyInterpreter.Interprete.AST
{
    class BinOpNode : NodeAST
    {
        protected BinNumOpType binOpType;
        public BinOpNode(Token token) : base(token)
        {
        }

        public BinNumOpType BinOpType { get => binOpType; set => binOpType = value; }

        public override object Accept(Visitor v)
        {
            return v.VisitBinOp(this);
        }
    }
}
