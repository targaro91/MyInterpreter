using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyInterpreter.Interprete.Context;
using MyInterpreter.Interprete.Execute;
using MyInterpreter.Interprete.AST;
using MyInterpreter.Interprete.Semantic;

namespace MyInterpreter.Interprete.Context
{
    class MyContext
    {
        private Stack<SegmentContex> pila;

        public MyContext() 
        {
            pila = new Stack<SegmentContex>();
            
            
            
        }

        public void pushSegment() 
        {
            pila.Push(new SegmentContex());

        }

        public void popSegment()
        {
            pila.Pop();

        }

        public VarConext GetVar(NodeAST node, string nombre) 
        {
            VarConext varContext=null;

            
            foreach (SegmentContex item in pila.AsEnumerable())
            {
                try
                {
                    varContext=item.Variables[node.TokenNombre.Values[0]];
                    return varContext;
                }
                catch (KeyNotFoundException)
                {
                }

            }

            throw new ExceptionSemantic(MsgType.MSG_VAR_DECLARE, node.Token);
        }

        public MethodContext GetMethod(NodeAST node, string nombre)
        {
            MethodContext methodContext = null;


            foreach (SegmentContex item in pila.AsEnumerable())
            {
                try
                {
                    methodContext = item.Metodos[nombre];
                    return methodContext;
                }
                catch (KeyNotFoundException)
                {
                }

            }

            throw new ExceptionSemantic(MsgType.MSG_METHOD_DECLARE, node.Token);
        }

        public SegmentContex SegmentContext() 
        {
            return pila.Peek();
        }



        //public Stack<SegmentContex> Pila { get => pila; set => pila = value; }
    }
}
