using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Cleanup
{
    public partial class ProgressOutput : Form
    {
        public ProgressOutput()
        {
            InitializeComponent();
        }

        public string WindowConsole
        {
            get { return progTextBox.Text; }
            set { progTextBox.Text = value; }
        }

        public bool CloseButtonEnabled
        {
            set { finBtn.Enabled = value; }
        }

        public ProgressBar ProfileProccessBar
        {
            get { return profileRemoveProgressBar; }
        }

        private void progOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void ProgressOutput_Load(object sender, EventArgs e)
        {

        }
    }
}
