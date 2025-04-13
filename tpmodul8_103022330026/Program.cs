using System;
using tpmodul8_103022330026;

namespace tpmodul8_103022330026
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Membuat Objeck CovidConfig
            var config = new CovidConfig();
            // Memamnggil Method loadConfig
            config.loadConfig();


            Console.WriteLine("Selamat datang di aplikasi pemeriksaan Covid-19");
            Console.WriteLine($"Satuan suhu saat ini: {config.SatuanSuhu}");
            Console.Write("Apakah anda ingin mengubah satuan suhu? (Y/N) : ");
            string ubahSatuan = Console.ReadLine();
            if (ubahSatuan.ToLower() == "y")
            {
                config.UbahSatuan();
                Console.WriteLine($"Satuan suhu diubah menjadi: {config.SatuanSuhu}");
            }

            Console.WriteLine();
            Console.WriteLine("Pertanyaan");
            Console.Write($"Berapa suhu badan anda saat ini? (dalam nilai {config.SatuanSuhu}): ");
            double suhu = Convert.ToDouble(Console.ReadLine());

            if (config.SatuanSuhu == "celcius")
            {
                if (suhu < 36.5 || suhu > 37.5)
                {
                    Console.WriteLine();
                    Console.WriteLine(config.GetMessage("ditolak"));
                    return;
                }
            }
            else
            {
                if (suhu < 97.7 || suhu > 99.5)
                {
                    Console.WriteLine();
                    Console.WriteLine(config.GetMessage("ditolak"));
                    return;
                }
            }

            Console.Write("Berapa hari yang lalu anda terakhir memiliki gejala demam? : ");
            int hari = Convert.ToInt32(Console.ReadLine());
            if (hari <= config.BatasDemam)
            {
                Console.WriteLine();
                Console.WriteLine(config.GetMessage("ditolak"));
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(config.GetMessage("diterima"));
            }
        }
    }
}