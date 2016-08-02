using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoA
{
    class FuncionesCliente
    {
        private string conexion;
        private string consultaGlobal;
        private MySqlConnection con = new MySqlConnection();
        private int indicePgn, tamPgn = 2, totalPgn;


        ///*
        // * BÚSQUEDA
        // * 
        // */
        
        //Boton que inicia la búsqueda
        public void botonIniciarBusqueda(Form1 f)
        {
            string[] cadenaLabels = new string[12];
            string consulta, camposWhere;
            indicePgn = 0;

            camposWhere = rellenarSelect(f);

            //Select count(*)
            consulta = "select count(*) as contandoF from (select Codigo, group_concat(t.NumeroTlf separator ', ') as ntlf "
            + "from cliente c left join telefono t on c.Codigo = t.Cliente_Codigo "
            + "and c.NombreEmpresa = t.Cliente_NombreEmpresa ";
            consulta += camposWhere;
            consulta += "group by Codigo, NombreEmpresa) algo";
            totalPgn = devolverContar(consulta, f);

            //Select normal
            consulta = "select c.*, group_concat(t.NumeroTlf separator ', ') as ntlf " +
                       "from cliente c left join telefono t on c.Codigo = t.Cliente_Codigo " +
                       "and c.NombreEmpresa = t.Cliente_NombreEmpresa";
            consulta += camposWhere + " group by Codigo, NombreEmpresa";

            if (totalPgn > tamPgn)
            {
                consultaGlobal = consulta;
                consulta += " order by codigo and codigo limit 0, " + tamPgn.ToString();
                visibilidadBotonesPgn(f);
            }
            devolverConsulta(consulta, f);
            mostrarPgn(f);
        }

        //Devuelve el count
        private int devolverContar(string consulta, Form1 f)
        {
            try
            {
                f.dataGridView1.RowCount = 1;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = consulta;
                MySqlDataReader leer = comandos.ExecuteReader();

                if (leer.HasRows)
                {
                    leer.Read();
                    return Int32.Parse(leer["contandoF"].ToString());
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
                catch (Exception e)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(e));
                }
            }
            return 0;
        }

        //CREA EL WHERE DE LA SELECT
        private string rellenarSelect(Form1 f)
        {
            bool a = false;
            string c = " where";

            if (f.codigoTextBox.Text != "")
            {
                c += " Codigo like '%" + f.codigoTextBox.Text + "%'";
                a = true;
            }

            if (f.nombreEmpresaTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NombreEmpresa like '%" + f.nombreEmpresaTextBox.Text + "%'";
                    a = true;
                }
                else c += " and NombreEmpresa like '%" + f.nombreEmpresaTextBox.Text + "%'";
            }

            if (f.cadenaTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cadena like '%" + f.cadenaTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cadena like '%" + f.cadenaTextBox.Text + "%'";
            }

            if (f.cifTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cif like '%" + f.cifTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cif like '%" + f.cifTextBox.Text + "%'";
            }

            if (f.direccionTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Direccion like '%" + f.direccionTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Direccion like '%" + f.direccionTextBox.Text + "%'";
            }

            if (f.poblacionTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Poblacion like '%" + f.poblacionTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Poblacion like '%" + f.poblacionTextBox.Text + "%'";
            }

            if (f.cpTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Cp like '%" + f.cpTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Cp like '%" + f.cpTextBox.Text + "%'";
            }

            if (f.nombrApellidosTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NombreApellidos like '%" + f.nombrApellidosTextBox.Text + "%'";
                    a = true;
                }
                else c += " and NombreApellidos like '%" + f.nombrApellidosTextBox.Text + "%'";
            }

            if (f.dniTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " Dni like '%" + f.dniTextBox.Text + "%'";
                    a = true;
                }
                else c += " and Dni like '%" + f.dniTextBox.Text + "%'";
            }

            if (f.radioButton1.Checked)
            {
                if (a == false)
                {
                    c += " Activo = 'Sí'";
                    a = true;
                }
                else c += " and Activo = 'Sí'";
            }

            else if (f.radioButton2.Checked)
            {
                if (a == false)
                {
                    c += " Activo = 'No'";
                    a = true;
                }
                else c += " and Activo = 'No'";
            }

            if (f.radioButton3.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado != ''";
                    a = true;
                }
                else c += " and Actualizado != ''";
            }
            else if (f.radioButton4.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado = ''";
                    a = true;
                }
                else c += " and Actualizado = ''";
            }

            if (f.telefonoTextBox.Text != "")
            {
                if (a == false)
                {
                    c += " NumeroTlf like '%" + f.telefonoTextBox.Text + "%'";
                }
                else c += " and NumeroTlf like '%" + f.telefonoTextBox.Text + "%'";
            }

            if (c.Length > 6) return c;
            else return "";
        }
        
        //REALIZA LA CONEXION CON LA BD Y REALIZA LA SELECT
        public void devolverConsulta(string consulta, Form1 f)
        {
            try
            {
                f.dataGridView1.RowCount = 1;
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
                        f.dataGridView1.Rows.Add();

                        f.dataGridView1.Rows[cont].Cells[0].Value = leer["Codigo"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[1].Value = leer["NombreEmpresa"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[2].Value = leer["Cadena"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[3].Value = leer["Cif"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[4].Value = leer["Direccion"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[5].Value = leer["Poblacion"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[6].Value = leer["Cp"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[7].Value = leer["NombreApellidos"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[8].Value = leer["Dni"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[9].Value = leer["ntlf"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[10].Value = leer["Observaciones"] + Environment.NewLine;
                        f.dataGridView1.Rows[cont].Cells[11].Value = leer["Activo"] + Environment.NewLine;

                        if (leer["Actualizado"] + Environment.NewLine != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for (int x = 0; x < cad.Length; x++)
                            {
                                f.dataGridView1.Rows[cont].Cells[(int)cad[x] - 48].Style.BackColor = Color.LightSkyBlue;
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
                catch (Exception e)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(e));
                }
            }
        }

        //BOTON LIMPIAR CAMPOS CLIENTE-AÑADIR
        public void limpCampCliB(Form1 f)
        {
            f.codigoTextBox.Text = null;
            f.nombreEmpresaTextBox.Text = null;
            f.cadenaTextBox.Text = null;
            f.cifTextBox.Text = null;
            f.direccionTextBox.Text = null;
            f.poblacionTextBox.Text = null;
            f.cpTextBox.Text = null;
            f.nombrApellidosTextBox.Text = null;
            f.dniTextBox.Text = null;
            f.telefonoTextBox.Text = null;
            f.radioButton1.Checked = false;
            f.radioButton2.Checked = false;
            f.radioButton3.Checked = false;
            f.radioButton4.Checked = false;
            f.radioButton5.Checked = true;
            f.radioButton6.Checked = true;
        }

        //Esconde o muestra los botones de paginación
        public void visibilidadBotonesPgn(Form1 f)
        {
            if (indicePgn < tamPgn)
            {
                f.buttonIz.Hide();
                f.buttonIzIz.Hide();
            }
            else
            {
                f.buttonIzIz.Show();
                f.buttonIz.Show();
            }
            if (indicePgn >= totalPgn - tamPgn)
            {
                f.buttonDeDe.Hide();
                f.buttonDe.Hide();
            }
            else
            {
                f.buttonDe.Show();
                f.buttonDeDe.Show();
            }
        }

        //Botón paginación >
        public void botonPgnD(Form1 f)
        {
            string cons;
            indicePgn += tamPgn;
            cons = consultaGlobal + " order by codigo and codigo limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón paginación >>
        public void botonPgnDD(Form1 f)
        {
            string cons;
            int r = totalPgn % tamPgn;
            indicePgn = totalPgn - r;
            cons = consultaGlobal + " order by codigo and codigo limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón paginación <
        public void botonPgnI(Form1 f)
        {
            string cons;
            indicePgn -= tamPgn;
            cons = consultaGlobal + " order by codigo and codigo limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón paginación <<
        public void botonPgnII(Form1 f)
        {
            string cons;
            indicePgn = 0;
            cons = consultaGlobal + " order by codigo and codigo limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón que reinicia la misma página
        public void reiniciarPgn(Form1 f)
        {
            string cons;
            cons = consultaGlobal + " order by codigo and codigo limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }
        
        //Método que muestra la página en la que se está
        private void mostrarPgn(Form1 f)
        {
            double x = 1, y = 1;
                      
            if (indicePgn != 0) x=Math.Truncate((double)indicePgn / (double)tamPgn) + 1;
            y = Math.Ceiling((double)totalPgn / (double)tamPgn);
            
            if(x==1 && y == 1) f.numPgnLabel.Text = " ";
            else f.numPgnLabel.Text = "Pgn. " + x + "/" + y; 
        }


        ///*
        // * AÑADIR
        // * 
        // */

        //PRIMER MÉTODO LLAMADO PARA AGREGAR CLIENTE 
        public void botonAgregarClientes(Form1 f)
        {
            //Comprobación de que los campos obligatorios están completos y los datos introducidos son correctos, en tal caso, añadir datos  
            if (!comprobarCamposCliA(f))
            {
                string[] cadTlf;
                string insercion;
                bool ba = true, bo = false;

                DialogResult b = MessageBox.Show("Confirmar añadir cliente.", "Confirmación", MessageBoxButtons.OKCancel);
                if (b == DialogResult.OK)
                {
                    insercion = "insert cliente values ('";

                    insercion += agregarCamposInsertCliente(f);

                    ba = insertClientes(insercion);

                    if (f.cli_a_tlf_textBox.Text != "")
                    {
                        cadTlf = agregarCamposTlf(f.cli_a_tlf_textBox.Text);
                        bo = insertarTelefono(cadTlf, f.cli_a_cod_textBox.Text, f.cli_a_nombEm_textBox.Text);
                    }

                    if (ba && bo)
                    {
                        DialogResult q = MessageBox.Show("Usuario añadido correctamente.\n\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                    else if (ba && !bo)
                    {
                        DialogResult q = MessageBox.Show("Usuario añadido correctamente, pero error a la hora de insertar los teléfonos" +
                            "\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                    else if (!ba && bo)
                    {
                        DialogResult q = MessageBox.Show("Error al añadir cliente, pero teléfono insertado correctamente. " +
                            "El usuario debía de existir previamente.\n\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                }
            }
        }

        //GENERA LA SENTENCIA SQL PARA INSERTAR EL CLIENTE
        private bool insertClientes(string insercion)
        {
            bool resultado = false;
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
                MessageBox.Show("Error producido. Compruebe que el cliente no exista ya:\n\n" + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch(Exception erro)
                {
                    MessageBox.Show("Error producido al cerrar sesión:\n\n" + Convert.ToString(erro));
                }
            }
            return resultado;
        }
                       
        //Rellena los datos para realizar posteriormente el insert de cliente
        private string agregarCamposInsertCliente(Form1 f)
        {
            string cadenaCamposI = "";
            //campos normales TABLA CLIENTE
            cadenaCamposI += f.cli_a_cod_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_nombEm_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_cad_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_cif_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_dir_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_poblacion_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_cp_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_nombApell_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_dni_textBox.Text;
            cadenaCamposI += "', '" + f.cli_a_observacion_richTextBox.Text;
            if (f.cli_a_activo_SI_radioButton.Checked) cadenaCamposI += "', 'Sí'";
            else cadenaCamposI += "', 'No'";

            //Actualizado TABLA CLIENTE
            cadenaCamposI += ",'";
            if (f.checkBox_cli_a_cod.Checked) cadenaCamposI += "0";
            if (f.checkBox_cli_a_nombEm.Checked) cadenaCamposI += "1";
            if (f.checkBox_cli_a_cad.Checked) cadenaCamposI += "2";
            if (f.checkBox_cli_a_cif.Checked) cadenaCamposI += "3";
            if (f.checkBox_cli_a_dir.Checked) cadenaCamposI += "4";
            if (f.checkBox_cli_a_poblacion.Checked) cadenaCamposI += "5";
            if (f.checkBox_cli_a_cp.Checked) cadenaCamposI += "6";
            if (f.checkBox_cli_a_nomAp.Checked) cadenaCamposI += "7";
            if (f.checkBox_cli_a_dni.Checked) cadenaCamposI += "8";
            if (f.checkBox_cli_a_tlf.Checked) cadenaCamposI += "9";
            cadenaCamposI += "')";

            return cadenaCamposI;
        }
                
        //Rellena los datos de telefono en un array
        public string[] agregarCamposTlf(string tlfs)
        {
            string[] cadenaTlf;

            tlfs = tlfs.Replace(" ", string.Empty);
            tlfs = tlfs.Replace(';', ',');
            tlfs = tlfs.Replace(':', ',');
            tlfs = tlfs.Replace('-', ',');
            tlfs = tlfs.Replace('.', ',');

            cadenaTlf = tlfs.Split(',');

            return cadenaTlf;
        }

        //Realiza la inserción de los tlfs
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

        //Limpia los campos de cliente añadir
        public void limpCampCliA(Form1 f)
        {
            f.cli_a_activo_NO_radioButton.Checked = false;
            f.cli_a_activo_SI_radioButton.Checked = false;
            f.cli_a_cad_textBox.Text = null;
            f.cli_a_cif_textBox.Text = null;
            f.cli_a_cod_textBox.Text = null;
            f.cli_a_cp_textBox.Text = null;
            f.cli_a_dir_textBox.Text = null;
            f.cli_a_dni_textBox.Text = null;
            f.cli_a_nombApell_textBox.Text = null;
            f.cli_a_nombEm_textBox.Text = null;
            f.cli_a_observacion_richTextBox.Text = null;
            f.cli_a_poblacion_textBox.Text = null;
            f.cli_a_tlf_textBox.Text = null;
            f.checkBox_cli_a_cad.Checked = false;
            f.checkBox_cli_a_cif.Checked = false;
            f.checkBox_cli_a_cod.Checked = false;
            f.checkBox_cli_a_cp.Checked = false;
            f.checkBox_cli_a_dir.Checked = false;
            f.checkBox_cli_a_dni.Checked = false;
            f.checkBox_cli_a_nomAp.Checked = false;
            f.checkBox_cli_a_nombEm.Checked = false;
            f.checkBox_cli_a_poblacion.Checked = false;
            f.checkBox_cli_a_tlf.Checked = false;
        }

        //Hace la comprobación de que los campos obligatorios correspondientes a CLIENTE-AÑADIR estén completos
        private bool comprobarCamposCliA(Form1 f)
        {
            bool error = false;

            if (f.cli_a_cod_textBox.Text == null || f.cli_a_cod_textBox.Text == "") error = true;
            if (f.cli_a_nombEm_textBox.Text == null || f.cli_a_nombEm_textBox.Text == "") error = true;
            if (f.cli_a_dir_textBox.Text == null || f.cli_a_dir_textBox.Text == "") error = true;
            if (f.cli_a_dni_textBox.Text == null || f.cli_a_dni_textBox.Text == "") error = true;
            if (f.cli_a_poblacion_textBox.Text == null || f.cli_a_poblacion_textBox.Text == "") error = true;
            if (f.cli_a_activo_NO_radioButton.Checked == false && f.cli_a_activo_SI_radioButton.Checked == false) error = true;

            if (error != true) 
            {
                //Comprobar que el CODIGO cumple los requisitos
                error = comprobarCodigo(f);

                //Comprobar que el CIF cumple los requisitos
                if (!error) error = comprobarCif(f); 

                //Comprobar que el DNI cumple los requisitos
                if (!error) error = comprobarDni(f);

                //Comprobar que el CP cumple los requisitos
                if (!error) error = comprobarCp(f);

                //Comprobar que los TLF cumplen los requisitos
                if (!error) error = comprobarTlf(f);

                //Comprueba longitud de campos
                if (!error) if (f.cli_a_nombEm_textBox.TextLength > 45) error = true;
                if (!error) if (f.cli_a_cad_textBox.TextLength > 45) error = true;
                if (!error) if (f.cli_a_dir_textBox.TextLength > 45) error = true;
                if (!error) if (f.cli_a_poblacion_textBox.TextLength > 45) error = true;
                if (!error) if (f.cli_a_nombEm_textBox.TextLength > 45) error = true;
                if (!error) if (f.cli_a_observacion_richTextBox.TextLength > 700) error = true;

            }
            if(error==true) MessageBox.Show("No puede agregarse el cliente, compruebe los campos obligatorios y el cumplimiento de las condiciones.");
            return error;
        }

        //Complementa a comprobarCamposCliA()
        public bool comprobarTlf(Form1 f)
        {
            if (f.cli_a_tlf_textBox.Text == null || f.cli_a_tlf_textBox.Text == "") { }
            else
            {
                string[] cadenaTlf;
                string tlfs;
                char[] tlfDesc;

                tlfs = f.cli_a_tlf_textBox.Text;
                tlfs = tlfs.Replace(" ", string.Empty);
                tlfs = tlfs.Replace(';', ',');
                tlfs = tlfs.Replace(':', ',');
                tlfs = tlfs.Replace('-', ',');
                tlfs = tlfs.Replace('.', ',');

                cadenaTlf = tlfs.Split(',');

                for (int x = 0; x < cadenaTlf.Length; x++)
                {
                    tlfDesc = cadenaTlf[x].ToCharArray();
                    if (tlfDesc.Length == 9)
                    {
                        for (int i = 0; i < 9; i++) if (tlfDesc[i] < 48 || tlfDesc[i] > 57) return true;
                    }
                    else return true;
                }
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        public bool comprobarCp(Form1 f)
        {
            if (f.cli_a_cp_textBox.Text == null || f.cli_a_cp_textBox.Text == "") { }
            else
            {
                char[] cpArray = f.cli_a_cp_textBox.Text.ToCharArray();
                if (cpArray.Length == 5)
                {
                    for (int i = 0; i < 5; i++) if (cpArray[i] < 48 || cpArray[i] > 57) return true;
                }
                else return true;
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        public bool comprobarDni(Form1 f)
        {
            char[] dniArray = f.cli_a_dni_textBox.Text.ToCharArray();
            if (dniArray.Length == 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0)
                    {
                        if ((dniArray[i] < 48 || dniArray[i] > 57) && (dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))  return true; 
                    }
                    else if (i == 8)
                    {
                        if ((dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))  return true;
                    }
                    else
                    {
                        if (dniArray[i] < 48 || dniArray[i] > 57) return true;
                    }
                }
            }
            else return true;
            
            return false;
        }
        //Complementa a comprobarCamposCliA()
        public bool comprobarCif(Form1 f)
        {
            if (f.cli_a_cif_textBox.Text == null || f.cli_a_cif_textBox.Text == "") { }
            else
            {
                char[] cifArray = f.cli_a_cif_textBox.Text.ToCharArray();
                if (cifArray.Length == 9)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (i == 8)
                        {
                            if ((cifArray[i] < 48 || cifArray[i] > 57) && (cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122)) return true;
                        }
                        else if (i == 0)
                        {
                            if ((cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122)) return true;
                            
                        }
                        else if (cifArray[i] < 48 || cifArray[i] > 57)  return true;
                    }
                }
                else return true;
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        public bool comprobarCodigo(Form1 f)
        {
            char[] codigoArray = f.cli_a_cod_textBox.Text.ToCharArray();
            if (codigoArray.Length == 5)
            {
                for (int i = 0; i < 5; i++) if (codigoArray[i] < 48 || codigoArray[i] > 57)  return true;
            }
            else return true;
            
            return false;
        }

       
    }
}
