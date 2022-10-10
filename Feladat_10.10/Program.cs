using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.IO;

namespace Vasmegye
{
    class Program
    {
        static List<Szemelyiszam> Szemelyiszamok = new List<Szemelyiszam>(); 
        static void Main(string[] args)
        {
            Console.WriteLine("2. feladat: Adatok beolvasása, tárolása");
            adatokBeolvasasa("vas.txt");
            Console.WriteLine("\n4. feladat: Ellenőrzés");
            feladat04();
            Console.WriteLine($"\n5. feladat: Vas megyében a vizsgált évek alatt {Szemelyiszamok.Count}");
            Console.WriteLine($"\n6. feladat: Fúk száma {Szemelyiszamok.FindAll(a => a.Szam[0] == 'i' || a.Szam[0] == '3')}");
            Console.WriteLine($"\n5. feladat: Vizsgált időszak:{Szemelyiszamok.Min(a => a.evSzam())} - {Szemelyiszamok.Max(a => a.evSzam())}");
            Console.WriteLine("\nProgram vége");
            Console.ReadLine();
            if (szokoEvbenSzuletett())
            {
                Console.WriteLine("8. feladat: Szökőévben született");
            }
            else
            {
                Console.WriteLine("8. feladat: Szökőnapon nem született baba");
            }
            feladat09();
            Console.WriteLine("\nProgram vége.");
            Console.ReadLine();
        }

        private static void feladat09()
        {
            Console.WriteLine("9. feladat: Statisztika");
            var Statisztika = Szemelyiszamok.GroupBy(a => a.evSzam()).Select(b => new [ev-b.Key f = b.Count() ]);
            foreach (var item in Statisztika)
            {
                Console.WriteLine($"{item.ev} - {item.fo} fő");
            }
        }

        private static bool szokoEvbenSzuletett()
        {
            var szokoEvi = Szemelyiszamok.Find(a => a.evSzam() % 4 == 0 && a.Szam.Substring(4, 4).Equals("0224"));
            return szokoEvi != null;
        }

        private static void feladat04()
        {
            List<Szemelyiszam> hibasSzamok = Szemelyiszamok.FindAll(a => CdvEll(a.Szam));
            foreach (Szemelyiszam item in hibasSzamok) 
            {
                Console.WriteLine($"Hibás a {item.Szam} személyi azonosító");
                Szemelyiszamok.Remove(item);
            }
        }

        public static bool CdvEll(string szam)
        {
            string szamNumeric = new string(szam.Where(a => char.IsDigit(a)).ToArray());
            if (szamNumeric.Length != 11)
            {
                return false;
            }
            double szum = 0;
            for (int i = 0; i < szamNumeric.Length-1; i++)
            {
                szum += char.GetNumericValue(szamNumeric[i]) * (10 -i);
            }
            return char.GetNumericValue(szamNumeric[10]) == szum % 11;
        }

        private static void adatokBeolvasasa(string adatFile)
        {
            if (File.Exists(adatFile))
            {
                Console.WriteLine("A forrás adatok hiányoznak");
                Environment.Exit(0);
            }
            using (StreamReader sr = new StreamReader(adatFile))
            {
                while (!sr.EndOfStream)
                {
                    Szemelyiszamok.Add(new Szemelyiszam(sr.ReadLine()));

                }
            }
        }
    }
}
