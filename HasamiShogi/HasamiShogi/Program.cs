using System;
using System.Text;

namespace HasamiShogi
{
    class MainClass
    {
        public static string Parancs;
        public static int JatekTipus;
        public static bool JatekVege = false;
        public static Jatekos JelenlegiJatekos;

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Select game type to start the game [1 or 2]: ");
            JatekTipus = int.Parse(Console.ReadLine());

            Jatekos ElsoJatekos = new Jatekos(1, JatekTipus);
            Jatekos MasodikJatekos = new Jatekos(2, JatekTipus);
            Jatekos[] jatekosok = new Jatekos[] { ElsoJatekos, MasodikJatekos };
            Tabla Tabla = new Tabla(jatekosok, ElsoJatekos);
            JelenlegiJatekos = ElsoJatekos;

            Tabla.AlapTablaKirajzolasa();
            Tabla.JatekosokFelallitasa(Tabla.Jatekosok);

            Console.Write("(" + JelenlegiJatekos.JatekosSzam + ") ");
            Parancs = Console.ReadLine();

            while (!JatekVege)
            {
                if (Parancs.Length > 4 && Parancs.Substring(0, 4) == "move")
                {
                    string[] koordinatak = Parancs.Split(' ');
                    char[] regiKoordinata = new char[2];
                    char[] ujKoordinata = new char[2];
                    int[] regiIndex = new int[2];
                    int[] ujIndex = new int[2];

                    regiKoordinata[0] = (char)koordinatak[1][0];
                    regiKoordinata[1] = (char)koordinatak[1][1];
                    ujKoordinata[0] = (char)koordinatak[2][0];
                    ujKoordinata[1] = (char)koordinatak[2][1];

                    for (int i = 0; i < Tabla.Betuk.Length; i++)
                    {
                        if (Tabla.Betuk[i] == regiKoordinata[0])
                        {
                            regiIndex[0] = i;
                        }
                        if (Tabla.Betuk[i] == ujKoordinata[0])
                        {
                            ujIndex[0] = i;
                        }
                    }

                    for (int j = 0; j < Tabla.Szamok.Length; j++)
                    {
                        if (Tabla.Szamok[j] == regiKoordinata[1])
                        {
                            regiIndex[1] = j;
                        }
                        if (Tabla.Szamok[j] == ujKoordinata[1])
                        {
                            ujIndex[1] = j;
                        }
                    }

                    string valasz = Tabla.JatekosBabujanakMozgatasa(regiIndex, ujIndex, Tabla);

                    if (valasz == "")
                    {
                        Tabla.JatekTer[regiIndex[0], regiIndex[1]] = '-';

                        if (JelenlegiJatekos == ElsoJatekos)
                        {
                            JelenlegiJatekos = MasodikJatekos;
                        }
                        else
                        {
                            JelenlegiJatekos = ElsoJatekos;
                        }
                    }

                    Tabla.TablaFrissitese(valasz);
                }

                else if (Parancs == "help")
                {
                    Tabla.TablaFrissitese("COMMANDS: move <X1 Y2> | save <fileName> | open <fileName> | giveup | help | exit");
                }

                else if (Parancs == "exit")
                {
                    Environment.Exit(0);
                }

                else if (Parancs == "giveup")
                {
                    Console.WriteLine("Player number {0} won the game!", Tabla.MasikJatekosVisszaadasa().JatekosSzam);
                    Environment.Exit(0);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Tabla.TablaFrissitese("This command does not exist!");
                }
            }
        }

    }
}

