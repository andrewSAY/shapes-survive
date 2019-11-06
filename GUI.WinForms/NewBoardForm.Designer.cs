namespace GUI.WinForms
{
    partial class NewBoardForm
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
            this.lWidth = new System.Windows.Forms.Label();
            this.lHeight = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tblHeight = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Location = new System.Drawing.Point(12, 20);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(46, 13);
            this.lWidth.TabIndex = 0;
            this.lWidth.Text = "Ширина";
            // 
            // lHeight
            // 
            this.lHeight.AutoSize = true;
            this.lHeight.Location = new System.Drawing.Point(13, 57);
            this.lHeight.Name = "lHeight";
            this.lHeight.Size = new System.Drawing.Size(45, 13);
            this.lHeight.TabIndex = 1;
            this.lHeight.Text = "Высота";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(73, 17);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(31, 20);
            this.tbWidth.TabIndex = 2;
            this.tbWidth.Text = "0";
            // 
            // tblHeight
            // 
            this.tblHeight.Location = new System.Drawing.Point(73, 57);
            this.tblHeight.Name = "tblHeight";
            this.tblHeight.Size = new System.Drawing.Size(31, 20);
            this.tblHeight.TabIndex = 3;
            this.tblHeight.Text = "0";
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(120, 17);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(71, 60);
            this.bOk.TabIndex = 4;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // NewBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 85);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.tblHeight);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.lHeight);
            this.Controls.Add(this.lWidth);
            this.Name = "NewBoardForm";
            this.Text = "NewBoardForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tblHeight;
        private System.Windows.Forms.Button bOk;
    }
}