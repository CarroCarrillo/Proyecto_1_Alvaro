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

        public PerfilMaquinas()
        {
            InitializeComponent();
        }

        public PerfilMaquinas(string x)
        {
            InitializeComponent();
            this.a = x;
            devolverMaquina();
            boton_modificar.BackColor = Color.SkyBlue;
            boton_eliminar.BackColor = Color.Salmon;
            actualizarImagenesTick();
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
                        comboBox2.Text = leer["Modelo_Nombre"].ToString();
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

        private void actualizarImagenesTick()
        {
            MessageBox.Show("implementar");
        }

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
                    targetPath = System.IO.Directory.GetCurrentDirectory() + @"\maquinas\" + f.textBox10.Text + Path.GetExtension(f.openFileDialog1.FileName);
                    cadenaCamposI += "', '" + targetPath;
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
            for (int x = p.Length; x > 0; x--)
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
                if (!error) error = comprobarFamilia("familia", f.comboBox8.Text);
                if (!error) error = comprobarFamilia("modelo", f.comboBox2.Text);
                if (!error) error = comprobarFamilia("estadosmaquina", f.comboBox1.Text);
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
            return false;
        }
        //Comprueba el valor de la familia, estado y modelo
        public bool comprobarFamilia(string f, string f2)
        {
            if (f == null || f == "") { return true; }
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
                    comandos.CommandText = "select count(*) as contado from " + f + " where Nombre = '" + f2 + "';";
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
            catch { return false; }
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

                string query = "select * from modelo where Familia_Nombre = '" + f.comboBox8.Text + "';";

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

    }
}
