using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete;
using MyInterpreter.Interprete.AST;
using MyInterpreter.Interprete.Context;

namespace MyInterpreter.Interprete.Semantic
{
    class VisitorSemantic: Visitor
    {
        private MyContext myContext;

        public VisitorSemantic()
        {
            myContext = new MyContext();
        }

        

        object Visitor.VisitSegment(SegmentNode node)
        {

            List<NodeAST> childs = node.Childs;

            for (int i = 0; i < childs.Count - 1; i++)
            {
                node.Childs.ElementAt(i).Accept(this);

            }

            NodeAST lastNode = childs.Last();


            if (lastNode is ReturnNode)
                return lastNode.Childs.ElementAt(0).Accept(this);
            else
                lastNode.Accept(this);


            return null;
        }
        object Visitor.VisitConstantBool(NodeAST node)
        {
            return TypeVar.BOOL;
        }
        object Visitor.VisitConstantNum(NodeAST node)
        {
            return TypeVar.NUM;
        }
        object Visitor.VisitConstantText(NodeAST node)
        {
            return TypeVar.TEXT;
        }
        object Visitor.VisitVar(NodeAST node)
        {
            string nameVar = node.Token.Values[0];
            VarConext var;
            try
            {
                var = pila.Peek().Variables[nameVar];

            }
            catch (KeyNotFoundException)
            {
                throw new ExceptionSemantic(MsgType.MSG_VAR_DECLARE, node.Token);
            }

            return var.TypeVar;
        }
        object Visitor.VisitBinOp(NodeAST node)
        {
            TypeVar izq = (TypeVar)node.Childs.ElementAt(0).Accept(this);
            TypeVar der = (TypeVar)node.Childs.ElementAt(1).Accept(this);

            TypeVar result =0;
            switch (((BinOpNode)node).BinOpType)
            {
                //Nota: Valorar sumar texto
                case BinNumOpType.SUM:
                    if ((izq != TypeVar.NUM || der != TypeVar.TEXT) && (izq != der))
                        throw new ExceptionSemantic(MsgType.MSG_TYPE, node.Token);
                    result = izq;
                    break;
                case BinNumOpType.DIV:
                case BinNumOpType.MIN:
                case BinNumOpType.MULT:
                case BinNumOpType.LESS_THAN:
                case BinNumOpType.GREATER_THAN:
                    if (izq != TypeVar.NUM  && (izq != der))
                        throw new ExceptionSemantic(MsgType.MSG_TYPE, node.Token);
                    result = izq;
                    break;
                case BinNumOpType.AND:
                case BinNumOpType.OR:
                    if (izq != TypeVar.BOOL && (izq != der))
                        throw new ExceptionSemantic(MsgType.MSG_TYPE, node.Token);
                    result = izq;
                    break;
                case BinNumOpType.NOT_EQUAL:
                case BinNumOpType.EQUAL_EQUAL:
                    if ((izq != TypeVar.NUM || der != TypeVar.BOOL || der != TypeVar.TEXT) && (izq != der))
                        throw new ExceptionSemantic(MsgType.MSG_TYPE, node.Token);
                    result = izq;
                    break;
                default:
                    break;
            }

            return result;
        }
        object Visitor.VisitAssign(NodeAST node)
        {
            string varName = node.Childs.ElementAt(0).Token.Values[0];
            TypeVar der = (TypeVar)node.Childs.ElementAt(1).Accept(this);
            VarConext var=null;
            try
            {
                var = pila.Peek().Variables[varName];
            }
            catch (KeyNotFoundException)
            {
                throw new ExceptionSemantic(MsgType.MSG_VAR_DECLARE, node.Token);
            }

            if(var.TypeVar != der)
                throw new ExceptionSemantic(MsgType.MSG_TYPE, node.Token);

            return null;
        }



        //Debe de crearse la variable en semantic para poder saber el tipo de la variable
        
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
            pila.Peek().Variables[nameVar] = varExcute;

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
            TypeVar typeVar;
            int i = 0;
            //Verifico que coincida la cantidad de parametros pasados con las declarados
            if(paramExpresions.Count != paramExpresions.Count)
                throw new ExceptionSemantic(MsgType.MSG_COUNT_PARAM, node.Token);

            //Verifico que coincida los tipos de los parametros
            foreach (DeclareVarNode item in paramVar)
            {
                item.Accept(this);
                typeVar = (TypeVar)paramExpresions.ElementAt(i).Accept(this);
                if(typeVar != VarConext.GetTypeVar(item.Token.TokenType))
                    throw new ExceptionSemantic(MsgType.MSG_TYPE, methodContext.DeclareMethodNode.Token);

            }

            myContext.popSegment();

            typeVar = VarConext.GetTypeVar(methodContext.DeclareMethodNode.Token.TokenType);
            TypeVar result = (TypeVar)methodContext.DeclareMethodNode.Segment.Accept(this);

            //Se comprueba que el tipo devuelto sean iguales
            if (typeVar != result)
                throw new ExceptionSemantic(MsgType.MSG_TYPE, methodContext.DeclareMethodNode.Token);

            
            return result;
        }

    }
}
