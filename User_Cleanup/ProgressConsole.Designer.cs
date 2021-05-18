
namespace User_Cleanup
{
    partial class ProgressOutput
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
            this.progTextBox = new System.Windows.Forms.RichTextBox();
            this.profileRemoveProgressBar = new System.Windows.Forms.ProgressBar();
            this.finBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progTextBox
            // 
            this.progTextBox.Location = new System.Drawing.Point(12, 12);
            this.progTextBox.Name = "progTextBox";
            this.progTextBox.ReadOnly = true;
            this.progTextBox.Size = new System.Drawing.Size(483, 301);
            this.progTextBox.TabIndex = 0;
            this.progTextBox.Text = "";
            // 
            // profileRemoveProgressBar
            // 
            this.profileRemoveProgressBar.Location = new System.Drawing.Point(13, 319);
            this.profileRemoveProgressBar.Name = "profileRemoveProgressBar";
            this.profileRemoveProgressBar.Size = new System.Drawing.Size(482, 23);
            this.profileRemoveProgressBar.TabIndex = 1;
            this.profileRemoveProgressBar.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // finBtn
            // 
            this.finBtn.Enabled = false;
            this.finBtn.Location = new System.Drawing.Point(420, 348);
            this.finBtn.Name = "finBtn";
            this.finBtn.Size = new System.Drawing.Size(75, 23);
            this.finBtn.TabIndex = 2;
            this.finBtn.Text = "Finish";
            this.finBtn.UseCompatibleTextRendering = true;
            this.finBtn.UseVisualStyleBackColor = true;
            // 
            // ProgressOutput
            // 
            this.ClientSize = new System.Drawing.Size(507, 374);
            this.Controls.Add(this.finBtn);
            this.Controls.Add(this.profileRemoveProgressBar);
            this.Controls.Add(this.progTextBox);
            this.Name = "ProgressOutput";
            this.Load += new System.EventHandler(this.ProgressOutput_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.RichTextBox progressTextBox;
        private System.Windows.Forms.RichTextBox progTextBox;
        private System.Windows.Forms.ProgressBar profileRemoveProgressBar;
        private System.Windows.Forms.Button finBtn;
    }
}