using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoA
{
    public partial class PerfilCliente : Form
    {
        private string conexion;
        private string a;
        private string b;
        private MySqlConnection con = new MySqlConnection();

        public PerfilCliente()
        {
            InitializeComponent();
        }

        public PerfilCliente(string x, string z)
        {
            InitializeComponent();
            this.a = x;
            this.b = z;
            devolverCliente(a, b);
            boton_modificar.BackColor = Color.SkyBlue;
            boton_eliminar.BackColor = Color.Salmon;
        }

        private void devolverCliente(string a, string b)
        {
            try
            {
                string consulta;
                consulta = "select c.*, group_concat(t.NumeroTlf separator ', ') as ntlf from cliente c left join telefono t on c.Codigo = t.Cliente_Codigo and " +
                             "c.NombreEmpresa = t.Cliente_NombreEmpresa where c.Codigo='" + a + "' and c.NombreEmpresa='" + b + "' group by Codigo, NombreEmpresa";

                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();
                char[] cad;

                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = consulta;
                MySqlDataReader leer = comandos.ExecuteReader();


                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        cli_a_cod_textBox.Text = leer["Codigo"].ToString();
                        cli_a_nombEm_textBox.Text = leer["NombreEmpresa"].ToString();
                        cli_a_cad_textBox.Text = leer["Cadena"].ToString();
                        cli_a_cif_textBox.Text = leer["Cif"].ToString();
                        cli_a_dir_textBox.Text = leer["Direccion"].ToString();
                        cli_a_poblacion_textBox.Text = leer["Poblacion"].ToString();
                        cli_a_cp_textBox.Text = leer["Cp"].ToString();
                        cli_a_nombApell_textBox.Text = leer["NombreApellidos"].ToString();
                        cli_a_dni_textBox.Text = leer["Dni"].ToString();
                        cli_a_tlf_textBox.Text = leer["ntlf"].ToString();
                        cli_a_observacion_richTextBox.Text = leer["Observaciones"].ToString();
                        if (leer["Activo"].ToString()  == "Sí") cli_a_activo_SI_radioButton.Checked = true;
                        else cli_a_activo_NO_radioButton.Checked = true;
                        if (leer["Actualizado"].ToString() != "")
                        {
                            cad = leer["Actualizado"].ToString().ToCharArray();

                            for (int x = 0; x < cad.Length; x++)
                            {
                                if (cad[x] == '0') checkBox_cli_a_cod.Checked = true;
                                else if (cad[x] == '1') checkBox_cli_a_nombEm.Checked = true;
                                else if (cad[x] == '2') checkBox_cli_a_cad.Checked = true;
                                else if (cad[x] == '3') checkBox_cli_a_cif.Checked = true;
                                else if (cad[x] == '4') checkBox_cli_a_dir.Checked = true;
                                else if (cad[x] == '5') checkBox_cli_a_poblacion.Checked = true;
                                else if (cad[x] == '6') checkBox_cli_a_cp.Checked = true;
                                else if (cad[x] == '7') checkBox_cli_a_nomAp.Checked = true;
                                else if (cad[x] == '8') checkBox_cli_a_dni.Checked = true;
                                else if (cad[x] == '9') checkBox_cli_a_tlf.Checked = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox_cli_a_cod.Checked = false;
            checkBox_cli_a_nombEm.Checked = false;
            checkBox_cli_a_cad.Checked = false;
            checkBox_cli_a_cif.Checked = false;
            checkBox_cli_a_dir.Checked = false;
            checkBox_cli_a_poblacion.Checked = false;
            checkBox_cli_a_cp.Checked = false;
            checkBox_cli_a_nomAp.Checked = false;
            checkBox_cli_a_dni.Checked = false;
            checkBox_cli_a_tlf.Checked = false;
            devolverCliente(a, b);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Hace la comprobación de que los campos obligatorios correspondientes a CLIENTE-AÑADIR estén completos
        private bool comprobarCamposCliA()
        {
            bool error = false;

            if (cli_a_cod_textBox.Text == null || cli_a_cod_textBox.Text == "") error = true;
            if (cli_a_nombEm_textBox.Text == null || cli_a_nombEm_textBox.Text == "") error = true;
            if (cli_a_dir_textBox.Text == null || cli_a_dir_textBox.Text == "") error = true;
            if (cli_a_dni_textBox.Text == null || cli_a_dni_textBox.Text == "") error = true;
            if (cli_a_poblacion_textBox.Text == null || cli_a_poblacion_textBox.Text == "") error = true;
            if (cli_a_activo_NO_radioButton.Checked == false && cli_a_activo_SI_radioButton.Checked == false) error = true;

            if (error == true) MessageBox.Show("Uno o más campos obligatorios no están rellenados.");
            else
            {
                //Comprobar que el CODIGO cumple los requisitos
                error = comprobarCodigo();

                //Comprobar que el CIF cumple los requisitos
                if (!error)
                {
                    error = comprobarCif();
                }


                //Comprobar que el DNI cumple los requisitos
                if (!error)
                {
                    error = comprobarDni();
                }

                //Comprobar que el CP cumple los requisitos
                if (!error)
                {
                   error = comprobarCp();
                }

                //Comprobar que los TLF cumplen los requisitos
                if (!error)
                {
                    error = comprobarTlf();
                }
            }
            return error;
        }

        //Complementa a comprobarCamposCliA()
        private bool comprobarTlf()
        {
            if (cli_a_tlf_textBox.Text == null || cli_a_tlf_textBox.Text == "") { }
            else
            {
                string[] cadenaTlf;
                string tlfs;
                char[] tlfDesc;

                tlfs = cli_a_tlf_textBox.Text;
                tlfs = tlfs.Replace(" ", string.Empty);
                tlfs = tlfs.Replace(';', ',');
                tlfs = tlfs.Replace(':', ',');
                tlfs = tlfs.Replace('-', ',');

                cadenaTlf = tlfs.Split(',');

                for (int x = 0; x < cadenaTlf.Length; x++)
                {
                    tlfDesc = cadenaTlf[x].ToCharArray();
                    if (tlfDesc.Length == 9)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (tlfDesc[i] < 48 || tlfDesc[i] > 57)
                            {
                                MessageBox.Show("El campo 'Teléfono' no se ha rellenado correctamente.\nComprobar que se han escrito 9 dígitos por teléfono y, en caso de haber" +
                                " más de un teléfono, se han separado por una coma. Pueden escribirse espacios mientras no se usen para separar un teléfono de otro." + 
                                "\n\n          Ejemplo: '123456789, 987654321'");
                                return true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo 'Teléfono' no se ha rellenado correctamente.\nComprobar que se han escrito 9 dígitos por teléfono y, en caso de haber" +
                                " más de un teléfono, se han separado por una coma. Pueden escribirse espacios mientras no se usen para separar un teléfono de otro." +
                                "\n\n          Ejemplo: '123456789, 987654321'");
                        return true;
                    }
                }
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        private bool comprobarCp()
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
                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El campo 'Código Postal' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                                "          Ejemplo: '03500'");
                    return true;
                }
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        private bool comprobarDni()
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
                            return true;
                        }
                    }
                    else if (i == 8)
                    {
                        if ((dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122))
                        {
                            MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
                            return true;
                        }
                    }
                    else
                    {
                        if (dniArray[i] < 48 || dniArray[i] > 57)
                        {
                            MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
                            return true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El campo 'DNI/NIE' no se ha rellenado correctamente.\nDependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
                return true;
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        private bool comprobarCif()
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
                                return true;
                            }
                        }
                        else if (i == 0)
                        {
                            if ((cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122))
                            {
                                MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                    " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                return true;
                            }
                        }
                        else
                        {
                            if (cifArray[i] < 48 || cifArray[i] > 57)
                            {
                                MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                    " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                    " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                    return true;
                }
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        private bool comprobarCodigo()
        {
            char[] codigoArray = cli_a_cod_textBox.Text.ToCharArray();
            if (codigoArray.Length == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (codigoArray[i] < 48 || codigoArray[i] > 57)
                    {
                        MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                            "          Ejemplo: '00023'");
                        return true;
                    }
                }
            }
            else
            {
                MessageBox.Show("El campo 'Código' no se ha rellenado correctamente.\nEs obligatorio que se componga por un número de cinco cifras.\n\n" +
                            "          Ejemplo: '00023'");
                return true;
            }
            return false;
        }

        private void boton_modificar_Click(object sender, EventArgs e)
        {
            comprobarCamposCliA();
        }
    }
}
