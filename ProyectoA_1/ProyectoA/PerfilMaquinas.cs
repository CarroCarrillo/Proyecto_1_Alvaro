using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoA
{
    public partial class PerfilMaquinas : Form
    {
        private string a;
        private string ModelO = "";

        public PerfilMaquinas()
        {
            InitializeComponent();
        }

        public PerfilMaquinas(string x)
        {
            InitializeComponent();
            this.a = x;
            boton_modificar.BackColor = Color.SkyBlue;
            boton_eliminar.BackColor = Color.Salmon;
        }

        private void PerfilMaquinas_Load(object sender, EventArgs e)
        {
            rellenarComboBoxEstadoA();
            rellenarComboBoxFamiliaA();
            rellenarComboBoxModeloA();
            devolverMaquina();
            actualizarImagenesTick();
            comprobarCambiosTextBox();
            
        }

        private void devolverMaquina()
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
            try
            {
                string consulta;
                consulta = "select m.*, mo.Familia_Nombre, e.Nombre  from maquina m, modelo mo, estadosmaquina e where "+
                    "m.Numero = '" + a + "' and mo.Nombre = m.Modelo_Nombre and id = m.id_maquina;";

                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = consulta;
                MySqlDataReader leer = comandos.ExecuteReader();
                char[] cad;


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        comboBox8.Text = leer["Familia_Nombre"].ToString();
                        ModelO = comboBox2.Text = leer["Modelo_Nombre"].ToString();
                        dateTimePicker2.Text = "01/01/" + leer["Anyo"].ToString();
                        textBox10.Text = leer["Numero"].ToString();
                        textBox8.Text = leer["Descripcion"].ToString();
                        comboBox1.Text = leer["Nombre"].ToString();
                        textBox7.Text = leer["DatosCompra"].ToString();
                        dateTimePicker5.Text = leer["FechaAdquisicion"].ToString();
                        comboBox7.Text = leer["cliente_Codigo"].ToString();
                        comboBox3.Text = leer["cliente_NombreEmpresa"].ToString();
                        dateTimePicker1.Text = leer["fechaVenta"].ToString();
                        richTextBox1.Text = leer["Observaciones"].ToString();
                        richTextBox2.Text = leer["ObservacionesVenta"].ToString();
                        if (leer["Actualizado"].ToString() != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for (int x = 0; x < cad.Length; x++)
                            {
                                if (cad[x] == '0') checkBox10.Checked = true;
                                else if (cad[x] == '3') checkBox8.Checked = true;
                                else if (cad[x] == '4') checkBox7.Checked = true;
                                else if (cad[x] == '5') checkBox5.Checked = true;
                                else if (cad[x] == '6') checkBox4.Checked = true;
                                else if (cad[x] == '7') checkBox3.Checked = true;
                                else if (cad[x] == '8') checkBox2.Checked = true;
                                else if (cad[x] == '9') checkBox1.Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha encontrado dicho cliente.");
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

        
        
        //Rellena los datos para realizar posteriormente el insert de cliente
        private string agregarCamposInsertMaquinas()
        {
            string cadenaCamposI = "Numero = '";
            //campos normales TABLA CLIENTE
            string tqw = buscarNumEstado(comboBox1.Text);
            cadenaCamposI += textBox10.Text;
            cadenaCamposI += "', Modelo_Nombre = '" + comboBox2.Text;
            cadenaCamposI += "', Anyo = '" + dateTimePicker2.Text;
            cadenaCamposI += "', Descripcion = '" + textBox8.Text;
            cadenaCamposI += "', id_maquina = '" + tqw;
            //cadenaCamposI += "', '" + f.comboBox1.Text;
            cadenaCamposI += "', Observaciones = '" + richTextBox1.Text;

            cadenaCamposI += "', Actualizado = '";
            if (checkBox10.Checked) cadenaCamposI += "012";
            if (checkBox8.Checked) cadenaCamposI += "3";
            if (checkBox7.Checked) cadenaCamposI += "4";
            if (checkBox5.Checked) cadenaCamposI += "5";
            if (checkBox4.Checked) cadenaCamposI += "6";
            if (checkBox3.Checked) cadenaCamposI += "7";
            if (checkBox2.Checked) cadenaCamposI += "8";
            if (checkBox1.Checked) cadenaCamposI += "9";

            cadenaCamposI += "', DatosCompra = '" + textBox7.Text;
            if (textBox7.Text == "")
            {
                cadenaCamposI += "', FechaAdquisicion = null";
                cadenaCamposI += ", ObservacionesVenta = '" + richTextBox2.Text;
            }
            else
            {
                cadenaCamposI += "', FechaAdquisicion = '" + invierteFecha(dateTimePicker5.Text);
                cadenaCamposI += "', ObservacionesVenta = '" + richTextBox2.Text;
            }

            if (comboBox7.Text == "")
            {
                cadenaCamposI += "', cliente_Codigo = null, cliente_NombreEmpresa = null, fechaVenta = null, fichero = '";

            }
            else
            {
                cadenaCamposI += "', cliente_Codigo = '" + comboBox7.Text;
                cadenaCamposI += "', cliente_NombreEmpresa = '" + comboBox3.Text;
                cadenaCamposI += "', fechaVenta = '" + invierteFecha(dateTimePicker1.Text);
                string targetPath = "";
                if (textBox11.Text != "")
                {
                    //targetPath = System.IO.Directory.GetCurrentDirectory() + @"\maquinas\" + textBox10.Text + Path.GetExtension(openFileDialog1.FileName);
                    cadenaCamposI += "', fichero = '" + targetPath;
                }
                else cadenaCamposI += "', fichero = '";
            }

            cadenaCamposI += "' ";

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
            for (int x = p.Length; x > 0; x--)
            {
                if (x - 1 == 0) resultado += p[x - 1];
                else resultado += p[x - 1] + "-";
            }
            return resultado;
        }
        
        //Hace la comprobación de que los campos obligatorios correspondientes a MÁQUINA-AÑADIR estén completos
        public bool comprobarCamposMaq()
        {
            bool error = false;

            if (comboBox8.Text == null || comboBox8.Text == "") error = true;
            if (comboBox2.Text == null || comboBox2.Text == "") error = true;
            if (dateTimePicker2.Text == null || dateTimePicker2.Text == "") error = true;
            if (textBox10.Text == null || textBox10.Text == "") error = true;
            if (comboBox1.Text == null || comboBox1.Text == "") error = true;

            if (error != true)
            {

                //Comprobar que el CP cumple los requisitos
                if (!error) error = comprobarFamilia(comboBox8.Text);
                if (!error) error = comprobarModelo(comboBox2.Text);
                if (!error) error = comprobarEstadMa(comboBox1.Text);
                if (!error) error = comprobarNumero(textBox10.Text);

                //Comprueba longitud de campos
                if (!error) if (textBox8.TextLength > 45) error = true;
                if (!error) if (textBox7.TextLength > 45) error = true;
                if (!error) if (richTextBox1.TextLength > 700) error = true;
                if (!error) if (richTextBox2.TextLength > 700) error = true;
                if (!error) error = comprobarDatosVenta(comboBox7.Text, comboBox3.Text);
                if (!error) if ((comboBox7.Text == "" && comboBox3.Text != "") || (comboBox7.Text != "" && comboBox3.Text == "")) error = true;
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
                    comandos.CommandText = "select count(*) as contado from cliente where Codigo = '" + f + "' and NombreEmpresa = '" + f2 + "';";
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
        public bool comprobarModelo(string f2)
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

                    comandos.CommandText = "select count(*) as contado from modelo where Nombre = '" + f2 + "' and Familia_Nombre = '" + comboBox8.Text + "';";
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
        private bool anadirFichero(OpenFileDialog fo)
        {
            //fo.InitialDirectory = "";
            //fo.RestoreDirectory = true;

            try
            {
                string fileName = a + Path.GetExtension(fo.FileName);
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
            catch { return false; }
        }
        public void abreDialogo()
        {
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox11.Text = openFileDialog1.FileName;
            }
        }

        internal void rellenarComboBoxFamiliaA()
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
                comboBox8.DisplayMember = "Nombre";
                //f.comboBox8.ValueMember = "ID";
                comboBox8.DataSource = bindingSource1;
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
        internal void rellenarComboBoxModeloA()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                string conexion;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                string query = "select * from modelo where Familia_Nombre = '" + comboBox8.Text + "';";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);

                BindingSource bindingSource3 = new BindingSource();
                bindingSource3.DataSource = table1;
                comboBox2.DisplayMember = "Nombre";
                //f.comboBox8.ValueMember = "ID";
                comboBox2.DataSource = bindingSource3;

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
        internal void rellenarComboBoxEstadoA()
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
                comboBox1.DisplayMember = "Nombre";
                //f.comboBox1.ValueMember = "ID";
                comboBox1.DataSource = bindingSource6;
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

                string query = "select distinct NombreEmpresa from cliente where Codigo like '%" + valor + "%' and NombreEmpresa like '%" + valor2 + "%' limit 0, 10;";

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

        private void boton_eliminar_Click(object sender, EventArgs e)
        {
            DialogResult k = MessageBox.Show("Borrar la máquina implicará eliminar también cualquier contrato existente que la incluyera.\n" +
                "¿Está seguro?", "Confirmación", MessageBoxButtons.OKCancel);
            if (k == DialogResult.OK)
            {
                string sentencia;
                string conexion;
                MySqlConnection con = new MySqlConnection();

                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    sentencia = "delete from maquina where Numero = '" + a + "';";
                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;
                    comandos.CommandText = sentencia;
                    comandos.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error en el proceso de eliminación: \n\n" + Convert.ToString(error));
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show("Error en el proceso de eliminación: \n\n" + Convert.ToString(et));
                    }
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rellenarComboBoxEstadoA();
            rellenarComboBoxFamiliaA();
            rellenarComboBoxModeloA();
            devolverMaquina();
            actualizarImagenesTick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void boton_modificar_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposMaq())
            {
                string modificar;

                DialogResult b = MessageBox.Show("Confirmar modificación máquina.", "Confirmación", MessageBoxButtons.OKCancel);
                if (b == DialogResult.OK)
                {
                    modificar = "update maquina set ";

                    modificar += agregarCamposInsertMaquinas();

                    modificar += " where Numero = '" + a + "'";
                    Console.WriteLine(modificar);
                    if (insertMaquinas(modificar))
                    {
                        if (textBox11.Text != "")
                        {
                            if (anadirFichero(openFileDialog1))
                            {
                                DialogResult q = MessageBox.Show("Máquina modificada correctamente.\n¿Desea cerrar?", "", MessageBoxButtons.OKCancel);
                                if (q == DialogResult.OK) this.Close();
                            }
                            else
                            {
                                DialogResult q = MessageBox.Show("Ha habido problemas en la modificación. El archivo adjunto no ha podido copiarse.\n¿Desea cerrar?", "", MessageBoxButtons.OKCancel);
                                if (q == DialogResult.OK) this.Close();
                            }

                        }
                        else
                        {
                            DialogResult q = MessageBox.Show("Máquina modificada correctamente.\n¿Desea cerrar?", "", MessageBoxButtons.OKCancel);
                            if (q == DialogResult.OK) this.Close();
                        }
                    }
                    else
                    {
                        DialogResult q = MessageBox.Show("Error al modificar la máquina.\n¿Desea cerrar?", "", MessageBoxButtons.OKCancel);
                        if (q == DialogResult.OK) this.Close();
                    }
                }
            }
            else MessageBox.Show("No puede modificarse la máquina, compruebe los campos obligatorios y el cumplimiento de las condiciones.\nSi se modificó el número de la máquina, asegurarse de que éste no estuviera ya en uso.");
        }

        private bool insertMaquinas(string insercion)
        {
            MySqlConnection con = new MySqlConnection();
            string conexion;
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

        
        private void comprobarCambiosTextBox()
        {
            comboBox8.TextChanged += ComboBox8_TextChanged;
            comboBox7.DropDown += ComboBox7_DropDown;
            comboBox3.DropDown += ComboBox3_DropDown;
            comboBox2.TextChanged += ComboBox2_TextChanged;
            textBox10.TextChanged += TextBox10_TextChanged;
            comboBox1.TextChanged += ComboBox1_TextChanged;
            textBox7.TextChanged += TextBox7_TextChanged;
            textBox8.TextChanged += TextBox8_TextChanged;
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
            richTextBox2.TextChanged += RichTextBox2_TextChanged;
            comboBox7.TextChanged += ComboBox7_TextChanged;
            comboBox3.TextChanged += ComboBox3_TextChanged;
        }
        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox19, "Se ha superado la cantidad de caracteres permitidos: " + textBox8.TextLength + "/45");
                pictureBox19.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox19, "");
                pictureBox19.Image = null;
            }
        }
        private void ComboBox3_TextChanged(object sender, EventArgs e)
        {
            if (!comprobarDatosVenta(comboBox7.Text, comboBox3.Text))
            {
                toolTip1.SetToolTip(pictureBox15, "");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickPos;
                toolTip1.SetToolTip(pictureBox17, "");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox15, "Para completar los datos de venta es necesario rellenar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickNeg;
                toolTip1.SetToolTip(pictureBox17, "Para completar los datos de venta es necesario rellenar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            if (comboBox3.Text == "" && comboBox7.Text == "")
            {
                pictureBox17.Image = null;
                pictureBox15.Image = null;
            }
        }
        private void ComboBox7_TextChanged(object sender, EventArgs e)
        {
            if (!comprobarDatosVenta(comboBox7.Text, comboBox3.Text))
            {
                toolTip1.SetToolTip(pictureBox15, "");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickPos;
                toolTip1.SetToolTip(pictureBox17, "");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox15, "Necesario completar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickNeg;
                toolTip1.SetToolTip(pictureBox17, "Necesario completar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            if (comboBox3.Text == "" && comboBox7.Text == "")
            {
                pictureBox17.Image = null;
                pictureBox15.Image = null;
            }
        }
        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox2.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox20, "Se ha superado la cantidad de caracteres permitidos: " + richTextBox2.TextLength + "/700");
                pictureBox20.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox20, "");
                pictureBox20.Image = null;
            }
        }
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox21, "Se ha superado la cantidad de caracteres permitidos: " + richTextBox1.TextLength + "/700");
                pictureBox21.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox21, "");
                pictureBox21.Image = null;
            }
        }
        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox22, "Se ha superado la cantidad de caracteres permitidos: " + textBox7.TextLength + "/45");
                pictureBox22.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox22, "");
                pictureBox22.Image = null;
            }
        }
        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comprobarEstadMa(comboBox1.Text))
            {
                toolTip1.SetToolTip(pictureBox16, "Campo obligatorio. Asegúrese de que el estado existe.");
                pictureBox16.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox16, "");
                pictureBox16.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }
        private void TextBox10_TextChanged(object sender, EventArgs e)
        {
            if (comprobarModelo(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }
        private void ComboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comprobarModelo(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
            if (!comprobarFamilia(comboBox8.Text))
            {
                toolTip1.SetToolTip(pictureBox23, "");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox23, "Campo obligatorio. Asegúrese de que la familia existe.");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }
        private void ComboBox3_DropDown(object sender, EventArgs e)
        {
            string a = comboBox7.Text;
            string b = comboBox3.Text;
            actualizarTextBoxVentaN(a, b, comboBox3);
            actualizarTextBoxVentaC(b, a, comboBox7);
            if (a == null || a == "") comboBox7.SelectedItem = null;
        }
        private void ComboBox7_DropDown(object sender, EventArgs e)
        {
            string a = comboBox7.Text;
            string b = comboBox3.Text;
            actualizarTextBoxVentaC(b, a, comboBox7);
            actualizarTextBoxVentaN(a, b, comboBox3);
            if (b == null || b == "") comboBox3.SelectedItem = null;
        }
        private void ComboBox8_TextChanged(object sender, EventArgs e)
        {
            if (!comprobarFamilia(comboBox8.Text))
            {
                toolTip1.SetToolTip(pictureBox23, "");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox23, "Campo obligatorio. Asegúrese de que la familia existe.");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            rellenarComboBoxModeloA();
            if (comprobarModelo(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }
        
        private void actualizarImagenesTick()
        {
            if (textBox8.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox19, "Se ha superado la cantidad de caracteres permitidos: " + textBox8.TextLength + "/45");
                pictureBox19.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox19, "");
                pictureBox19.Image = null;
            }
            if (!comprobarDatosVenta(comboBox7.Text, comboBox3.Text))
            {
                toolTip1.SetToolTip(pictureBox15, "");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickPos;
                toolTip1.SetToolTip(pictureBox17, "");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox15, "Para completar los datos de venta es necesario rellenar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickNeg;
                toolTip1.SetToolTip(pictureBox17, "Para completar los datos de venta es necesario rellenar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            if (comboBox3.Text == "" && comboBox7.Text == "")
            {
                pictureBox17.Image = null;
                pictureBox15.Image = null;
            }
            if (!comprobarDatosVenta(comboBox7.Text, comboBox3.Text))
            {
                toolTip1.SetToolTip(pictureBox15, "");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickPos;
                toolTip1.SetToolTip(pictureBox17, "");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox15, "Necesario completar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox15.Image = ProyectoA.Properties.Resources.tickNeg;
                toolTip1.SetToolTip(pictureBox17, "Necesario completar tanto el código como el nombre de empresa. Comprobar que exista la empresa con dicho código y nombre.");
                pictureBox17.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            if (comboBox3.Text == "" && comboBox7.Text == "")
            {
                pictureBox17.Image = null;
                pictureBox15.Image = null;
            }
            if (richTextBox2.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox20, "Se ha superado la cantidad de caracteres permitidos: " + richTextBox2.TextLength + "/700");
                pictureBox20.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox20, "");
                pictureBox20.Image = null;
            }
            if (richTextBox1.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox21, "Se ha superado la cantidad de caracteres permitidos: " + richTextBox1.TextLength + "/700");
                pictureBox21.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox21, "");
                pictureBox21.Image = null;
            }
            if (textBox7.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox22, "Se ha superado la cantidad de caracteres permitidos: " + textBox7.TextLength + "/45");
                pictureBox22.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox22, "");
                pictureBox22.Image = null;
            }
            if (comprobarEstadMa(comboBox1.Text))
            {
                toolTip1.SetToolTip(pictureBox16, "Campo obligatorio. Asegúrese de que el estado existe.");
                pictureBox16.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox16, "");
                pictureBox16.Image = ProyectoA.Properties.Resources.tickPos;
            }
            if (comprobarFamilia(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
            if (comprobarFamilia(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
            
            if (!comprobarFamilia(comboBox8.Text))
            {
                toolTip1.SetToolTip(pictureBox23, "");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox23, "Campo obligatorio. Asegúrese de que la familia existe.");
                pictureBox23.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            rellenarComboBoxModeloA();
            comboBox2.Text = ModelO;
            if (comprobarModelo(comboBox2.Text) || comprobarNumero(textBox10.Text))
            {
                toolTip1.SetToolTip(pictureBox13, "Campo obligatorio. Asegúrese de que el modelo existe para esa familia y el valor número contenga solo dígitos o no exista ya.");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox13, "");
                pictureBox13.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form EdicM = new EdicMacFM();
            EdicM.Show();
            EdicM.FormClosed += EdicM_FormClosed;
        }
        private void EdicM_FormClosed(object sender, FormClosedEventArgs e)
        {
            rellenarComboBoxFamiliaA();
            rellenarComboBoxModeloA();
            comboBox8.SelectedItem = null;
            comboBox2.SelectedItem = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form estMa = new estadoMaquina();
            estMa.Show();
            estMa.FormClosed += EstMa_FormClosed;
        }
        private void EstMa_FormClosed(object sender, FormClosedEventArgs e)
        {
            rellenarComboBoxEstadoA();
            comboBox1.SelectedItem = null;
        }
    }
}
