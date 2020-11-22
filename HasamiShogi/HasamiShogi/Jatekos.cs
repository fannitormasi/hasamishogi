using System;
namespace HasamiShogi
{
    public class Jatekos
    {
        public int JatekosSzam { get; }
        public int PontSzam { get; }
        public Babu[] Babuk { get; }

        public Jatekos(int jatekosSzam, int jatekTipus)
        {
            JatekosSzam = jatekosSzam;
            PontSzam = 0;

            if (jatekTipus == 1)
            {
                Babuk = new Babu[18];

                for (int i = 0; i < Babuk.Length / 2; i++)
                {
                    Babuk[i] = new Babu(JatekosSzam);
                    Babuk[i + 9] = new Babu(JatekosSzam);
                    if (JatekosSzam == 1)
                    {
                        Babuk[i].Pozicio[0] = 8;
                        Babuk[i].Pozicio[1] = i;

                        Babuk[i + 9].Pozicio[0] = 7;
                        Babuk[i + 9].Pozicio[1] = i;
                    }
                    else
                    {
                        Babuk[i].Pozicio[0] = 0;
                        Babuk[i].Pozicio[1] = i;

                        Babuk[i + 9].Pozicio[0] = 1;
                        Babuk[i + 9].Pozicio[1] = i;
                    }
                }
            }
            else if (jatekTipus == 2)
            {
                Babuk = new Babu[9];

                for (int i = 0; i < Babuk.Length; i++)
                {
                    Babuk[i] = new Babu(JatekosSzam);
                    if (JatekosSzam == 1)
                    {
                        Babuk[i].Pozicio[0] = 8;
                        Babuk[i].Pozicio[1] = i;
                    }
                    else
                    {
                        Babuk[i].Pozicio[0] = 0;
                        Babuk[i].Pozicio[1] = i;
                    }
                }
            }
            else
            {
                Console.WriteLine("This game type isn't exist.");
                Environment.Exit(0);
            }
        }
    }
}
