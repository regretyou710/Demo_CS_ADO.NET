using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Demo_CS_ADO.NET
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;        
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("請輸入資料庫名稱");
            }
            else
            {
                try
                {
                    //string connStr = "server=" + textBox4.Text.Trim() + "," + textBox5.Text.Trim() + "\\SQLEXPRES;database=" + textBox1.Text.Trim() + ";uid=" + textBox2.Text.Trim() + ";pwd=" + textBox3.Text.Trim();

                    //string connStr = "Data Source=" + textBox4.Text.Trim() + "," + textBox5.Text.Trim() + "\\SQLEXPRES;Initial Catalog=" + textBox1.Text.Trim() + ";User ID=" + textBox2.Text.Trim() + ";Password=" + textBox3.Text.Trim();                   
                    //使用組態檔配置連接字串
                    //使用ConfigurationManager類System.Configuration要將加入參考
                    //從配置檔讀取字串
                    string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
                    //格式化字串
                    string connStrFormat = string.Format(connStr, textBox4.Text.Trim(), textBox5.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim());


                    using (conn = new SqlConnection(connStrFormat))
                    {
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            richTextBox1.Text = textBox1.Text.Trim() + "資料庫已經打開..";
                        }
                    }

                }
                catch (Exception ex)
                {
                    richTextBox1.Text = ex.Message;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Dispose();
                if (conn.State == ConnectionState.Closed)
                {
                    richTextBox1.Text = "資料庫已經成功關閉..";
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                if (conn.State == ConnectionState.Closed)
                {
                    richTextBox1.Text = "資料庫已經成功關閉..";
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    richTextBox1.Text = textBox1.Text.Trim() + "資料庫已經打開..";
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {            
            Demo01 demo01 = new Demo01();
            richTextBox2.Text = demo01.Show1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //使用SqlParameters方式傳遞Sql語句
            Demo02 demo02 = new Demo02();
            richTextBox2.Text = demo02.Show1();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //DataTable的使用
            Demo03 demo03 = new Demo03();
            richTextBox2.Text = demo03.Show1();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //DataRelation的使用
            Demo05 demo05 = new Demo05();
            richTextBox2.Text = demo05.Show1();
        }
    }
}
