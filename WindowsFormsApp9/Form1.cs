﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        
        static string connectionString = @"Server=localhost;Database=postgres;User ID=postgres;Password=1234;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string SQL = @"select * from car_data";
            dataGridView1.DataSource = GetAll(SQL);
        }
        static DataTable GetAll(string _SQLCommand)
        {
            DataTable oDataTable = new DataTable();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlDataAdapter oAdapter = new NpgsqlDataAdapter(_SQLCommand, conn);
                oAdapter.Fill(oDataTable);
                if (oAdapter != null)
                    oAdapter.Dispose();
            }
            return oDataTable;
        }
        static int ExecNonQuery(string _SQLCommand,string ans)
        {
            int result = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
               
                NpgsqlCommand cmd = new NpgsqlCommand(_SQLCommand, connection);
                cmd.Parameters.AddWithValue("@ans",ans);
                cmd.CommandType = CommandType.Text;

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }

            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string SQL = @"select * from car_data";
            Console.WriteLine(GetAll(SQL).Rows.Count);
            foreach(DataRow row in GetAll(SQL).Rows)
            {
                Console.WriteLine(row[0]);
            }
            dataGridView1.DataSource = GetAll(SQL);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string ans = comboBox1.Text;
            string SQL = "insert into car_data (car_no,cust_id,unit_id,product_id,status,tran_time) values (@ans,'w0','e0','r0',1,'t0');";
            Console.WriteLine("新增結果:" + ExecNonQuery(SQL,ans));

            dataGridView1.Update();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
