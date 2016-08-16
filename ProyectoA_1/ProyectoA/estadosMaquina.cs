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
    public partial class estadoMaquina : Form
    {
        public estadoMaquina()
        {
            InitializeComponent();
            devolverEstadosMa();
        }

        private void estadoMaquina_Load(object sender, EventArgs e)
        {
            textBox2.TextChanged += TextBox2_TextChanged;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength != 0)
            {
                if (!comprobarCamposEst())
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

        private void devolverEstadosMa()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
                dataGridView1.RowCount = 1;
                string conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();

                int cont = 0;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = "select * from estadosmaquina";
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposEst())
            {
                string insercion;
                insercion = "insert estadosmaquina (Nombre) values ('" + textBox2.Text + "')";

                if (insertaEstadoMa(insercion))
                {
                    //MessageBox.Show("Familia agregada correctamente.");
                    devolverEstadosMa();
                    devolverEstadosMa();
                    textBox2.Text = null;
                }
            }
            else MessageBox.Show("Comprobar que se ha escrito el nombre del estado.");
        }

        private bool comprobarCamposEst()
        {
            if (textBox2.Text == null || textBox2.Text == "") return true;
            else
            {
                if (textBox2.TextLength > 45) return true;
            }
            return false;
        }

        private bool insertaEstadoMa(string insercion)
        {
            bool resultado = false;
            MySqlConnection con = new MySqlConnection();

            try
            {
                string conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
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

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            if (dataGridView1.CurrentCell.Value != null)
            {
                DialogResult k = MessageBox.Show("Las máquinas que tengan el estado seleccionado lo perderán.\n" +
                "¿Está seguro?", "Confirmación", MessageBoxButtons.OKCancel);
                if (k == DialogResult.OK)
                {
                    string sentencia;

                    try
                    {
                        string conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                        con.ConnectionString = conexion;
                        con.Open();

                        sentencia = "delete from estadosmaquina where Nombre = '" + dataGridView1.CurrentCell.Value.ToString() + "';";
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
                    devolverEstadosMa();
                }
            }
            else MessageBox.Show("Ha de seleccionar una familia para poder eliminar.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposEst() && textBox2.Text != null && dataGridView1.CurrentCell.Value != null)
            {
                string modificar;

                modificar = "update estadosmaquina set Nombre = '" + textBox2.Text + "' where Nombre = '" + dataGridView1.CurrentCell.Value.ToString() + "';";

                if (modifEstado(modificar))
                {
                    devolverEstadosMa();
                    textBox2.Text = null;
                    MessageBox.Show("Estado modificado correctamente.");
                }
            }
            else MessageBox.Show("Para modificar un estado selecciónelo y escriba en el cuadro de texto su reemplazo. Compruebe que el nuevo nombre cumple con los requisitos.");
        }

        private bool modifEstado(string modificar)
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
                MessageBox.Show("No se ha podido modificar. Compruebe que no existe ya.\n\n" + Convert.ToString(error));
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
    }
}
