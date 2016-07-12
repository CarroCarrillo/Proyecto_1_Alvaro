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
            panel1.Hide();
            panel2.Hide();
            
        }

        private void clienteBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bDADataSet.Cliente' Puede moverla o quitarla según sea necesario.
            this.clienteTableAdapter.Fill(this.bDADataSet.Cliente);
            // TODO: esta línea de código carga datos en la tabla 'bDADataSet.Cliente' Puede moverla o quitarla según sea necesario.
            //this.clienteTableAdapter.Fill(this.bDADataSet.Cliente);

            //Para que esté siempre ajustado el datagridview cuando se mueva la ventana
            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[10].MinimumWidth = 120;

            
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

        private void botonClienB_Click(object sender, EventArgs e)
        {
            panel1.Show();
            if (panel1.Visible) botonClienB.BackColor = Color.WhiteSmoke;
            panel2.Hide();
            if (!panel2.Visible) botonClienA.BackColor = Color.Transparent;
        }

        private void botonClienA_Click(object sender, EventArgs e)
        {
            panel2.Show();
            if (panel2.Visible) botonClienA.BackColor = Color.WhiteSmoke;
            panel1.Hide();
            if (!panel1.Visible) botonClienB.BackColor = Color.Transparent;
        }

        private void botonMaqA_Click(object sender, EventArgs e)
        {

        }

        private void botonMaqB_Click(object sender, EventArgs e)
        {
            
        }

        private void botonConB_Click(object sender, EventArgs e)
        {
            
        }

        private void botonConG_Click(object sender, EventArgs e)
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
        
        //BOTON INICIAR BÚSQUEDA CLIENTE-BUSCAR
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

                for (int j = 0; j < 11; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = t[i][j].ToString();

                }

            }

            dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[10].MinimumWidth = 120;

        }

        //BOTON LIMPIAR BUSQUEDA CLIENTE-BUSCAR
        private void botonClienLB_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 1;
        }

        //BOTON LIMPIAR CAMPOS CLIENTE-BUSCAR
        private void botonClienLC_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = null;
            nombreEmpresaTextBox.Text = null;
            cadenaTextBox.Text = null;
            cifTextBox.Text = null;
            direccionTextBox.Text = null;
            poblacionTextBox.Text = null;
            cpTextBox.Text = null;
            nombrApellidosTextBox.Text = null;
            dniTextBox.Text = null;
            telefonoTextBox.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        //BOTON LIMPIAR CAMPOS CLIENTE-AÑADIR
        private void cli_a_LC_button_Click(object sender, EventArgs e)
        {
            cli_a_activo_NO_radioButton.Checked = false;
            cli_a_activo_SI_radioButton.Checked = false;
            cli_a_cad_textBox.Text = null;
            cli_a_cif_textBox.Text = null;
            cli_a_cod_textBox.Text = null;
            cli_a_cp_textBox.Text = null;
            cli_a_dir_textBox.Text = null;
            cli_a_dni_textBox.Text = null;
            cli_a_nombApell_textBox.Text = null;
            cli_a_nombEm_textBox.Text = null;
            cli_a_observacion_richTextBox.Text = null;
            cli_a_poblacion_textBox.Text = null;
            cli_a_tlf_textBox.Text = null;
            checkBox_cli_a_cad.Checked = false;
            checkBox_cli_a_cif.Checked = false;
            checkBox_cli_a_cod.Checked = false;
            checkBox_cli_a_cp.Checked = false;
            checkBox_cli_a_dir.Checked = false;
            checkBox_cli_a_dni.Checked = false;
            checkBox_cli_a_nomAp.Checked = false;
            checkBox_cli_a_nombEm.Checked = false;
            checkBox_cli_a_poblacion.Checked = false;
            checkBox_cli_a_tlf.Checked = false;
        }

        //BOTON AGREGAR CLIENTE CLIENTE-AÑADIR
        private void cli_a_Agregar_button_Click(object sender, EventArgs e)
        {
            //Comprobación de que los campos obligatorios están completos y los datos introducidos son correctos          
            comprobarCamposCliA(); 
        }

        //Hace la comprobación de que los campos obligatorios correspondientes a CLIENTE-AÑADIR estén completos
        private void comprobarCamposCliA()
        {
            bool error = false;

            if (cli_a_cod_textBox.Text == null || cli_a_cod_textBox.Text == "") error = true;
            if (cli_a_nombEm_textBox.Text == null || cli_a_nombEm_textBox.Text == "") error = true;
            if (cli_a_dir_textBox.Text == null || cli_a_dir_textBox.Text == "") error = true;
            if (cli_a_dni_textBox.Text == null || cli_a_dni_textBox.Text == "") error = true;
            if (cli_a_poblacion_textBox.Text == null || cli_a_poblacion_textBox.Text == "") error = true;

            if(error == true) MessageBox.Show("Uno o más campos obligatorios no están rellenados.");
            else
            {
                //Comprobar que el CODIGO cumple los requisitos
                char[] codigoArray = cli_a_cod_textBox.Text.ToCharArray();
                if(codigoArray.Length == 5)
                {
                    for(int i=0; i<5; i++)
                    {
                        if (codigoArray[i] < 48 || codigoArray[i] > 57)
                        {
                            MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n"+
                                "          Ejemplo: '00023'");
                            i = 5;
                        }
                    }
                }
                else MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                "          Ejemplo: '00023'");

                //Comprobar que el CIF cumple los requisitos
                if (cli_a_cif_textBox.Text == null || cli_a_cif_textBox.Text == "") { }
                else
                {
                    char[] cifArray = cli_a_cif_textBox.Text.ToCharArray();
                    if(cifArray.Length == 9)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 8)
                            {
                                if ((cifArray[i] < 48 || cifArray[i] > 57) && (cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122))
                                {
                                    MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                        " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                    i = 9;
                                }
                            }
                            else if (i == 0)
                            {
                                if ((cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122))
                                {
                                    MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                        " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                    i = 9;
                                }
                            }
                            else
                            {
                                if (cifArray[i] < 48 || cifArray[i] > 57)
                                {
                                    MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                        " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                    i = 9;
                                }
                            }
                        }
                    
                    }
                    else MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                        " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                }
                    

                //Comprobar que el DNI cumple los requisitos
                char[] dniArray = cli_a_dni_textBox.Text.ToCharArray();
                if (dniArray.Length == 9)
                {
                    for(int i=0; i<9; i++)
                    {
                        if (i == 0)
                        {
                            if((dniArray[i]<48 || dniArray[i] > 57) && (dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))
                            {
                                MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                "\n\n          Ejemplo NIE: 'X1234567A'");
                                i = 9;
                            }
                        }
                        else if (i == 8)
                        {
                            if ((dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))
                            {
                                MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                "\n\n          Ejemplo NIE: 'X1234567A'");
                                i = 9;
                            }
                        }
                        else
                        {
                            if(dniArray[i] < 48 || dniArray[i] > 57){
                                MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                "\n\n          Ejemplo NIE: 'X1234567A'");
                                i = 9;
                            }
                        }
                    }
                }
                else MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                " o bien por una letra, siete dígitos y otra letra."+"\n\n          Ejemplo DNI: '12345678A'"+
                                "\n\n          Ejemplo NIE: 'X1234567A'");

                //Comprobar que el CP cumple los requisitos
                if (cli_a_cp_textBox.Text == null || cli_a_cp_textBox.Text == "") { }
                else
                {
                    char[] cpArray = cli_a_cp_textBox.Text.ToCharArray();
                    if (cpArray.Length == 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (cpArray[i] < 48 || cpArray[i] > 57)
                            {
                                MessageBox.Show("El campo 'Código Postal' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                    "          Ejemplo: '03500'");
                                i = 5;
                            }
                        }
                    }
                    else MessageBox.Show("El campo 'Código Postal' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                    "          Ejemplo: '03500'");
                }
                

                //Comprobar que los TLF cumplen los requisitos
            }
        }
        
    }
}
