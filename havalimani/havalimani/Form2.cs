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

namespace havalimani
{
    public partial class Form2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database= havalimani; user ID = postgres; password=123456");
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string liste = "Select admins_username , admins_password from admins where admins_username=@p1 and admins_password=@p2";
                NpgsqlParameter prmt1 = new NpgsqlParameter("p1", textBox1.Text.Trim());
                NpgsqlParameter prmt2 = new NpgsqlParameter("p2", int.Parse( textBox2.Text.Trim()));
                NpgsqlCommand komut = new NpgsqlCommand(liste, baglanti);
                komut.Parameters.Add(prmt1);
                komut.Parameters.Add(prmt2);
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Form4 frm4 = new Form4();
                    frm4.Show();
                    this.Hide();
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Giriş Yaptınız");
                baglanti.Close();
            }

        }
    }
}
