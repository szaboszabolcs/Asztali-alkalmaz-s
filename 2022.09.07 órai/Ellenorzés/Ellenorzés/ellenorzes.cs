using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ellenorzés
{
    internal class ellenorzes
    {
        static void Main(string[] args)
        {
            List<Record> records = new List<Record>();
            StreamReader reader = new StreamReader("meresek.txt");
            while (!reader.EndOfStream)
            {
                Record egyRecord= new Record(reader.ReadLine());
                records.Add(egyRecord);

            }
            reader.Close();
            Console.WriteLine("2. feladat");
            Console.WriteLine($"A mérés során {records.Count} jármű adatait rögzítették.");
            Console.WriteLine("3. feladat");
            int db = 0;
            foreach (Record egyRecord in records)
            {
                if (egyRecord.kOra<9)
                {
                    db++;
                }
            }
            Console.WriteLine($"9 óra előtt {db} jármű haladt el a végponti mérőnél.");
            Console.ReadKey();
        }
    }
}