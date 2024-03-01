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


        static int kivalasztott_opcio = 0;
        static List<Eszkozok> Adatok = new List<Eszkozok>();

        public static void KeszletKilistazasa()
        {
            for (int i = 0; i < Adatok.Count; i++)
            {
                Console.WriteLine($"...");
            }
        }

        public static void TermekHozzaadasa()
        {
            Console.Clear();
            Console.WriteLine("Termék hozzáadása:\n Írja be a termék paramétereit:");
            Console.Write("A termék neve:");
            string termekNeve = Console.ReadLine();
            Console.Write("A termék ára:");
            int termekAra = Convert.ToInt32(Console.ReadLine());
            Console.Write("Az árusítani kívánt mennyiség:");
            int mennyiseg = Convert.ToInt16(Console.ReadLine());
            Console.Write("Egyéb paraméterek, leírás:");
            string leiras = Console.ReadLine();
            Console.Write("Elavuló a termék (i/n):");
            char elavuloBe = Convert.ToChar(Console.ReadLine());
            bool elavulo = false;
            if (elavuloBe == 'i')
            {
                elavulo = true;
            }
            Eszkozok eszkot = new Eszkozok(termekNeve, termekAra, mennyiseg, leiras, elavulo);
        }

        static void Menu()
        {
            string[] menupontok = { "Készlet kilistázása", "Termék hozzáadása", "Termék törlése", "Termék módosítása", "File-ba írás" };

            
            ConsoleKeyInfo lenyomott;

            do
            {
                do
                { 
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;

                    for (int i = 0; i < menupontok.Length; i++)
                    {
                        if (i == kivalasztott_opcio)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine(i + 1 + ")" + menupontok[i]);
                    }

                    lenyomott = Console.ReadKey();
                    switch (lenyomott.Key)
                    {
                        case ConsoleKey.UpArrow: if (kivalasztott_opcio > 0) kivalasztott_opcio--; break;
                        case ConsoleKey.DownArrow: if (kivalasztott_opcio < menupontok.Length - 1) kivalasztott_opcio++; break;
                    }

                } while (lenyomott.Key != ConsoleKey.Enter);

                switch (kivalasztott_opcio)
                {
                    case 0: KeszletKilistazasa(); break;
                    case 1: TermekHozzaadasa(); break;
                        /*case 3: TermekTorlese(); break;
                        case 4: TermekModositsa(); break;
                        case 5: Fileba(); break;*/
                }
            
            } while (lenyomott.Key != ConsoleKey.Escape);
        }

        static void Main(string[] args)
        {
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

            Menu();
        }
    }
}
