namespace Opyum.WindowsPlatform.Settings
{
    partial class SettingsForm
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
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.SettingsTreeView = new System.Windows.Forms.TreeView();
            this.SearchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ContentPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(205, 1);
            this.ContentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(578, 459);
            this.ContentPanel.TabIndex = 0;
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this.SettingsTreeView);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchPanel.Location = new System.Drawing.Point(1, 1);
            this.SearchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(204, 459);
            this.SearchPanel.TabIndex = 1;
            // 
            // SettingsTreeView
            // 
            this.SettingsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTreeView.Location = new System.Drawing.Point(0, 0);
            this.SettingsTreeView.Name = "SettingsTreeView";
            this.SettingsTreeView.Size = new System.Drawing.Size(204, 459);
            this.SettingsTreeView.TabIndex = 0;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.SearchPanel);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Settings";
            this.SearchPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.TreeView SettingsTreeView;
    }
}