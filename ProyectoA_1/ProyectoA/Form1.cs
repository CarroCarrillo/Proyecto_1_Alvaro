using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoA
{
    public partial class Form1 : Form
    {
        private FuncionesCliente funClien = new FuncionesCliente();

        public Form1()
        {
            InitializeComponent();
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
                        
            buttonDe.Hide();
            buttonDeDe.Hide();
            buttonIz.Hide();
            buttonIzIz.Hide();
        }

        private void clienteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[10].MinimumWidth = 120;
            cli_a_cod_textBox.TextChanged += Cli_a_cod_textBox_TextChanged;
            cli_a_nombEm_textBox.TextChanged += Cli_a_nombEm_textBox_TextChanged;
            cli_a_cif_textBox.TextChanged += Cli_a_cif_textBox_TextChanged;
            cli_a_dir_textBox.TextChanged += Cli_a_dir_textBox_TextChanged;
            cli_a_cp_textBox.TextChanged += Cli_a_cp_textBox_TextChanged;
            cli_a_poblacion_textBox.TextChanged += Cli_a_poblacion_textBox_TextChanged;
            cli_a_dni_textBox.TextChanged += Cli_a_dni_textBox_TextChanged;
            cli_a_tlf_textBox.TextChanged += Cli_a_tlf_textBox_TextChanged;
        }

        private void Cli_a_tlf_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarTlf(this)) cli_a_tlf_textBox.ForeColor = Color.Blue;
            else cli_a_tlf_textBox.ForeColor = Color.Red;
        }

        private void Cli_a_dni_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarDni(this)) cli_a_dni_textBox.ForeColor = Color.Blue;
            else cli_a_dni_textBox.ForeColor = Color.Red;
        }

        private void Cli_a_poblacion_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_poblacion_textBox.TextLength!=0) cli_a_poblacion_textBox.ForeColor = Color.Blue;
            else cli_a_poblacion_textBox.BackColor = Color.Red;
        }

        private void Cli_a_cp_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarCp(this)) cli_a_cp_textBox.ForeColor = Color.Blue;
            else cli_a_cp_textBox.ForeColor = Color.Red;
        }

        private void Cli_a_dir_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_dir_textBox.TextLength!=0) cli_a_dir_textBox.ForeColor = Color.Blue;
            else cli_a_dir_textBox.BackColor = Color.Red;
        }

        private void Cli_a_cif_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarCif(this)) cli_a_cif_textBox.ForeColor = Color.Blue;
            else cli_a_cif_textBox.ForeColor = Color.Red;
        }
        
        private void Cli_a_nombEm_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_nombEm_textBox.TextLength!=0) cli_a_cod_textBox.ForeColor = Color.Blue;
            else cli_a_nombEm_textBox.BackColor = Color.Red;
        }

        private void Cli_a_cod_textBox_TextChanged(object sender, EventArgs e)
        {
            if(!funClien.comprobarCodigo(this)) cli_a_cod_textBox.ForeColor = Color.Blue;
            else cli_a_cod_textBox.ForeColor = Color.Red;
        }


        //MENU AYUDA
        private void informaciónClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AClienteForm = new AyudaCliente();
            AClienteForm.Show();
        }
        //MENU AYUDA
        private void informaciónMáquinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AMaquinaForm = new AyudaMaquina();
            AMaquinaForm.Show();
        }
        //MENU AYUDA
        private void informaciónContratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form AContratoForm = new AyudaContrato();
            AContratoForm.Show();
        }

        //BOTON PRINCIPAL MENU
        private void botonClienB_Click(object sender, EventArgs e)
        {
            panel1.Show();
            if (panel1.Visible) botonClienB.BackColor = Color.WhiteSmoke;
            panel2.Hide();
            if (!panel2.Visible) botonClienA.BackColor = Color.Transparent;
        }
        //BOTON PRINCIPAL MENU
        private void botonClienA_Click(object sender, EventArgs e)
        {
            panel2.Show();
            if (panel2.Visible) botonClienA.BackColor = Color.WhiteSmoke;
            panel1.Hide();
            if (!panel1.Visible) botonClienB.BackColor = Color.Transparent;
        }
        //BOTON PRINCIPAL MENU
        private void botonMaqA_Click(object sender, EventArgs e)
        {
            panel4.Show();
            if (panel4.Visible) botonMaqA.BackColor = Color.WhiteSmoke;
            panel3.Hide();
            if (!panel3.Visible) botonMaqB.BackColor = Color.Transparent;
        }
        //BOTON PRINCIPAL MENU
        private void botonMaqB_Click(object sender, EventArgs e)
        {
            panel3.Show();
            if (panel3.Visible) botonMaqB.BackColor = Color.WhiteSmoke;
            panel4.Hide();
            if (!panel4.Visible) botonMaqA.BackColor = Color.Transparent;
        }
        //BOTON PRINCIPAL MENU
        private void botonConB_Click(object sender, EventArgs e)
        {
            
        }
        //BOTON PRINCIPAL MENU
        private void botonConG_Click(object sender, EventArgs e)
        {
            
        }

        

        ///*
        // * 
        // * 
        // * TODO LO REFERENTE AL APARTADO        CLIENTE
        // * 
        // * 
        // */

            ///*
            // * BÚSQUEDA
            // * 
            // */

        //BOTON INICIAR BÚSQUEDA CLIENTE-BUSCAR
        private void button3_Click(object sender, EventArgs e)
        {
            funClien.botonIniciarBusqueda(this);
        }

        //BOTON LIMPIAR BUSQUEDA CLIENTE-BUSCAR
        private void botonClienLB_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 1;
            buttonDeDe.Hide();
            buttonDe.Hide();
            buttonIzIz.Hide();
            buttonIz.Hide();
            numPgnLabel.Text = " ";
        }

        //BOTON LIMPIAR CAMPOS BÚSQUEDA
        private void botonClienLC_Click(object sender, EventArgs e)
        {
            funClien.limpCampCliB(this);
        }

       
            ///*
            // * AÑADIR
            // * 
            // */

        //BOTON AGREGAR CLIENTE CLIENTE-AÑADIR  
        private void cli_a_Agregar_button_Click(object sender, EventArgs e)
        {
            funClien.botonAgregarClientes(this);
        }
        
        //BOTON LIMPIAR CAMPOS CLIENTE-AÑADIR
        private void cli_a_LC_button_Click(object sender, EventArgs e)
        {
            funClien.limpCampCliA(this);
        }
        
        //BOTON PAGINACION
        private void buttonDe_Click(object sender, EventArgs e)
        {
            funClien.botonPgnD(this);
        }
        //BOTON PAGINACION
        private void buttonDeDe_Click(object sender, EventArgs e)
        {
            funClien.botonPgnDD(this);
        }
        //BOTON PAGINACION
        private void buttonIz_Click(object sender, EventArgs e)
        {
            funClien.botonPgnI(this);
        }
        //BOTON PAGINACION
        private void buttonIzIz_Click(object sender, EventArgs e)
        {
            funClien.botonPgnII(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string a = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string b = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            a = a.Replace(Environment.NewLine, string.Empty);
            b = b.Replace(Environment.NewLine, string.Empty);
            Form perfilClient = new PerfilCliente(a, b);
            perfilClient.Show();
        }

        ///*
        // * 
        // * 
        // * TODO LO REFERENTE AL APARTADO        MÁQUINA
        // * 
        // * 
        // */


    }
}
