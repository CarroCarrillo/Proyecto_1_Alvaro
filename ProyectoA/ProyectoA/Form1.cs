using ProyectoA.BDADataSetTableAdapters;
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



namespace ProyectoA
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox3.Hide();
            groupBox4.Hide();
            groupBox5.Hide();
            groupBox6.Hide();
        }

        private void clienteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bDADataSet.Cliente' Puede moverla o quitarla según sea necesario.
            this.clienteTableAdapter.Fill(this.bDADataSet.Cliente);
            // TODO: esta línea de código carga datos en la tabla 'bDADataSet.Cliente' Puede moverla o quitarla según sea necesario.
            //this.clienteTableAdapter.Fill(this.bDADataSet.Cliente);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void informaciónClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AClienteForm = new AyudaCliente();
            AClienteForm.Show();
        }

        private void informaciónMáquinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AMaquinaForm = new AyudaMaquina();
            AMaquinaForm.Show();
        }

        private void informaciónContratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AContratoForm = new AyudaContrato();
            AContratoForm.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
            groupBox1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Hide();
            groupBox2.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void clienteDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cifLabel_Click(object sender, EventArgs e)
        {

        }

        private void activoCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void codigoLabel_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox4.Hide();
            groupBox3.Show();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox6.Hide();
            groupBox5.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox5.Hide();
            groupBox6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox3.Hide();
            groupBox4.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SqlConnection sqlConnection1 = new SqlConnection("Data Source=|DataDirectory|\\BDA.accdb");
            //SqlCommand cmd = new SqlCommand();
            //SqlDataReader reader;

            //cmd.CommandText = "SELECT * FROM Cliente";
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();

            //reader = cmd.ExecuteReader();
            //// Data is accessible through the DataReader object here.

            //Console.Write(reader[0].ToString());

            //sqlConnection1.Close();


            ClienteTableAdapter tableAdapter = new ClienteTableAdapter();

            dataGridView1.RowCount = 1;

            for(int i = 0; i < 3; i++)
            {
                dataGridView1.Rows.Add();

                for(int j = 0; j < 12; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tableAdapter.devolverCli()[i][j].ToString();
                }
            }

            dataGridView1.AutoResizeColumns();
             
            //string sConnectionString;
            //sConnectionString = "Data Source = localhost";
            //SqlConnection objConn
            //    = new SqlConnection(sConnectionString);
            //objConn.Open();

            //SqlDataAdapter daAuthors
            //    = new SqlDataAdapter("Select * From Cliente", objConn);
            //DataSet dsPubs = new DataSet("Pubs");
            //daAuthors.FillSchema(dsPubs, SchemaType.Source, "Clientes");
            //daAuthors.Fill(dsPubs, "Clientes");

            //DataTable tblAuthors;
            //tblAuthors = dsPubs.Tables["Cliente"];

            //foreach (DataRow drCurrent in tblAuthors.Rows)
            //{
            //    Console.WriteLine("{0} {1}",
            //        drCurrent["au_fname"].ToString(),
            //        drCurrent["au_lname"].ToString());
            //}
            //Console.ReadLine();

        }
    }
}
