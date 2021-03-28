using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SafeSingleton
{
    class Program
    {
        static void Main(String[] args)
        {
            MechIPaprocie mechipaprocie = MechIPaprocie.Instance;
            mechipaprocie.Zwirek = "To mój mech";
            mechipaprocie.Muchomorek = "A to moje paprocie";

            Console.WriteLine("Referencja nr 1: \n" + mechipaprocie.Zwirek + " - powiedział żwirek \n" + mechipaprocie.Muchomorek + " - odparł muchomorek \n");

            MechIPaprocie mechipaprocie2 = MechIPaprocie.Instance;
            Console.WriteLine("Referencja nr 2: \n" + mechipaprocie2.Zwirek + " - powiedział żwirek \n" + mechipaprocie2.Muchomorek + " - odparł muchomorek \n");

            MechIPaprocie mechipaprocie3 = MechIPaprocie.Instance;
            mechipaprocie3.Zwirek = "Sprzedaj mi troche paproci";
            mechipaprocie3.Muchomorek = "Nie";
            Console.WriteLine("Referencja nr 3: \n" + mechipaprocie3.Zwirek + " - poprosił żwirek \n" + mechipaprocie3.Muchomorek + " - odpowiedział muchomorek \n");

            Console.ReadKey();

            Thread process1 = new Thread(() =>
            {
                ThreadMechIPaprocieM("MECH");
            });
            Thread process2 = new Thread(() =>
            {
                ThreadMechIPaprocieM("PAPROCIE");
            });

            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();

            Console.ReadKey();
        }

        public static void ThreadMechIPaprocieM(string value)
        {
            ThreadMechIPaprocie singleton = ThreadMechIPaprocie.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    }
    class MechIPaprocie
    {
        private static MechIPaprocie instance = new MechIPaprocie();
        private string co_powiedzial_zwirek;
        private string co_powiedzial_muchomorek;
        private MechIPaprocie() { }
        internal static MechIPaprocie Instance { get => instance; set => instance = value; }
        public string Zwirek { get => co_powiedzial_zwirek; set => co_powiedzial_zwirek = value; }
        public string Muchomorek { get => co_powiedzial_muchomorek; set => co_powiedzial_muchomorek = value; }
    }


    class ThreadMechIPaprocie
    {
        private ThreadMechIPaprocie() { }

        private static ThreadMechIPaprocie _instance;

        private static readonly object _lock = new object();

        public static ThreadMechIPaprocie GetInstance(string value)
        {

            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ThreadMechIPaprocie();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }
        public string Value { get; set; }
    }

}
