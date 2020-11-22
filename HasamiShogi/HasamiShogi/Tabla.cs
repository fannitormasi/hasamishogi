using System;
using System.Threading;

namespace HasamiShogi
{
    public class Tabla
    {
        public object[,] JatekTer { get; set; }
        public char[] Betuk { get; }
        public char[] Szamok { get; }
        public Jatekos[] Jatekosok { get; }
        private bool Betoltott = false;

        public Tabla(Jatekos[] jatekosok, Jatekos jelenlegiJatekos)
        {
            JatekTer = new object[9, 9];
            Betuk = new char[9];
            Szamok = new char[9];
            Jatekosok = jatekosok;

            for (int i = 0; i < JatekTer.GetLength(0); i++)
            {
                for (int j = 0; j < JatekTer.GetLength(1); j++)
                {
                    JatekTer[i, j] = '-';
                }
            }

            int charNumber = 65;
            for (int k = 0; k < Betuk.Length; k++)
            {
                Betuk[k] = (char)charNumber;
                charNumber++;
            }

            charNumber = 49;
            for (int l = 0; l < Szamok.Length; l++)
            {
                Szamok[l] = (char)charNumber;
                charNumber++;
            }
        }

        public void AlapTablaKirajzolasa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("({0}:{1})", Jatekosok[0].PontSzam, Jatekosok[1].PontSzam);

            for (int k = 0; k < Szamok.Length; k++)
            {
                Console.Write("[" + Szamok[k] + "] ");
            }
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < JatekTer.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("[" + Betuk[i] + "]  ");
                for (int j = 0; j < JatekTer.GetLength(0); j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" " + JatekTer[i, j] + "  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void JatekosokFelallitasa(Jatekos[] jatekosok)
        {
            if (!Betoltott)
            {
                for (int i = 0; i < jatekosok.Length; i++)
                {
                    for (int j = 0; j < jatekosok[i].Babuk.Length; j++)
                    {
                        JatekTer[jatekosok[i].Babuk[j].Pozicio[0], jatekosok[i].Babuk[j].Pozicio[1]] = jatekosok[i].JatekosSzam;
                        AlapTablaKirajzolasa();
                        Thread.Sleep(100);
                    }
                }
                Betoltott = true;
            }
            else
            {
                for (int i = 0; i < jatekosok.Length; i++)
                {
                    for (int j = 0; j < jatekosok[i].Babuk.Length; j++)
                    {
                        JatekTer[jatekosok[i].Babuk[j].Pozicio[0], jatekosok[i].Babuk[j].Pozicio[1]] = jatekosok[i].JatekosSzam;
                        AlapTablaKirajzolasa();
                    }
                }
            }
        }

        public void TablaFrissitese(string uzenet)
        {
            JatekosokFelallitasa(Jatekosok);
            if (uzenet != "")
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(uzenet);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("(" + MainClass.JelenlegiJatekos.JatekosSzam + ") ");
            MainClass.Parancs = Console.ReadLine();
        }

        public Jatekos MasikJatekosVisszaadasa()
        {
            if (MainClass.JelenlegiJatekos.PontSzam == 1)
            {
                return Jatekosok[1];
            }
            else
            {
                return Jatekosok[0];
            }
        }

        public string JatekosBabujanakMozgatasa(int[] regiPozicio, int[] ujPozicio, Tabla tabla)
        {
            bool megfeleloALepesszam = EgyetLepett(regiPozicio, ujPozicio);
            bool nemLepettAtlosan = NemLepettAtlosan(regiPozicio, ujPozicio);
            bool uresAzUjMezo = UresAzUjMezo(ujPozicio, tabla);


            if (!UgrasE(regiPozicio, ujPozicio, tabla))
            {
                return "You can't move more than one step";
            }

            if (!nemLepettAtlosan)
            {
                return "You can't move diagonally";
            }

            if (!uresAzUjMezo)
            {
                return "The target field is not empty";
            }

            for (int i = 0; i < MainClass.JelenlegiJatekos.Babuk.Length; i++)
            {

                if (MainClass.JelenlegiJatekos.Babuk[i].Pozicio[0] == regiPozicio[0]
                    && MainClass.JelenlegiJatekos.Babuk[i].Pozicio[1] == regiPozicio[1])
                {
                    MainClass.JelenlegiJatekos.Babuk[i].Pozicio = ujPozicio;
                    return "";
                }
            }

            return "The start field isn't yours";
        }

        public bool EgyetLepett(int[] regiPozicio, int[] ujPozicio)
        {
            return (regiPozicio[0] - ujPozicio[0] == 1 || regiPozicio[0] - ujPozicio[0] == -1)
            || (regiPozicio[1] - ujPozicio[1] == 1 || regiPozicio[1] - ujPozicio[1] == -1);
        }

        public bool NemLepettAtlosan(int[] regiPozicio, int[] ujPozicio)
        {
            return regiPozicio[1] == ujPozicio[1] || regiPozicio[0] == ujPozicio[0];
        }

        public bool UresAzUjMezo(int[] ujPozicio, Tabla tabla)
        {
            return tabla.JatekTer[ujPozicio[0], ujPozicio[1]].Equals('-');
        }

        public bool UgrasE(int[] regiPozicio, int[] ujPozicio, Tabla tabla)
        {
            int index;
            if (regiPozicio[1] == ujPozicio[1])
            {
                if (regiPozicio[0] < ujPozicio[0])
                {
                    index = regiPozicio[0] + 1;

                    while (index < ujPozicio[0] && !tabla.JatekTer[index, regiPozicio[1]].Equals('-'))
                    {
                        index++;
                    }
                    return index >= ujPozicio[0];
                }
                else
                {
                    index = ujPozicio[0] + 1;

                    while (index  < regiPozicio[0] && !tabla.JatekTer[index, regiPozicio[1]].Equals('-'))
                    {
                        index++;
                    }
                    return index >= regiPozicio[0];
                }
            }
            if (regiPozicio[0] == ujPozicio[0])
            {
                if (regiPozicio[1] < ujPozicio[1])
                {
                    index = regiPozicio[1] + 1;

                    while (index < ujPozicio[1] && !tabla.JatekTer[regiPozicio[0], index].Equals('-'))
                    {
                        index++;
                    }
                    return index >= ujPozicio[1];
                }
                else
                {
                    index = ujPozicio[1] + 1;

                    while (index < regiPozicio[1] && !tabla.JatekTer[regiPozicio[0], index].Equals('-'))
                    {
                        index++;
                    }
                    return index >= regiPozicio[1];
                }
            }
            else
            {
                return true;

            }
        }
    }
}
