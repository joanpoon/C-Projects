using System;
using System.Collections.Generic;
using System.Linq;

namespace LWTech.JoanPoon.class8
{
    class Program
    {

        static void Main(string[] args)
        {
            List<KeyValuePair<string, int>> bars = new List<KeyValuePair<string, int>>();

            bars.Add(new KeyValuePair<string, int>("One", 1));
            bars.Add(new KeyValuePair<string, int>("Two", 2));
            bars.Add(new KeyValuePair<string, int>("Three", 3));
            bars.Add(new KeyValuePair<string, int>("Four", 4));

            DrawHistogram(bars, totalWidth: 50, labelWidth: 8);
        }

        private static void DrawHistogram(List<KeyValuePair<string, int>> bars, int totalWidth = 100, int labelWidth = 10)
        {
            int maxBarWidth = totalWidth - labelWidth - 2;

            // Find the max value
            int maxValue = 0;
            foreach (var bar in bars)
            {
                if (bar.Value > maxValue)
                    maxValue = bar.Value;
            }

            foreach (var pair in bars)
            {
                string s = "";
                if (pair.Key.Length > labelWidth)
                    s += pair.Key.Substring(0, labelWidth);
                else
                    s += pair.Key.PadLeft(labelWidth);

                s += " |";

                int barSize = (int)(((double)pair.Value / maxValue) * maxBarWidth);
                s += "".PadRight(barSize, '*');
                s += " " + pair.Value;

                Console.WriteLine(s);
            }
        }
                        
    }
}
