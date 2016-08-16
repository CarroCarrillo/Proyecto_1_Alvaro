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
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ProyectoA
{
    public partial class EdicMacFM : Form
    {

        private string conexion;
        private MySqlConnection con = new MySqlConnection();
        
        public EdicMacFM()
        {
            InitializeComponent();
            devolverConsultaFamilia();
            devolverConsulModelPrimPos(null);
        }

        private void EdicMacFM_Load(object sender, EventArgs e)
        {
            textBox2.TextChanged += TextBox2_TextChanged;
            textBox1.TextChanged += TextBox1_TextChanged;
        }

        //UNA VEZ SE HAN CARGADO LAS FAMILIAS, carga por defecto los modelos de la familia que esté en primera posición o la pasada por parámetro
        private void devolverConsulModelPrimPos(string famil)
        {
            try
            {
                if (famil == null)
                {
                    famil = dataGridView1.Rows[0].Cells[0].Value.ToString();
                }
            }
            catch { }
            try
            {
                dataGridView2.RowCount = 1;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                int cont = 0;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = "select * from modelo where Familia_Nombre = '" + famil + "'";
                MySqlDataReader leer = comandos.ExecuteReader();


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[cont].Cells[0].Value = leer["Nombre"].ToString();
                        cont++;
                    }
                }
            }
            catch (MySqlException error)
            {
                MessageBox.Show("Error: \n\n" + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(err));
                }
            }
        }
    
        //CARGA LAS FAMILIAS
        private void devolverConsultaFamilia()
        {
            try
            {
                dataGridView1.RowCount = 1;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                int cont = 0;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = "select * from familia";
                MySqlDataReader leer = comandos.ExecuteReader();


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[cont].Cells[0].Value = leer["Nombre"].ToString();
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
      
        //Carga los modelos de las familias
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string famil = "";
            try
            {
                famil = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch { }
            try
            {
                dataGridView2.RowCount = 1;
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                int cont = 0;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = "select * from modelo where Familia_Nombre = '" + famil + "'";
                MySqlDataReader leer = comandos.ExecuteReader();


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[cont].Cells[0].Value = leer["Nombre"].ToString();
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
                catch (Exception err)
                {
                    MessageBox.Show("Error producido al cerrar sesión: \n\n" + Convert.ToString(err));
                }
            }
        }

        //BOTÓN PARA AÑADIR UNA FAMILIA
        private void button1_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposFam())
            {
                string insercion;
                insercion = "insert familia values ('" + textBox2.Text + "')";

                if (insertaFamiliaModelo(insercion))
                {
                    //MessageBox.Show("Familia agregada correctamente.");
                    devolverConsultaFamilia();
                    dataGridView2.RowCount = 1;
                    textBox2.Text = null;
                }
            }
            else MessageBox.Show("Comprobar que se ha escrito el nombre de la familia a insertar o cumpla las características permitidas.");
        }

        //BOTÓN PARA AÑADIR UN MODELO
        private void button2_Click(object sender, EventArgs e)
        {
            string valorFam = null;
            try
            {
                valorFam = dataGridView1.CurrentCell.Value.ToString();
            }
            catch { }
            if (valorFam != null)
            {
                if (!comprobarCamposMod())
                {
                    string insercion;
                    insercion = "insert modelo values ('" + textBox1.Text + "', '" + valorFam + "')";

                    if (insertaFamiliaModelo(insercion))
                    {
                        //MessageBox.Show("Modelo agregado correctamente.");
                        devolverConsulModelPrimPos(valorFam);
                        textBox1.Text = null;
                    }
                }
                else MessageBox.Show("Comprobar que se ha escrito el nombre del modelo a insertar o cumpla las características permitidas.");
            }
            else MessageBox.Show("No se ha seleccionado ninguna familia.");
        }

        //Ejecuta la select insert pasada por parámetro
        private bool insertaFamiliaModelo(string insercion)
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
                MessageBox.Show("Error producido en la inserción. Compruebe que no exista ya:\n\n" + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Error producido al cerrar sesión:\n\n" + Convert.ToString(erro));
                }
            }
            return resultado;
        }

        //Comprueba campos familia
        private bool comprobarCamposFam()
        { 
            if (textBox2.Text == null || textBox2.Text == "") return true;
            else
            {
                if (textBox2.TextLength > 45) return true;
            }
            return false;
        }

        //Comprueba campos modelo
        private bool comprobarCamposMod()
        { 
            if (textBox1.Text == null || textBox1.Text == "") return true;
            else
            {
                char[] modeloN; 
                modeloN = textBox1.Text.ToCharArray();
                if (modeloN.Length == 4)
                {
                    for (int i = 0; i < 4; i++) if ((modeloN[i] < 48 || modeloN[i] > 57) && (modeloN[i] < 65 || modeloN[i] > 90) && (modeloN[i] < 97 || modeloN[i] > 122)) return true;
                }
                else return true; 
            } 
            return false;
        }
        
        //Muestra los errores si está mal escrito
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0)
            {
                if (!comprobarCamposMod())
                {
                    toolTip1.SetToolTip(pictureBox1, "");
                    pictureBox1.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox1, "El modelo se compone por cuatro caracteres alfanuméricos.");
                    pictureBox1.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, "");
                pictureBox1.Image = null;
            }
        }

        //Muestra los errores si está mal escrito
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength != 0)
            {
                if (!comprobarCamposFam())
                {
                    toolTip1.SetToolTip(pictureBox3, "");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox3, "Se ha superado la cantidad de caracteres permitidos: " + textBox2.TextLength + "/45");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox3, "");
                pictureBox3.Image = null;
            }
        }

        //BOTÓN PARA ELIMINAR UNA FAMILIA
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Value != null)
            {
                DialogResult k = MessageBox.Show("Borrar la familia implicará eliminar también todos sus modelos. Las máquinas no serán eliminadas, pero dejarán de tener un modelo.\n" +
                "¿Está seguro?", "Confirmación", MessageBoxButtons.OKCancel);
                if (k == DialogResult.OK)
                {
                    string sentencia;

                    try
                    {
                        conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                        con.ConnectionString = conexion;
                        con.Open();

                        sentencia = "delete from familia where Nombre = '" + dataGridView1.CurrentCell.Value.ToString() + "';";
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
                    devolverConsultaFamilia();
                    dataGridView2.RowCount = 1;
                }
            }
            else MessageBox.Show("Ha de seleccionar una familia para poder eliminar.");
        }

        //BOTÓN PARA ELIMINAR UN MODELO
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Value != null && dataGridView2.CurrentCell.Value != null)
            {
                DialogResult k = MessageBox.Show("Borrar el modelo no hará que esas máquinas no sean eliminadas, pero dejarán de tener un modelo.\n" +
                    "¿Está seguro?", "Confirmación", MessageBoxButtons.OKCancel);
                if (k == DialogResult.OK)
                {
                    string sentencia;

                    try
                    {
                        conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                        con.ConnectionString = conexion;
                        con.Open();

                        sentencia = "delete from modelo where Familia_Nombre = '" + dataGridView1.CurrentCell.Value.ToString() + "' and Nombre = '" + dataGridView2.CurrentCell.Value.ToString() + "';";
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
                    devolverConsulModelPrimPos(dataGridView1.CurrentCell.Value.ToString());
                }
            }
            else MessageBox.Show("Ha de seleccionar una familia y un modelo para poder eliminar.");
        }

        //BOTÓN PARA MODIFICAR LA FAMILIA
        private void button5_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposFam() && textBox2.Text != null && dataGridView1.CurrentCell.Value != null)
            {
                string modificar;

                modificar = "update familia set Nombre = '" + textBox2.Text + "' where Nombre = '" + dataGridView1.CurrentCell.Value.ToString() + "';";

                if (modificarFamMod(modificar))
                {
                    devolverConsultaFamilia();
                    dataGridView2.RowCount = 1;
                    textBox2.Text = null;
                    MessageBox.Show("Familia modificada correctamente.");
                }
            }
            else MessageBox.Show("Para modificar una familia selecciónela y escriba en el cuadro de texto su reemplazo. Compruebe que el nuevo nombre cumple con los requisitos.");
        }

        private bool modificarFamMod(string modificar)
        {
            MySqlConnection con = new MySqlConnection();
            bool resultado = false;
            try
            {
                string conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = modificar;
                comandos.ExecuteNonQuery();

                resultado = true;
            }
            catch (Exception error)
            {
                MessageBox.Show("No se ha podido modificar. Compruebe que no exista ya.\n\n" + Convert.ToString(error));
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

        //BOTÓN MODIFICAR MODELO
        private void button6_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposMod() && textBox1.Text != null && dataGridView1.CurrentCell.Value != null && dataGridView2.CurrentCell.Value != null)
            {
                string modificar;

                modificar = "update modelo set Nombre = '" + textBox1.Text + "' where Nombre = '" + dataGridView2.CurrentCell.Value.ToString() + "' and Familia_Nombre = '"+ dataGridView1.CurrentCell.Value.ToString() + "';";

                if (modificarFamMod(modificar))
                {
                    devolverConsulModelPrimPos(dataGridView1.CurrentCell.Value.ToString());
                    textBox1.Text = null;
                    MessageBox.Show("Modelo modificado correctamente.");
                }
            }
            else MessageBox.Show("Para modificar un modelo seleccione una familia, un modelo y escriba en el cuadro de texto correspondiente su reemplazo. Compruebe que el nuevo nombre cumple con los requisitos.");
        }
    }
    
}
