using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    class ExecuteMethodNode : NodeAST
    {
        List<NodeAST> paramExpresions;
        public ExecuteMethodNode(Token token) : base(token)
        {
        }

        public List<NodeAST> ParamExpresions { get => paramExpresions; set => paramExpresions = value; }

        public override object Accept(Visitor v)
        {
            return v.VisitExecuteMethod(this);
        }
    }
}
