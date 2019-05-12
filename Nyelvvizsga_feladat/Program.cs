using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvvizsga_feladat
{
    class Program
    {
        public static List<Vizsgak> sikeres = new List<Vizsgak>();
        public static List<Vizsgak> sikertelen = new List<Vizsgak>();
        public static int lines;
        public static int row;
        public static string[] vizsgaEvek;
        public static int keres;
        public static int keresID;

        public static void beolv_fajlbol(string f_neve)
        {
            Console.WriteLine("Beolvasás a " + f_neve + " nevű fájlból...");
            try
            {
                lines = File.ReadAllLines(f_neve).Length - 1;
                FileStream f = new FileStream(f_neve, FileMode.Open);
                StreamReader r = new StreamReader(f, Encoding.Default);
                vizsgaEvek = r.ReadLine().Split(';');
                row = vizsgaEvek[0].Length;



                while (!r.EndOfStream)
                {

                    string[] t = r.ReadLine().Split(';');
                    string nev = t[0];
                    int[] ev = new int[] { int.Parse(t[1]), int.Parse(t[2]), int.Parse(t[3]), int.Parse(t[4]), int.Parse(t[5]), int.Parse(t[6]), int.Parse(t[7]), int.Parse(t[8]), int.Parse(t[9]), int.Parse(t[10]) };

                    if (f_neve == "sikeres.csv")
                        sikeres.Add(new Vizsgak(nev, ev));
                    else
                        sikertelen.Add(new Vizsgak(nev, ev));
                }
                Console.WriteLine("\t\t...sikeresen megtörtént!");
                f.Close();
                r.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("\t\t...sikertelen volt:\n" + e);
            }
        }
        public static void top3()
        {
            Console.WriteLine("2. Feladat: A legnépszerübb nyelvek:");
            SortedDictionary<int, string> ossz = new SortedDictionary<int, string>();
            for (int i = 0; i < lines; i++)
            {
                ossz.Add((sikeres[i].ossz + sikertelen[i].ossz), sikeres[i].nev);
            }
            for (int i = ossz.Count - 1; i >= ossz.Count - 3; i--)
            {
                Console.WriteLine("\t" + ossz.ElementAt(i).Value);
            }
        }
        public static void kereses()
        {
            Console.WriteLine("3. feladat:");
            Console.Write("\tVizsgálandó év:  ");
            string input = Console.ReadLine();
            for (int i = 0; i < vizsgaEvek.Length; i++)
            {
                if (vizsgaEvek[i] == input)
                {
                    keres = int.Parse(input);
                    keresID = i - 1;
                }
            }
        }
        public static void topSikertelen()
        {
            Console.WriteLine("4. feladat:\n");
            double max = int.MinValue;
            int maxID = int.MinValue;
            for (int i = 0; i < lines - 1; i++)
            {

                if (sikertelen.ElementAt(i).ev[keresID] == 0 || sikeres.ElementAt(i).ev[keresID] == 0)
                {
                    i++;
                }
                double arany = (double)(sikertelen.ElementAt(i).ev[keresID] * 100) / (sikeres.ElementAt(i).ev[keresID] + sikertelen.ElementAt(i).ev[keresID]);
                if (arany > max)
                {
                    max = arany;
                    maxID = i;
                }
            }
            Console.WriteLine("\t" + keres + "-ban/ben a(z)" + sikeres.ElementAt(maxID).nev + "  nyelvből a sikertelen vizsgák aránya " + Math.Round(max, 2) + "%");
        }
        public static void nemVoltVizsgazo()
        {
            Console.WriteLine("5. feladat:");
            for (int i = 0; i < lines - 0; i++)
            {
                if ((sikeres.ElementAt(i).ev[keresID] == 0) && (sikertelen.ElementAt(i).ev[keresID] == 0))
                {
                    Console.WriteLine("\t" + sikeres.ElementAt(i).nev);
                }
            }
        }
        public static void osszesites()
        {
            FileStream f = new FileStream("osszesites.csv", FileMode.OpenOrCreate);
            StreamWriter w = new StreamWriter(f);
            for (int i = 0; i < lines - 1; i++)
            {
                double arany=0;
                if (sikertelen.ElementAt(i).ev[keresID] != 0 || sikeres.ElementAt(i).ev[keresID] != 0)
                {
                    arany = (double)(sikertelen.ElementAt(i).ev[keresID] * 100) / (sikeres.ElementAt(i).ev[keresID] + sikertelen.ElementAt(i).ev[keresID]);
                }

                w.WriteLine(sikeres.ElementAt(i).nev + ";" +sikeres.ElementAt(i).ossz+ ";" + Math.Round(arany, 2) + "%");
            }
            w.Close();
            f.Close();
            Console.WriteLine("6. feladat:\n\tÍrás sikeresen megtörtént");

        }

        static void Main(string[] args)
        {
            beolv_fajlbol("sikeres.csv");
            beolv_fajlbol("sikertelen.csv");
            top3();
            kereses();
            topSikertelen();
            nemVoltVizsgazo();
            osszesites();

            Console.ReadKey();
        }
    }
}
