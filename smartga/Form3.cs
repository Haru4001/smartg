using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Response;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace smartga
{
    public partial class Form3 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "aaa", BasePath = "bbb" // Firebase에서 가져온 것으로 대체해야 함
        };

        IFirebaseClient client;

        DataTable dt = new DataTable();

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("순서");
            dt.Columns.Add("미세먼지");
            dt.Columns.Add("온습도");

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            export();
        }

        private async void export()
        {
            FirebaseResponse response = await client.GetAsync(""); // Firebase 데이터 경로를 넣어야함
            if (response.Body != null)
            {
                var data = response.ResultAs<Dictionary<string, DataModel>>();

                dt.Rows.Clear();

                int order = 1;
                foreach (var item in data)
                {
                    string dust = item.Value.DustLevel.ToString();
                    string temperature = item.Value.Temperature.ToString();

                    dt.Rows.Add(order, dust, temperature);
                    order++;
                }
            }
        }
    }
}
public class DataModel
{
    public int DustLevel { get; set; }
    public double Temperature { get; set; }
}
