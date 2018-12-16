using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LWTech.JoanPoon.Assignment7
{
    class Program
    {
        static void Main(string[] args)
        {

            string readLine;
            string status;
            string ip;
            string url;
            string[] urlArray;
            string[] statusArray;

            var statusCount = new Dictionary<string, int>();
            var ipCount = new Dictionary<string, int>();
            var urlCount = new Dictionary<string, int>();

            Console.WriteLine("Joan Poon \t\t Assignment 7");
            Console.WriteLine("Access Log Analyzer");
            Console.WriteLine("================================================\n\n\n");


            try
            {
                using (StreamReader read = new StreamReader("access-log.txt"))
                    while (!read.EndOfStream)
                    {
                        readLine = read.ReadLine();
                        string[] spaceSplit = readLine.Split(" ");
                        string[] quoteSplit = readLine.Split('"');


                        status = quoteSplit[2].Trim();
                        statusArray = status.Split(" ");
                        status = statusArray[0];

                        ip = spaceSplit[0];

                        url = spaceSplit[6];
                        urlArray = url.Split("?");
                        url = urlArray[0];



                        if (statusCount.ContainsKey(status))
                            statusCount[status]++;
                        else
                            statusCount.Add(status, 1);



                        if (ipCount.ContainsKey(ip))
                            ipCount[ip]++;
                        else
                            ipCount.Add(ip, 1);



                        if (urlCount.ContainsKey(url))
                            urlCount[url]++;
                        else
                            urlCount.Add(url, 1);
                    }
            }
            catch(IOException ex)
            {
                Console.WriteLine("IO Exception: Error reading file..." + ex.Message);
            }


            //--------------------------------------------------------------------------------------------------

            try
            {
                var csv = new StringBuilder();


                Console.WriteLine("\n\n\nStatus Frequencies: (Lowest to highest)");
                Console.WriteLine("================================================");
                csv.AppendLine("Status Frequencies: (Lowest to highest)");


                var statusList = new List<KeyValuePair<string, int>>(statusCount);
                statusList.Sort((x, y) => x.Value.CompareTo(y.Value));


                foreach (KeyValuePair<string, int> p in statusList)
                {
                    Console.WriteLine($"{p.Value} : {p.Key}");
                    csv.AppendLine($"{p.Value} , {p.Key}");
                }


                //-------------------------------------------------------------------------------------------


                Console.WriteLine("\n\n\nIP Frequencies: (Highest to lowest)");
                Console.WriteLine("================================================");
                csv.AppendLine("IP Frequencies: (Highest to lowest)");


                var ipList = new List<KeyValuePair<string, int>>(ipCount);
                ipList.Sort((x, y) => y.Value.CompareTo(x.Value));


                foreach (KeyValuePair<string, int> p in ipList)
                    if (p.Value > 9)
                    {
                        Console.WriteLine($"{p.Value} : {p.Key}");
                        csv.AppendLine($"{p.Value} , {p.Key}");
                    }

                //-------------------------------------------------------------------------------------------


                Console.WriteLine("\n\n\nURL Frequencies: (Highest to lowest)");
                Console.WriteLine("================================================");
                csv.AppendLine("URL Frequencies: (Highest to lowest)");


                var urlList = new List<KeyValuePair<string, int>>(urlCount);
                urlList.Sort((x, y) => y.Value.CompareTo(x.Value));


                foreach (KeyValuePair<string, int> p in urlList)
                    if (p.Value > 9)
                    {
                        Console.WriteLine($"{p.Value} : {p.Key}");
                        csv.AppendLine($"{p.Value} , {p.Key}");
                    }

                //-------------------------------------------------------------------------------------------


                File.AppendAllText("access-summary.csv", csv.ToString());


                Console.WriteLine("File 'access-summary.csv' is successfully written.");
            }
            catch(IOException ex)
            {
                Console.WriteLine("IO Exception: Error writing file..." + ex.Message);
            }

        }
    }
}
