namespace GUI.WinForms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newBoard = new System.Windows.Forms.ToolStripMenuItem();
            this.addShape = new System.Windows.Forms.ToolStripMenuItem();
            this.addCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.addTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.addSquare = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.nexStep = new System.Windows.Forms.Button();
            this.boardPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1392, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem
            // 
            this.menuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBoard,
            this.addShape,
            this.exit});
            this.menuItem.Name = "menuItem";
            this.menuItem.Size = new System.Drawing.Size(48, 20);
            this.menuItem.Text = "Меню";
            // 
            // newBoard
            // 
            this.newBoard.Name = "newBoard";
            this.newBoard.Size = new System.Drawing.Size(164, 22);
            this.newBoard.Text = "Новая доска";
            this.newBoard.Click += new System.EventHandler(this.NewBoard_Click);
            // 
            // addShape
            // 
            this.addShape.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCircle,
            this.addTriangle,
            this.addSquare});
            this.addShape.Name = "addShape";
            this.addShape.Size = new System.Drawing.Size(164, 22);
            this.addShape.Text = "Добавить фигуру";
            // 
            // addCircle
            // 
            this.addCircle.Name = "addCircle";
            this.addCircle.Size = new System.Drawing.Size(152, 22);
            this.addCircle.Text = "Круг";
            this.addCircle.Click += new System.EventHandler(this.AddCircle_Click);
            // 
            // addTriangle
            // 
            this.addTriangle.Name = "addTriangle";
            this.addTriangle.Size = new System.Drawing.Size(152, 22);
            this.addTriangle.Text = "Треугольник";
            this.addTriangle.Click += new System.EventHandler(this.AddTriangle_Click);
            // 
            // addSquare
            // 
            this.addSquare.Name = "addSquare";
            this.addSquare.Size = new System.Drawing.Size(152, 22);
            this.addSquare.Text = "Квадрат";
            this.addSquare.Click += new System.EventHandler(this.AddSquare_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(164, 22);
            this.exit.Text = "Выход";
            this.exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // nexStep
            // 
            this.nexStep.Location = new System.Drawing.Point(0, 707);
            this.nexStep.Name = "nexStep";
            this.nexStep.Size = new System.Drawing.Size(1392, 23);
            this.nexStep.TabIndex = 1;
            this.nexStep.Text = "Следующий шаг";
            this.nexStep.UseVisualStyleBackColor = true;
            this.nexStep.Click += new System.EventHandler(this.NexStep_Click);
            // 
            // boardPanel
            // 
            this.boardPanel.Location = new System.Drawing.Point(0, 28);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(1392, 673);
            this.boardPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 733);
            this.Controls.Add(this.boardPanel);
            this.Controls.Add(this.nexStep);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem;
        private System.Windows.Forms.ToolStripMenuItem newBoard;
        private System.Windows.Forms.ToolStripMenuItem addShape;
        private System.Windows.Forms.ToolStripMenuItem addCircle;
        private System.Windows.Forms.ToolStripMenuItem addTriangle;
        private System.Windows.Forms.ToolStripMenuItem addSquare;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.Button nexStep;
        private System.Windows.Forms.Panel boardPanel;
        public System.Windows.Forms.Panel BoardPanel
        {
            get
            {
                return this.boardPanel;
            }
        }
    }
}

