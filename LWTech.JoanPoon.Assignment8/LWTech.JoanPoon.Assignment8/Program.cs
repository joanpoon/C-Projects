using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LWTech.JoanPoon.Assignment8
{
    class AirplaneProperties
    {
        public string Type { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Joan Poon \t\t\t\t Assignment 8 (w/JSON.net):");
            Console.WriteLine("=============================================================================================");

            string jsonText = "";
            WebClient client = new WebClient();

            try
            {
                Stream stream = client.OpenRead("https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?fTypQN=");
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonText = reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("A network error occurred.  " + ex.Message);
                Console.WriteLine("Unable to continue.");
                return;
            }

            JObject planeJO = JObject.Parse(jsonText);
            List<JToken> planeJA = planeJO["acList"].Children().ToList();

            List<string> planeList = new List<string>();
            string planeText = "";

            foreach (JToken plane in planeJA)
            {
                AirplaneProperties airplaneProperties = plane.ToObject<AirplaneProperties>();

                planeText = airplaneProperties.Type;

                if(planeText[0] == 'B' && planeText[1] == '7' && planeText.Length == 4)
                {
                    planeText = planeText.Remove(planeText.Length - 1);
                    planeText += "7";
                    planeList.Add(planeText);
                }
                else if(planeText[0] == 'A' && planeText[1] == '3' && planeText.Length == 4)
                {
                    planeText = planeText.Remove(planeText.Length - 1);
                    planeText += "0";
                    planeList.Add(planeText);
                }



            }

            Histogram planeHistogram = new Histogram(planeList, width: 100, maxLabelWidth: 5);
            planeHistogram.Sort((x, y) => y.Value.CompareTo(x.Value));
            Console.WriteLine(planeHistogram);
        }

    }
    class Histogram
    {
        private int width;
        private int maxBarWidth;
        private int maxLabelWidth;
        private int minValue;
        private List<KeyValuePair<string, int>> bars;

        public Histogram(List<string> data, int width = 80, int maxLabelWidth = 10, int minValue = 0)
        {
            this.width = width;
            this.maxLabelWidth = maxLabelWidth;
            this.minValue = minValue;
            this.maxBarWidth = width - maxLabelWidth - 2;   // -2 for the space and pipe separator

            var barCounts = new Dictionary<string, int>();

            foreach (string item in data)
            {
                if (barCounts.ContainsKey(item))
                    barCounts[item]++;
                else
                    barCounts.Add(item, 1);
            }

            this.bars = new List<KeyValuePair<string, int>>(barCounts);

        }

        public void Sort(Comparison<KeyValuePair<string, int>> f)
        {
            bars.Sort(f);
        }

        public override string ToString()
        {
            string s = "";
            string blankLabel = "".PadRight(maxLabelWidth);

            int maxValue = 0;
            foreach (KeyValuePair<string, int> bar in bars)
            {
                if (bar.Value > maxValue)
                    maxValue = bar.Value;
            }

            foreach (KeyValuePair<string, int> bar in bars)
            {
                string key = bar.Key;
                int value = bar.Value;

                if (value >= minValue)
                {
                    string label;
                    if (key.Length < maxLabelWidth)
                        label = key.PadLeft(maxLabelWidth);
                    else
                        label = key.Substring(0, maxLabelWidth);

                    int barSize = (int)(((double)value / maxValue) * maxBarWidth);
                    string barStars = "".PadRight(barSize, '*');

                    s += label + " |" + barStars + " " + value + "\n";
                }
            }

            string axis = blankLabel + " +".PadRight(maxBarWidth + 2, '-') + "\n";    //TODO: Why is +2 is needed?
            s += axis;

            return s;
        }
    }
}
