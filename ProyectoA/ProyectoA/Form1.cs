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

        private void comprobarLabelsCliB(string[] cad)
        {

            if (codigoTextBox.Text != "") cad[0] = codigoTextBox.Text;
            else cad[0] = "%";

            if (nombreEmpresaTextBox.Text != "") cad[1] = nombreEmpresaTextBox.Text;
            else cad[1] = "%";

            if (cadenaTextBox.Text != "") cad[2] = cadenaTextBox.Text;
            else cad[2] = "%";

            if (cifTextBox.Text != "") cad[3] = cifTextBox.Text;
            else cad[3] = "%";

            if (direccionTextBox.Text != "") cad[4] = direccionTextBox.Text;
            else cad[4] = "%";

            if (poblacionTextBox.Text != "") cad[5] = poblacionTextBox.Text;
            else cad[5] = "%";

            if (cpTextBox.Text != "") cad[6] = cpTextBox.Text;
            else cad[6] = "%";

            if (nombrApellidosTextBox.Text != "") cad[7] = nombrApellidosTextBox.Text;
            else cad[7] = "%";

            if (dniTextBox.Text != "") cad[8] = dniTextBox.Text;
            else cad[8] = "%";

            cad[9] = "%";

            //TODO Checkbox Activo Actualizado
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] cadi = new string[10];
            comprobarLabelsCliB(cadi);
            Console.WriteLine(cadi[6]);

            ClienteTableAdapter tableAdapter = new ClienteTableAdapter();
            dataGridView1.RowCount = 1;
            ProyectoA.BDADataSet.ClienteDataTable t = tableAdapter.Consulta(cadi[0], cadi[1], cadi[2], cadi[3], cadi[4], cadi[5], cadi[6], cadi[7], cadi[8], cadi[9]);
            
            for (int i = 0; i < t.Count(); i++)
            {
                dataGridView1.Rows.Add();

                for(int j = 0; j < 11; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = t[i][j].ToString();
                }
            }

            dataGridView1.AutoResizeColumns();
        }
    }
}
