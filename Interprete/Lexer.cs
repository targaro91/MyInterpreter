using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace MyInterpreter.Interprete
{
    class Lexer
    {
        private string[] lineas;
        private int index; //index de lineas
        private int pos; //pos de string
        

        public Lexer(string codigo) 
        {
            index = 0;
            pos = 0;
            lineas = codigo.Split('\n');
        }

        private bool isEnd() 
        {
            return index >= lineas.Length && pos >= lineas[index].Length;
        }

        private char consume() 
        {
            if (pos < lineas[index].Length) 
                pos++;                        
            else 
            {
                pos = 0;
                index++;
            }

            return lineas[index][pos];
        }

        public Token nextToken() 
        {
            return null;
        }

    }
}
