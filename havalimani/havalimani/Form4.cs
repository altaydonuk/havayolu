using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace havalimani
{
    public partial class Form4 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database= havalimani; user ID = postgres; password=123456");
        public Form4()
        {
            InitializeComponent();
            PlaneCapacity();
            FromCity();
            WhereCity();
            Company();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel5.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible= false;
            string sorgu = ("select * from ucuslar");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            this.dataGridView1.Columns["flightid"].Visible = false;


        }
        public void PlaneCapacity()
        {
            cbCapacity.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorgua = new NpgsqlCommand("Select plane_capacity from plane", baglanti);

            sorgua.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgua);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                cbCapacity.Items.Add(dr["plane_capacity"].ToString());
            }
            baglanti.Close();
            sorgua.Parameters.Clear();
        }

        private void cbCapacity_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select plane_id from plane where plane_capacity=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", int.Parse((string)cbCapacity.SelectedItem));
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                capacityID.Text = (dr["plane_id"].ToString());
            }
            baglanti.Close();

        }
        public void FromCity()
        {
            cbFrom.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorguc = new NpgsqlCommand("Select fromcity_name from fromcity order by fromcity_country_id", baglanti);
            sorguc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorguc);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                cbFrom.Items.Add(dr["fromcity_name"].ToString());
            }
            baglanti.Close();
            sorguc.Parameters.Clear();

        }
        public void WhereCity()
        {
            cbWhere.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorguc = new NpgsqlCommand("Select wherecity_name from wherecity order by wherecity_country_id", baglanti);
            sorguc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorguc);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                cbWhere.Items.Add(dr["wherecity_name"].ToString());
            }
            baglanti.Close();
            sorguc.Parameters.Clear();

        }

        public void Company()
        {
            comboBox1.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorgub = new NpgsqlCommand("Select company_name from company order by company_id", baglanti);
            sorgub.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgub);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["company_name"].ToString());
            }
            baglanti.Close();
            sorgub.Parameters.Clear();

        }

        private void cbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select fromcity_id from fromcity where fromcity_name=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", cbFrom.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                fromID.Text = (dr["fromcity_id"].ToString());
            }
            baglanti.Close();

        }

        private void cbWhere_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select wherecity_id from wherecity where wherecity_name=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", cbWhere.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                whereID.Text = (dr["wherecity_id"].ToString());
            }
            baglanti.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select company_id from company where company_name=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", comboBox1.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label11.Text = (dr["company_id"].ToString());
            }
            baglanti.Close();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            NpgsqlCommand komut1 = new NpgsqlCommand("insert into flight(flight_plane_id ,flight_fromcity_id ,flight_wherecity_id ,flight_date ,flight_time ,flight_price ,flight_company_id) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)",baglanti );
            komut1.Parameters.AddWithValue("@p1", int.Parse(capacityID.Text));
            komut1.Parameters.AddWithValue("@p2", int.Parse(fromID.Text));
            komut1.Parameters.AddWithValue("@p3", int.Parse(whereID.Text));
            komut1.Parameters.AddWithValue("@p4", dateTimePicker1.Value);
            komut1.Parameters.AddWithValue("@p5", Convert.ToDateTime(textBox1.Text));
            komut1.Parameters.AddWithValue("@p6", int.Parse(textBox5.Text));
            komut1.Parameters.AddWithValue("@p7", int.Parse(label11.Text));

            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Uçuş eklendi.");
        


        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand sorgu2 = new NpgsqlCommand("delete from flight where flight_id=@p1", baglanti);
            sorgu2.Parameters.AddWithValue("@p1", int.Parse(flightID.Text));
            sorgu2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Uçus Silindi.");

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            flightID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cbFrom.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbWhere.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox1.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cbCapacity.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string sorgu = ("select * from ucuslar");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            this.dataGridView1.Columns["flightid"].Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update flight set  flight_plane_id = @p1, flight_fromcity_id =@p2 ,flight_wherecity_id =@p3,flight_date=@p4 ,flight_time=@p5,flight_price=@p6 ,flight_company_id =@p7 where flight_id=@p8", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(capacityID.Text));
            komut3.Parameters.AddWithValue("@p2", int.Parse(fromID.Text));
            komut3.Parameters.AddWithValue("@p3", int.Parse(whereID.Text));
            komut3.Parameters.AddWithValue("@p4", dateTimePicker1.Value);
            komut3.Parameters.AddWithValue("@p5", Convert.ToDateTime(textBox1.Text));
            komut3.Parameters.AddWithValue("@p6", int.Parse(textBox5.Text));
            komut3.Parameters.AddWithValue("@p7", int.Parse(label11.Text));
            komut3.Parameters.AddWithValue("@p8", int.Parse(flightID.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Uçuş güncellendi.");
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel5.Visible = false;
            panel3.Visible = true;
            panel1.Visible= false;
            panel4.Visible = false;
            string sorgu = ("select * from company");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            this.dataGridView2.Columns["company_id"].Visible = false;
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label12.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into company(company_name ,company_planes ,company_info) values(@p1,@p2,@p3)", baglanti);
            
            komut1.Parameters.AddWithValue("@p1", textBox2.Text);
            komut1.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            komut1.Parameters.AddWithValue("@p3", textBox4.Text);

            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Şirket eklendi.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand sorgu2 = new NpgsqlCommand("delete from company where company_id=@p1", baglanti);
            sorgu2.Parameters.AddWithValue("@p1", int.Parse(label12.Text));
            sorgu2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Şirket Silindi.");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string sorgu = ("select * from company");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            this.dataGridView2.Columns["company_id"].Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update company set company_name =@p1 ,company_planes =@p2,company_info=@p3 where company_id=@p4", baglanti);
            komut3.Parameters.AddWithValue("@p1", textBox2.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            komut3.Parameters.AddWithValue("@p3", textBox4.Text);
            komut3.Parameters.AddWithValue("@p4", int.Parse(label12.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Şirket güncellendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Visible = true;
            panel4.Visible = true;
            panel5.Visible = false;
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("select * from bilet where flightid=@p1", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(flightID.Text));
            komut1.ExecuteNonQuery();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            dataGridView3.DataSource = ds.Tables[0];

            this.dataGridView3.Columns["flightid"].Visible = false;
            this.dataGridView3.Columns["ticketid"].Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel5.Visible = true;
            panel3.Visible = false;
            panel1.Visible = false;
            panel4.Visible = false;
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("select maliyet(flight_price),flight_id, flight_date, flight_time, flight_price from flight", baglanti);
            komut1.ExecuteNonQuery();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            dataGridView4.DataSource = ds.Tables[0];
            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }
    }
}
