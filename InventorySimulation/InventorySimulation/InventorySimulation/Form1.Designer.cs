namespace InventorySimulation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.n_txt = new System.Windows.Forms.Label();
            this.noDays_txt = new System.Windows.Forms.Label();
            this.BIQ_txt = new System.Windows.Forms.Label();
            this.FOAA_txt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_txtbox = new System.Windows.Forms.TextBox();
            this.n_txtbox = new System.Windows.Forms.TextBox();
            this.noDays_txtbox = new System.Windows.Forms.TextBox();
            this.BIQ_txtbox = new System.Windows.Forms.TextBox();
            this.FOAA_txtbox = new System.Windows.Forms.TextBox();
            this.FOQuant_txtbox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BR_btn = new System.Windows.Forms.Button();
            this.import_btn = new System.Windows.Forms.Button();
            this.sim_btn = new System.Windows.Forms.Button();
            this.Browse_TB = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(546, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "The Order-up-to-level (M)";
            // 
            // n_txt
            // 
            this.n_txt.ForeColor = System.Drawing.Color.White;
            this.n_txt.Location = new System.Drawing.Point(543, 48);
            this.n_txt.Name = "n_txt";
            this.n_txt.Size = new System.Drawing.Size(200, 23);
            this.n_txt.TabIndex = 1;
            this.n_txt.Text = "The Review Period (N)";
            // 
            // noDays_txt
            // 
            this.noDays_txt.ForeColor = System.Drawing.Color.White;
            this.noDays_txt.Location = new System.Drawing.Point(543, 77);
            this.noDays_txt.Name = "noDays_txt";
            this.noDays_txt.Size = new System.Drawing.Size(179, 25);
            this.noDays_txt.TabIndex = 2;
            this.noDays_txt.Text = "Number of Days ";
            // 
            // BIQ_txt
            // 
            this.BIQ_txt.ForeColor = System.Drawing.Color.White;
            this.BIQ_txt.Location = new System.Drawing.Point(543, 105);
            this.BIQ_txt.Name = "BIQ_txt";
            this.BIQ_txt.Size = new System.Drawing.Size(179, 17);
            this.BIQ_txt.TabIndex = 3;
            this.BIQ_txt.Text = "Beginning Inventory Quantity";
            // 
            // FOAA_txt
            // 
            this.FOAA_txt.ForeColor = System.Drawing.Color.White;
            this.FOAA_txt.Location = new System.Drawing.Point(543, 138);
            this.FOAA_txt.Name = "FOAA_txt";
            this.FOAA_txt.Size = new System.Drawing.Size(179, 24);
            this.FOAA_txt.TabIndex = 4;
            this.FOAA_txt.Text = "First Order arrives after ?";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(543, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "First Order Quantity";
            // 
            // m_txtbox
            // 
            this.m_txtbox.Location = new System.Drawing.Point(723, 19);
            this.m_txtbox.Name = "m_txtbox";
            this.m_txtbox.Size = new System.Drawing.Size(186, 22);
            this.m_txtbox.TabIndex = 6;
            // 
            // n_txtbox
            // 
            this.n_txtbox.Location = new System.Drawing.Point(723, 47);
            this.n_txtbox.Name = "n_txtbox";
            this.n_txtbox.Size = new System.Drawing.Size(186, 22);
            this.n_txtbox.TabIndex = 7;
            // 
            // noDays_txtbox
            // 
            this.noDays_txtbox.Location = new System.Drawing.Point(723, 77);
            this.noDays_txtbox.Name = "noDays_txtbox";
            this.noDays_txtbox.Size = new System.Drawing.Size(186, 22);
            this.noDays_txtbox.TabIndex = 8;
            // 
            // BIQ_txtbox
            // 
            this.BIQ_txtbox.Location = new System.Drawing.Point(723, 105);
            this.BIQ_txtbox.Name = "BIQ_txtbox";
            this.BIQ_txtbox.Size = new System.Drawing.Size(186, 22);
            this.BIQ_txtbox.TabIndex = 9;
            // 
            // FOAA_txtbox
            // 
            this.FOAA_txtbox.Location = new System.Drawing.Point(723, 135);
            this.FOAA_txtbox.Name = "FOAA_txtbox";
            this.FOAA_txtbox.Size = new System.Drawing.Size(186, 22);
            this.FOAA_txtbox.TabIndex = 10;
            // 
            // FOQuant_txtbox
            // 
            this.FOQuant_txtbox.Location = new System.Drawing.Point(723, 165);
            this.FOQuant_txtbox.Name = "FOQuant_txtbox";
            this.FOQuant_txtbox.Size = new System.Drawing.Size(186, 22);
            this.FOQuant_txtbox.TabIndex = 11;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BR_btn
            // 
            this.BR_btn.Location = new System.Drawing.Point(282, 36);
            this.BR_btn.Name = "BR_btn";
            this.BR_btn.Size = new System.Drawing.Size(137, 35);
            this.BR_btn.TabIndex = 12;
            this.BR_btn.Text = "Browse";
            this.BR_btn.UseVisualStyleBackColor = true;
            this.BR_btn.Click += new System.EventHandler(this.BR_btn_Click);
            // 
            // import_btn
            // 
            this.import_btn.Location = new System.Drawing.Point(37, 77);
            this.import_btn.Name = "import_btn";
            this.import_btn.Size = new System.Drawing.Size(116, 35);
            this.import_btn.TabIndex = 13;
            this.import_btn.Text = "import";
            this.import_btn.UseVisualStyleBackColor = true;
            this.import_btn.Click += new System.EventHandler(this.import_btn_Click);
            // 
            // sim_btn
            // 
            this.sim_btn.Location = new System.Drawing.Point(178, 77);
            this.sim_btn.Name = "sim_btn";
            this.sim_btn.Size = new System.Drawing.Size(116, 35);
            this.sim_btn.TabIndex = 14;
            this.sim_btn.Text = "simulation table";
            this.sim_btn.UseVisualStyleBackColor = true;
            this.sim_btn.Click += new System.EventHandler(this.sim_btn_Click);
            // 
            // Browse_TB
            // 
            this.Browse_TB.Location = new System.Drawing.Point(70, 44);
            this.Browse_TB.Name = "Browse_TB";
            this.Browse_TB.Size = new System.Drawing.Size(186, 22);
            this.Browse_TB.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 217);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(979, 229);
            this.dataGridView1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1003, 458);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Browse_TB);
            this.Controls.Add(this.sim_btn);
            this.Controls.Add(this.import_btn);
            this.Controls.Add(this.BR_btn);
            this.Controls.Add(this.FOQuant_txtbox);
            this.Controls.Add(this.FOAA_txtbox);
            this.Controls.Add(this.BIQ_txtbox);
            this.Controls.Add(this.noDays_txtbox);
            this.Controls.Add(this.n_txtbox);
            this.Controls.Add(this.m_txtbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FOAA_txt);
            this.Controls.Add(this.BIQ_txt);
            this.Controls.Add(this.noDays_txt);
            this.Controls.Add(this.n_txt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label n_txt;
        private System.Windows.Forms.Label noDays_txt;
        private System.Windows.Forms.Label BIQ_txt;
        private System.Windows.Forms.Label FOAA_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_txtbox;
        private System.Windows.Forms.TextBox n_txtbox;
        private System.Windows.Forms.TextBox noDays_txtbox;
        private System.Windows.Forms.TextBox BIQ_txtbox;
        private System.Windows.Forms.TextBox FOAA_txtbox;
        private System.Windows.Forms.TextBox FOQuant_txtbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BR_btn;
        private System.Windows.Forms.Button import_btn;
        private System.Windows.Forms.Button sim_btn;
        private System.Windows.Forms.TextBox Browse_TB;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}