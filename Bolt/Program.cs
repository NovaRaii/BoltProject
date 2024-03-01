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
            private int id;
            private string nev;
            private int ar;
            private int mennyiseg;
            private string parameterek;
            private bool elavulo;

            public Eszkozok(string nev, int ar, int mennyiseg, string parameterek, bool elavulo)
            {
                this.nev = nev;
                this.ar = ar;
                this.mennyiseg = mennyiseg;
                this.parameterek = parameterek;
                this.elavulo = elavulo;

            }

            public int setId(int id) => this.id = id; public int getId() => this.id;

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
                bool avul = false;
                string sor = sr.ReadLine();
                string[] sorok = sor.Split(';');
                if (sorok[4] == "igen")
                {
                    avul = true;
                }
                Eszkozok eszkoz = new Eszkozok(sorok[0], Convert.ToInt32(sorok[1]), Convert.ToInt32(sorok[2]), sorok[3], avul);
                if (Adatok.Count > 0)
                {
                    int utoloId = Adatok[Adatok.Count - 1].getId();
                    eszkoz.setId(utoloId + 1);
                } else
                {
                    eszkoz.setId(0);
                }
                Adatok.Add(eszkoz);
            }
            sr.Close();
            
        
        }
    }
}
