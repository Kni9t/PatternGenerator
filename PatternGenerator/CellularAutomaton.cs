using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternGenerator
{
    public class CellularAutomaton
    {
        Random R = new Random();
        int SquareSize, RuleSize;
        bool[] Line, RuleLine;
        public int Rule;
        struct RuleUnit
        {
            public bool[] RuleLine;
            public bool Rule; 
        }
        string ConvertToByteString(int Number)
        {
            string result = "";

            if (Convert.ToString(Number, 2).Length < RuleSize) // Корректировка на незначащие нули
                for (int i = 0; i < (RuleSize - Convert.ToString(Number, 2).Length); i++)
                {
                    result += "0";
                }

            result += Convert.ToString(Number, 2);

            return result;

        }
        public CellularAutomaton(int LengthLine = 50, int SquareSize = 10)
        {
            Line = new bool[LengthLine];
            this.SquareSize = SquareSize;
        }
        void RandomFill()
        {
            for (int i = 0; i < Line.Length; i++) if (R.Next(0, 101) <= 25) Line[i] = true;
        }
        public void SetRule(int Rule = 110, int Start = 1, int RuleSize = 3)
        {
            this.RuleSize = RuleSize;
            int i = 0;
            this.Rule = Rule;
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

            if (Start == 1) RandomFill();
            if (Start == 0) Line[Line.Length / 2] = true;
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
        void UpdateLogicUPD() // сделать генератор правил для общего случая
        {
            RuleUnit[] ruleUnit = new RuleUnit[(int)Math.Pow(2, RuleSize)];

            for (int i = 0; i < ruleUnit.Length; i++)
            {
                bool[] buf = new bool[RuleSize];

            }
        }
        public void StartIteration (Graphics G, int CountIteration = 100)
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
