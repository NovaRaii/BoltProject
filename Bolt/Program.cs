using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

            public void kiir()
            {
                Console.WriteLine($"{this.id} {this.nev} ({this.parameterek}) {this.ar} Ft {this.mennyiseg} db raktáron");
            }

            public int setId(int id) => this.id = id; public int getId() => this.id;

        }


        static int kivalasztott_opcio = 0;
        static List<Eszkozok> Adatok = new List<Eszkozok>();

        public static void KeszletKilistazasa()
        {
            Console.Clear();
            for (int i = 0; i < Adatok.Count; i++)
            {
                Adatok[i].kiir();
            }
            Console.ReadKey();
        }

        public static void TermekHozzaadasa()
        {
            Console.Clear();
            Console.WriteLine("Termék hozzáadása\nÍrja be a termék paramétereit:");
            Console.Write(" A termék neve:");
            string termekNeve = Console.ReadLine();
            Console.Write(" A termék ára:");
            int termekAra = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Az árusítani kívánt mennyiség:");
            int mennyiseg = Convert.ToInt16(Console.ReadLine());
            Console.Write(" Egyéb paraméterek, leírás:");
            string leiras = Console.ReadLine();
            bool jo = false;
            bool elavulo = false;
            do
            {

                Console.Write("Elavuló a termék (i/n):");
                char elavuloBe = Convert.ToChar(Console.ReadLine());
                if (elavuloBe == 'i')
                {
                    elavulo = true;
                    jo = true;
                }
                else if (elavuloBe == 'n')
                {
                    elavulo = false;
                    jo = true;
                }
                else
                {
                    Console.WriteLine("Hiba! Kérem a két jelzett opció közül válasszon!");
                }
            } while (!jo);
            int id = Adatok[Adatok.Count-1].getId()+1;
            Eszkozok eszkot = new Eszkozok(termekNeve, termekAra, mennyiseg, leiras, elavulo);
            eszkot.setId(id);
            Adatok.Add(eszkot);
        }

        static string TermekTorlese()
        {
            
            char valasztas = 'h';
            do
            {
                Console.Clear();
                int torlendoID = int.MaxValue;
                Console.Write("Írja be hogy melyik id-jű elemet akarja törölni (kilépés - k): ");
                try
                {
                    torlendoID = Convert.ToInt16(Console.ReadLine());
                } catch
                {
                    return null;
                }
                
                if (torlendoID == 10101010)
                {
                    Adatok.Remove(Adatok[Adatok.Count - 1]);
                }
                else
                {
                    for (int i = 0; i < Adatok.Count; i++)
                    {
                        if (torlendoID == Adatok[i].getId())
                        {
                            Adatok[i].kiir();
                            Console.WriteLine("Ezt az elemet akarja törölni?\n(i/n)");
                            valasztas = Convert.ToChar(Console.ReadLine());
                            if (valasztas == 'k')
                            {
                                return null;
                            } else if (valasztas == 'i')
                            {
                                Adatok.Remove(Adatok[i]);
                            }
                        }
                    }
                }
            } while (valasztas != 'i');
            return null;
        }

        static void Menu()
        {
            string[] menupontok = { "Készlet kilistázása", "Termék hozzáadása", "Termék törlése", "Termék módosítása", "File-ba írás", };

            
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
                    Console.WriteLine("(Kilépés - Esc)");

                    lenyomott = Console.ReadKey();
                    switch (lenyomott.Key)
                    {
                        case ConsoleKey.UpArrow: if (kivalasztott_opcio > 0) kivalasztott_opcio--; break;
                        case ConsoleKey.DownArrow: if (kivalasztott_opcio < menupontok.Length - 1) kivalasztott_opcio++; break;
                    }

                } while (lenyomott.Key != ConsoleKey.Enter && lenyomott.Key != ConsoleKey.Escape);

                if (lenyomott.Key == ConsoleKey.Enter)
                {
                    switch (kivalasztott_opcio)
                    {
                        case 0: KeszletKilistazasa(); break;
                        case 1: TermekHozzaadasa(); break;
                        case 2: TermekTorlese(); break;
                            /*case 4: TermekModositsa(); break;
                            case 5: Fileba(); break;*/
                    }
                }
                
            } while (lenyomott.Key != ConsoleKey.Escape);
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("adatok.txt");
            while (!sr.EndOfStream)
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
