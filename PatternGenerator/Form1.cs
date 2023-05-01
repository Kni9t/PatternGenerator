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
        int MapHeight, MapWidth;
        SquareGrid SG;

        private void Form1_Load(object sender, EventArgs e)
        {
            MapHeight = pictureBox1.Height / SIZE;
            MapWidth = pictureBox1.Width / SIZE;

            SG = new SquareGrid(MapHeight, MapWidth, SIZE);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            SG.MouseActive(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SG.UpdateLogic();
        }
         
        private void timer2_Tick(object sender, EventArgs e)
        {
            Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics G = Graphics.FromImage(BitMap);

            SG.UpdatePrint(G);

            pictureBox1.Image = BitMap;
        }
    }
}
