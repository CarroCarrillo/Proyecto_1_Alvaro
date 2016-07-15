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
        private ClienteTableAdapter tableAdapterCli = new ClienteTableAdapter();
        private ClienteTelefonoTableAdapter tableAdapterCliTel = new ClienteTelefonoTableAdapter();
        private TelefonoTableAdapter tableAdapterTlf = new TelefonoTableAdapter();

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
            string[] cadenaLabels = new string[12];
            comprobarLabelsCliB(cadenaLabels);

            if (cadenaLabels[11] == "%") selectSinTlf(cadenaLabels);
            else selectConTlf(cadenaLabels);
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

        //Comprueba los labels con la información que filtrará la búsqueda
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
            
            if (radioButton4.Checked) cad[9] = "_%";
            else cad[9] = "%";

            if (telefonoTextBox.Text != "") cad[11] = telefonoTextBox.Text;
            else cad[11] = "%";
        }
        
        //SENTENCIA SQL SELECT cuando sí que hay teléfonos dentro de las búsquedas
        private void selectConTlf(string[] cadi)
        {
            
        }

        //SENTENCIA SQL SELECT cuando no hay teléfonos dentro de las búsquedas  
        private void selectSinTlf(string[] cadi)
        {
            string tel;
            char[] colorearAct;

            dataGridView1.RowCount = 1;

            ProyectoA.BDADataSet.ClienteDataTable t;
            
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                //Select (activos + no activos) y (no actualizados / actualizados + no actualizados)
                if (!radioButton3.Checked) t = tableAdapterCli.Consulta(cadi[0], cadi[1], cadi[2], cadi[3], cadi[4], cadi[5], cadi[6], cadi[7], cadi[8], "%", cadi[9]);
                //Select (activos + no activos) y (actualizados)
                else t = tableAdapterCli.ConsultaTresActualizados(cadi[0], cadi[1], cadi[2], cadi[3], cadi[4], cadi[5], cadi[6], cadi[7], cadi[8], "%");
            }
            else
            {
                //Select (activos / no activos) y (no actualizados / actualizados + no actualizados)
                if (!radioButton3.Checked) t = tableAdapterCli.ConsultaDos(cadi[0], cadi[1], cadi[2], cadi[3], cadi[4], cadi[5], cadi[6], cadi[7], cadi[8], radioButton1.Checked, cadi[9]);
                //Select (activos / no activos) y (actualizados)
                else t = tableAdapterCli.ConsultaCuatroActualizado(cadi[0], cadi[1], cadi[2], cadi[3], cadi[4], cadi[5], cadi[6], cadi[7], cadi[8], radioButton1.Checked);
            }
            
            ProyectoA.BDADataSet.ClienteTelefonoDataTable tCliTel;

            for (int i = 0; i < t.Count(); i++)
            {
                dataGridView1.Rows.Add();
                tCliTel = tableAdapterCliTel.ConsultaTelefono(t[i][0].ToString(), t[i][1].ToString());
                colorearAct = null;

                //Comprobamos si está actualizado, si no, guardamos en un charArray la columna actualizar
                if (t[i][11].ToString() != "") colorearAct = t[i][11].ToString().ToCharArray();
                
                for (int j = 0; j < 12; j++)
                {
                    //Coloreamos las celdas correspondientes
                    if (colorearAct != null) 
                    {
                        if (j < 10)
                        {
                            for (int z = 0; z < colorearAct.Length; z++)
                            {
                                if (j + 48 == (int)colorearAct[z]) dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LightSkyBlue; 
                            }
                        }
                    }
                    if (j < 9) dataGridView1.Rows[i].Cells[j].Value = t[i][j].ToString();
                    else if (j > 9)
                    {
                        if (j == 11)
                        {
                            if (t[i][j - 1].ToString() == "True") dataGridView1.Rows[i].Cells[j].Value = "Sí";
                            else dataGridView1.Rows[i].Cells[j].Value = "No";
                        }
                        else dataGridView1.Rows[i].Cells[j].Value = t[i][j - 1].ToString();
                    }
                    else
                    {
                        tel = "";
                        for (int k = 0; k < tCliTel.Count(); k++)
                        {
                            if (k == 0) tel += tCliTel[k][2].ToString();
                            else tel += ", " + tCliTel[k][2].ToString();
                        }
                        dataGridView1.Rows[i].Cells[j].Value = tel;
                    }
                }

            }
        }

            ///*
            // * AÑADIR
            // * 
            // */

        //BOTON AGREGAR CLIENTE CLIENTE-AÑADIR   ----- TODO las excepciones a la hora de agregar
        private void cli_a_Agregar_button_Click(object sender, EventArgs e)
        {
            string[] ccI = new string[11];
            string[] cadTlf;

            //Comprobación de que los campos obligatorios están completos y los datos introducidos son correctos, en tal caso, añadir datos  
            if (!comprobarCamposCliA())
            {
                try
                {
                    DialogResult b = MessageBox.Show("          Confirmar añadir cliente.", "Confirmación", MessageBoxButtons.OKCancel);
                    if (b == DialogResult.OK)
                    {
                        agregarCamposInsertCliente(ccI);

                        tableAdapterCli.InsertQueryCliente(ccI[0], ccI[1], ccI[2], ccI[3], ccI[4], ccI[5], ccI[6], ccI[7], ccI[8], ccI[9], cli_a_activo_SI_radioButton.Checked, ccI[10]);

                        if (cli_a_tlf_textBox.Text != "")
                        {
                            cadTlf = agregarCamposTlf();
                            for (int i = 0; i < cadTlf.Length; i++)
                            {
                                try
                                {
                                    tableAdapterTlf.InsertQueryTlf(cadTlf[i]);
                                }
                                catch {}
                                tableAdapterCliTel.InsertQueryClienteTelefono(ccI[0], ccI[1], cadTlf[i]);
                            }
                        }
                        DialogResult q = MessageBox.Show("          Usuario añadido correctamente.\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA();
                    }
                }
                catch
                {
                    tableAdapterCli.ConsultaErrorInsert(cli_a_cod_textBox.Text, cli_a_nombEm_textBox.Text);
                    MessageBox.Show("Se ha producido un error. No ha podido insertarse.");
                }
                
            }
        }

        //BOTON LIMPIAR CAMPOS CLIENTE-AÑADIR
        private void cli_a_LC_button_Click(object sender, EventArgs e)
        {
            limpCampCliA();
        }

        //Funcion para limpiar los campos de cliente-añadir
        private void limpCampCliA()
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

        //Rellena los datos de telefono en un array
        private string[] agregarCamposTlf()
        {
            string[] cadenaTlf;
            char[] separador = new char[5];

            separador[0] = ',';
            separador[1] = ';';
            separador[2] = '.';
            separador[3] = ':';
            separador[4] = '-';

            cadenaTlf = cli_a_tlf_textBox.Text.Split(separador);

            return cadenaTlf;
        }

        //Rellena los datos para realizar posteriormente el insert de cliente
        private void agregarCamposInsertCliente(string[] cadenaCamposI)
        {
            //campos normales TABLA CLIENTE
            cadenaCamposI[0] = cli_a_cod_textBox.Text;
            cadenaCamposI[1] = cli_a_nombEm_textBox.Text;
            if (cli_a_cad_textBox.Text != "") cadenaCamposI[2] = cli_a_cad_textBox.Text;
            if (cli_a_cif_textBox.Text != "") cadenaCamposI[3] = cli_a_cif_textBox.Text;
            cadenaCamposI[4] = cli_a_dir_textBox.Text;
            cadenaCamposI[5] = cli_a_poblacion_textBox.Text;
            if (cli_a_cp_textBox.Text != "") cadenaCamposI[6] = cli_a_cp_textBox.Text;
            if (cli_a_nombApell_textBox.Text != "") cadenaCamposI[7] = cli_a_nombApell_textBox.Text;
            cadenaCamposI[8] = cli_a_dni_textBox.Text;
            if (cli_a_observacion_richTextBox.Text != "") cadenaCamposI[9] = cli_a_observacion_richTextBox.Text;

            //Actualizado TABLA CLIENTE
            if (checkBox_cli_a_cod.Checked) cadenaCamposI[10] += "0";
            if (checkBox_cli_a_nombEm.Checked) cadenaCamposI[10] += "1";
            if (checkBox_cli_a_cad.Checked) cadenaCamposI[10] += "2";
            if (checkBox_cli_a_cif.Checked) cadenaCamposI[10] += "3";
            if (checkBox_cli_a_dir.Checked) cadenaCamposI[10] += "4";
            if (checkBox_cli_a_poblacion.Checked) cadenaCamposI[10] += "5";
            if (checkBox_cli_a_cp.Checked) cadenaCamposI[10] += "6";
            if (checkBox_cli_a_nomAp.Checked) cadenaCamposI[10] += "7";
            if (checkBox_cli_a_dni.Checked) cadenaCamposI[10] += "8";
            if (checkBox_cli_a_tlf.Checked) cadenaCamposI[10] += "9"; 
        }

        //Hace la comprobación de que los campos obligatorios correspondientes a CLIENTE-AÑADIR estén completos  TODO -- filtro teléfono
        private bool comprobarCamposCliA()
        {
            bool error = false;

            if (cli_a_cod_textBox.Text == null || cli_a_cod_textBox.Text == "") error = true;
            if (cli_a_nombEm_textBox.Text == null || cli_a_nombEm_textBox.Text == "") error = true;
            if (cli_a_dir_textBox.Text == null || cli_a_dir_textBox.Text == "") error = true;
            if (cli_a_dni_textBox.Text == null || cli_a_dni_textBox.Text == "") error = true;
            if (cli_a_poblacion_textBox.Text == null || cli_a_poblacion_textBox.Text == "") error = true;
            if (cli_a_activo_NO_radioButton.Checked == false && cli_a_activo_SI_radioButton.Checked == false) error = true;

            if(error == true) MessageBox.Show("Uno o más campos obligatorios no están rellenados.");
            else
            {
                //Comprobar que el CODIGO cumple los requisitos
                char[] codigoArray = cli_a_cod_textBox.Text.ToCharArray();
                if (codigoArray.Length == 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (codigoArray[i] < 48 || codigoArray[i] > 57)
                        {
                            MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                "          Ejemplo: '00023'");
                            error = true;
                            i = 5;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                "          Ejemplo: '00023'");
                    error = true;
                }

                //Comprobar que el CIF cumple los requisitos
                if (!error)
                {
                    if (cli_a_cif_textBox.Text == null || cli_a_cif_textBox.Text == "") { }
                    else
                    {
                        char[] cifArray = cli_a_cif_textBox.Text.ToCharArray();
                        if (cifArray.Length == 9)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                if (i == 8)
                                {
                                    if ((cifArray[i] < 48 || cifArray[i] > 57) && (cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122))
                                    {
                                        MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                            " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                        error = true;
                                        i = 9;
                                    }
                                }
                                else if (i == 0)
                                {
                                    if ((cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122))
                                    {
                                        MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                            " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                        error = true;
                                        i = 9;
                                    }
                                }
                                else
                                {
                                    if (cifArray[i] < 48 || cifArray[i] > 57)
                                    {
                                        MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                            " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                        error = true;
                                        i = 9;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                            " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                            error = true;
                        }
                    }
                }


                //Comprobar que el DNI cumple los requisitos
                if (!error)
                {
                    char[] dniArray = cli_a_dni_textBox.Text.ToCharArray();
                    if (dniArray.Length == 9)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 0)
                            {
                                if ((dniArray[i] < 48 || dniArray[i] > 57) && (dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))
                                {
                                    MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                    " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                    "\n\n          Ejemplo NIE: 'X1234567A'");
                                    error = true;
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
                                    error = true;
                                    i = 9;
                                }
                            }
                            else
                            {
                                if (dniArray[i] < 48 || dniArray[i] > 57)
                                {
                                    MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                    " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                    "\n\n          Ejemplo NIE: 'X1234567A'");
                                    error = true;
                                    i = 9;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                                    " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                                    "\n\n          Ejemplo NIE: 'X1234567A'");
                        error = true;
                    }
                }

                //Comprobar que el CP cumple los requisitos
                if (!error)
                {
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
                                    error = true;
                                    i = 5;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo 'Código Postal' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                        "          Ejemplo: '03500'");
                            error = true;
                        }
                    }
                }
                
                //TODO Comprobar que los TLF cumplen los requisitos
            }
            return error;
        }
        
    }
}
