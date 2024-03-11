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

            public T getErtek<T>(string _melyik)
            {
                if (typeof(T) == typeof(string))
                {
                    if (_melyik == "nev")
                    {
                        return (T)(object)nev;
                    } 
                    else if (_melyik == "parameter")
                    {
                        return (T)(object)parameterek;
                    }
                    else
                    {
                        throw new ArgumentException("nem létező string típusú kérés!");
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    if (_melyik == "id")
                    {
                        return (T)(object)id;
                    } 
                    else if (_melyik == "ar")
                    {
                        return (T)(object)ar;
                    } 
                    else if (_melyik == "menyiseg")
                    {
                        return (T)(object)mennyiseg;
                    }
                    else
                    {
                        throw new ArgumentException("nem létező int típusú kérés!");
                    }
                }
                else if (typeof(T) == typeof(bool))
                {
                    if (_melyik == "elavulo")
                    {
                        return (T)(object)elavulo;
                    }
                    else
                    {
                        throw new ArgumentException("nem létező bool típusú kérés!");
                    }
                }
                else
                {
                    throw new ArgumentException("nem létező típusú kérés!");
                }
            }

            public void setErtek<T>(string _melyik, T ertek)
            {
                if (typeof(T) == typeof(string))
                {
                    if (_melyik == "nev")
                    {
                        nev = (string)(object)ertek;
                    }
                    else if (_melyik == "parameter")
                    {
                        parameterek = (string)(object)ertek;
                    }
                    else
                    {
                        throw new ArgumentException("nem létező string típusú feltöltés!");
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    if (_melyik == "ar")
                    {
                        ar = (int)(object)ertek;
                    }
                    else if (_melyik == "menyiseg")
                    {
                        mennyiseg = (int)(object)ertek;
                    }
                    else if (_melyik == "elavulo")
                    {
                        if (elavulo == true)
                        {
                            elavulo = false;
                        }
                        else
                        {
                            elavulo = true;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("nem létező int típusú feltöltés!");
                    }
                }
                else
                {
                    throw new ArgumentException("nem létező típusú feltöltés!");
                }
            }

        }


        static int kivalasztott_opcio = 0;
        static List<Eszkozok> Adatok = new List<Eszkozok>();

        public static void KeszletKilistazasa()
        {
            Console.Clear();
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].getErtek<int>("menyiseg") < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Adatok[i].kiir();
                    Console.ForegroundColor = ConsoleColor.White;
                }
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

        static string TermekIDtKeres(string muvelet)
        {
            
            char valasztas = 'h';
            do
            {
                Console.Clear();
                int ID = int.MaxValue;
                Console.Write($"Írja be hogy melyik id-jű elemet akarja {muvelet} (kilépés - k): ");
                try
                {
                    ID = Convert.ToInt16(Console.ReadLine());
                } catch
                {
                    return null;
                }
                
                if (ID == -1)
                {
                    return Convert.ToString(Adatok.Count - 1);
                }
                else
                {
                    for (int i = 0; i < Adatok.Count; i++)
                    {
                        if (ID == Adatok[i].getId())
                        {
                            Adatok[i].kiir();
                            Console.WriteLine($"Ezt az elemet akarja {muvelet}?\n(i/n)");
                            valasztas = Convert.ToChar(Console.ReadLine());
                            if (valasztas == 'k')
                            {
                                return null;
                            } else if (valasztas == 'i')
                            {
                                return Convert.ToString(ID);
                            }
                        }
                    }
                }
            } while (valasztas != 'i');
            return null;
        }

        static string TermekTorlese()
        {
            string torlendoIDStrig = TermekIDtKeres("törölni");
            if (torlendoIDStrig != null)
            {
                int torlendoID = Convert.ToInt16(torlendoIDStrig);
                Adatok.Remove(Adatok[torlendoID]);
                return null;
            } else
            {
                return null;
            }
        }

        static string TermekModositsa()
        {
            Console.Clear();
            string modositandoIDStrig = TermekIDtKeres("módosítani");
            if (modositandoIDStrig != null)
            {
                int modositandoID = Convert.ToInt16(modositandoIDStrig);
                string[] opcio = { "név |", "| leirás |", "| darabszám |", "| ár |", "| elavuló-e |", "| kilépés a módosításból" };
                ConsoleKeyInfo lenyomott;
                int kivalasztott = 0;
                bool fut = false;
                do
                {
                    do
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Adatok[modositandoID].kiir();
                        Console.WriteLine("Mit akar módosítani a kiválaszott elemen?");
                        for (int i = 0; i < opcio.Length; i++)
                        {
                            if (kivalasztott == i)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.Write(opcio[i]);
                        }
                        lenyomott = Console.ReadKey();
                        if (lenyomott.Key == ConsoleKey.RightArrow)
                        {
                            if (kivalasztott == opcio.Length - 1)
                            {
                                kivalasztott = -1;
                            }
                            kivalasztott++;
                        }
                        else if (lenyomott.Key == ConsoleKey.LeftArrow)
                        {
                            if (kivalasztott < 1)
                            {
                                kivalasztott = opcio.Length;
                            }
                            kivalasztott--;
                        }

                    } while (lenyomott.Key != ConsoleKey.Enter);
                        switch (kivalasztott)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine($"régi név: {Adatok[modositandoID].getErtek<string>("nev")}");
                                Console.Write("Írja be az új nevét a terméknek:");
                                string ujnev = Console.ReadLine();
                                Adatok[modositandoID].setErtek<string>("nev", ujnev);
                                break;
                            case 1:
                                Console.Clear();
                                Console.WriteLine($"régi leírás: {Adatok[modositandoID].getErtek<string>("parameter")}");
                                Console.Write("Írja be az új leírását a terméknek:");
                                string ujleiras = Console.ReadLine();
                                Adatok[modositandoID].setErtek<string>("parameter", ujleiras);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine($"régi darabszám: {Adatok[modositandoID].getErtek<int>("menyiseg")}");
                                Console.Write("Írja be az új darabszámát a terméknek:");
                                int ujdarabszam = Convert.ToInt16(Console.ReadLine());
                                Adatok[modositandoID].setErtek<int>("menyiseg", ujdarabszam);
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine($"régi ár: {Adatok[modositandoID].getErtek<int>("ar")}");
                                Console.Write("Írja be az új árát a terméknek:");
                                int ujar = Convert.ToInt16(Console.ReadLine());
                                Adatok[modositandoID].setErtek<int>("ar", ujar);
                                break;
                            case 4:
                                Adatok[modositandoID].setErtek<int>("elavulo", 1);
                                break;
                            case 5:
                                fut = true;
                                break;
                        }
                } while (!fut);
                return null;
            }
            else
            {
                return null;
            }
        }

        static void Fileba()
        {
            StreamWriter r = new StreamWriter("adatok.txt");
            for (int i = 0; i < Adatok.Count; i++)
            {
                string elavuloki = "nem";
                if (Adatok[i].getErtek<bool>("elavulo"))
                {
                    elavuloki = "igen";
                }
                r.WriteLine($"{Adatok[i].getErtek<string>("nev")};{Adatok[i].getErtek<int>("ar")};{Adatok[i].getErtek<int>("menyiseg")};{Adatok[i].getErtek<string>("parameter")};{elavuloki}");
            }
            r.Close();
        }

        static void LearazottKilistazas()
        {
            Console.Clear();
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].getErtek<bool>("elavulo"))
                {
                    Adatok[i].kiir();
                }
            }
            Console.ReadKey();
        }

        static void Menu()
        {
            string[] menupontok = { "Készlet kilistázása", "Termék hozzáadása", "Termék törlése", "Termék módosítása", "Learazott áruk kilistázása", "File-ba írás" };

            
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
                    Console.ForegroundColor = ConsoleColor.White;
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
                        case 3: TermekModositsa(); break;
                        case 4: LearazottKilistazas(); break;
                        case 5: Fileba(); break;
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
