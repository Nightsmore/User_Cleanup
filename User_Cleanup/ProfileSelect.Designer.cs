
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.prof_clear_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 32);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(302, 229);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
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
            this.Controls.Add(this.checkedListBox1);
            this.Name = "ProfileSelect";
            this.Text = "Profile Cleaner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button prof_clear_btn;
    }
}

