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
            devolverCliente();
            boton_modificar.BackColor = Color.SkyBlue;
            boton_eliminar.BackColor = Color.Salmon;
            actualizarImagenesTick();
        }

        //ACTUALIZAR LAS IMAGENES TICK AL ABRIR EL PERFIL
        private void actualizarImagenesTick()
        {
            if (cli_a_observacion_richTextBox.TextLength > 700) pictureBox12.Image = ProyectoA.Properties.Resources.tickNeg;
            else pictureBox12.Image = null;
            if (cli_a_nombApell_textBox.TextLength > 45) pictureBox11.Image = ProyectoA.Properties.Resources.tickNeg;
            else pictureBox11.Image = null;
            if (cli_a_cad_textBox.TextLength > 45) pictureBox10.Image = ProyectoA.Properties.Resources.tickNeg;
            else pictureBox10.Image = null;
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked)
            {
                pictureBox9.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else pictureBox9.Image = ProyectoA.Properties.Resources.tickPos;
            if (cli_a_tlf_textBox.TextLength != 0)
            {
                if (cli_a_tlf_textBox.TextLength < 101)
                {
                    if (!comprobarTlf()) pictureBox8.Image = ProyectoA.Properties.Resources.tickPos;
                    else pictureBox8.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else pictureBox8.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else pictureBox8.Image = null;
            if (!comprobarDni()) pictureBox7.Image = ProyectoA.Properties.Resources.tickPos;
            else pictureBox7.Image = ProyectoA.Properties.Resources.tickNeg;
            if (cli_a_poblacion_textBox.TextLength != 0)
            {
                if (cli_a_poblacion_textBox.TextLength > 45) pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
                else pictureBox6.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
            if (cli_a_cp_textBox.TextLength != 0)
            {
                if (!comprobarCp()) pictureBox5.Image = ProyectoA.Properties.Resources.tickPos;
                else pictureBox5.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else pictureBox5.Image = null;
            if (cli_a_dir_textBox.TextLength != 0)
            {
                if (cli_a_dir_textBox.TextLength > 45) pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
                else pictureBox4.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
            if (cli_a_cif_textBox.TextLength != 0)
            {
                if (!comprobarCif()) pictureBox3.Image = ProyectoA.Properties.Resources.tickPos;
                else pictureBox3.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else pictureBox3.Image = null;
            if (cli_a_nombEm_textBox.TextLength != 0)
            {
                if (cli_a_nombEm_textBox.TextLength > 45) pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
                else pictureBox2.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
            if (!comprobarCodigo()) pictureBox1.Image = ProyectoA.Properties.Resources.tickPos;
            else pictureBox1.Image = ProyectoA.Properties.Resources.tickNeg;
        }

        private void PerfilCliente_Load(object sender, EventArgs e)
        {
            modificacionTextosClienteA();
        }

        //Cada vez que se modifica un label comprueba los errores
        private void modificacionTextosClienteA()
        {
            cli_a_cod_textBox.TextChanged += Cli_a_cod_textBox_TextChanged;
            cli_a_nombEm_textBox.TextChanged += Cli_a_nombEm_textBox_TextChanged;
            cli_a_cif_textBox.TextChanged += Cli_a_cif_textBox_TextChanged;
            cli_a_dir_textBox.TextChanged += Cli_a_dir_textBox_TextChanged;
            cli_a_cp_textBox.TextChanged += Cli_a_cp_textBox_TextChanged;
            cli_a_poblacion_textBox.TextChanged += Cli_a_poblacion_textBox_TextChanged;
            cli_a_dni_textBox.TextChanged += Cli_a_dni_textBox_TextChanged;
            cli_a_tlf_textBox.TextChanged += Cli_a_tlf_textBox_TextChanged;
            cli_a_activo_SI_radioButton.CheckedChanged += Cli_a_activo_SI_radioButton_CheckedChanged;
            cli_a_activo_NO_radioButton.CheckedChanged += Cli_a_activo_NO_radioButton_CheckedChanged;
            cli_a_cad_textBox.TextChanged += Cli_a_cad_textBox_TextChanged;
            cli_a_nombApell_textBox.TextChanged += Cli_a_nombApell_textBox_TextChanged;
            cli_a_observacion_richTextBox.TextChanged += Cli_a_observacion_richTextBox_TextChanged;
        }
        
        private void Cli_a_observacion_richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_observacion_richTextBox.TextLength > 700)
            {
                toolTip1.SetToolTip(pictureBox12, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_observacion_richTextBox.TextLength + "/700");
                pictureBox12.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox12, "");
                pictureBox12.Image = null;
            }
        }

        private void Cli_a_nombApell_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_nombApell_textBox.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox11, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_nombApell_textBox.TextLength + "/45");
                pictureBox11.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox11, "");
                pictureBox11.Image = null;
            }
        }

        private void Cli_a_cad_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cad_textBox.TextLength > 45)
            {
                toolTip1.SetToolTip(pictureBox10, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_cad_textBox.TextLength + "/45");
                pictureBox10.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox10, "");
                pictureBox10.Image = null;
            }
        }

        private void Cli_a_activo_NO_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked)
            {
                toolTip1.SetToolTip(pictureBox9, "Campo obligatorio.");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox9, "");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }

        private void Cli_a_activo_SI_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!cli_a_activo_SI_radioButton.Checked && !cli_a_activo_NO_radioButton.Checked)
            {
                toolTip1.SetToolTip(pictureBox9, "Campo obligatorio.");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickNeg;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox9, "");
                pictureBox9.Image = ProyectoA.Properties.Resources.tickPos;
            }
        }

        private void Cli_a_tlf_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_tlf_textBox.TextLength != 0)
            {
                if (!comprobarTlf())
                {
                    toolTip1.SetToolTip(pictureBox8, "");
                    pictureBox8.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox8, "El campo 'Teléfono' no se ha rellenado correctamente.\nComprobar que se han escrito 9 dígitos por teléfono y, en caso de haber" +
                                " más de un teléfono, se han separado por una coma. Pueden escribirse espacios mientras no se usen para separar un teléfono de otro." +
                                "\n\n          Ejemplo: '123456789, 987654321'");
                    pictureBox8.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox8, "");
                pictureBox8.Image = null;
            }
        }

        private void Cli_a_dni_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!comprobarDni())
            {
                toolTip1.SetToolTip(pictureBox7, "");
                pictureBox7.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox7, "Campo obligatorio.\n" +
                            "Dependiendo de si se trata de un DNI o un NIE, puede componerse por un número de ocho cifras y una letra," +
                            " o bien por una letra, siete dígitos y otra letra." + "\n\n          Ejemplo DNI: '12345678A'" +
                            "\n\n          Ejemplo NIE: 'X1234567A'");
                pictureBox7.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_poblacion_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_poblacion_textBox.TextLength != 0)
            {
                if (cli_a_poblacion_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox6, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_poblacion_textBox.TextLength + "/45");
                    pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox6, "");
                    pictureBox6.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox6, "Campo obligatorio.");
                pictureBox6.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cp_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cp_textBox.TextLength != 0)
            {
                if (!comprobarCp())
                {
                    toolTip1.SetToolTip(pictureBox5, "");
                    pictureBox5.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox5, "Se ha de componer por cinco números.");
                    pictureBox5.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox5, "");
                pictureBox5.Image = null;
            }
        }

        private void Cli_a_dir_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_dir_textBox.TextLength != 0)
            {
                if (cli_a_dir_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox4, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_dir_textBox.TextLength + "/45");
                    pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox4, "");
                    pictureBox4.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox4, "Campo obligatorio.");
                pictureBox4.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cif_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_cif_textBox.TextLength != 0)
            {
                if (!comprobarCif())
                {
                    toolTip1.SetToolTip(pictureBox3, "");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickPos;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox3, "El campo 'CIF' no se ha rellenado correctamente.\nSe compone por nueve carácteres: el primero es una letra, los siete siguientes números" +
                                    " y el último puede ser una letra o un número.\n\n          Ejemplo: 'A1234567B', 'A12345678'...");
                    pictureBox3.Image = ProyectoA.Properties.Resources.tickNeg;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox3, "");
                pictureBox3.Image = null;
            }
        }

        private void Cli_a_nombEm_textBox_TextChanged(object sender, EventArgs e)
        {
            if (cli_a_nombEm_textBox.TextLength != 0)
            {
                if (cli_a_nombEm_textBox.TextLength > 45)
                {
                    toolTip1.SetToolTip(pictureBox2, "Se ha superado la cantidad de carácteres permitidos: " + cli_a_nombEm_textBox.TextLength + "/45");
                    pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
                }
                else
                {
                    toolTip1.SetToolTip(pictureBox2, "");
                    pictureBox2.Image = ProyectoA.Properties.Resources.tickPos;
                }
            }
            else
            {
                toolTip1.SetToolTip(pictureBox2, "Campo obligatorio.");
                pictureBox2.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        private void Cli_a_cod_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!comprobarCodigo())
            {
                toolTip1.SetToolTip(pictureBox1, "");
                pictureBox1.Image = ProyectoA.Properties.Resources.tickPos;
            }
            else
            {
                toolTip1.SetToolTip(pictureBox1, "Campo obligatorio. Se compone por cinco números.\n\nEjemplo: '00001'");
                pictureBox1.Image = ProyectoA.Properties.Resources.tickNeg;
            }
        }

        
        //Devuelve el cliente para rellenar el perfil
        private void devolverCliente()
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
            devolverCliente();
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

            if (error != true)
            {
                //Comprobar que el CODIGO cumple los requisitos
                error = comprobarCodigo();

                //Comprobar que el CIF cumple los requisitos
                if (!error) error = comprobarCif();

                //Comprobar que el DNI cumple los requisitos
                if (!error) error = comprobarDni();

                //Comprobar que el CP cumple los requisitos
                if (!error) error = comprobarCp();

                //Comprobar que los TLF cumplen los requisitos
                if (!error) error = comprobarTlf();

                //Comprueba longitud de campos
                if (!error) if (cli_a_nombEm_textBox.TextLength > 45) error = true;
                if (!error) if (cli_a_cad_textBox.TextLength > 45) error = true;
                if (!error) if (cli_a_dir_textBox.TextLength > 45) error = true;
                if (!error) if (cli_a_poblacion_textBox.TextLength > 45) error = true;
                if (!error) if (cli_a_nombEm_textBox.TextLength > 45) error = true;
                if (!error) if (cli_a_observacion_richTextBox.TextLength > 700) error = true;
            }
            if (error == true) MessageBox.Show("No puede agregarse el cliente, compruebe los campos obligatorios y el cumplimiento de las condiciones.");
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
        private bool comprobarCp()
        {
            if (cli_a_cp_textBox.Text == null || cli_a_cp_textBox.Text == "") { }
            else
            {
                char[] cpArray = cli_a_cp_textBox.Text.ToCharArray();
                if (cpArray.Length == 5)
                {
                    for (int i = 0; i < 5; i++) if (cpArray[i] < 48 || cpArray[i] > 57) return true;
                }
                else return true;
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
                        if ((dniArray[i] < 48 || dniArray[i] > 57) && (dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122)) return true;
                    }
                    else if (i == 8)
                    {
                        if ((dniArray[i] < 65 || dniArray[i] > 90) && (dniArray[i] < 97 || dniArray[i] > 122)) return true;
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
                            if ((cifArray[i] < 48 || cifArray[i] > 57) && (cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122)) return true;
                        }
                        else if (i == 0)
                        {
                            if ((cifArray[i] < 65 || cifArray[i] > 90) && (cifArray[i] < 97 || cifArray[i] > 122)) return true;

                        }
                        else if (cifArray[i] < 48 || cifArray[i] > 57) return true;
                    }
                }
                else return true;
            }
            return false;
        }
        //Complementa a comprobarCamposCliA()
        private bool comprobarCodigo()
        {
            char[] codigoArray = cli_a_cod_textBox.Text.ToCharArray();
            if (codigoArray.Length == 5)
            {
                for (int i = 0; i < 5; i++) if (codigoArray[i] < 48 || codigoArray[i] > 57) return true;
            }
            else return true;

            return false;
        }

        //BOTÓN INICIA MODIFICACIÓN
        private void boton_modificar_Click(object sender, EventArgs e)
        {
            if (!comprobarCamposCliA())
            {
                string[] cadTlf;
                string modificar;
                bool ba = true,  bo = true;

                DialogResult k = MessageBox.Show("          Confirmar modificar cliente.", "Confirmación", MessageBoxButtons.OKCancel);
                if (k == DialogResult.OK)
                {
                    modificar = "update cliente set ";

                    modificar += agregarCamposInsertCliente();

                    modificar += " where Codigo = '" + a + "' and NombreEmpresa = '" + b + "'";
                    
                    ba = modifClientes(modificar);

                    if (cli_a_tlf_textBox.Text != "" && ba)
                    {
                        cadTlf = agregarCamposTlf(cli_a_tlf_textBox.Text);
                        borraTelefonos(cli_a_cod_textBox.Text, cli_a_nombEm_textBox.Text);
                        bo = modifTelefono(cadTlf, cli_a_cod_textBox.Text, cli_a_nombEm_textBox.Text);
                    }

                    if (ba && bo)
                    {
                        MessageBox.Show("Usuario modificado correctamente.");
                    }
                    else if (ba && !bo)
                    {
                        MessageBox.Show("Usuario modificado correctamente, pero error a la hora de modificar los teléfonos");
                    }
                }
            }
        }

        private void borraTelefonos(string text1, string text2)
        {
            string sentencia;

            try
            {
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                con.ConnectionString = conexion;
                con.Open();
                
                sentencia = "delete from telefono where Cliente_Codigo = '" + text1 + "' and Cliente_NombreEmpresa = '" + text2 + "';";
                MySqlCommand comandos = new MySqlCommand();
                comandos.Connection = con;
                comandos.CommandText = sentencia;
                comandos.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error en el proceso de modificación de los teléfonos: \n\n" + Convert.ToString(error));
            }
            finally
            {
                try
                {
                    con.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error en el proceso de modificación de los teléfonos: \n\n" + Convert.ToString(e));
                }
            }
        }

        private bool modifTelefono(string[] cadTlf, string text1, string text2)
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

        private bool modifClientes(string modificar)
        {
            bool resultado = false;
            try
            {
                conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
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
                MessageBox.Show("No se ha podido modificar el cliente. Compruebe que no haya un cliente registrado con el mismo Código y Nombre empresa.\n\n" + Convert.ToString(error));
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
        private string agregarCamposInsertCliente()
        {
            string cadenaCamposI = "";
            //campos normales TABLA CLIENTE
            cadenaCamposI += "Codigo = '" + cli_a_cod_textBox.Text;
            cadenaCamposI += "', NombreEmpresa = '" + cli_a_nombEm_textBox.Text;
            cadenaCamposI += "', Cadena = '" + cli_a_cad_textBox.Text;
            cadenaCamposI += "', Cif = '" + cli_a_cif_textBox.Text;
            cadenaCamposI += "', Direccion = '" + cli_a_dir_textBox.Text;
            cadenaCamposI += "', Poblacion = '" + cli_a_poblacion_textBox.Text;
            cadenaCamposI += "', Cp = '" + cli_a_cp_textBox.Text;
            cadenaCamposI += "', NombreApellidos = '" + cli_a_nombApell_textBox.Text;
            cadenaCamposI += "', Dni = '" + cli_a_dni_textBox.Text;
            cadenaCamposI += "', Observaciones = '" + cli_a_observacion_richTextBox.Text;
            if (cli_a_activo_SI_radioButton.Checked) cadenaCamposI += "', Activo = 'Sí'";
            else cadenaCamposI += "', Activo = 'No'";

            //Actualizado TABLA CLIENTE
            cadenaCamposI += ", Actualizado = '";
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
            cadenaCamposI += "'";

            return cadenaCamposI;
        }

        //Rellena los datos de telefono en un array
        private string[] agregarCamposTlf(string tlfs)
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

        private void boton_eliminar_Click(object sender, EventArgs e)
        {
            DialogResult k = MessageBox.Show("Borrar el cliente implicará eliminar también cualquier contrato existente que lo incluyera.\n"+
                "¿Está seguro?", "Confirmación", MessageBoxButtons.OKCancel);
            if (k == DialogResult.OK)
            {
                string sentencia;

                try
                {
                    conexion = "server=localhost;user id=root;persistsecurityinfo=True;database=proyectoa_bd;Password=maiz";
                    con.ConnectionString = conexion;
                    con.Open();

                    sentencia = "delete from cliente where Codigo = '" + a + "' and NombreEmpresa = '" + b + "';";
                    MySqlCommand comandos = new MySqlCommand();
                    comandos.Connection = con;
                    comandos.CommandText = sentencia;
                    comandos.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error en el proceso de modificación de los teléfonos: \n\n" + Convert.ToString(error));
                }
                finally
                {
                    try
                    {
                        con.Close();
                    }
                    catch (Exception et)
                    {
                        MessageBox.Show("Error en el proceso de modificación de los teléfonos: \n\n" + Convert.ToString(et));
                    }
                }
                this.Close();
            }
        }
    }
}
