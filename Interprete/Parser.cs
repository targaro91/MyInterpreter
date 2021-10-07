using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.AST;
using MyInterpreter.Interprete.Context;

namespace MyInterpreter.Interprete
{
    class Parser
    {
        private Lexer lexer;
        private Token currentToken;
        Queue<Token> tokens = new Queue<Token>();
        private SegmentNode arbolAST;

        public SegmentNode ArbolAST { get => arbolAST; set => arbolAST = value; }

        public Parser(Lexer lexer) 
        {
            this.lexer = lexer;
            currentToken = null;

            tokens.Enqueue(new Token(TokenType.KEY_OPEN));

            tokens.Enqueue(new Token(TokenType.TYPE_NUM));
            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num1" }, 1));
            tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            //Declaracion de methodo
            //tokens.Enqueue(new Token(TokenType.TYPE_NUM));
            //tokens.Enqueue(new Token(TokenType.METHOD, new string[] { "sumar(" }, 1));

            //tokens.Enqueue(new Token(TokenType.TYPE_NUM));
            //tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num1" }, 1));
            //tokens.Enqueue(new Token(TokenType.PARENT_CLOSE));
            //tokens.Enqueue(new Token(TokenType.KEY_OPEN));

            //tokens.Enqueue(new Token(TokenType.TYPE_NUM));
            //tokens.Enqueue(new Token(TokenType.VAR, new string[] { "numMethod1" }, 1));
            //tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            //tokens.Enqueue(new Token(TokenType.KEY_CLOSE));
            //


            tokens.Enqueue(new Token(TokenType.TYPE_BOOL));
            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "bool1" }, 1));
            tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            tokens.Enqueue(new Token(TokenType.TYPE_NUM));
            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num2" }, 1));
            tokens.Enqueue(new Token(TokenType.EQUAL));
            tokens.Enqueue(new Token(TokenType.CONSTANT_NUM, new string[] { "23" }, 1));
            tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num1" }, 1));
            tokens.Enqueue(new Token(TokenType.EQUAL));
            tokens.Enqueue(new Token(TokenType.CONSTANT_NUM, new string[] { "12.90" }, 1));
            tokens.Enqueue(new Token(TokenType.BIN_OPERATOR_SUM));
            tokens.Enqueue(new Token(TokenType.CONSTANT_NUM, new string[] { "14" }, 1));
            tokens.Enqueue(new Token(TokenType.BIN_OPERATOR_MULT));
            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num2" }, 1));
            tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "bool1" }, 1));
            tokens.Enqueue(new Token(TokenType.EQUAL));
            tokens.Enqueue(new Token(TokenType.CONSTANT_BOOL, new string[] { "true" }, 1));
            tokens.Enqueue(new Token(TokenType.BIN_OPERATOR_AND));
            tokens.Enqueue(new Token(TokenType.CONSTANT_NUM, new string[] { "14" }, 1));
            tokens.Enqueue(new Token(TokenType.BIN_OPERATOR_GREATER_THAN));
            tokens.Enqueue(new Token(TokenType.VAR, new string[] { "num2" }, 1));
            tokens.Enqueue(new Token(TokenType.SEMI_COLON));

            tokens.Enqueue(new Token(TokenType.KEY_CLOSE));
            tokens.Enqueue(new Token(TokenType.END));
        }

        private void Consume() 
        {
            currentToken = tokens.Dequeue();
           

        }
        private Token NextToken()
        {
            return tokens.Peek();


        }

        private void MatchNext( TokenType tokenType)
        {
            Token token = NextToken();
            if (token.TokenType != tokenType)
                throw new ExceptionParser(token);

        }
        private void MatchCurrent(TokenType tokenType)
        {
            if (this.currentToken.TokenType != tokenType)
                throw new ExceptionParser(currentToken);

        }

        public void Parsear() 
        {
            Consume();
            arbolAST =Segment();
        }

        public SegmentNode Segment() 
        {
            SegmentNode result = new SegmentNode();
            bool continuar=true;

            MatchCurrent(TokenType.KEY_OPEN);
            Consume();

            while (continuar)
            {
                
                switch (currentToken.TokenType)
                {
                    case TokenType.TYPE_NUM:
                    case TokenType.TYPE_TEXT:
                    case TokenType.TYPE_BOOL:
                        Token tp = currentToken;

                        Consume();
                        if (currentToken.TokenType == TokenType.VAR)
                        {
                            DeclareVarNode dNode = new DeclareVarNode(tp, currentToken);

                            result.AppendChild(dNode);

                            if (NextToken().TokenType == TokenType.EQUAL)
                                goto expresionParse;
                            else
                            {
                                Consume();
                                MatchCurrent(TokenType.SEMI_COLON);
                                Consume();
                            }
                        }
                        //Entonces es una declaracion de un metodo
                        else 
                        {
                            MatchCurrent(TokenType.METHOD);
                            DeclareMethodNode dNode = new DeclareMethodNode(tp, currentToken);

                            tp = currentToken;
                                                        
                            Consume();
                            List<DeclareVarNode> param=ParamDeclarate();

                            SegmentNode segment = Segment();
                            dNode.ParamsVar = param;
                            dNode.Segment = segment;

                            result.AppendChild(dNode);
                        }
                        
                            
                        
                        break;
                    case TokenType.IF:

                        break;
                    case TokenType.WHILE:
                        break;
                    //Asignacion ej: num1=13; num1=2+90; num1=num2+12;num1=num1+12;
                    //Adaptar asignacion para otros tipos de datos ej: BOOL, TEXT
                    case TokenType.VAR:
                        expresionParse:
                        VarNode varNode = new VarNode(currentToken);

                        Consume();
                        MatchCurrent(TokenType.EQUAL);
                        AssignNode aNode = new AssignNode(currentToken);
                        aNode.AppendChild(varNode);

                        Consume();
                        NodeAST expresion = Exp();
                        aNode.AppendChild(expresion);

                        MatchCurrent(TokenType.SEMI_COLON);
                        result.AppendChild(aNode);
                        Consume();

                        break;
                    case TokenType.RESERVED_METHOD:
                        break;
                    default :
                        throw new ExceptionParser(currentToken);
                        
                }


                if (TokenType.KEY_CLOSE == currentToken.TokenType) 
                {
                    Consume();
                    continuar = false;
                }
                        
                
            }

            return result;
            

        }

        private List<DeclareVarNode> ParamDeclarate() 
        {
            List<DeclareVarNode> result = new List<DeclareVarNode>();

            bool continuar = true;

            while (continuar)
            {
                switch (currentToken.TokenType)
                {
                    case TokenType.TYPE_NUM:
                    case TokenType.TYPE_TEXT:
                    case TokenType.TYPE_BOOL:
                        Token tp = currentToken;
                        Consume();
                        MatchCurrent(TokenType.VAR);
                        DeclareVarNode dNode = new DeclareVarNode(tp, currentToken);

                        result.Add(dNode);
                        Consume();

                        break;

                    default:
                        break;
                }
                
                if (currentToken.TokenType == TokenType.PARENT_CLOSE)
                    continuar = false;
                else if (currentToken.TokenType != TokenType.COLON)
                    throw new ExceptionParser(currentToken);
                Consume();


            }

            return result;
            
         }


        private List<NodeAST> ParamExpresions()
        {
            List<NodeAST> result = new List<NodeAST>();

            bool continuar = true;

            while (continuar)
            {

                if (currentToken.TokenType == TokenType.PARENT_CLOSE)
                    continuar = false;
                else
                {
                    result.Add(Exp());
                    if (currentToken.TokenType == TokenType.PARENT_CLOSE)
                        continuar = false;
                    else if (currentToken.TokenType != TokenType.COLON)
                        throw new ExceptionParser(currentToken);


                }                 
                    

                
                Consume();
            }

            return result;

        }

        public NodeAST Exp()
        {
            //BinNumOpNode expr = new BinNumOpNode(currentToken);
            NodeAST expr = SecundaryMultExp();

            while (TokenType.BIN_OPERATOR_SUM == currentToken.TokenType || TokenType.BIN_OPERATOR_MIN == currentToken.TokenType || 
                TokenType.BIN_OPERATOR_OR == currentToken.TokenType || TokenType.BIN_OPERATOR_AND == currentToken.TokenType)
            {
                NodeAST izq = expr;
                expr = new BinOpNode(currentToken);

                switch (currentToken.TokenType)
                {
                    case TokenType.BIN_OPERATOR_SUM:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.SUM;
                        break;
                    case TokenType.BIN_OPERATOR_MIN:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.MIN;
                        break;
                    case TokenType.BIN_OPERATOR_AND:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.AND;
                        break;
                    case TokenType.BIN_OPERATOR_OR:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.OR;
                        break;
                    default:
                        throw new ExceptionParser(currentToken);
                }
                    

                Consume();
                NodeAST der = SecundaryMultExp();
                expr.AppendChild(izq);
                expr.AppendChild(der);
            }


            return expr;

        }

        public NodeAST SecundaryMultExp()
        {
            //BinNumOpNode expr = new BinNumOpNode(currentToken);
            NodeAST expr = PrimaryExp();

            while (TokenType.BIN_OPERATOR_MULT == currentToken.TokenType || TokenType.BIN_OPERATOR_DIV == currentToken.TokenType ||
                TokenType.BIN_OPERATOR_NOT_EQUAL == currentToken.TokenType || TokenType.BIN_OPERATOR_EQUAL_EQUAL == currentToken.TokenType || TokenType.BIN_OPERATOR_GREATER_THAN == currentToken.TokenType || TokenType.BIN_OPERATOR_LESS_THAN == currentToken.TokenType) 
            {
                NodeAST izq = expr;
                expr = new BinOpNode(currentToken);

                switch (currentToken.TokenType)
                {
                    case TokenType.BIN_OPERATOR_MULT:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.MULT;
                        break;
                    case TokenType.BIN_OPERATOR_DIV:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.DIV;
                        break;
                    case TokenType.BIN_OPERATOR_EQUAL_EQUAL:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.EQUAL_EQUAL;
                        break;
                    case TokenType.BIN_OPERATOR_NOT_EQUAL:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.NOT_EQUAL;
                        break;
                    case TokenType.BIN_OPERATOR_LESS_THAN:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.LESS_THAN;
                        break;
                    case TokenType.BIN_OPERATOR_GREATER_THAN:
                        ((BinOpNode)expr).BinOpType = BinNumOpType.GREATER_THAN;
                        break;
                    default:
                        throw new ExceptionParser(currentToken);
                }

                Consume();
                NodeAST der = PrimaryExp();
                expr.AppendChild(izq);
                expr.AppendChild(der);
            }
            

            return expr;

        }



        public NodeAST PrimaryExp()
        {
            NodeAST result;
            switch (currentToken.TokenType)
            {
                case TokenType.PARENT_OPEN:
                    Consume();
                    result = Exp();
                    MatchCurrent(TokenType.PARENT_CLOSE);
                    Consume();
                    break;
                case TokenType.CONSTANT_NUM:
                    result = new ConstantNumNode(currentToken);
                    Consume();
                    break;
                case TokenType.CONSTANT_BOOL:
                    result = new ConstantBoolNode(currentToken);
                    Consume();
                    break;
                case TokenType.CONSTANT_TEXT:
                    result = new ConstantTextNode(currentToken);
                    Consume();
                    break;
                case TokenType.VAR:
                    result = new VarNode(currentToken);
                    Consume();
                    break;
                case TokenType.METHOD:
                    result = new ExecuteMethodNode(currentToken);
                    //Se agrega a ExecuteMethodNode como hijos las Expresiones (parametros)

                    Consume();
                    List<NodeAST> paramExpresions = ParamExpresions();
                    ((ExecuteMethodNode)result).ParamExpresions = paramExpresions;
                    Consume();
                    break;
                default:
                    throw new ExceptionParser(currentToken);
            }

            return result;

        }


    }
}
