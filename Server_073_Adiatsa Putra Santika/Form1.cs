
using ServiceRest_073_Adiatsa_Putra_Santika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel.Channels;
using System.Windows.Forms;

namespace Server_073_Adiatsa_Putra_Santika
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Server Nyala";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceHost hostObjek = null;
            Uri address = new Uri("http://localhost:1907/Mahasiswa");
            WebHttpBinding bind = new WebHttpBinding();
            try
            {
                hostObjek = new ServiceHost(typeof(TI_UMY), address);
                //ALAMAT BASE ADDRESS
                hostObjek.AddServiceEndpoint(typeof(ITI_UMY), bind, "");


                //ALAMAT ENDPOINT
                //wsdl
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior(); //Service Runtime Player
                smb.HttpGetEnabled = true; //untuk mengaktifkan wsdl (dibuka saat development, tidak untuk dibuka)
                hostObjek.Description.Behaviors.Add(smb);
                //mex
                System.ServiceModel.Channels.Binding mexbind = MetadataExchangeBindings.CreateMexHttpBinding();
                hostObjek.AddServiceEndpoint(typeof(IMetadataExchange), mexbind, "mex");

                WebHttpBehavior whb = new WebHttpBehavior();
                whb.HelpEnabled = true;
                hostObjek.Description.Endpoints[0].EndpointBehaviors.Add(whb);

                hostObjek.Open();
                label1.Text = "Server Nyala";
                hostObjek.Close();
            }
            catch (Exception ex)
            {
                hostObjek = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceHost hostObjek = null;
            Uri address = new Uri("http://localhost:1907/Mahasiswa");
            WebHttpBinding bind = new WebHttpBinding();
            try
            {
                hostObjek = new ServiceHost(typeof(TI_UMY), address);
                hostObjek.AddServiceEndpoint(typeof(ITI_UMY), bind, "");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                hostObjek.Description.Behaviors.Add(smb);
                System.ServiceModel.Channels.Binding mexbind = MetadataExchangeBindings.CreateMexHttpBinding();
                hostObjek.AddServiceEndpoint(typeof(IMetadataExchange), mexbind, "mex");

                WebHttpBehavior whb = new WebHttpBehavior();
                whb.HelpEnabled = true;
                hostObjek.Description.Endpoints[0].EndpointBehaviors.Add(whb);

                hostObjek.Abort();
                label1.Text = "Silakan Nyalakan Server";
                button1.Enabled = false;
                button2.Enabled = false;

            }
            catch (Exception ex)
            {
                hostObjek = null;
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
