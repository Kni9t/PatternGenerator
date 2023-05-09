using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int SIZE = 3;
        CellularAutomaton SA;

        void Print(int Rule = 110, int IterationCount = 100)
        {
            SA = new CellularAutomaton(pictureBox1.Width / SIZE, SIZE);

            Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics G = Graphics.FromImage(BitMap);

            SA.SetRule(Rule);
            SA.StartIteration(IterationCount, G);

            pictureBox1.Image = BitMap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Print();
        }
         
        private void timer2_Tick(object sender, EventArgs e)
        {
        }
    }
}
