﻿using System;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;


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
            this.listviewshortcuts = new System.Windows.Forms.ListView();
            this.Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Shortcut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Global = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Disabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameLabel = new System.Windows.Forms.Label();
            this.ShortcutTextLabel = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonClearSearch = new System.Windows.Forms.Button();
            this.buttonSaveShortcut = new System.Windows.Forms.Button();
            this.isGlobalCheckBox = new System.Windows.Forms.CheckBox();
            this.isDisabledCheckBox = new System.Windows.Forms.CheckBox();
            this.textBoxShortcut = new Opyum.WindowsPlatform.Settings.Panels.CustomPanelControls.TextBoxForShortcutCapture();
            this.clearButtonShortcut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listviewshortcuts
            // 
            this.listviewshortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewshortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Action,
            this.Shortcut,
            this.Global,
            this.Disabled,
            this.Description});
            this.listviewshortcuts.FullRowSelect = true;
            this.listviewshortcuts.GridLines = true;
            this.listviewshortcuts.HideSelection = false;
            this.listviewshortcuts.Location = new System.Drawing.Point(3, 32);
            this.listviewshortcuts.MultiSelect = false;
            this.listviewshortcuts.Name = "listviewshortcuts";
            this.listviewshortcuts.Size = new System.Drawing.Size(494, 302);
            this.listviewshortcuts.TabIndex = 3;
            this.listviewshortcuts.UseCompatibleStateImageBehavior = false;
            this.listviewshortcuts.View = System.Windows.Forms.View.Details;
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
            // Global
            // 
            this.Global.Text = "Global";
            // 
            // Disabled
            // 
            this.Disabled.Text = "Disabled";
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 400;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(5, 8);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(5);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(55, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Shortcuts:";
            // 
            // ShortcutTextLabel
            // 
            this.ShortcutTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShortcutTextLabel.AutoSize = true;
            this.ShortcutTextLabel.Location = new System.Drawing.Point(5, 343);
            this.ShortcutTextLabel.Margin = new System.Windows.Forms.Padding(5);
            this.ShortcutTextLabel.Name = "ShortcutTextLabel";
            this.ShortcutTextLabel.Size = new System.Drawing.Size(50, 13);
            this.ShortcutTextLabel.TabIndex = 0;
            this.ShortcutTextLabel.Text = "Shortcut:";
            // 
            // labelSearch
            // 
            this.labelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(118, 8);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(44, 13);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Search:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(168, 5);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(248, 20);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChange);
            // 
            // buttonClearSearch
            // 
            this.buttonClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearSearch.Location = new System.Drawing.Point(422, 4);
            this.buttonClearSearch.Name = "buttonClearSearch";
            this.buttonClearSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonClearSearch.TabIndex = 2;
            this.buttonClearSearch.Text = "Clear";
            this.buttonClearSearch.UseVisualStyleBackColor = true;
            this.buttonClearSearch.Click += new System.EventHandler(this.buttonClearSearch_Click);
            // 
            // buttonSaveShortcut
            // 
            this.buttonSaveShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveShortcut.Location = new System.Drawing.Point(237, 338);
            this.buttonSaveShortcut.Name = "buttonSaveShortcut";
            this.buttonSaveShortcut.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveShortcut.TabIndex = 5;
            this.buttonSaveShortcut.Text = "Save";
            this.buttonSaveShortcut.UseVisualStyleBackColor = true;
            this.buttonSaveShortcut.Click += new System.EventHandler(this.buttonSaveShortcut_Click);
            // 
            // isGlobalCheckBox
            // 
            this.isGlobalCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isGlobalCheckBox.AutoSize = true;
            this.isGlobalCheckBox.Enabled = false;
            this.isGlobalCheckBox.Location = new System.Drawing.Point(63, 366);
            this.isGlobalCheckBox.Name = "isGlobalCheckBox";
            this.isGlobalCheckBox.Size = new System.Drawing.Size(56, 17);
            this.isGlobalCheckBox.TabIndex = 7;
            this.isGlobalCheckBox.Text = "Global";
            this.isGlobalCheckBox.UseVisualStyleBackColor = true;
            // 
            // isDisabledCheckBox
            // 
            this.isDisabledCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.isDisabledCheckBox.AutoSize = true;
            this.isDisabledCheckBox.Enabled = false;
            this.isDisabledCheckBox.Location = new System.Drawing.Point(125, 366);
            this.isDisabledCheckBox.Name = "isDisabledCheckBox";
            this.isDisabledCheckBox.Size = new System.Drawing.Size(67, 17);
            this.isDisabledCheckBox.TabIndex = 8;
            this.isDisabledCheckBox.Text = "Disabled";
            this.isDisabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBoxShortcut
            // 
            this.textBoxShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxShortcut.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxShortcut.Location = new System.Drawing.Point(63, 340);
            this.textBoxShortcut.Name = "textBoxShortcut";
            this.textBoxShortcut.ReadOnly = true;
            this.textBoxShortcut.Size = new System.Drawing.Size(168, 20);
            this.textBoxShortcut.TabIndex = 4;
            this.textBoxShortcut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.getShortcut);
            // 
            // clearButtonShortcut
            // 
            this.clearButtonShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButtonShortcut.Location = new System.Drawing.Point(318, 338);
            this.clearButtonShortcut.Name = "clearButtonShortcut";
            this.clearButtonShortcut.Size = new System.Drawing.Size(75, 23);
            this.clearButtonShortcut.TabIndex = 6;
            this.clearButtonShortcut.Text = "Clear";
            this.clearButtonShortcut.UseVisualStyleBackColor = true;
            this.clearButtonShortcut.Click += new System.EventHandler(this.buttonClearShortcut_Click);
            // 
            // ShortcutPanelElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.isDisabledCheckBox);
            this.Controls.Add(this.isGlobalCheckBox);
            this.Controls.Add(this.clearButtonShortcut);
            this.Controls.Add(this.buttonSaveShortcut);
            this.Controls.Add(this.buttonClearSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxShortcut);
            this.Controls.Add(this.ShortcutTextLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.listviewshortcuts);
            this.Name = "ShortcutPanelElement";
            this.Size = new System.Drawing.Size(500, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listviewshortcuts;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ColumnHeader Action;
        private System.Windows.Forms.ColumnHeader Shortcut;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Label ShortcutTextLabel;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonClearSearch;
        private System.Windows.Forms.Button buttonSaveShortcut;
        private WindowsPlatform.Settings.Panels.CustomPanelControls.TextBoxForShortcutCapture textBoxShortcut;
        private System.Windows.Forms.CheckBox isGlobalCheckBox;
        private System.Windows.Forms.CheckBox isDisabledCheckBox;
        private System.Windows.Forms.ColumnHeader Global;
        private System.Windows.Forms.ColumnHeader Disabled;
        private System.Windows.Forms.Button clearButtonShortcut;
    }
}
