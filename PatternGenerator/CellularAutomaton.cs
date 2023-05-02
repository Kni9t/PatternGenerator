using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternGenerator
{
    public class CellularAutomaton
    {
        Random R = new Random();
        int SquareSize;
        bool[] Line, RuleLine;

        public CellularAutomaton(int LengthLine = 50, int SquareSize = 10)
        {
            Line = new bool[LengthLine];
            this.SquareSize = SquareSize;
            for (int i = 0; i < LengthLine; i++) if (R.Next(0, 101) <= 25) Line[i] = true;
        }
        public void SetRule(int Rule)
        {
            int i = 0;
            RuleLine = new bool[8];

            if (Convert.ToString(Rule, 2).Length < 8) // Корректировка на незначащие нули
                for (i = 0; i < (8 - Convert.ToString(Rule, 2).Length); i++)
                {
                    RuleLine[i] = false;
                }

            foreach (char x in Convert.ToString(Rule, 2))
            {
                if (x == '0') RuleLine[i] = false;
                else RuleLine[i] = true;
                i++;
            }
        }
        void UpdateLogic()
        {
            bool[] buf = new bool[Line.Length];

            if ((Line[0] == true) && (Line[1] == true)) buf[0] = RuleLine[4];
            if ((Line[0] == true) && (Line[1] == false)) buf[0] = RuleLine[5];
            if ((Line[0] == false) && (Line[1] == true)) buf[0] = RuleLine[6];
            if ((Line[0] == false) && (Line[1] == false)) buf[0] = RuleLine[7];

            if ((Line[Line.Length - 2] == true) && (Line[Line.Length - 1] == true)) buf[Line.Length - 1] = RuleLine[1];
            if ((Line[Line.Length - 2] == true) && (Line[Line.Length - 1] == false)) buf[Line.Length - 1] = RuleLine[3];
            if ((Line[Line.Length - 2] == false) && (Line[Line.Length - 1] == true)) buf[Line.Length - 1] = RuleLine[5];
            if ((Line[Line.Length - 2] == false) && (Line[Line.Length - 1] == false)) buf[Line.Length - 1] = RuleLine[7];

            for (int i = 1; i < buf.Length - 1; i++)
            {
                if ((Line[i - 1] == true) && (Line[i] == true) && (Line[i + 1] == true)) buf[i] = RuleLine[0];
                if ((Line[i - 1] == true) && (Line[i] == true) && (Line[i + 1] == false)) buf[i] = RuleLine[1];
                if ((Line[i - 1] == true) && (Line[i] == false) && (Line[i + 1] == true)) buf[i] = RuleLine[2];
                if ((Line[i - 1] == true) && (Line[i] == false) && (Line[i + 1] == false)) buf[i] = RuleLine[3];
                if ((Line[i - 1] == false) && (Line[i] == true) && (Line[i + 1] == true)) buf[i] = RuleLine[4];
                if ((Line[i - 1] == false) && (Line[i] == true) && (Line[i + 1] == false)) buf[i] = RuleLine[5];
                if ((Line[i - 1] == false) && (Line[i] == false) && (Line[i + 1] == true)) buf[i] = RuleLine[6];
                if ((Line[i - 1] == false) && (Line[i] == false) && (Line[i + 1] == false)) buf[i] = RuleLine[7];
            }

            for (int i = 0; i < buf.Length; i++)
                Line[i] = buf[i];
        }
        public void StartIteration(int CountIteration, Graphics G)
        {
            for (int i = 0; i < CountIteration; i++)
            {
                for (int j = 0; j < Line.Length; j++)
                    if (Line[j]) G.FillRectangle(Brushes.Black, 0 + SquareSize * j, 0 + SquareSize * i, SquareSize, SquareSize);
                    else G.FillRectangle(Brushes.White, 0 + SquareSize * j, 0 + SquareSize * i, SquareSize, SquareSize);
                UpdateLogic();
            }
        }
    }
}
