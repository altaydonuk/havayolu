using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace havalimani
{
    public partial class Form5 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database= havalimani; user ID = postgres; password=123456");
        public Form5()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu = new NpgsqlCommand ("select * from bilet where identity like '%" + textBox5.Text + "%'", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            dataGridView1.DataSource = ds.Tables[0];
            this.dataGridView1.Columns["flightid"].Visible = false;
            this.dataGridView1.Columns["ticketid"].Visible = false;
            this.dataGridView1.Columns["identity"].Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}
