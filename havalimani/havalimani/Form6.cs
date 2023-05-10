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
    public partial class Form6 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database= havalimani; user ID = postgres; password=123456");
        public Form6()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          /*DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("call admin_ekle(p1,p2)", baglanti  );
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("admins_username= @p1" , textBox1.Text);
            komut.Parameters.AddWithValue("admins_password= @p2" ,  int.Parse(textBox2.Text)); 
            komut.ExecuteNonQuery();
            baglanti.Close();*/
           
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }
    }
}
