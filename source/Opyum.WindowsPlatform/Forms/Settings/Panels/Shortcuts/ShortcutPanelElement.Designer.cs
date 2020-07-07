namespace Opyum.WindowsPlatform.Forms.Settings
{
    partial class ShortcutPanelElement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewshortcuts = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Shortcut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameLabel = new System.Windows.Forms.Label();
            this.ShortcutTextLabel = new System.Windows.Forms.Label();
            this.textBoxShortcut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listViewshortcuts
            // 
            this.listViewshortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewshortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Action,
            this.Shortcut,
            this.Description});
            this.listViewshortcuts.FullRowSelect = true;
            this.listViewshortcuts.GridLines = true;
            this.listViewshortcuts.HideSelection = false;
            this.listViewshortcuts.Location = new System.Drawing.Point(3, 20);
            this.listViewshortcuts.MultiSelect = false;
            this.listViewshortcuts.Name = "listViewshortcuts";
            this.listViewshortcuts.Size = new System.Drawing.Size(494, 302);
            this.listViewshortcuts.TabIndex = 0;
            this.listViewshortcuts.UseCompatibleStateImageBehavior = false;
            this.listViewshortcuts.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            this.Index.Width = 51;
            // 
            // Action
            // 
            this.Action.Text = "Action";
            this.Action.Width = 144;
            // 
            // Shortcut
            // 
            this.Shortcut.Text = "Shortcut";
            this.Shortcut.Width = 119;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 400;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(4, 4);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(55, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Shortcuts:";
            // 
            // ShortcutTextLabel
            // 
            this.ShortcutTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortcutTextLabel.AutoSize = true;
            this.ShortcutTextLabel.Location = new System.Drawing.Point(273, 331);
            this.ShortcutTextLabel.Name = "ShortcutTextLabel";
            this.ShortcutTextLabel.Size = new System.Drawing.Size(50, 13);
            this.ShortcutTextLabel.TabIndex = 2;
            this.ShortcutTextLabel.Text = "Shortcut:";
            // 
            // textBoxShortcut
            // 
            this.textBoxShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortcut.Location = new System.Drawing.Point(329, 328);
            this.textBoxShortcut.Name = "textBoxShortcut";
            this.textBoxShortcut.Size = new System.Drawing.Size(168, 20);
            this.textBoxShortcut.TabIndex = 3;
            // 
            // ShortcutPanelElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxShortcut);
            this.Controls.Add(this.ShortcutTextLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.listViewshortcuts);
            this.Name = "ShortcutPanelElement";
            this.Size = new System.Drawing.Size(500, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewshortcuts;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Action;
        private System.Windows.Forms.ColumnHeader Shortcut;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Label ShortcutTextLabel;
        private System.Windows.Forms.TextBox textBoxShortcut;
    }
}
