
namespace User_Cleanup
{
    partial class ProfileSelect
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
            this.profileList_checkbox = new System.Windows.Forms.CheckedListBox();
            this.prof_clear_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // profileList_checkbox
            // 
            this.profileList_checkbox.FormattingEnabled = true;
            this.profileList_checkbox.Location = new System.Drawing.Point(12, 32);
            this.profileList_checkbox.Name = "profileList_checkbox";
            this.profileList_checkbox.Size = new System.Drawing.Size(302, 229);
            this.profileList_checkbox.TabIndex = 0;
            this.profileList_checkbox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // prof_clear_btn
            // 
            this.prof_clear_btn.Location = new System.Drawing.Point(213, 267);
            this.prof_clear_btn.Name = "prof_clear_btn";
            this.prof_clear_btn.Size = new System.Drawing.Size(101, 23);
            this.prof_clear_btn.TabIndex = 1;
            this.prof_clear_btn.Text = "Clear Profiles";
            this.prof_clear_btn.UseVisualStyleBackColor = true;
            this.prof_clear_btn.Click += new System.EventHandler(this.prof_clear_btn_Click);
            // 
            // ProfileSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 298);
            this.Controls.Add(this.prof_clear_btn);
            this.Controls.Add(this.profileList_checkbox);
            this.Name = "ProfileSelect";
            this.Text = "Profile Cleaner";
            this.Load += new System.EventHandler(this.ProfileSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox profileList_checkbox;
        private System.Windows.Forms.Button prof_clear_btn;
    }
}

