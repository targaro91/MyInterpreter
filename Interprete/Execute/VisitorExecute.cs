using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete;
using MyInterpreter.Interprete.AST;
using MyInterpreter.Interprete.Context;
using MyInterpreter.Interprete.Execute;

namespace MyInterpreter.Interprete.Execute
{
    class VisitorExcute : Visitor
    {
        
        private MyContext myContext;

        public VisitorExcute()
        {
            myContext = new MyContext();            
        }

        object Visitor.VisitContext(NodeAST node)
        {
            myContext.pushSegment();
            foreach (NodeAST item in node.Childs)
            {
                item.Accept(this);

            }

            myContext.popSegment();

            return null;
        }
            object Visitor.VisitSegment(SegmentNode node)
            {

            List<NodeAST> childs = node.Childs;

            for (int i = 0; i < childs.Count-1; i++)
            {
                node.Childs.ElementAt(i).Accept(this);

            }

            NodeAST lastNode = childs.Last();

            object result = lastNode.Childs.ElementAt(0).Accept(this);
            if (lastNode is ReturnNode)
                return result;         


            return null;
        }
        object Visitor.VisitConstantBool(NodeAST node)
        {
            return bool.Parse(node.Token.Values[0]);
        }

        object Visitor.VisitConstantNum(NodeAST node)
        {
            return double.Parse(node.Token.Values[0]);
        }
        object Visitor.VisitConstantText(NodeAST node)
        {
            return node.Token.Values[0];
        }
        object Visitor.VisitVar(NodeAST node)
        {
            string nameVar = node.Token.Values[0];
            VarConext var;
            
            var = myContext.getVar(node);

            

            if (var.Value == null)
                throw new ExceptionExecute("Variable " + node.Token.Values[0] + " no inicializada.", node.Token);

            switch (var.TypeVar)
                {
                    case TypeVar.NUM:
                        return double.Parse(var.Value, System.Globalization.CultureInfo.InvariantCulture);
                    case TypeVar.TEXT:
                        return var.Value;
                    case TypeVar.BOOL:
                        return bool.Parse(var.Value);
                    default:
                        break;
                }
            return null;
        }

        object Visitor.VisitBinOp(NodeAST node)
        {
            object izq = node.Childs.ElementAt(0).Accept(this);
            object der = node.Childs.ElementAt(1).Accept(this);

            object result=null;
            switch (((BinOpNode)node).BinOpType)
            {
                //Nota: Valorar sumar texto
                case BinNumOpType.SUM:
                    result = (double)izq + (double)der;
                    break;
                case BinNumOpType.MIN:
                    result = (double)izq - (double)der;
                    break;
                case BinNumOpType.DIV:
                    result = (double)izq / (double)der;
                    break;
                case BinNumOpType.MULT:
                    result = (double)izq * (double)der;
                    break;
                case BinNumOpType.AND:
                    result = (bool)izq && (bool)der;
                    break;
                case BinNumOpType.OR:
                    result = (bool)izq || (bool)der;
                    break;
                case BinNumOpType.LESS_THAN:
                    result = (double)izq < (double)der;
                    break;
                case BinNumOpType.GREATER_THAN:
                    result = (double)izq > (double)der;
                    break;
                case BinNumOpType.EQUAL_EQUAL:
                    result = izq == der;
                    break;
                case BinNumOpType.NOT_EQUAL:
                    result = izq != der;
                    break;
                default:
                    break;
            }

            return result;
        }

        object Visitor.VisitDeclareVar(DeclareVarNode node) 
        {

            NodeAST child = node.Childs.ElementAt(0);
            String nameVar = node.TokenNombre.Values[0];
            TypeVar typeVar = TypeVar.NUM;

            switch (node.Token.TokenType)
            {
                case TokenType.TYPE_TEXT:
                    typeVar = TypeVar.TEXT;
                    break;
                case TokenType.TYPE_BOOL:
                    typeVar = TypeVar.BOOL;
                    break;
                default:
                    break;
            }

            VarConext varExcute = new VarConext(nameVar, typeVar);
            myContext.SegmentContext().Variables[nameVar] = varExcute;


            return null;
        }

        object Visitor.VisitAssign(NodeAST node)
        {
            
               
                string value = node.Childs.ElementAt(1).Accept(this).ToString();

                VarConext var = myContext.getVar(node.Childs.ElementAt(0));
                var.Value = value;

            

            return null;
        }

        object Visitor.VisitDeclareMethod(DeclareMethodNode node)
        {
            String nameVar = node.TokenNombre.Values[0];
            TypeVar typeVar = TypeVar.NUM;

            switch (node.Token.TokenType)
            {
                case TokenType.TYPE_TEXT:
                    typeVar = TypeVar.TEXT;
                    break;
                case TokenType.TYPE_BOOL:
                    typeVar = TypeVar.BOOL;
                    break;
                default:
                    break;
            }

            MethodContext methodConext = new MethodContext(nameVar, typeVar, node);
            myContext.SegmentContext().Metodos[nameVar] = methodConext;

            return null;
        }
        
        
        object Visitor.VisitExecuteMethod(ExecuteMethodNode node)
        {
            myContext.pushSegment();

            string methodName = node.Token.Values[0];
            MethodContext methodContext = myContext.GetMethod(node, methodName);
            List<DeclareVarNode> paramVar = methodContext.DeclareMethodNode.ParamsVar;

            List<NodeAST> paramExpresions = node.ParamExpresions;
            int i = 0;
            //Declaro variables y les asigno el resultado de la evaluacion de las expresiones
            foreach (DeclareVarNode item in paramVar)
            {
                item.Accept(this);
                string value = paramExpresions.ElementAt(i).Accept(this).ToString();
                myContext.GetVar(node, item.TokenNombre.Values[0]).Value=value;

            }

            myContext.popSegment();


            return methodContext.DeclareMethodNode.Segment.Accept(this);
            
        }

        
    }
}
