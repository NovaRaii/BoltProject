using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolt
{
    internal class Program
    {
        class Eszkozok
        {
            private string nev;
            private int ar;
            private int mennyiseg;
            private string parameterek;

            public Eszkozok(string nev, int ar, int mennyiseg, string parameterek)
            {
                this.nev = nev;
                this.ar = ar;
                this.mennyiseg = mennyiseg;
                this.parameterek = parameterek;
            }
        }


        static void Menu()
        {
            return;
        }
        static void Main(string[] args)
        {
            List<Eszkozok> Adatok = new List<Eszkozok>();
            StreamReader sr = new StreamReader("adatok.txt");
            while (sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                string[] sorok = sor.Split(';');
                Eszkozok eszkoz = new Eszkozok(sorok[0], Convert.ToInt32(sorok[1]), Convert.ToInt32(sorok[2]), sorok[3]);
                Adatok.Add(eszkoz);
            }
            sr.Close();
            
        
        }
    }
}
