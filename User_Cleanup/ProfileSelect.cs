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
            profileList_checkbox.DisplayMember = "ProfileName";
            profileList_checkbox.Items.AddRange(Program.profileList.ToArray());    
            
        }

        // I'll want the ranges to disable the admin profile and probably "Default" 
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void prof_clear_btn_Click(object sender, EventArgs e)
        {
            ProgressOutput progOut = new ProgressOutput();
            progOut.Activate();
            progOut.Visible = true;

            progOut.ProfileProccessBar.Style = ProgressBarStyle.Continuous;
            
            // display message box for confirmation
            var mb = MessageBox.Show($"Are you sure you wish to delete the selected profiles: {profileList_checkbox.CheckedItems}?", "Alert!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            
            if(mb == DialogResult.Yes)
            {
                progOut.WindowConsole += "Removing profiles...\n";
                StringBuilder sb = new System.Text.StringBuilder();
                CheckedListBox.CheckedItemCollection coll;
                coll = profileList_checkbox.CheckedItems;

                // Convert this to asyncronous tasks
                // Add tasks to a list and get object totals 
                // set total to max length for progbar
                
                foreach (var item in coll)
                {
                    progOut.WindowConsole += $"processing {item}";
                    //Program.DeleteProfile((Profile)item);
                }
            }

            progOut.CloseButtonEnabled = true;
            
        }

        

        private void ProfileSelect_Load(object sender, EventArgs e)
        {
           
        }
    }
}
