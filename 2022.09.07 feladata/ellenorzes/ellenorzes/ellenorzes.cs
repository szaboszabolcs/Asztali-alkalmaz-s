using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace ellenorzes
{
    class ellenorzes
    {
        static List<string> JarmuvekAdatai = new List<string>();
        static int mertszakasz = 10;

        static List<string> beolvas(string fajlnev)
        {
            List<string> adatok = new List<string>(File.ReadAllLines(fajlnev));
            return adatok;
        }

        static int ForgalomNagysag()
        {
            int forgnagysag = JarmuvekAdatai.Count;
            int db = 0;
            for (int i = forgnagysag - 1; i >= 0; i--)
            {
                if (JarmuvekAdatai[i].Split(' ')[5] == "9")
                {
                    db++;
                }
            }
            return forgnagysag - db;
        }

        static int AthaladKezdetiPonton(int ora, int perc)
        {
            int db = 0;
            for (int i = 0; i < JarmuvekAdatai.Count; i++)
            {
                if (Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[1]) == ora && Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[2]) == perc)
                {
                    db++;
                }
            }
            return db;
        }

        static double Atlagsebesseg(DateTime ido, int ut)
        {
            double t = (ido.Hour * 3600 + ido.Minute * 60 + ido.Second + (double)ido.Millisecond / 1000);
            return ((double)ut * 1000 / t) * 3.6;
        }

        static DateTime ElteltIdo(int sorszam)
        {
            return new DateTime() + IdostringToDateTime(JarmuvekAdatai[sorszam], 1).Subtract(IdostringToDateTime(JarmuvekAdatai[sorszam], 0));
        }

        static double ForgalomSuruseg(string oraperc)
        {
            int db = 0;
            for (int i = 0; i < JarmuvekAdatai.Count; i++)
            {
                int belepperc = Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[1]) * 3600 + Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[2]) * 60;
                int kilepperc = Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[5]) * 3600 + Convert.ToInt32(JarmuvekAdatai[i].Split(' ')[6]) * 60;
                int idopont = Convert.ToInt32(oraperc.Split(' ')[0]) * 3600 + Convert.ToInt32(oraperc.Split(' ')[1]) * 60;
                if (belepperc <= idopont && idopont <= kilepperc)
                {
                    db++;
                }
            }
            return (double)db / 10;
        }

        static DateTime IdostringToDateTime(string adatsor, int melyik)
        {
            int i = 0;
            if (melyik == 1) i = 4;
            string idostring = adatsor.Split(' ')[i + 1] + ":" +
                         adatsor.Split(' ')[i + 2] + ":" +
                         adatsor.Split(' ')[i + 3] + "." +
                         adatsor.Split(' ')[i + 4];
            DateTime ido = Convert.ToDateTime(idostring);
            return ido;
        }

        static int MaxSebesseg()
        {
            int max = 0;
            double maxertek = Atlagsebesseg(ElteltIdo(max), mertszakasz);
            for (int i = 1; i < JarmuvekAdatai.Count; i++)
            {
                if (maxertek < Atlagsebesseg(ElteltIdo(i), mertszakasz))
                {
                    max = i;
                    maxertek = Atlagsebesseg(ElteltIdo(max), mertszakasz);
                }
            }
            return max;
        }

        static int ElozesekSzama(int sorszam)
        {
            int elozes = 0;
            for (int i = sorszam - 1; i >= 0; i--)
            {
                if (IdostringToDateTime(JarmuvekAdatai[sorszam], 1) <= IdostringToDateTime(JarmuvekAdatai[i], 1))
                {
                    elozes++;
                }
            }
            return elozes;
        }

        static double GyorshajtokSzazalek()
        {
            int db = 0;
            for (int i = 0; i < JarmuvekAdatai.Count; i++)
            {
                if (90 < Atlagsebesseg(ElteltIdo(i), mertszakasz))
                {
                    db++;
                }
            }
            return (double)db / JarmuvekAdatai.Count;
        }
        static int BuntetesErteke(double sebesseg)
        {
            int buntetes = 0;
            if (sebesseg >= 104 && sebesseg < 121) buntetes = 30000;
            if (sebesseg >= 121 && sebesseg < 136) buntetes = 45000;
            if (sebesseg >= 136 && sebesseg < 151) buntetes = 60000;
            if (sebesseg >= 151) buntetes = 200000;
            return buntetes;
        }


        static void Main(string[] args)
        {
            // 1. feladat
            JarmuvekAdatai = beolvas("meresek.txt");

            //2. feladat
            Console.WriteLine("2. feladat");
            Console.WriteLine("A mérés során {0} jármű adatait rögzítették.", JarmuvekAdatai.Count);
            Console.WriteLine();

            //3. feladat
            Console.WriteLine("3. feladat");
            Console.WriteLine("9 óra előtt {0} jármű haladt el a végponti mérőnél.", ForgalomNagysag());
            Console.WriteLine();

            //4. feladat
            Console.WriteLine("4. feladat");
            Console.Write("Adjon meg egy óra és perc értéket! ");
            string oraperc = Console.ReadLine();
            int ora = Convert.ToInt32(oraperc.Split(' ')[0]);
            int perc = Convert.ToInt32(oraperc.Split(' ')[1]);
            int athaladtakdb = AthaladKezdetiPonton(ora, perc);
            Console.WriteLine("\ta. A kezdeti méréspontnál elhaladt járművek száma: {0} ", athaladtakdb.ToString());
            Console.WriteLine("\tb. A forgalomsűrűség: {0}", ForgalomSuruseg(oraperc).ToString("F1"));
            Console.WriteLine();

            //5. feladat
            Console.WriteLine("5. feladat");
            int max = MaxSebesseg();
            double maxv = Atlagsebesseg(ElteltIdo(max), mertszakasz);
            int elozesdb = ElozesekSzama(max);
            Console.WriteLine("A legnagyobb sebességgel haladó jármű \r\n" +
                                  "\trendszáma: {0} \r\n" +
                                  "\tátlagsebessége: {1} km/h \r\n" +
                                  "\táltal lehagyott járművek száma: {2}", JarmuvekAdatai[max].Split(' ')[0], ((int)maxv).ToString(), elozesdb.ToString());
            Console.WriteLine();

            //6. feladat
            Console.WriteLine("6. feladat");
            Console.WriteLine("A járművek {0}-a volt gyorshajtó.", GyorshajtokSzazalek().ToString("P"));
            Console.WriteLine();

            //7. feladat
            FileStream fs = new FileStream("buntetes.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < JarmuvekAdatai.Count; i++)
            {
                double v = Atlagsebesseg(ElteltIdo(i), mertszakasz);
                if (104 <= v)
                {
                    sw.WriteLine("{0}\t{1} km/h\t{2} Ft", JarmuvekAdatai[i].Split(' ')[0], ((int)v).ToString(), BuntetesErteke(v));
                }
            }
            sw.Close();
            fs.Close();

            Console.WriteLine("A fájl elkészült.");
            Console.ReadLine();
        }
    }
}