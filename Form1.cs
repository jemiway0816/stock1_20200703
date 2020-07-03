using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace stock1
{
    public partial class STOCK1 : Form
    {
        public STOCK1()
        {
            InitializeComponent();
        }

        SQLiteConnection dbConnection = null;

        private void STOCK1_Load(object sender, EventArgs e)
        {
            textBox1.Text = sqlStringGet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dbPath = @".\data.db";
            string cnStr = "data source=" + dbPath;

            DataTable f_dataTable = new DataTable();

            using (dbConnection = new SQLiteConnection(cnStr))
            {
                dbConnection.Open();

                SQLiteCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = textBox1.Text;
                SQLiteDataReader dataReader = dbCommand.ExecuteReader();

                f_dataTable.Load(dataReader);
                dataReader.Close();
                dbCommand.Dispose();
            }

            dataGridView1.DataSource = f_dataTable;
        }

        public string sqlStringGet()
        {
            string sqlString = "select * from price where sid=\"" + textBox4.Text + "\"";

            string startDate = "\"" + textBox2.Text + " 00:00:00\"";
            string endDate = "\"" + textBox3.Text + " 00:00:00\"";

            sqlString = sqlString + " and date >= " + startDate + " and date <= " + endDate;

            return sqlString;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = sqlStringGet();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = sqlStringGet();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = sqlStringGet();
        }
    }
}
