using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace tpmodul8_103022330026
{
    class CovidConfig
    {
        // Path untuk menyimpan file konfigurasi
        public static string filePath = Path.Combine(Directory.GetCurrentDirectory(), "covid_config.json");

        [JsonPropertyName("satuan_suhu")] // 
        public string SatuanSuhu { get; set; }
        [JsonPropertyName("batas_hari_demam")]
        public int BatasDemam { get; set; }
        [JsonPropertyName("pesan_ditolak")]
        public string PesanDitolak { get; set; }
        [JsonPropertyName("pesan_diterima")]
        public string PesanDiterima { get; set; }

        // Constructor parameterless default untuk default config
        public CovidConfig()
        {
            setDefault();
        }

        public CovidConfig(string satuan_suhu, int batas_hari_demam, string pesan_ditolak, string pesan_diterima)
        // construktor untuk config baru
        {
            this.SatuanSuhu = satuan_suhu;
            this.BatasDemam = batas_hari_demam;
            this.PesanDitolak = pesan_ditolak;
            this.PesanDiterima = pesan_diterima;
        }

        public void UbahSatuan()
        // Method untuk mengubah satuan suhu
        {
            if (SatuanSuhu == "celcius")
            {
                SatuanSuhu = "fahrenheit";
            }
            else
            {
                SatuanSuhu = "celcius";
            }
            // Simpan perubahan ke file
            SaveNewConfig();
        }

        public void loadConfig()
        // Method untuk memuat konfigurasi dari file
        {
            // pengecekan file
            if (File.Exists(filePath))
            {
                try
                {
                    // Membaca file JSON
                    string config_json = File.ReadAllText(filePath);
                    // Menggunakan JsonSerializer untuk mendeserialisasi JSON ke objek CovidConfig
                    CovidConfig configFromFile = JsonSerializer.Deserialize<CovidConfig>(config_json);

                    // Perbarui nilai properti dengan hasil dari file
                    SatuanSuhu = configFromFile.SatuanSuhu;
                    BatasDemam = configFromFile.BatasDemam;
                    PesanDitolak = configFromFile.PesanDitolak;
                    PesanDiterima = configFromFile.PesanDiterima;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                }
            }
            else
            {
                // Jika file tidak ditemukan, simpan objek yang sudah memiliki default ke file
                SaveNewConfig();
                Console.WriteLine("File tidak ditemukan, membuat file baru");
            }
        }

        public void SaveNewConfig()
        {
            try
            {
                // Menggunakan JsonSerializer untuk serialisasi objek ke JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                // Menyimpan JSON ke file
                string jsonString = JsonSerializer.Serialize(this, options);
                // Menulis JSON ke file
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine("Berhasil menyimpan konfigurasi baru");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal menyimpan konfigurasi baru: " + ex.Message);
            }
        }

        private void setDefault()
        // Method untuk mengatur nilai default
        {
            this.SatuanSuhu = "celcius";
            this.BatasDemam = 14;
            this.PesanDitolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            this.PesanDiterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        public string GetMessage(string status)
        // Method untuk mendapatkan pesan berdasarkan status
        {
            return status == "ditolak" ? PesanDitolak : PesanDiterima;
        }
    }
}