using System.Linq;

namespace Nyelvvizsga_feladat
{
    public class Vizsgak
    {
        public string nev { get; set; }
        public int[] ev { get; set; }
        public int ossz { get; set; }


        public Vizsgak(string nev, int[] ev)
        {
           
            this.nev = nev;
            this.ev= ev;
            this.ossz = ev.Sum();

        }
        public override string ToString()
        {
            string s = nev;
            foreach(int e in ev)
            {
                s += " " + e +"";
            }

            return s+" "+ossz;
        }
    }
}