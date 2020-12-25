using Newtonsoft.Json;
using ServiceRest_073_Adiatsa_Putra_Santika;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise2_073_Adiatsa_Putra_Santika
{
    public partial class Form1 : Form
    {
        string baseUrl = "http://localhost:1907/";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Mahasiswa mhs = new Mahasiswa();
            mhs.nama = textBox1.Text;
            mhs.nim = textBox2.Text;
            mhs.prodi = textBox3.Text;
            mhs.angkatan = textBox4.Text;

            var data = JsonConvert.SerializeObject(mhs);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseUrl + "Mahasiswa", data);
                Console.WriteLine(response);

            
        }
    }

    class ClassData
    {
        public void getData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            //foreach (var mhs in data)
            //{
            //    Console.WriteLine("NIM: " + mhs.nim);
            //    Console.WriteLine("Nama: " + mhs.nama);
            //    Console.WriteLine("Prodi: " + mhs.prodi);
            //    Console.WriteLine("Angkatan: " + mhs.angkatan);
            //}
            //Console.ReadLine();
        }
    
    }


}
