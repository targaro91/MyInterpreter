using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyInterpreter.Interprete;
using MyInterpreter.Interprete.Execute;
using MyInterpreter.Interprete.Context;
using MyInterpreter.Interprete.Semantic;

namespace MyInterpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Proyecto inconcluso falta declaracion de methodos etc


            Parser parser = new Parser(new Lexer(""));
            MyContext context = new MyContext();
            VisitorExcute exec = new VisitorExcute();
            VisitorSemantic semantic = new VisitorSemantic();
            parser.Parsear();

            //Analisis semantico
            parser.ArbolAST.Accept(semantic, new string[] { });
            //Ejecución
            parser.ArbolAST.Accept(exec, new string[] { });
            Dictionary<string,VarConext> variables = context.Variables;
        }
    }
}
