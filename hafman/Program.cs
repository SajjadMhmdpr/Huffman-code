using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace hafman
{
    class Program
    {
        static void Main(string[] args)
        {
            //------------------------------File-------------------------------------
            string text = File.ReadAllText("test1.txt");

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
