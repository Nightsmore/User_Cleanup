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
    public partial class ProfileSelect : Form
    {
        public ProfileSelect()
        {
            InitializeComponent();
            checkedListBox1.Items.AddRange(Program.indexItems);
        }

        // I'll want the ranges to disable the admin profile and probably "Default" 
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void prof_clear_btn_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            CheckedListBox.CheckedItemCollection coll;
            coll = checkedListBox1.CheckedItems;

            foreach(var item in coll)
            {
                sb.Append($"{item.ToString()}, ");
            }
            MessageBox.Show(sb.ToString());
        }
    }
}
