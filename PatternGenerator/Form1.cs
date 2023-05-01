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
        bool Mode = true, Time = true;

        bool[,] Map; // Height на Width

        private void Form1_Load(object sender, EventArgs e)
        {
            MapHeight = pictureBox1.Height / SIZE;
            MapWidth = pictureBox1.Width / SIZE;

            Map = new bool[MapHeight, MapWidth];

            for (int i = 0; i < Map.GetLength(0); i++)
                for (int j = 0; j < Map.GetLength(1); j++) Map[i, j] = false;

            timer1.Enabled = Time;
        }
        int StringCheck(int id)
        {
            if (id >= MapHeight) return 0;
            if (id < 0) return (MapHeight - 1);
            return id;
        }
        int ColumCheck(int id)
        {
            if (id >= MapWidth) return 0;
            if (id < 0) return (MapWidth - 1);
            return id;
        }
        void UpdatePrint()
        {
            Bitmap BitMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics G = Graphics.FromImage(BitMap);

            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                    if (Map[i, j]) G.FillRectangle(Brushes.White, 0 + SIZE * j, 0 + SIZE * i, SIZE, SIZE);
                    else G.FillRectangle(Brushes.Black, 0 + SIZE * j, 0 + SIZE * i, SIZE, SIZE);

            pictureBox1.Image = BitMap;
            GC.Collect();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Random R = new Random();
            if (e.Button == MouseButtons.Left)
            {
                int PositionX = e.X / SIZE, PositionY = e.Y / SIZE;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY - 1), ColumCheck(PositionX + 1)] = true;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY), ColumCheck(PositionX + 1)] = true;

                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX - 1)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX)] = true;
                if (R.Next(0, 2) == 1) Map[StringCheck(PositionY + 1), ColumCheck(PositionX + 1)] = true;
            }

            if (e.Button == MouseButtons.Right) Map[StringCheck(e.Y / SIZE), ColumCheck(e.X / SIZE)] = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLogic();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            UpdatePrint();
        }

        void UpdateLogic()
        {
            bool[,] Buf = new bool[MapHeight, MapWidth];

            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    int bufcount = 0;

                    if (Map[StringCheck(i - 1), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i - 1), ColumCheck(j)]) bufcount++;
                    if (Map[StringCheck(i - 1), ColumCheck(j + 1)]) bufcount++;

                    if (Map[StringCheck(i), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i), ColumCheck(j + 1)]) bufcount++;

                    if (Map[StringCheck(i + 1), ColumCheck(j - 1)]) bufcount++;
                    if (Map[StringCheck(i + 1), ColumCheck(j)]) bufcount++;
                    if (Map[StringCheck(i + 1), ColumCheck(j + 1)]) bufcount++;

                    if (Map[i, j] == false) // Стандарт
                    {
                        if (bufcount == 3) Buf[i, j] = true;
                        else Buf[i, j] = false;
                    }
                    else
                    {
                        if ((bufcount == 2) || (bufcount == 3)) Buf[i, j] = true;
                        else Buf[i, j] = false;
                    }
                }
            }

            Map = Buf;
        }
        void ClearMap()
        {
            Map = new bool[MapHeight, MapWidth];

            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++) Map[i, j] = false;
        }
    }
}
