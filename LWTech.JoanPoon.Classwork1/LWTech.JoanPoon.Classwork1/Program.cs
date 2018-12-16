using System;

namespace LWTech.JoanPoon.Classwork1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Now-Is The*Time*For All Good^Programmers to#come#to the aid-of-their,school!";
            s = s.Replace("-",",");
            s = s.Replace("*", ",");
            s = s.Replace(" ", ",");
            s = s.Replace("#", ",");
            s = s.Replace("^", ",");
            s = s.Replace("!", ",");
            string[] token = s.Replace("!", ",").Split(',');

            foreach (string str in token)
                Console.WriteLine(str);
        }
    }
}
