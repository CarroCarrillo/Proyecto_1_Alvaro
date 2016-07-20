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
using MySql.Data.MySqlClient;



namespace ProyectoA
{
    public partial class Form1 : Form
    {
        public string conexion;
        public string consulta;
        public MySqlConnection con = new MySqlConnection();

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
            
            string consulta;

            consulta = "select c.*, group_concat(t.NumeroTlf separator ', ') as ntlf " +
                       "from cliente c left join telefono t on c.Codigo = t.Cliente_Codigo "+
                       "and c.NombreEmpresa = t.Cliente_NombreEmpresa";

            consulta += rellenarSelect();

            consulta += " group by Codigo, NombreEmpresa";
            //consulta += " order by codigo and codigo limit 1,2";

            devolverConsulta(consulta);
        }

        //CREA EL WHERE DE LA SELECT
        private string rellenarSelect()
        {
            bool a = false;
            string c = " where";

            if (codigoTextBox.Text != "")
            {
                c += " Codigo like '%" + codigoTextBox.Text + "%'";
                a = true;
            }

            if (nombreEmpresaTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NombreEmpresa like '%" + nombreEmpresaTextBox.Text + "%'";
                    a = true;
                }
                else c += " and NombreEmpresa like '%" + nombreEmpresaTextBox.Text + "%'";
            }

            if (cadenaTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cadena like '%" + cadenaTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cadena like '%" + cadenaTextBox.Text + "%'";
            }

            if (cifTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cif like '%" + cifTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cif like '%" + cifTextBox.Text + "%'";
            }

            if (direccionTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Direccion like '%" + direccionTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Direccion like '%" + direccionTextBox.Text + "%'";
            }

            if (poblacionTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Poblacion like '%" + poblacionTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Poblacion like '%" + poblacionTextBox.Text + "%'";
            }

            if (cpTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cp like '%" + cpTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cp like '%" + cpTextBox.Text + "%'";
            }

            if (nombrApellidosTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NombreApellidos like '%" + nombrApellidosTextBox.Text + "%'";
                    a = true;
                }
                else c += " and NombreApellidos like '%" + nombrApellidosTextBox.Text + "%'";
            }

            if (dniTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Dni like '%" + dniTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Dni like '%" + dniTextBox.Text + "%'";
            }

            if (radioButton1.Checked)
            {
                if (a == false)
                {
                    c += " Activo = 'Sí'";
                    a = true;
                }
                else c += " and Activo = 'Sí'";
            }

            else if (radioButton2.Checked)
            {
                if (a == false)
                {
                    c += " Activo = 'No'";
                    a = true;
                }
                else c += " and Activo = 'No'";
            }

            if (radioButton3.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado != ''";
                    a = true;
                }
                else c += " and Actualizado != ''";
            }
            else if (radioButton4.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado = ''";
                    a = true;
                }
                else c += " and Actualizado = ''";
            }

            if (telefonoTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NumeroTlf like '%" + telefonoTextBox.Text + "%'";
                }
                else c += " and NumeroTlf like '%" + telefonoTextBox.Text + "%'";
            }

            if (c.Length > 6) return c;
            else return "";
        }

        //REALIZA LA CONEXION CON LA BD Y REALIZA LA SELECT
        private void devolverConsulta(string consulta)
        {
            try
            {
                dataGridView1.RowCount = 1;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                int cont = 0;
                char[] cad;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = consulta;
                MySqlDataReader leer = comandos.ExecuteReader();


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {                       
                        dataGridView1.Rows.Add();

                        dataGridView1.Rows[cont].Cells[0].Value = leer["Codigo"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[1].Value = leer["NombreEmpresa"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[2].Value = leer["Cadena"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[3].Value = leer["Cif"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[4].Value = leer["Direccion"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[5].Value = leer["Poblacion"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[6].Value = leer["Cp"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[7].Value = leer["NombreApellidos"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[8].Value = leer["Dni"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[9].Value = leer["ntlf"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[10].Value = leer["Observaciones"] + Environment.NewLine;
                        dataGridView1.Rows[cont].Cells[11].Value = leer["Activo"] + Environment.NewLine;

                        if(leer["Actualizado"] + Environment.NewLine != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for(int x = 0; x < cad.Length; x++)
                            {
                                dataGridView1.Rows[cont].Cells[(int)cad[x] - 48].Style.BackColor = Color.LightSkyBlue;
                            }
                        }
                       
                        cont++;
                    }
                }
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error: " + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(e));
                }
            }
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
            radioButton5.Checked = true;
            radioButton6.Checked = true;
        }

       
            ///*
            // * AÑADIR
            // * 
            // */

        //BOTON AGREGAR CLIENTE CLIENTE-AÑADIR   ----- TODO las excepciones a la hora de agregar
        private void cli_a_Agregar_button_Click(object sender, EventArgs e)
        {
            string[] cadTlf;
            string insercion;
            bool ba=true, bo=true;

            //Comprobación de que los campos obligatorios están completos y los datos introducidos son correctos, en tal caso, añadir datos  
            if (!comprobarCamposCliA())
            {
                
                DialogResult b = MessageBox.Show("          Confirmar añadir cliente.", "Confirmación", MessageBoxButtons.OKCancel);
                if (b == DialogResult.OK)
                {
                    insercion = "insert cliente values ('";

                    insercion += agregarCamposInsertCliente();

                    ba=insertClientes(insercion);

                    if (cli_a_tlf_textBox.Text != "")
                    {
                        cadTlf = agregarCamposTlf(cli_a_tlf_textBox.Text);
                        bo=insertarTelefono(cadTlf, cli_a_cod_textBox.Text, cli_a_nombEm_textBox.Text);
                    }

                    if(ba && bo)
                    {
                        DialogResult q = MessageBox.Show("          Usuario añadido correctamente.\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA();
                    }
                    else if(ba && !bo)
                    {
                        DialogResult q = MessageBox.Show("          Usuario añadido correctamente, pero error a la hora de insertar los teléfonos"+
                            "\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA();
                    }
                    else if(!ba && bo)
                    {
                        DialogResult q = MessageBox.Show("          Error al añadir cliente, pero teléfono insertado correctamente. "+
                            "El usuario debía de existir previamente.\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA();
                    }
                }
            }
        }

        private bool insertarTelefono(string[] cadTlf, string text1, string text2)
        {
            string sentencia;
            bool resultado = true;

            try
            {
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();
                for (int x = 0; x < cadTlf.Length; x++)
                {
                    sentencia = "insert telefono values ('" + cadTlf[x] + "','" + text1 + "','" + text2 + "')";
                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;
                    comandos.CommandText = sentencia;
                    comandos.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error insertando uno o más teléfonos: \n\n" + Convert.ToString(error));
                resultado = false;
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(e));
                }
            }
            return resultado;
        }

        //GENERA LA SENTENCIA SQL PARA INSERTAR EL CLIENTE
        private bool insertClientes(string insercion)
        {
            bool resultado=false;
            try
            {
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();
                
                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = insercion;
                comandos.ExecuteNonQuery();

                resultado = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(e));
                }
            }
            return resultado;
        }

        //BOTON LIMPIAR CAMPOS CLIENTE-AÑADIR
        private void cli_a_LC_button_Click(object sender, EventArgs e)
        {
            limpCampCliA();
        }

        //Funcion para limpiar los campos de cliente-añadir
        public void limpCampCliA()
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
        public string[] agregarCamposTlf(string texto)
        {
            string[] cadenaTlf;
            char[] separador = new char[6];

            separador[0] = ',';
            separador[1] = ';';
            separador[2] = '.';
            separador[3] = ':';
            separador[4] = '-';
            separador[5] = ' ';

            texto = texto.Replace(" ", string.Empty);
            cadenaTlf = texto.Split(separador);
            return cadenaTlf;
        }

        //Rellena los datos para realizar posteriormente el insert de cliente
        private string agregarCamposInsertCliente()
        {
            string cadenaCamposI = "";
            //campos normales TABLA CLIENTE
            cadenaCamposI += cli_a_cod_textBox.Text;
            cadenaCamposI += "', '" + cli_a_nombEm_textBox.Text;
            cadenaCamposI += "', '" + cli_a_cad_textBox.Text;
            cadenaCamposI += "', '" + cli_a_cif_textBox.Text;
            cadenaCamposI += "', '" + cli_a_dir_textBox.Text;
            cadenaCamposI += "', '" + cli_a_poblacion_textBox.Text;
            cadenaCamposI += "', '" + cli_a_cp_textBox.Text;
            cadenaCamposI += "', '" + cli_a_nombApell_textBox.Text;
            cadenaCamposI += "', '" + cli_a_dni_textBox.Text;
            cadenaCamposI += "', '" + cli_a_observacion_richTextBox.Text;
            if (cli_a_activo_SI_radioButton.Checked) cadenaCamposI += "', 'Sí'";
            else cadenaCamposI += "', 'No'";

            //Actualizado TABLA CLIENTE
            cadenaCamposI += ",'";
            if (checkBox_cli_a_cod.Checked) cadenaCamposI += "0";
            if (checkBox_cli_a_nombEm.Checked) cadenaCamposI += "1";
            if (checkBox_cli_a_cad.Checked) cadenaCamposI += "2";
            if (checkBox_cli_a_cif.Checked) cadenaCamposI += "3";
            if (checkBox_cli_a_dir.Checked) cadenaCamposI += "4";
            if (checkBox_cli_a_poblacion.Checked) cadenaCamposI += "5";
            if (checkBox_cli_a_cp.Checked) cadenaCamposI += "6";
            if (checkBox_cli_a_nomAp.Checked) cadenaCamposI += "7";
            if (checkBox_cli_a_dni.Checked) cadenaCamposI += "8";
            if (checkBox_cli_a_tlf.Checked) cadenaCamposI += "9";
            cadenaCamposI += "')";

            return cadenaCamposI;
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
