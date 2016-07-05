namespace ProyectoA
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label nombreEmpresaLabel;
            System.Windows.Forms.Label cadenaLabel;
            System.Windows.Forms.Label cifLabel;
            System.Windows.Forms.Label direccionLabel;
            System.Windows.Forms.Label poblacionLabel;
            System.Windows.Forms.Label cpLabel;
            System.Windows.Forms.Label nombreApellidosLabel;
            System.Windows.Forms.Label dniLabel;
            System.Windows.Forms.Label activoLabel;
            System.Windows.Forms.Label actualizadoLabel;
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.clienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bDADataSet = new ProyectoA.BDADataSet();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.prueba1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prueba2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ediciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaciónClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaciónMáquinasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaciónContratosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteTableAdapter = new ProyectoA.BDADataSetTableAdapters.ClienteTableAdapter();
            this.tableAdapterManager = new ProyectoA.BDADataSetTableAdapters.TableAdapterManager();
            this.label1 = new System.Windows.Forms.Label();
            this.codigoTextBox = new System.Windows.Forms.TextBox();
            this.nombreEmpresaTextBox = new System.Windows.Forms.TextBox();
            this.cadenaTextBox = new System.Windows.Forms.TextBox();
            this.cifTextBox = new System.Windows.Forms.TextBox();
            this.direccionTextBox = new System.Windows.Forms.TextBox();
            this.cpTextBox = new System.Windows.Forms.TextBox();
            this.poblacionTextBox = new System.Windows.Forms.TextBox();
            this.nombrApellidosTextBox = new System.Windows.Forms.TextBox();
            this.dniTextBox = new System.Windows.Forms.TextBox();
            this.telefonoTextBox = new System.Windows.Forms.TextBox();
            this.activoCheckBox = new System.Windows.Forms.CheckBox();
            this.actualizadoCheckBox = new System.Windows.Forms.CheckBox();
            codigoLabel = new System.Windows.Forms.Label();
            nombreEmpresaLabel = new System.Windows.Forms.Label();
            cadenaLabel = new System.Windows.Forms.Label();
            cifLabel = new System.Windows.Forms.Label();
            direccionLabel = new System.Windows.Forms.Label();
            poblacionLabel = new System.Windows.Forms.Label();
            cpLabel = new System.Windows.Forms.Label();
            nombreApellidosLabel = new System.Windows.Forms.Label();
            dniLabel = new System.Windows.Forms.Label();
            activoLabel = new System.Windows.Forms.Label();
            actualizadoLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDADataSet)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Location = new System.Drawing.Point(34, 38);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(43, 13);
            codigoLabel.TabIndex = 0;
            codigoLabel.Text = "Código:";
            codigoLabel.Click += new System.EventHandler(this.codigoLabel_Click);
            // 
            // nombreEmpresaLabel
            // 
            nombreEmpresaLabel.AutoSize = true;
            nombreEmpresaLabel.Location = new System.Drawing.Point(34, 64);
            nombreEmpresaLabel.Name = "nombreEmpresaLabel";
            nombreEmpresaLabel.Size = new System.Drawing.Size(90, 13);
            nombreEmpresaLabel.TabIndex = 2;
            nombreEmpresaLabel.Text = "Nombre empresa:";
            // 
            // cadenaLabel
            // 
            cadenaLabel.AutoSize = true;
            cadenaLabel.Location = new System.Drawing.Point(34, 92);
            cadenaLabel.Name = "cadenaLabel";
            cadenaLabel.Size = new System.Drawing.Size(47, 13);
            cadenaLabel.TabIndex = 4;
            cadenaLabel.Text = "Cadena:";
            // 
            // cifLabel
            // 
            cifLabel.AutoSize = true;
            cifLabel.Location = new System.Drawing.Point(34, 118);
            cifLabel.Name = "cifLabel";
            cifLabel.Size = new System.Drawing.Size(26, 13);
            cifLabel.TabIndex = 6;
            cifLabel.Text = "CIF:";
            cifLabel.Click += new System.EventHandler(this.cifLabel_Click);
            // 
            // direccionLabel
            // 
            direccionLabel.AutoSize = true;
            direccionLabel.Location = new System.Drawing.Point(322, 42);
            direccionLabel.Name = "direccionLabel";
            direccionLabel.Size = new System.Drawing.Size(55, 13);
            direccionLabel.TabIndex = 8;
            direccionLabel.Text = "Dirección:";
            // 
            // poblacionLabel
            // 
            poblacionLabel.AutoSize = true;
            poblacionLabel.Location = new System.Drawing.Point(322, 96);
            poblacionLabel.Name = "poblacionLabel";
            poblacionLabel.Size = new System.Drawing.Size(57, 13);
            poblacionLabel.TabIndex = 10;
            poblacionLabel.Text = "Población:";
            // 
            // cpLabel
            // 
            cpLabel.AutoSize = true;
            cpLabel.Location = new System.Drawing.Point(322, 70);
            cpLabel.Name = "cpLabel";
            cpLabel.Size = new System.Drawing.Size(24, 13);
            cpLabel.TabIndex = 12;
            cpLabel.Text = "CP:";
            // 
            // nombreApellidosLabel
            // 
            nombreApellidosLabel.AutoSize = true;
            nombreApellidosLabel.Location = new System.Drawing.Point(583, 42);
            nombreApellidosLabel.Name = "nombreApellidosLabel";
            nombreApellidosLabel.Size = new System.Drawing.Size(99, 13);
            nombreApellidosLabel.TabIndex = 14;
            nombreApellidosLabel.Text = "Nombre y apellidos:";
            // 
            // dniLabel
            // 
            dniLabel.AutoSize = true;
            dniLabel.Location = new System.Drawing.Point(583, 70);
            dniLabel.Name = "dniLabel";
            dniLabel.Size = new System.Drawing.Size(29, 13);
            dniLabel.TabIndex = 16;
            dniLabel.Text = "DNI:";
            // 
            // activoLabel
            // 
            activoLabel.AutoSize = true;
            activoLabel.Location = new System.Drawing.Point(918, 42);
            activoLabel.Name = "activoLabel";
            activoLabel.Size = new System.Drawing.Size(40, 13);
            activoLabel.TabIndex = 20;
            activoLabel.Text = "Activo:";
            // 
            // actualizadoLabel
            // 
            actualizadoLabel.AutoSize = true;
            actualizadoLabel.Location = new System.Drawing.Point(918, 70);
            actualizadoLabel.Name = "actualizadoLabel";
            actualizadoLabel.Size = new System.Drawing.Size(65, 13);
            actualizadoLabel.TabIndex = 22;
            actualizadoLabel.Text = "Actualizado:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1186, 586);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1178, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clientes";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(960, 377);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Añadir";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.actualizadoCheckBox);
            this.groupBox1.Controls.Add(this.activoCheckBox);
            this.groupBox1.Controls.Add(this.telefonoTextBox);
            this.groupBox1.Controls.Add(this.dniTextBox);
            this.groupBox1.Controls.Add(this.nombrApellidosTextBox);
            this.groupBox1.Controls.Add(this.poblacionTextBox);
            this.groupBox1.Controls.Add(this.cpTextBox);
            this.groupBox1.Controls.Add(this.direccionTextBox);
            this.groupBox1.Controls.Add(this.cifTextBox);
            this.groupBox1.Controls.Add(this.cadenaTextBox);
            this.groupBox1.Controls.Add(this.nombreEmpresaTextBox);
            this.groupBox1.Controls.Add(this.codigoTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(codigoLabel);
            this.groupBox1.Controls.Add(nombreEmpresaLabel);
            this.groupBox1.Controls.Add(cadenaLabel);
            this.groupBox1.Controls.Add(cifLabel);
            this.groupBox1.Controls.Add(direccionLabel);
            this.groupBox1.Controls.Add(poblacionLabel);
            this.groupBox1.Controls.Add(cpLabel);
            this.groupBox1.Controls.Add(nombreApellidosLabel);
            this.groupBox1.Controls.Add(dniLabel);
            this.groupBox1.Controls.Add(activoLabel);
            this.groupBox1.Controls.Add(actualizadoLabel);
            this.groupBox1.Location = new System.Drawing.Point(6, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1166, 494);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buscar";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(473, 142);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(241, 38);
            this.button3.TabIndex = 24;
            this.button3.Text = "INICIAR BÚSQUEDA";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // clienteBindingSource
            // 
            this.clienteBindingSource.DataMember = "Cliente";
            this.clienteBindingSource.DataSource = this.bDADataSet;
            // 
            // bDADataSet
            // 
            this.bDADataSet.DataSetName = "BDADataSet";
            this.bDADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1178, 57);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(592, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(583, 51);
            this.button2.TabIndex = 1;
            this.button2.Text = "AÑADIR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(583, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "BUSCAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1178, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Máquinas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1178, 560);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Contratos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ediciónToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1210, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prueba1ToolStripMenuItem,
            this.prueba2ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 20);
            this.toolStripMenuItem1.Text = "Archivo";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // prueba1ToolStripMenuItem
            // 
            this.prueba1ToolStripMenuItem.Name = "prueba1ToolStripMenuItem";
            this.prueba1ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.prueba1ToolStripMenuItem.Text = "Prueba1";
            // 
            // prueba2ToolStripMenuItem
            // 
            this.prueba2ToolStripMenuItem.Name = "prueba2ToolStripMenuItem";
            this.prueba2ToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.prueba2ToolStripMenuItem.Text = "Prueba 2";
            // 
            // ediciónToolStripMenuItem
            // 
            this.ediciónToolStripMenuItem.Name = "ediciónToolStripMenuItem";
            this.ediciónToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.ediciónToolStripMenuItem.Text = "Edición";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informaciónClientesToolStripMenuItem,
            this.informaciónMáquinasToolStripMenuItem,
            this.informaciónContratosToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // informaciónClientesToolStripMenuItem
            // 
            this.informaciónClientesToolStripMenuItem.Name = "informaciónClientesToolStripMenuItem";
            this.informaciónClientesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.informaciónClientesToolStripMenuItem.Text = "Información Clientes";
            this.informaciónClientesToolStripMenuItem.Click += new System.EventHandler(this.informaciónClientesToolStripMenuItem_Click);
            // 
            // informaciónMáquinasToolStripMenuItem
            // 
            this.informaciónMáquinasToolStripMenuItem.Name = "informaciónMáquinasToolStripMenuItem";
            this.informaciónMáquinasToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.informaciónMáquinasToolStripMenuItem.Text = "Información Máquinas";
            this.informaciónMáquinasToolStripMenuItem.Click += new System.EventHandler(this.informaciónMáquinasToolStripMenuItem_Click);
            // 
            // informaciónContratosToolStripMenuItem
            // 
            this.informaciónContratosToolStripMenuItem.Name = "informaciónContratosToolStripMenuItem";
            this.informaciónContratosToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.informaciónContratosToolStripMenuItem.Text = "Información Contratos";
            this.informaciónContratosToolStripMenuItem.Click += new System.EventHandler(this.informaciónContratosToolStripMenuItem_Click);
            // 
            // clienteTableAdapter
            // 
            this.clienteTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ClienteTableAdapter = this.clienteTableAdapter;
            this.tableAdapterManager.ClienteTelefonoTableAdapter = null;
            this.tableAdapterManager.ContratoTableAdapter = null;
            this.tableAdapterManager.EstadosContratoTableAdapter = null;
            this.tableAdapterManager.EstadosMaquinaTableAdapter = null;
            this.tableAdapterManager.FamiliaTableAdapter = null;
            this.tableAdapterManager.MaquinaTableAdapter = null;
            this.tableAdapterManager.ModeloTableAdapter = null;
            this.tableAdapterManager.TelefonoTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = ProyectoA.BDADataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsuarioTableAdapter = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Teléfono:";
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.Location = new System.Drawing.Point(134, 35);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.Size = new System.Drawing.Size(133, 20);
            this.codigoTextBox.TabIndex = 26;
            // 
            // nombreEmpresaTextBox
            // 
            this.nombreEmpresaTextBox.Location = new System.Drawing.Point(134, 61);
            this.nombreEmpresaTextBox.Name = "nombreEmpresaTextBox";
            this.nombreEmpresaTextBox.Size = new System.Drawing.Size(133, 20);
            this.nombreEmpresaTextBox.TabIndex = 27;
            // 
            // cadenaTextBox
            // 
            this.cadenaTextBox.Location = new System.Drawing.Point(134, 89);
            this.cadenaTextBox.Name = "cadenaTextBox";
            this.cadenaTextBox.Size = new System.Drawing.Size(133, 20);
            this.cadenaTextBox.TabIndex = 28;
            // 
            // cifTextBox
            // 
            this.cifTextBox.Location = new System.Drawing.Point(134, 115);
            this.cifTextBox.Name = "cifTextBox";
            this.cifTextBox.Size = new System.Drawing.Size(133, 20);
            this.cifTextBox.TabIndex = 29;
            // 
            // direccionTextBox
            // 
            this.direccionTextBox.Location = new System.Drawing.Point(397, 35);
            this.direccionTextBox.Name = "direccionTextBox";
            this.direccionTextBox.Size = new System.Drawing.Size(148, 20);
            this.direccionTextBox.TabIndex = 30;
            // 
            // cpTextBox
            // 
            this.cpTextBox.Location = new System.Drawing.Point(397, 67);
            this.cpTextBox.Name = "cpTextBox";
            this.cpTextBox.Size = new System.Drawing.Size(148, 20);
            this.cpTextBox.TabIndex = 31;
            // 
            // poblacionTextBox
            // 
            this.poblacionTextBox.Location = new System.Drawing.Point(397, 93);
            this.poblacionTextBox.Name = "poblacionTextBox";
            this.poblacionTextBox.Size = new System.Drawing.Size(148, 20);
            this.poblacionTextBox.TabIndex = 32;
            // 
            // nombrApellidosTextBox
            // 
            this.nombrApellidosTextBox.Location = new System.Drawing.Point(705, 38);
            this.nombrApellidosTextBox.Name = "nombrApellidosTextBox";
            this.nombrApellidosTextBox.Size = new System.Drawing.Size(160, 20);
            this.nombrApellidosTextBox.TabIndex = 33;
            // 
            // dniTextBox
            // 
            this.dniTextBox.Location = new System.Drawing.Point(705, 67);
            this.dniTextBox.Name = "dniTextBox";
            this.dniTextBox.Size = new System.Drawing.Size(160, 20);
            this.dniTextBox.TabIndex = 34;
            // 
            // telefonoTextBox
            // 
            this.telefonoTextBox.Location = new System.Drawing.Point(705, 93);
            this.telefonoTextBox.Name = "telefonoTextBox";
            this.telefonoTextBox.Size = new System.Drawing.Size(160, 20);
            this.telefonoTextBox.TabIndex = 35;
            // 
            // activoCheckBox
            // 
            this.activoCheckBox.AutoSize = true;
            this.activoCheckBox.Location = new System.Drawing.Point(1002, 40);
            this.activoCheckBox.Name = "activoCheckBox";
            this.activoCheckBox.Size = new System.Drawing.Size(15, 14);
            this.activoCheckBox.TabIndex = 36;
            this.activoCheckBox.UseVisualStyleBackColor = true;
            // 
            // actualizadoCheckBox
            // 
            this.actualizadoCheckBox.AutoSize = true;
            this.actualizadoCheckBox.Location = new System.Drawing.Point(1002, 69);
            this.actualizadoCheckBox.Name = "actualizadoCheckBox";
            this.actualizadoCheckBox.Size = new System.Drawing.Size(15, 14);
            this.actualizadoCheckBox.TabIndex = 37;
            this.actualizadoCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1210, 625);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Máquinas";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDADataSet)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ediciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prueba1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prueba2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaciónClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaciónMáquinasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaciónContratosToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private BDADataSet bDADataSet;
        private System.Windows.Forms.BindingSource clienteBindingSource;
        private BDADataSetTableAdapters.ClienteTableAdapter clienteTableAdapter;
        private BDADataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox actualizadoCheckBox;
        private System.Windows.Forms.CheckBox activoCheckBox;
        private System.Windows.Forms.TextBox telefonoTextBox;
        private System.Windows.Forms.TextBox dniTextBox;
        private System.Windows.Forms.TextBox nombrApellidosTextBox;
        private System.Windows.Forms.TextBox poblacionTextBox;
        private System.Windows.Forms.TextBox cpTextBox;
        private System.Windows.Forms.TextBox direccionTextBox;
        private System.Windows.Forms.TextBox cifTextBox;
        private System.Windows.Forms.TextBox cadenaTextBox;
        private System.Windows.Forms.TextBox nombreEmpresaTextBox;
        private System.Windows.Forms.TextBox codigoTextBox;
        private System.Windows.Forms.Label label1;
    }
}

