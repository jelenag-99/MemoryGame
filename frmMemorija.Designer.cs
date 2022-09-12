namespace Memorija
{
    partial class frmMemorija
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemorija));
            this.pnlSlike = new System.Windows.Forms.Panel();
            this.lblVrijeme = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBez = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlSlike
            // 
            this.pnlSlike.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSlike.BackgroundImage = global::Memorija.Properties.Resources.glavnaPozadina3;
            this.pnlSlike.Location = new System.Drawing.Point(12, 12);
            this.pnlSlike.Name = "pnlSlike";
            this.pnlSlike.Size = new System.Drawing.Size(626, 637);
            this.pnlSlike.TabIndex = 0;
            // 
            // lblVrijeme
            // 
            this.lblVrijeme.AutoSize = true;
            this.lblVrijeme.BackColor = System.Drawing.Color.Transparent;
            this.lblVrijeme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVrijeme.ForeColor = System.Drawing.Color.LightCoral;
            this.lblVrijeme.Location = new System.Drawing.Point(638, 406);
            this.lblVrijeme.Name = "lblVrijeme";
            this.lblVrijeme.Size = new System.Drawing.Size(19, 20);
            this.lblVrijeme.TabIndex = 1;
            this.lblVrijeme.Text = "0";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.LightCoral;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStart.ForeColor = System.Drawing.Color.Snow;
            this.btnStart.Location = new System.Drawing.Point(602, 176);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 58);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Sa paralelizacijom";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.LightCoral;
            this.label1.Location = new System.Drawing.Point(619, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Proteklo vrijeme:";
            // 
            // btnBez
            // 
            this.btnBez.BackColor = System.Drawing.Color.LightCoral;
            this.btnBez.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBez.ForeColor = System.Drawing.Color.Snow;
            this.btnBez.Location = new System.Drawing.Point(602, 283);
            this.btnBez.Name = "btnBez";
            this.btnBez.Size = new System.Drawing.Size(151, 58);
            this.btnBez.TabIndex = 5;
            this.btnBez.Text = "Bez paralelizacije";
            this.btnBez.UseVisualStyleBackColor = false;
            this.btnBez.Click += new System.EventHandler(this.btnBez_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.LightCoral;
            this.label3.Location = new System.Drawing.Point(688, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "s";
            // 
            // frmMemorija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Memorija.Properties.Resources.glavnaPozadina3;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVrijeme);
            this.Controls.Add(this.btnBez);
            this.Controls.Add(this.pnlSlike);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(530, 400);
            this.Name = "frmMemorija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memorija";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMemorija_FormClosed);
            this.Load += new System.EventHandler(this.frmMemorija_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlSlike;
        private System.Windows.Forms.Label lblVrijeme;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBez;
        private System.Windows.Forms.Label label3;
    }
}

