using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hafman
{

    class Word
    {
        public char c;
        public string cn;
        public int number;
        public int id;
        public string code;

        public Word(char cc = '\0')
        {
            if (cc == '\n')
                cn = "Enter";
            else if (cc == '\t')
                cn = "Tab";
            else if (cc == '\r')
                cn = "\\r";
            else if (cc == '\v')
                cn = "\\v";
            else if (cc == '\f')
                cn = "\\f";
            else if (cc == ' ')
                cn = "Space";
            else
            {
                cn = cc.ToString();
            }
            c = cc;
            number = 1;
            code = null;
            id = -1;
        }
        public int incNum()
        {
            return ++number;
        }
        public bool Equals(Word w)
        {
            if (w.c == this.c && w.number == this.number && w.id == this.id)
                return true;

            return false;
        }
        public bool NotEquals(Word w)
        {
            if (w.c != this.c || w.number != this.number)
                return true;

            return false;
        }

    }

    class Tree
    {
        public Word rishe;
        public Word right;
        public Word left;

    }

    static class Metod
    {
        public static List<Word> extractWord(string s)
        {
            List<Word> lw = new List<Word>();

            foreach (var i in s)
            {
                Word w1 = new Word(i);

                Word w2 = lw.Where(l => l.c == i).FirstOrDefault();
                if (w2 != null)
                {
                    w2.incNum();
                }
                else
                {
                    lw.Add(w1);
                }

            }

            return lw.OrderBy(o => o.number).ToList();
        }

        public static List<Tree> createTree(List<Word> lw)
        {
            List<Tree> lt = new List<Tree>();

            int i = 0;

            while (i < lw.Count())
            {
                Tree t1 = new Tree();
                Word w1 = new Word();


                w1.number = lw[i].number + lw[i + 1].number;
                w1.id = i;
                lw.Add(w1);
                lw = lw.OrderBy(o => o.number).ToList();

                t1.rishe = w1;
                if (lw[i + 1].number == lw[i].number)
                {
                    if ((int)lw[i + 1].c > (int)lw[i].c)
                    {
                        t1.right = lw[i + 1];
                        t1.left = lw[i];
                    }
                    else
                    {
                        t1.right = lw[i];
                        t1.left = lw[i + 1];
                    }
                }
                else
                {
                    t1.right = lw[i + 1];
                    t1.left = lw[i];
                }


                lt.Add(t1);

                if (i + 3 == lw.Count())
                    i = i + 5;
                else
                    i = i + 2;

            }

            return lt;
        }

        public static List<Word> codingWord(List<Tree> lt, List<Word> lw)
        {
            Word w = new Word();

            foreach (var item in lw)
            {
                w = item;

                for (int i = 0; i < lt.Count; i++)
                {
                    if (w.Equals(lt[i].left))
                    {
                        item.code += "0";
                        w = lt[i].rishe;
                    }
                    else if (w.Equals(lt[i].right))
                    {
                        item.code += "1";
                        w = lt[i].rishe;
                    }
                }

                string reverse = String.Empty;
                for (int j = item.code.Length - 1; j > -1; j--)
                {
                    reverse += item.code[j];
                }
                item.code = reverse;
            }


            return lw;
        }

        public static void writeGuideFile(List<Word> lw)
        {
            string s = "";

            foreach (var item in lw)
            {
                s += item.cn + " " + item.code + "\n";
            }
            File.WriteAllText("Hufman_codes.txt", s);
        }

        public static string writeOutPutFile(string text, List<Word> lw, bool f = true)
        {
            string s = "";

            foreach (var i in text)
            {
                foreach (var j in lw)
                {
                    if (i == j.c)
                    {
                        s += j.code;
                        break;
                    }
                }

            }
            if (f)
                File.WriteAllText("output.txt", s);

            return s;
        }
    }

    //-------------------------------------------------------------------------------
    
    class Program
    {
        static void Main(string[] args)
        {
            //------------------------------File-------------------------------------
            string text = File.ReadAllText("test3.txt");

            var lw1 = Metod.extractWord(text);

            if (lw1.Count() > 1)
            {

                List<Word> lw2 = new List<Word>();
                lw2.AddRange(lw1);

                var lt = Metod.createTree(lw1);

                lw2 = Metod.codingWord(lt, lw2);

                Metod.writeGuideFile(lw2);

                Metod.writeOutPutFile(text, lw2);
            }
            else
            {
                File.WriteAllText("Hufman_codes.txt", text[0] + " 0");
                File.WriteAllText("output.txt", "0");

            }

            //------------------------------Console-------------------------------------
            Console.WriteLine("Enter your text : ");
            string mytxt = Console.ReadLine();

            var lw3 = Metod.extractWord(mytxt);

            if (lw1.Count() > 1)
            {

                List<Word> lw4 = new List<Word>();
                lw4.AddRange(lw3);

                var lt2 = Metod.createTree(lw3);

                lw4 = Metod.codingWord(lt2, lw4);

                string res = Metod.writeOutPutFile(mytxt, lw4, false);
                Console.WriteLine("huffman code out put : " + res);
            }
            else
                Console.WriteLine("huffman code out put : 0");


            Console.ReadKey();
        }
    }

}
