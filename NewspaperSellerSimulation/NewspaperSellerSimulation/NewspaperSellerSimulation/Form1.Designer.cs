namespace NewspaperSellerSimulation
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
            this.Browse_TB = new System.Windows.Forms.TextBox();
            this.Browse_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Import_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NNP_tb = new System.Windows.Forms.TextBox();
            this.NR_tb = new System.Windows.Forms.TextBox();
            this.PP_tb = new System.Windows.Forms.TextBox();
            this.ScrapPrice_tb = new System.Windows.Forms.TextBox();
            this.SellingPrice_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ST_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Browse_TB
            // 
            this.Browse_TB.Location = new System.Drawing.Point(12, 32);
            this.Browse_TB.Name = "Browse_TB";
            this.Browse_TB.Size = new System.Drawing.Size(204, 22);
            this.Browse_TB.TabIndex = 0;
            // 
            // Browse_btn
            // 
            this.Browse_btn.Location = new System.Drawing.Point(250, 28);
            this.Browse_btn.Name = "Browse_btn";
            this.Browse_btn.Size = new System.Drawing.Size(93, 31);
            this.Browse_btn.TabIndex = 1;
            this.Browse_btn.Text = "Browse";
            this.Browse_btn.UseVisualStyleBackColor = true;
            this.Browse_btn.Click += new System.EventHandler(this.Browse_btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Import_btn
            // 
            this.Import_btn.Location = new System.Drawing.Point(74, 60);
            this.Import_btn.Name = "Import_btn";
            this.Import_btn.Size = new System.Drawing.Size(85, 31);
            this.Import_btn.TabIndex = 2;
            this.Import_btn.Text = "Import";
            this.Import_btn.UseVisualStyleBackColor = true;
            this.Import_btn.Click += new System.EventHandler(this.Import_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 227);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(920, 274);
            this.dataGridView1.TabIndex = 3;
            // 
            // NNP_tb
            // 
            this.NNP_tb.Location = new System.Drawing.Point(718, 41);
            this.NNP_tb.Name = "NNP_tb";
            this.NNP_tb.Size = new System.Drawing.Size(178, 22);
            this.NNP_tb.TabIndex = 4;
            // 
            // NR_tb
            // 
            this.NR_tb.Location = new System.Drawing.Point(718, 69);
            this.NR_tb.Name = "NR_tb";
            this.NR_tb.Size = new System.Drawing.Size(178, 22);
            this.NR_tb.TabIndex = 5;
            // 
            // PP_tb
            // 
            this.PP_tb.Location = new System.Drawing.Point(718, 97);
            this.PP_tb.Name = "PP_tb";
            this.PP_tb.Size = new System.Drawing.Size(178, 22);
            this.PP_tb.TabIndex = 6;
            // 
            // ScrapPrice_tb
            // 
            this.ScrapPrice_tb.Location = new System.Drawing.Point(718, 125);
            this.ScrapPrice_tb.Name = "ScrapPrice_tb";
            this.ScrapPrice_tb.Size = new System.Drawing.Size(178, 22);
            this.ScrapPrice_tb.TabIndex = 7;
            // 
            // SellingPrice_tb
            // 
            this.SellingPrice_tb.Location = new System.Drawing.Point(718, 153);
            this.SellingPrice_tb.Name = "SellingPrice_tb";
            this.SellingPrice_tb.Size = new System.Drawing.Size(178, 22);
            this.SellingPrice_tb.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "NumOfNewspapers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(587, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "NumOfRecords";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(587, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "PurchasePrice";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(587, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "ScrapPrice";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(587, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "SellingPrice";
            // 
            // ST_btn
            // 
            this.ST_btn.Location = new System.Drawing.Point(48, 173);
            this.ST_btn.Name = "ST_btn";
            this.ST_btn.Size = new System.Drawing.Size(138, 36);
            this.ST_btn.TabIndex = 14;
            this.ST_btn.Text = "Simulation Table";
            this.ST_btn.UseVisualStyleBackColor = true;
            this.ST_btn.Click += new System.EventHandler(this.ST_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 513);
            this.Controls.Add(this.ST_btn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SellingPrice_tb);
            this.Controls.Add(this.ScrapPrice_tb);
            this.Controls.Add(this.PP_tb);
            this.Controls.Add(this.NR_tb);
            this.Controls.Add(this.NNP_tb);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Import_btn);
            this.Controls.Add(this.Browse_btn);
            this.Controls.Add(this.Browse_TB);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Browse_TB;
        private System.Windows.Forms.Button Browse_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Import_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox NNP_tb;
        private System.Windows.Forms.TextBox NR_tb;
        private System.Windows.Forms.TextBox PP_tb;
        private System.Windows.Forms.TextBox ScrapPrice_tb;
        private System.Windows.Forms.TextBox SellingPrice_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button ST_btn;
    }
}