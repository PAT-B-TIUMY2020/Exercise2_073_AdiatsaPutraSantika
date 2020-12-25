using Newtonsoft.Json;
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
using static Exercise2_073_Adiatsa_Putra_Santika.Form1;

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

        public class Mahasiswa
        {
            private string _nama, _nim, _prodi, _angkatan;
            public string nama
            {
                get { return _nama; }
                set { _nama = value; }
            }

            public string nim
            {
                get { return _nim; }
                set { _nim = value; }
            }

            public string prodi
            {
                get { return _prodi; }
                set { _prodi = value; }
            }

            public string angkatan
            {
                get { return _angkatan; }
                set { _angkatan = value; }
            }
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
            TampilData();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            if (MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string nim = textBox1.Text;
                var item = data.Where(x => x.nim == textBox1.Text).FirstOrDefault();
                if (item != null)
                {
                    data.Remove(item);
                    // Save everything
                    string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                    var postdata = new WebClient();
                    postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    string response = postdata.UploadString(baseUrl + "DeleteMahasiswa", output);
                    Console.WriteLine(response);
                    TampilData();
                }

            }
        }
        public void TampilData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            dataGridView1.DataSource = data;
        }

        public void SearchData()
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);
            string nim = textBox1.Text;
            if (nim == null || nim == "")
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                var item = data.Where(x => x.nim == textBox1.Text).ToList();

                dataGridView1.DataSource = item;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBox2.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            textBox3.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            textBox4.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("http://localhost:1907/Mahasiswa");
            var data = JsonConvert.DeserializeObject<List<Mahasiswa>>(json);

            string nim = textBox1.Text;
            var item = data.Where(x => x.nim == textBox1.Text).FirstOrDefault();
            if (item != null)
            {
                // update logger with your textboxes data
                item.nama = textBox1.Text;
                item.nim = textBox2.Text;
                item.prodi = textBox3.Text;
                item.angkatan = textBox4.Text;

                // Save everything
                string output = JsonConvert.SerializeObject(item, Formatting.Indented);
                var postdata = new WebClient();
                postdata.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = postdata.UploadString(baseUrl + "UpdateMahasiswa", output);
                Console.WriteLine(response);
                TampilData();
            }
        }
    }


    }


}
