using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInterpreter.Interprete.AST
{
    enum NodeASTType
    {
        DECLARATE_VAR, IF, FOR, IDENTIF_VAR, RESERVED_METHODS,
        EQUAL, OPERATOR_DIV, OPERATOR_SUM, OPERATOR_MULT, OPERATOR_MIN,
        PARENT_CLOSE, PARENT_OPEN, SEMI_COLON, END
    }
    abstract class NodeAST
    {
        protected Token token;
        protected List<NodeAST> childs;

        public NodeAST()
        {
            token = null;
            childs = new List<NodeAST>();

        }
        public NodeAST(Token token)
        {
            this.token = token;
            childs = new List<NodeAST>();
            
        }

        public void AppendChild(NodeAST child)
        {
            childs.Add(child);
        }

        public abstract Object Accept(Visitor v);


        internal Token Token { get => token; set => token = value; }
        internal List<NodeAST> Childs { get => childs; set => childs = value; }
    }
}
