using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProyectoA
{
    public partial class Form1 : Form
    {
        private FuncionesCliente funClien = new FuncionesCliente();
        private FuncionesMaquina funM = new FuncionesMaquina();

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
            //Panel1   Cliente Buscar
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[10].MinimumWidth = 120;
            
            //Panel2   Cliente Añadir
            modificacionTextosClienteA();
            explicacionErrorCamposCA();

            //Panel3   Máquina Añadir
            funM.rellenarComboBoxFamiliaA(this);
            funM.rellenarComboBoxFamiliaB(this);
            funM.rellenarComboBoxEstadoA(this);
            funM.rellenarComboBoxEstadoB(this);
            funM.actualizarTextBoxVentaC("", "", comboBox7);
            funM.actualizarTextBoxVentaN("", "", comboBox3);
            //comprobarCambiosTextBox();

            //Panel4   Máquina Buscar
            dataGridView2.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[6].MinimumWidth = 120;
            dataGridView2.Columns[10].MinimumWidth = 120;

            //Panel5

            //Panel6
            
        }

        private void comprobarCambiosTextBox()
        {
            comboBox8.TextChanged += ComboBox8_TextChanged;
            comboBox4.TextChanged += ComboBox4_TextChanged;
            comboBox7.TextChanged += ComboBox7_TextChanged;
            comboBox3.TextChanged += ComboBox3_TextChanged;
        }

        private void ComboBox3_TextChanged(object sender, EventArgs e)
        {
            string a = comboBox7.Text;
            string b = comboBox3.Text;
            funM.actualizarTextBoxVentaN(a, b, comboBox3);
            funM.actualizarTextBoxVentaC(b,a, comboBox7);

        }
        private void ComboBox7_TextChanged(object sender, EventArgs e)
        {
            string a = comboBox7.Text;
            string b = comboBox3.Text;
            funM.actualizarTextBoxVentaC(b,a, comboBox7);
            funM.actualizarTextBoxVentaN(a,b, comboBox3);

        }

        private void ComboBox4_TextChanged(object sender, EventArgs e)
        {
            funM.rellenarComboBoxModeloB(this);
        }

        private void ComboBox8_TextChanged(object sender, EventArgs e)
        {
            funM.rellenarComboBoxModeloA(this);
        }


        //Cada vez que se modifica un label comprueba los errores
        private void modificacionTextosClienteA()
        {
            cli_a_cod_textBox.TextChanged += Cli_a_cod_textBox_TextChanged;
            cli_a_nombEm_textBox.TextChanged += Cli_a_nombEm_textBox_TextChanged;
            cli_a_cif_textBox.TextChanged += Cli_a_cif_textBox_TextChanged;
            cli_a_dir_textBox.TextChanged += Cli_a_dir_textBox_TextChanged;
            cli_a_cp_textBox.TextChanged += Cli_a_cp_textBox_TextChanged;
            cli_a_poblacion_textBox.TextChanged += Cli_a_poblacion_textBox_TextChanged;
            cli_a_dni_textBox.TextChanged += Cli_a_dni_textBox_TextChanged;
            cli_a_tlf_textBox.TextChanged += Cli_a_tlf_textBox_TextChanged;
            cli_a_activo_SI_radioButton.CheckedChanged += Cli_a_activo_SI_radioButton_CheckedChanged;
            cli_a_activo_NO_radioButton.CheckedChanged += Cli_a_activo_NO_radioButton_CheckedChanged;
            cli_a_cad_textBox.TextChanged += Cli_a_cad_textBox_TextChanged;
            cli_a_nombApell_textBox.TextChanged += Cli_a_nombApell_textBox_TextChanged;
            cli_a_observacion_richTextBox.TextChanged += Cli_a_observacion_richTextBox_TextChanged;
        }

        //Muestra el error de los campos que son obligatorios cuando no se ha tocado ningún label
        private void explicacionErrorCamposCA()
        {
            if (cli_a_cod_textBox.TextLength == 0) toolTip1.SetToolTip(pictureBox1, "Campo obligatorio.\nSe compone por cinco números.\n\nEjemplo: '00001'");
            if (cli_a_nombEm_textBox.TextLength == 0) toolTip1.SetToolTip(pictureBox2, "Campo obligatorio.");
            if (cli_a_dir_textBox.TextLength == 0) toolTip1.SetToolTip(pictureBox4, "Campo obligatorio.");
            if (cli_a_poblacion_textBox.TextLength == 0) toolTip1.SetToolTip(pictureBox6, "Campo obligatorio.");
            if (cli_a_dni_textBox.TextLength == 0) toolTip1.SetToolTip(pictureBox7, "Campo obligatorio.\n" +
                            "Dependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked) toolTip1.SetToolTip(pictureBox9, "Campo obligatorio.");
        }
        
        private void Cli_a_observacion_richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_observacion_richTextBox.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox12, "Se ha superado la cantidad de caracteres permitidos: "+ cli_a_observacion_richTextBox.TextLength + "/700");
                pictureBox12.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox12, "");
                pictureBox12.Image = null;
            }
        }

        private void Cli_a_nombApell_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_nombApell_textBox.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox11, "Se ha superado la cantidad de caracteres permitidos: " + cli_a_nombApell_textBox.TextLength + "/45");
                pictureBox11.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox11, "");
                pictureBox11.Image = null;
            }
        }

        private void Cli_a_cad_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cad_textBox.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox10, "Se ha superado la cantidad de caracteres permitidos: " + cli_a_cad_textBox.TextLength + "/45");
                pictureBox10.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox10, "");
                pictureBox10.Image = null;
            }
        }

        private void Cli_a_activo_NO_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked)
            {
                toolTip1.SetToolTip(pictureBox9, "Campo obligatorio.");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox9, "");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }

        private void Cli_a_activo_SI_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked)
            {
                toolTip1.SetToolTip(pictureBox9, "Campo obligatorio.");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox9, "");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }

        private void Cli_a_tlf_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_tlf_textBox.TextLength != 0)
            {
                if (!funClien.comprobarTlf(this))
                {
                    toolTip1.SetToolTip(pictureBox8, "");
                    pictureBox8.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox8, "El campo 'Teléfono' no se ha rellenado correctamente.\nComprobar que se han escrito 9 dígitos por teléfono y, en caso de haber" +
                                " más de un teléfono, se han separado por una coma. Pueden escribirse espacios mientras no se usen para separar un teléfono de otro." +
                                "\n\n          Ejemplo: '123456789, 987654321'");
                    pictureBox8.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox8, "");
                pictureBox8.Image = null;
            }
        }

        private void Cli_a_dni_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarDni(this))
            {
                toolTip1.SetToolTip(pictureBox7, "");
                pictureBox7.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox7, "Campo obligatorio.\n" +
                            "Dependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
                pictureBox7.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_poblacion_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_poblacion_textBox.TextLength != 0)
            {
                if (cli_a_poblacion_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox6, "Se ha superado la cantidad de caracteres permitidos: " + cli_a_poblacion_textBox.TextLength + "/45");
                    pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox6, "");
                    pictureBox6.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox6, "Campo obligatorio.");
                pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cp_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cp_textBox.TextLength != 0)
            {
                if (!funClien.comprobarCp(this))
                {
                    toolTip1.SetToolTip(pictureBox5, "");
                    pictureBox5.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox5, "Se ha de componer por cinco números.");
                    pictureBox5.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox5, "");
                pictureBox5.Image = null;
            }
        }

        private void Cli_a_dir_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_dir_textBox.TextLength != 0)
            {
                if (cli_a_dir_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox4, "Se ha superado la cantidad de caracteres permitidos: " + cli_a_dir_textBox.TextLength + "/45");
                    pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox4, "");
                    pictureBox4.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox4, "Campo obligatorio.");
                pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cif_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cif_textBox.TextLength != 0)
            {
                if (!funClien.comprobarCif(this))
                {
                    toolTip1.SetToolTip(pictureBox3, "");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox3, "El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve caracteres: el primero es una letra, los siete siguientes números" +
                                    " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox3, "");
                pictureBox3.Image = null;
            }
        }
        
        private void Cli_a_nombEm_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_nombEm_textBox.TextLength != 0)
            {
                if (cli_a_nombEm_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox2, "Se ha superado la cantidad de caracteres permitidos: " + cli_a_nombEm_textBox.TextLength + "/45");
                    pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox2, "");
                    pictureBox2.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox2, "Campo obligatorio.");
                pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cod_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!funClien.comprobarCodigo(this))
            {
                toolTip1.SetToolTip(pictureBox1, "");
                pictureBox1.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, "Campo obligatorio. Se compone por cinco números.\n\nEjemplo: '00001'");
                pictureBox1.Image = ProyectoA.Properties.Resources.tickNeg;
            }
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

            //Reiniciar select de clientes en el apartado Máquina-añadir
            //funM.actualizarTextBoxVentaC("");
            //funM.actualizarTextBoxVentaN("");
            comboBox3.SelectedItem = null;
            comboBox7.SelectedItem = null;
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
            try
            {
                string a = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string b = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                a = a.Replace(Environment.NewLine, string.Empty);
                b = b.Replace(Environment.NewLine, string.Empty);
                Form perfilClient = new PerfilCliente(a, b);
                perfilClient.Show();
                perfilClient.FormClosed += PerfilClient_FormClosed;
                
            }
            catch{ }
        }

        private void PerfilClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            funClien.reiniciarPgn(this);
        }



        ///*
        // * 
        // * 
        // * TODO LO REFERENTE AL APARTADO        MÁQUINA
        // * 
        // * 
        // */


        //Boton limpiar campos búsqueda máquina
        private void button8_Click(object sender, EventArgs e)
        {
            funM.limpCampCliB(this);
        }

        //BOTÓN AÑADIR ARCHIVO
        private void BOTONprueba_Click(object sender, EventArgs e)
        {
            funM.abreDialogo(this);
            
        }

        //BOTÓN DE AGREGAR MÁQUINA
        private void button2_Click(object sender, EventArgs e)
        {
            funM.botonAgregarMaquina(this);
        }

        //BOTÓN LIMPIA CAMPOS AÑADIR MAQUINA
        private void button1_Click(object sender, EventArgs e)
        {
            funM.limpCampMaqA(this);
        }

        //PESTAÑA EDICIÓN MÁQUINAS FAMILIAS Y MODELOS
        private void familiasYModelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form EdicM = new EdicMacFM();
            EdicM.Show();
            EdicM.FormClosed += EdicM_FormClosed;
        }

        private void EdicM_FormClosed(object sender, FormClosedEventArgs e)
        {
            funM.rellenarComboBoxFamiliaA(this);
            funM.rellenarComboBoxFamiliaB(this);
            comboBox5.SelectedItem = null;
            comboBox2.SelectedItem = null;
        }

        //PESTAÑA EDICIÓN MÁQUINAS ESTADOS
        private void estadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form estMa = new estadoMaquina();
            estMa.Show();
            estMa.FormClosed += EstMa_FormClosed;
        }

        private void EstMa_FormClosed(object sender, FormClosedEventArgs e)
        {
            funM.rellenarComboBoxEstadoA(this);
            funM.rellenarComboBoxEstadoB(this);
        }
    }
}
