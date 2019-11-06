using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI.WinForms
{
    public partial class NewBoardForm : Form
    {
        private IMainForm form;

        public NewBoardForm(IMainForm _form)
        {
            this.form = _form;
            InitializeComponent();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            int width;
            int height;
            if (int.TryParse(tbWidth.Text, out width) == false) width = 0;
            if (int.TryParse(tblHeight.Text, out height) == false) height = 0;

            this.form.CreateNewBoard(width, height);
            this.Close();
        }

        
    }
}
