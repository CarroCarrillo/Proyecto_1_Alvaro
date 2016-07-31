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
    class FuncionesMaquina
    {
        private string conexion;
        private string consultaGlobal;
        private MySqlConnection con = new MySqlConnection();
        private int indicePgn, tamPgn = 30, totalPgn;


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
            consulta = "select count(*) from maquina ";
            consulta += camposWhere;
            totalPgn = devolverContar(consulta, f);

            //Select normal
            consulta = "select * from maquina ";
            consulta += camposWhere + " order by Modelo_Nombre, Anyo, Numero ";

            if (totalPgn > tamPgn)
            {
                consultaGlobal = consulta;
                consulta += " and Modelo_Nombre limit 0, " + tamPgn.ToString();
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
        //TODO Incluir lo de la tabla cliente y lo de los documentos pdf
        private string rellenarSelect(Form1 f)
        {
            bool a = false;
            string c = " where";

            if (f.textBox10.Text != "")
            {
                c += " Numero like '%" + f.textBox10.Text + "%'";
                a = true;
            }

            if (f.comboBox2.Text != "")
            {
                if (a == false)
                {
                    c += " Modelo_Nombre like '%" + f.comboBox2.Text + "%'";
                    a = true;
                }
                else c += " and Modelo_Nombre like '%" + f.comboBox2.Text + "%'";
            }

            if (f.dateTimePicker2.Text != "")
            {
                if (a == false)
                {
                    c += " Anyo like '%" + f.dateTimePicker2.Text + "%'";
                    a = true;
                }
                else c += " and Anyo like '%" + f.dateTimePicker2.Text + "%'";
            }

            if (f.textBox8.Text != "")
            {
                if (a == false)
                {
                    c += " Descripcion like '%" + f.textBox8.Text + "%'";
                    a = true;
                }
                else c += " and Descripcion like '%" + f.textBox8.Text + "%'";
            }

            if (f.comboBox1.Text != "")
            {
                if (a == false)
                {
                    c += " EstadosMaquina_Id like '%" + f.comboBox1.Text + "%'";
                    a = true;
                }
                else c += " and EstadosMaquina_Id like '%" + f.comboBox1.Text + "%'";
            }
            /*
            if (f.textBox3.Text != "")
            {
                if (a == false)
                {
                    c += " _ like '%" + f.textBox3.Text + "%'";
                    a = true;
                }
                else c += " and _ like '%" + f.textBox3.Text + "%'";
            }

            if (f.textBox2.Text != "")
            {
                if (a == false)
                {
                    c += " _ like '%" + f.textBox2.Text + "%'";
                    a = true;
                }
                else c += " and _ like '%" + f.textBox2.Text + "%'";
            }

            if (f.dateTimePicker1.Text != "")
            {
                if (a == false)
                {
                    c += " _ like '%" + f.dateTimePicker1.Text + "%'";
                    a = true;
                }
                else c += " and _ like '%" + f.dateTimePicker1.Text + "%'";
            }
            */
            if (f.richTextBox1.Text != "")
            {
                if (a == false)
                {
                    c += " Observaciones like '%" + f.richTextBox1.Text + "%'";
                    a = true;
                }
                else c += " and Observaciones like '%" + f.richTextBox1.Text + "%'";
            }
            
            if (f.richTextBox2.Text != "")
            {
                if (a == false)
                {
                    c += " ObservacionesAdquisicion like '%" + f.richTextBox2.Text + "%'";
                }
                else c += " and ObservacionesAdquisicion like '%" + f.richTextBox2.Text + "%'";
            }

            //INCLUIR LO DEL DOCUMENTO PDF 
            if (c.Length > 6) return c;
            else return "";
        }

        //REALIZA LA CONEXION CON LA BD Y REALIZA LA SELECT
        //TODO retocar parametros entrada y el actualizado
        public void devolverConsulta(string consulta, Form1 f)
        {
            try
            {
                f.dataGridView2.RowCount = 1;
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
                        
                        f.dataGridView2.Rows[cont].Cells[0].Value = leer["Modelo_Nombre"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[1].Value = leer["Anyo"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[2].Value = leer["Numero"] + Environment.NewLine;
                        //f.dataGridView2.Rows[cont].Cells[3].Value = leer["Cif"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[4].Value = leer["Descripcion"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[5].Value = leer["EstadosMaquina_Id"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[6].Value = leer["Observaciones"] + Environment.NewLine;
                        //f.dataGridView2.Rows[cont].Cells[7].Value = leer["CodigoAgente"] + Environment.NewLine;
                        //f.dataGridView2.Rows[cont].Cells[8].Value = leer["NombreAgente"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[9].Value = leer["FechaAdquisicion"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[10].Value = leer["ObservacionesAdquisicion"] + Environment.NewLine;
                        f.dataGridView2.Rows[cont].Cells[11].Value = leer["ImagenAdquisicion"] + Environment.NewLine;

                        if (leer["Actualizado"] + Environment.NewLine != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for (int x = 0; x < cad.Length; x++)
                            {
                                f.dataGridView2.Rows[cont].Cells[(int)cad[x] - 48].Style.BackColor = Color.LightSkyBlue;
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

        //BOTON LIMPIAR CAMPOS BUSCAR
        public void limpCampCliB(Form1 f)
        {
            f.comboBox4.Text = null;
            f.textBox6.Text = null;
            f.comboBox5.Text = null;
            f.dateTimePicker3.Text = null;
            f.textBox5.Text = null;
            f.comboBox6.Text = null;
            f.textBox4.Text = null;
            f.textBox1.Text = null;
            f.dateTimePicker4.Text = null;
            f.radioButton8.Checked = false;
            f.radioButton9.Checked = false;
            f.radioButton7.Checked = true;
        }

        //Esconde o muestra los botones de paginación
        public void visibilidadBotonesPgn(Form1 f)
        {
            if (indicePgn < tamPgn)
            {
                f.button6.Hide();
                f.button5.Hide();
            }
            else
            {
                f.button5.Show();
                f.button6.Show();
            }
            if (indicePgn >= totalPgn - tamPgn)
            {
                f.button4.Hide();
                f.button7.Hide();
            }
            else
            {
                f.button7.Show();
                f.button4.Show();
            }
        }

        //Botón paginación >
        public void botonPgnD(Form1 f)
        {
            string cons;
            indicePgn += tamPgn;
            cons = consultaGlobal + " and Modelo_Nombre limit " + indicePgn.ToString() + "," + tamPgn.ToString();
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
            cons = consultaGlobal + " and Modelo_Nombre limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón paginación <
        public void botonPgnI(Form1 f)
        {
            string cons;
            indicePgn -= tamPgn;
            cons = consultaGlobal + " and Modelo_Nombre limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Botón paginación <<
        public void botonPgnII(Form1 f)
        {
            string cons;
            indicePgn = 0;
            cons = consultaGlobal + " and Modelo_Nombre limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            devolverConsulta(cons, f);
            visibilidadBotonesPgn(f);
            mostrarPgn(f);
        }

        //Método que muestra la página en la que se está
        private void mostrarPgn(Form1 f)
        {
            double x = 1, y = 1;
              
            if (indicePgn != 0) x = Math.Truncate((double)indicePgn / (double)tamPgn) + 1;
            y = Math.Ceiling((double)totalPgn / (double)tamPgn);

            if (x == 1 && y == 1) f.label17.Text = " ";
            else f.label17.Text = "Pgn. " + x + "/" + y;
        }


        ///*
        // * AÑADIR
        // * 
        // */

        //PRIMER MÉTODO LLAMADO PARA AGREGAR MAQUINA 
        public void botonAgregarMaquina(Form1 f)
        {
            //Comprobación de que los campos obligatorios están completos y los datos introducidos son correctos, en tal caso, añadir datos  
            if (!comprobarCamposCliA(f))
            {
                string[] cadTlf;
                string insercion;
                bool ba = true, bo = true;

                DialogResult b = MessageBox.Show("          Confirmar añadir cliente.", "Confirmación", MessageBoxButtons.OKCancel);
                if (b == DialogResult.OK)
                {
                    insercion = "insert maquina values ('";

                    insercion += agregarCamposInsertMaquinas(f);

                    ba = insertMaquinas(insercion);

                    if (f.cli_a_tlf_textBox.Text != "")
                    {
                        //cadTlf = agregarCamposTlf(f.cli_a_tlf_textBox.Text);
                        //bo = insertarTelefono(cadTlf, f.cli_a_cod_textBox.Text, f.cli_a_nombEm_textBox.Text);
                    }

                    if (ba && bo)
                    {
                        DialogResult q = MessageBox.Show("          Usuario añadido correctamente.\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                    else if (ba && !bo)
                    {
                        DialogResult q = MessageBox.Show("          Usuario añadido correctamente, pero error a la hora de insertar los teléfonos" +
                            "\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                    else if (!ba && bo)
                    {
                        DialogResult q = MessageBox.Show("          Error al añadir cliente, pero teléfono insertado correctamente. " +
                            "El usuario debía de existir previamente.\n          ¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampCliA(f);
                    }
                }
            }
        }

        //GENERA LA SENTENCIA SQL PARA INSERTAR EL CLIENTE
        private bool insertMaquinas(string insercion)
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

        //Rellena los datos para realizar posteriormente el insert de cliente
        private string agregarCamposInsertMaquinas(Form1 f)
        {
            string cadenaCamposI = "";
            //campos normales TABLA CLIENTE
            cadenaCamposI += f.textBox10.Text;
            cadenaCamposI += "', '" + f.comboBox2.Text;
            cadenaCamposI += "', '" + f.dateTimePicker2.Text;
            cadenaCamposI += "', '" + f.textBox8.Text;
            cadenaCamposI += "', '" + f.richTextBox1.Text;

            cadenaCamposI += "', '";
            if (f.checkBox10.Checked) cadenaCamposI += "0";
            if (f.checkBox10.Checked) cadenaCamposI += "1";
            if (f.checkBox10.Checked) cadenaCamposI += "2";
            if (f.checkBox8.Checked) cadenaCamposI += "4";
            if (f.checkBox7.Checked) cadenaCamposI += "5";
            if (f.checkBox3.Checked) cadenaCamposI += "7";
            if (f.checkBox2.Checked) cadenaCamposI += "8";
            if (f.checkBox1.Checked) cadenaCamposI += "9";

            cadenaCamposI += "', '" + f.comboBox1.Text;
            cadenaCamposI += "', '" + f.dateTimePicker1.Text;
            cadenaCamposI += "', '" + f.textBox3.Text;
            cadenaCamposI += "', '" + f.textBox2.Text;
            cadenaCamposI += "', '" + f.richTextBox2.Text;

            //añadir aquí lo de los archivos pdf

            
            cadenaCamposI += "')";

            return cadenaCamposI;
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
            f.textBox10.Text = null;
            f.comboBox2.Text = null;
            f.dateTimePicker2.Text = null;
            f.textBox8.Text = null;
            f.comboBox1.Text = null;
            f.textBox3.Text = null;
            f.textBox2.Text = null;
            f.dateTimePicker1.Text = null;
            f.checkBox10.Checked = false;
            f.checkBox8.Checked = false;
            f.checkBox7.Checked = false;
            f.checkBox3.Checked = false;
            f.checkBox2.Checked = false;
            f.checkBox1.Checked = false;
        }

        //Hace la comprobación de que los campos obligatorios correspondientes a CLIENTE-AÑADIR estén completos
        private bool comprobarCamposCliA(Form1 f)
        {
            bool error = false;

            if (f.textBox10.Text == null || f.textBox10.Text == "") error = true;
            if (f.comboBox2.Text == null || f.comboBox2.Text == "") error = true;
            if (f.comboBox1.Text == null || f.comboBox1.Text == "") error = true;
            //if (f.textBox3.Text == null || f.textBox3.Text == "") error = true;
            //if (f.textBox2.Text == null || f.textBox2.Text == "") error = true;

            if (error != true)
            {
                //Comprobar que el CODIGO cumple los requisitos
               //error = comprobarCodigo(f);
               
                //Comprobar que el CP cumple los requisitos
                if (!error) error = comprobarNum(f);
                
                //Comprueba longitud de campos
                if (!error) if (f.textBox8.TextLength > 45) error = true;
                if (!error) if (f.richTextBox1.TextLength > 700) error = true;
                if (!error) if (f.richTextBox2.TextLength > 700) error = true;

            }
            if (error == true) MessageBox.Show("No puede agregarse el cliente, compruebe los campos obligatorios y el cumplimiento de las condiciones.");
            return error;
        }

        //Complementa a comprobarCamposCliA()
        public bool comprobarNum(Form1 f)
        {
            if (f.textBox10.Text == null || f.textBox10.Text == "") { }
            else
            {
                char[] numArray = f.textBox10.Text.ToCharArray();
                if (numArray.Length == 4)
                {
                    for (int i = 0; i < 4; i++) if (numArray[i] < 48 || numArray[i] > 57) return true;
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
                for (int i = 0; i < 5; i++) if (codigoArray[i] < 48 || codigoArray[i] > 57) return true;
            }
            else return true;

            return false;
        }
    }
}
