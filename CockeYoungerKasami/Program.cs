using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing.Imaging;
using System.Linq;
using System.Text.RegularExpressions;

namespace CockeYoungerKasami
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> grammar = new List<string>();
            string word = "";
            string axiom = "";

            axiom = "S";
//            word = "(i)+i";
            word = "i+i+i";
//             word = "i+n+i";

            grammar.Add("STX");
            grammar.Add("Si");
            grammar.Add("Sn");
            grammar.Add("SAB");
            grammar.Add("A(");
            grammar.Add("BEC");
            grammar.Add("C)");
            grammar.Add("ETX");
            grammar.Add("Ei");
            grammar.Add("En");
            grammar.Add("EAB");
            grammar.Add("XDE");
            grammar.Add("D+");
            grammar.Add("Ti");
            grammar.Add("Tn");
            grammar.Add("TAB");

//            Console.Write("Enter the word:  ");
//            string word = Console.ReadLine();

            bool flag = true;

            string[,] table = new string[word.Length, word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (i + j >= word.Length) table[i, j] = "empty";
                    else table[i, j] = "";
                }
            }

            /* генерирование первой строки */
            for (int i = 0; i < word.Length; i++)
            {
                string temp = "";
                char letter = word.ElementAt(i);
                for (int p = 0; p < grammar.Count; p++)
                {
                    if (grammar.ElementAt(p).Contains(letter))
                        temp += grammar.ElementAt(p).ToCharArray().ElementAt(0) + " ";
                }
                table[0, i] = temp;
                if (temp.Equals("")) flag = false;
            }

            if (flag)
            {
                for (int i = 1; i < word.Length; i++)
                {
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (table[i, j].Equals("empty")) continue;
                        string tempA = "", tempC = "";
                        var tempB = table[0, j];
                        for (int b = 1; b < i; b++)
                        {
                            tempB += table[i, b];
                        }

                        int tj = j;
                        for (int c = i; c >= 0; c--)
                        {
                            tempC += table[c, tj];
                            tj++;
                        }

                        for (int p = 0; p < grammar.Count; p++)
                        {
                            if (grammar.ElementAt(p).Length != 3) continue;
                            if (tempB.Contains(grammar.ElementAt(p).ToCharArray().ElementAt(1)) &&
                                tempC.Contains(grammar.ElementAt(p).ToCharArray().ElementAt(2)))
                                tempA += grammar.ElementAt(p).ToCharArray().ElementAt(0);
                        }
                        table[i, j] = tempA;
                    }
                }

                for (int i = 0; i < word.Length; i++)
                {
                    Console.Write("\t\t" + word.ElementAt(i));
                }

                Console.WriteLine("");
                Console.WriteLine(
                    "\t\t ==============================================================================================");

                for (int i = 0; i < word.Length; i++)
                {
                    Console.Write((i + 1));
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (table[i, j].Equals("")) table[i, j] = "empty";
                        Console.Write("\t\t" + table[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("==================================");

                //Console.WriteLine(table[word.Count() - 1, 0]);
                if (table[word.Count() - 1, 0].Contains(axiom))
                    Console.WriteLine("Successfull! The word " + word + " was recognized");
                else
                    Console.WriteLine("The word " + word + " wasn't recognized");
            }

            Environment.Exit(0);
        }
    }
}