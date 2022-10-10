using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.IO;

namespace Vasmegye
{
    class Szemelyiszam
    {
        readonly string szam;

        public string Szam => szam;

        public Szemelyiszam(string szam)
        {
            this.szam = szam;

        }
        public int evSzam()
        {
            int evSzam = int.Parse(szam.Substring(2, 2));
            evSzam = szam[0] == '|' || szam[0] == '2' ? 1900 + evSzam : 2000 + evSzam;
            return evSzam;
        }
    }
}
