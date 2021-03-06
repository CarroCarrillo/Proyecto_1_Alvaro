﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoA
{
    class FuncionesMaquina
    {
        private string consultaGlobal;
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
            consulta = "select count(*) contandoF from maquina ";
            consulta += camposWhere;
            totalPgn = devolverContar(consulta, f);

            //Select normal
            consulta = "select * from maquina ";
            consulta += camposWhere + " order by Modelo_Nombre, Anyo, Numero ";
            consultaGlobal = consulta;
            if (totalPgn > tamPgn)
            {
                consulta += " and Modelo_Nombre limit 0, " + tamPgn.ToString();
                visibilidadBotonesPgn(f);
            }
            devolverConsulta(consulta, f);
            mostrarPgn(f);
        }
        
        //Devuelve el count
        private int devolverContar(string consulta, Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
            try
            {
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

            if (f.textBox2.Text != "")
            {
                c += " Numero like '%" + f.textBox2.Text + "%'";
                a = true;
            }

            if (f.comboBox5.Text != "")
            {
                if (a == false)
                {
                    c += " Modelo_Nombre like '%" + f.comboBox5.Text + "%'";
                    a = true;
                }
                else c += " and Modelo_Nombre like '%" + f.comboBox5.Text + "%'";
            }

            if (f.textBox3.Text != "")
            {
                if (a == false)
                {
                    c += " Anyo like '%" + f.textBox3.Text + "%'";
                    a = true;
                }
                else c += " and Anyo like '%" + f.textBox3.Text + "%'";
            }

            if (f.textBox5.Text != "")
            {
                if (a == false)
                {
                    c += " Descripcion like '%" + f.textBox5.Text + "%'";
                    a = true;
                }
                else c += " and Descripcion like '%" + f.textBox5.Text + "%'";
            }

            if (f.comboBox6.Text != "")
            {
                if (a == false)
                {
                    c += " id_maquina = (select id from estadosmaquina where Nombre = '" + f.comboBox6.Text + "')"; 
                    a = true;
                }
                else c += " and id_maquina = (select id from estadosmaquina where Nombre = '" + f.comboBox6.Text + "')";
            }

            //Familia
            if (f.comboBox4.Text != "")
            {
                if (a == false)
                {
                    c += " Modelo_Nombre in (select Nombre from modelo where Familia_Nombre = '" + f.comboBox4.Text + "')";
                    a = true;
                }
                else c += " and Modelo_Nombre in (select Nombre from modelo where Familia_Nombre = '" + f.comboBox4.Text + "')";
            }

            if (f.textBox9.Text != "")
            {
                if (a == false)
                {
                    c += " DatosCompra like '%" + f.textBox9.Text + "%'";
                    a = true;
                }
                else c += " and DatosCompra like '%" + f.textBox9.Text + "%'";
            }

            if (f.textBox6.Text != "")
            {
                if (a == false)
                {
                    c += " year(FechaAdquisicion) = '" + f.textBox6.Text + "'";
                    a = true;
                }
                else c += " and year(FechaAdquisicion) = '" + f.textBox6.Text + "'";
            }

            if (f.textBox4.Text != "")
            {
                if (a == false)
                {
                    c += " month(FechaAdquisicion) = '" + f.textBox4.Text + "'";
                    a = true;
                }
                else c += " and month(FechaAdquisicion) = '" + f.textBox4.Text + "'";
            }

            if (f.comboBox9.Text != "")
            {
                if (a == false)
                {
                    c += " cliente_Codigo = '" + f.comboBox9.Text + "'";
                    a = true;
                }
                else c += " and cliente_Codigo = '" + f.comboBox9.Text + "'";
            }

            if (f.comboBox10.Text != "")
            {
                if (a == false)
                {
                    c += " cliente_NombreEmpresa = '" + f.comboBox10.Text + "'";
                    a = true;
                }
                else c += " and cliente_NombreEmpresa = '" + f.comboBox10.Text + "'";
            }

            if (f.textBox1.Text != "")
            {
                if (a == false)
                {
                    c += " year(fechaVenta) = '" + f.textBox1.Text + "'";
                    a = true;
                }
                else c += " and year(fechaVenta) = '" + f.textBox1.Text + "'";
            }

            if (f.textBox12.Text != "")
            {
                if (a == false)
                {
                    c += " month(fechaVenta) = '" + f.textBox12.Text + "'";
                    a = true;
                }
                else c += " and month(fechaVenta) = '" + f.textBox12.Text + "'";
            }

            if (f.radioButton9.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado != ''";
                    a = true;
                }
                else c += " and  Actualizado != ''";
            }

            if (f.radioButton8.Checked)
            {
                if (a == false)
                {
                    c += " Actualizado = ''";
                    a = true;
                }
                else c += " and  Actualizado = ''";
            }
            
            if (f.radioButton12.Checked)
            {
                if (a == false)
                {
                    c += " cliente_Codigo != ''";
                    a = true;
                }
                else c += " and  cliente_Codigo != ''";
            }

            if (f.radioButton11.Checked)
            {
                if (a == false)
                {
                    c += " cliente_Codigo is null";
                    a = true;
                }
                else c += " and  cliente_Codigo is null";
            }

            if (c.Length > 6) return c;
            else return "";
        }

        //REALIZA LA CONEXION CON LA BD Y REALIZA LA SELECT
        //TODO retocar parametros entrada y el actualizado
        public void devolverConsulta(string consulta, Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
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
                        f.dataGridView2.Rows.Add();
                        
                        f.dataGridView2.Rows[cont].Cells[0].Value = leer["Modelo_Nombre"].ToString();
                        f.dataGridView2.Rows[cont].Cells[1].Value = leer["Anyo"].ToString();
                        f.dataGridView2.Rows[cont].Cells[2].Value = leer["Numero"].ToString();
                        string consul1 = "select Familia_Nombre from modelo where Nombre = '" + leer["Modelo_Nombre"].ToString() + "';";
                        subConsultaSelect(consul1, true, cont, f.dataGridView2);
                        f.dataGridView2.Rows[cont].Cells[4].Value = leer["Descripcion"].ToString();
                        string consul2 = "select Nombre from estadosmaquina where id = '" + leer["id_maquina"].ToString() + "';";
                        subConsultaSelect(consul2, false, cont, f.dataGridView2);
                        f.dataGridView2.Rows[cont].Cells[6].Value = leer["Observaciones"].ToString();
                        f.dataGridView2.Rows[cont].Cells[7].Value = leer["DatosCompra"].ToString();
                        f.dataGridView2.Rows[cont].Cells[8].Value = leer["FechaAdquisicion"].ToString().Split(' ')[0];
                        f.dataGridView2.Rows[cont].Cells[9].Value = leer["cliente_Codigo"].ToString();
                        f.dataGridView2.Rows[cont].Cells[10].Value = leer["cliente_NombreEmpresa"].ToString();
                        f.dataGridView2.Rows[cont].Cells[11].Value = leer["fechaVenta"].ToString().Split(' ')[0];
                        f.dataGridView2.Rows[cont].Cells[12].Value = leer["ObservacionesVenta"].ToString();
                        f.dataGridView2.Rows[cont].Cells[13].Value = leer["fichero"].ToString();

                        if (leer["Actualizado"].ToString() != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for (int x = 0; x < cad.Length; x++)
                            {
                                if(x < 3) f.dataGridView2.Rows[cont].Cells[(int)cad[x] - 48].Style.BackColor = Color.LightSkyBlue;
                                else if(x < 5) f.dataGridView2.Rows[cont].Cells[(int)cad[x] - 47].Style.BackColor = Color.LightSkyBlue;
                                else f.dataGridView2.Rows[cont].Cells[(int)cad[x] - 46].Style.BackColor = Color.LightSkyBlue;
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

        private void subConsultaSelect(string consulta, bool posicion, int cont, DataGridView cuadros)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
            try
            {
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
                    if(posicion) cuadros.Rows[cont].Cells[3].Value = leer["Familia_Nombre"].ToString();
                    else cuadros.Rows[cont].Cells[5].Value = leer["Nombre"].ToString();
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
            f.comboBox5.Text = null;
            f.textBox2.Text = null;
            f.textBox3.Text = null;
            f.textBox9.Text = null;
            f.textBox6.Text = null;
            f.textBox5.Text = null;
            f.comboBox6.Text = null;
            f.textBox1.Text = null;
            f.radioButton8.Checked = false;
            f.radioButton9.Checked = false;
            f.radioButton7.Checked = true;
            actualizarTextBoxVentaC("", "", f.comboBox9);
            actualizarTextBoxVentaN("", "", f.comboBox10);
            f.comboBox9.SelectedItem = null;
            f.comboBox10.SelectedItem = null;
            f.radioButton10.Checked = true;
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

        public void reiniciarPgn(Form1 f)
        {
            string cons;
            if (totalPgn > tamPgn)
            {
                cons = consultaGlobal + " and Modelo_Nombre limit " + indicePgn.ToString() + "," + tamPgn.ToString();
            }
            else cons = consultaGlobal;
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
            if (!comprobarCamposMaq(f))
            {
                string insercion;

                DialogResult b = MessageBox.Show("          Confirmar añadir máquina.", "Confirmación", MessageBoxButtons.OKCancel);
                if (b == DialogResult.OK)
                {
                    insercion = "insert maquina values ('";

                    insercion += agregarCamposInsertMaquinas(f);
                    
                    if (insertMaquinas(insercion))
                    {
                        if (f.textBox11.Text != "")
                        {
                            if (anadirFichero(f.textBox10.Text, f.openFileDialog1))
                            {
                                DialogResult q = MessageBox.Show("Máquina añadida correctamente.\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                                if (q == DialogResult.OK) limpCampMaqA(f);
                            }
                            else
                            {
                                DialogResult q = MessageBox.Show("Ha habido problemas en la inserción. La máquina ha sido creada pero no se pudo subir el archivo adjunto.\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                                if (q == DialogResult.OK) limpCampMaqA(f);
                            }

                        }
                        else
                        {
                            DialogResult q = MessageBox.Show("Máquina añadida correctamente.\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                            if (q == DialogResult.OK) limpCampMaqA(f);
                        }
                    }
                    else
                    {
                        DialogResult q = MessageBox.Show("Error al añadir máquina. Compruebe que la máquina no exista ya y que se cumplen todas las condiciones." +
                            "\n¿Desea limpiar los campos?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) limpCampMaqA(f);
                    }
                }
            }
            else MessageBox.Show("No puede agregarse la máquina, compruebe los campos obligatorios y el cumplimiento de las condiciones.");
        }
        
        //GENERA LA SENTENCIA SQL PARA INSERTAR EL CLIENTE
        private bool insertMaquinas(string insercion)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
            bool resultado = false;
            try
            {
                Console.WriteLine("desde inser " + insercion);
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
            string tqw = buscarNumEstado(f.comboBox1.Text);
            cadenaCamposI += f.textBox10.Text;
            cadenaCamposI += "', '" + f.comboBox2.Text;
            cadenaCamposI += "', '" + f.dateTimePicker2.Text;
            cadenaCamposI += "', '" + f.textBox8.Text;
            cadenaCamposI += "', '" + tqw;
            //cadenaCamposI += "', '" + f.comboBox1.Text;
            cadenaCamposI += "', '" + f.richTextBox1.Text;

            cadenaCamposI += "', '";
            if (f.checkBox10.Checked) cadenaCamposI += "012";
            if (f.checkBox8.Checked) cadenaCamposI += "3";
            if (f.checkBox7.Checked) cadenaCamposI += "4";
            if (f.checkBox5.Checked) cadenaCamposI += "5";
            if (f.checkBox4.Checked) cadenaCamposI += "6";
            if (f.checkBox3.Checked) cadenaCamposI += "7";
            if (f.checkBox2.Checked) cadenaCamposI += "8";
            if (f.checkBox1.Checked) cadenaCamposI += "9";

            cadenaCamposI += "', '" + f.textBox7.Text;
            if (f.textBox7.Text == "")
            {
                cadenaCamposI += "', null";
                cadenaCamposI += ", '" + f.richTextBox2.Text;
            }
            else
            {
                cadenaCamposI += "', '" + invierteFecha(f.dateTimePicker5.Text);
                cadenaCamposI += "', '" + f.richTextBox2.Text;
            }
            
            if (f.comboBox7.Text == "")
            {
                cadenaCamposI += "', null, null, null, '";
                
            }
            else
            {
                cadenaCamposI += "', '" + f.comboBox7.Text;
                cadenaCamposI += "', '" + f.comboBox3.Text;
                cadenaCamposI += "', '" + invierteFecha(f.dateTimePicker1.Text);
                string targetPath = "";
                if (f.textBox10.Text != "")
                {
                    @targetPath = System.IO.Directory.GetCurrentDirectory() + @"\maquinas\" + f.textBox10.Text + Path.GetExtension(f.openFileDialog1.FileName);
                    targetPath = targetPath.Replace("\\", "\\\\");
                    @cadenaCamposI += "', '" + @targetPath;
                }
                else cadenaCamposI += "', '";
            }
            
            cadenaCamposI += "')";
            
            return cadenaCamposI;
        }

        private string buscarNumEstado(string text)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
            string resultado = "";
            try
            {
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = "select id from estadosmaquina where Nombre = '" + text + "'";
                MySqlDataReader leer = comandos.ExecuteReader();

                if (leer.HasRows)
                {
                    leer.Read();
                    resultado = leer["id"].ToString();
                }
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

        private string invierteFecha(string text)
        {
            string[] p = text.Split('/');
            string resultado = null;
            for(int x = p.Length; x>0; x--)
            {
                if (x - 1 == 0) resultado += p[x - 1];
                else resultado += p[x - 1] + "-";
            }
            return resultado;
        }

        //Limpia los campos de máquina añadir
        public void limpCampMaqA(Form1 f)
        {
            f.dateTimePicker5.Text = null;
            f.dateTimePicker2.Text = null;
            f.dateTimePicker1.Text = null;
            f.checkBox10.Checked = false;
            f.checkBox8.Checked = false;
            f.checkBox5.Checked = false;
            f.checkBox4.Checked = false;
            f.checkBox7.Checked = false;
            f.checkBox3.Checked = false;
            f.checkBox2.Checked = false;
            f.checkBox1.Checked = false;
            f.richTextBox1.Text = null;
            f.richTextBox2.Text = null;
            f.comboBox8.Text = null;
            f.comboBox1.Text = null;
            f.comboBox2.Text = null;
            f.textBox11.Text = null;
            f.textBox10.Text = null;
            f.textBox8.Text = null;
            f.textBox7.Text = null;
            actualizarTextBoxVentaC("", "", f.comboBox7);
            actualizarTextBoxVentaN("", "", f.comboBox3);
            f.comboBox3.SelectedItem = null;
            f.comboBox7.SelectedItem = null;

        }

        //Hace la comprobación de que los campos obligatorios correspondientes a MÁQUINA-AÑADIR estén completos
        public bool comprobarCamposMaq(Form1 f)
        {
            bool error = false;

            if (f.comboBox8.Text == null || f.comboBox8.Text == "") error = true;
            if (f.comboBox2.Text == null || f.comboBox2.Text == "") error = true;
            if (f.dateTimePicker2.Text == null || f.dateTimePicker2.Text == "") error = true;
            if (f.textBox10.Text == null || f.textBox10.Text == "") error = true;
            if (f.comboBox1.Text == null || f.comboBox1.Text == "") error = true;

            if (error != true)
            {
               
                //Comprobar que el CP cumple los requisitos
                if (!error) error = comprobarFamilia(f.comboBox8.Text);
                if (!error) error = comprobarModelo(f.comboBox2.Text, f.comboBox8.Text);
                if (!error) error = comprobarEstadMa(f.comboBox1.Text);
                if (!error) error = comprobarNumero(f.textBox10.Text);

                //Comprueba longitud de campos
                if (!error) if (f.textBox8.TextLength > 45) error = true;
                if (!error) if (f.textBox7.TextLength > 45) error = true;
                if (!error) if (f.richTextBox1.TextLength > 700) error = true;
                if (!error) if (f.richTextBox2.TextLength > 700) error = true;
                if (!error) error = comprobarDatosVenta(f.comboBox7.Text, f.comboBox3.Text);
                if (!error) if ((f.comboBox7.Text == "" && f.comboBox3.Text != "") || (f.comboBox7.Text != "" && f.comboBox3.Text == "")) error = true;

            }
            return error;
        }
        //Comprueba el valor del número de la máquina -------------TODO Comprobar si funciona
        public bool comprobarDatosVenta(string f, string f2)
        {
            if ((f == null || f == "") && (f2 == null || f2 == "")) { return false; }
            else
            {
                MySqlConnection con = new MySqlConnection();
                string conexion;
                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;
                    comandos.CommandText = "select count(*) as contado from cliente where Codigo = '" + f + "' and NombreEmpresa = '" + f2 +"';";
                    MySqlDataReader leer = comandos.ExecuteReader();

                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (Int32.Parse(leer["contado"].ToString()) > 0)
                            {
                                return false;
                            }
                            else return true;
                        }
                    }
                    return true;
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
                return false;
            }
        }
        //Comprueba el valor del número de la máquina
        public bool comprobarNumero(string f)
        {
            if (f != null || f != "")
            {
                char[] numArray = f.ToCharArray();
                if (numArray.Length == 4)
                {
                    for (int i = 0; i < 4; i++) if (numArray[i] < 48 || numArray[i] > 57) return true;
                }
                else return true;
            }
            return existeNumero(f);
        }
        //Comprueba el valor de la familia, estado y modelo
        public bool comprobarFamilia(string f2)
        {
            if (f2 == null || f2 == "") { return true; }
            else
            {
                MySqlConnection con = new MySqlConnection();
                string conexion;
                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;

                    comandos.CommandText = "select count(*) as contado from familia where Nombre = '" + f2 + "';";

                    MySqlDataReader leer = comandos.ExecuteReader();

                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (Int32.Parse(leer["contado"].ToString()) > 0)
                            {
                                return false;
                            }
                            else return true;
                        }
                    }
                    return true;
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
                return false;
            }

        }
        public bool comprobarModelo(string f2, string f3)
        {
            if (f2 == null || f2 == "") { return true; }
            else
            {
                MySqlConnection con = new MySqlConnection();
                string conexion;
                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;

                    comandos.CommandText = "select count(*) as contado from modelo where Nombre = '" + f2 + "' and Familia_Nombre = '" + f3 + "';";
                    MySqlDataReader leer = comandos.ExecuteReader();

                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (Int32.Parse(leer["contado"].ToString()) > 0)
                            {
                                return false;
                            }
                            else return true;
                        }
                    }
                    return true;
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
                return false;
            }

        }
        public bool comprobarEstadMa(string f2)
        {
            if (f2 == null || f2 == "") { return true; }
            else
            {
                MySqlConnection con = new MySqlConnection();
                string conexion;
                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;

                    comandos.CommandText = "select count(*) as contado from estadosmaquina where Nombre = '" + f2 + "';";
                    MySqlDataReader leer = comandos.ExecuteReader();

                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (Int32.Parse(leer["contado"].ToString()) > 0)
                            {
                                return false;
                            }
                            else return true;
                        }
                    }
                    return true;
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
                return false;
            }

        }

        //Incorpora el fichero
        private bool anadirFichero(string ft, OpenFileDialog fo)
        {
            //fo.InitialDirectory = "";
            //fo.RestoreDirectory = true;

            try
            {
                string fileName = ft + Path.GetExtension(fo.FileName);
                string sourcePath = fo.FileName;
                string targetPath = System.IO.Directory.GetCurrentDirectory() + @"\maquinas";
                string destFile = System.IO.Path.Combine(targetPath, fileName);

                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }
                System.IO.File.Copy(sourcePath, destFile, true);
                return true;
            }
            catch{ return false; }
        }
        public void abreDialogo(Form1 f)
        {
            f.openFileDialog1.InitialDirectory = "";
            f.openFileDialog1.RestoreDirectory = true;

            if (f.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                f.textBox11.Text = f.openFileDialog1.FileName;
            }
        }

        internal void rellenarComboBoxFamiliaA(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from familia";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = table;
                f.comboBox8.DisplayMember = "Nombre";
                //f.comboBox8.ValueMember = "ID";
                f.comboBox8.DataSource = bindingSource1;
                f.comboBox8.SelectedItem = null;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void rellenarComboBoxModeloA(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from modelo where Familia_Nombre = '"+ f.comboBox8.Text +"';";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);

                BindingSource bindingSource3 = new BindingSource();
                bindingSource3.DataSource = table1;
                f.comboBox2.DisplayMember = "Nombre";
                //f.comboBox8.ValueMember = "ID";
                f.comboBox2.DataSource = bindingSource3;
                f.comboBox2.SelectedItem = null;
                
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void rellenarComboBoxFamiliaB(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from familia";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource2 = new BindingSource();
                bindingSource2.DataSource = table;
                f.comboBox4.DisplayMember = "Nombre";
                f.comboBox4.DataSource = bindingSource2;
                f.comboBox4.SelectedItem = null;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void rellenarComboBoxModeloB(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from modelo where Familia_Nombre = '" + f.comboBox4.Text + "';";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);

                BindingSource bindingSource4 = new BindingSource();
                bindingSource4.DataSource = table1;
                f.comboBox5.DisplayMember = "Nombre";
                //f.comboBox5.ValueMember = "ID";
                f.comboBox5.DataSource = bindingSource4;
                f.comboBox5.SelectedItem = null;

            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void rellenarComboBoxEstadoB(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from estadosmaquina";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource5 = new BindingSource();
                bindingSource5.DataSource = table;
                f.comboBox6.DisplayMember = "Nombre";
                //f.comboBox8.ValueMember = "ID";
                f.comboBox6.DataSource = bindingSource5;
                f.comboBox6.SelectedItem = null;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void rellenarComboBoxEstadoA(Form1 f)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from estadosmaquina";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource6 = new BindingSource();
                bindingSource6.DataSource = table;
                f.comboBox1.DisplayMember = "Nombre";
                //f.comboBox1.ValueMember = "ID";
                f.comboBox1.DataSource = bindingSource6;
                f.comboBox1.SelectedItem = null;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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

        internal void actualizarTextBoxVentaN(string valor, string valor2, ComboBox x)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select distinct NombreEmpresa from cliente where Codigo like '%" + valor +"%' and NombreEmpresa like '%" + valor2 + "%' limit 0, 10;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = table;
                x.DisplayMember = "NombreEmpresa";
                x.DataSource = bindingSource1;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        internal void actualizarTextBoxVentaC(string valor, string valor2, ComboBox x)
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select distinct Codigo from cliente where NombreEmpresa like '%" + valor + "%' and Codigo like '%" + valor2 + "%' limit 0, 10;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table = new DataTable();
                adapter.Fill(table);

                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = table;
                x.DisplayMember = "Codigo";
                x.DataSource = bindingSource1;
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error:\n\n" + Convert.ToString(error));
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
        public bool existeNumero(string f2)
        {
            if (f2 == null || f2 == "") { return true; }
            else
            {
                MySqlConnection con = new MySqlConnection();
                string conexion;
                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;

                    comandos.CommandText = "select count(*) as contado from maquina where Numero = '" + f2 + "';";
                    MySqlDataReader leer = comandos.ExecuteReader();

                    if (leer.HasRows)
                    {
                        while (leer.Read())
                        {
                            if (Int32.Parse(leer["contado"].ToString()) == 0)
                            {
                                return false;
                            }
                            else return true;
                        }
                    }
                    return false;
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
                return false;
            }

        }
    }
}
