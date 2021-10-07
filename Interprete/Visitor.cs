using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.AST;
namespace MyInterpreter.Interprete
{
    interface Visitor
    {

        
        public abstract object VisitSegment(SegmentNode node);
        public abstract object VisitContext(NodeAST node);
        public abstract object VisitBinOp(NodeAST node);
        public abstract object VisitVar(NodeAST node);
        public abstract object VisitDeclareVar(DeclareVarNode node);
        public abstract object VisitAssign(NodeAST node);
        public abstract object VisitConstantBool(NodeAST node);
        public abstract object VisitConstantNum(NodeAST node);
        public abstract object VisitConstantText(NodeAST node);
        public abstract object VisitDeclareMethod(DeclareMethodNode node);
                
        public abstract object VisitExecuteMethod(ExecuteMethodNode node);



    }
}
