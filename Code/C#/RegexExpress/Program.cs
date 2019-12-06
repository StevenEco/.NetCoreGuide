using System;
using System.Text.RegularExpressions;

namespace RegexExpress
{
    class Program
    {
        static void Main(string[] args)
        {
            string reg = "\\d{3,6}";
            string input = "61762828 176 2991 871";
            foreach (Match m in Regex.Matches(input, reg))
            {
                Console.WriteLine(m.Value);
            }
        }
    }
}
