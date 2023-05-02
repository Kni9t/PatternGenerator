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

        const int SIZE = 5;
        CellularAutomaton SA;

        private void Form1_Load(object sender, EventArgs e)
        {
            SA = new CellularAutomaton(pictureBox1.Width / SIZE, SIZE);


            Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics G = Graphics.FromImage(BitMap);

            SA.SetRule(110);
            SA.StartIteration(100, G);

            pictureBox1.Image = BitMap;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
         
        private void timer2_Tick(object sender, EventArgs e)
        {
        }
    }
}
