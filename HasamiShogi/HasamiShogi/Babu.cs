using System;
namespace HasamiShogi
{
    public class Babu
    {
        public int JatekosSzama{ get; }
        public int[] Pozicio { get; set; }

        public Babu(int jatekosSzama)
        {
            JatekosSzama = jatekosSzama;
            Pozicio = new int[2];
        }
    }
}
