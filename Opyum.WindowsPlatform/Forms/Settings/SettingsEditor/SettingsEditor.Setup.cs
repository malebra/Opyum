﻿using Opyum.WindowsPlatform.Settings;
using Opyum.Structures.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System;
using Opyum.WindowsPlatform.Forms.Settings;
using Opyum.Structures.Global;
using Opyum.Engine.Settings;
using Opyum.Structures.Playlist;
using Autofac;

namespace Opyum.WindowsPlatform.Settings
{
    public partial class SettingsEditor
    {
        public SettingsContainer NewSettings { get; set; }

        protected void Setup()
        {
            NewSettings = SettingsManager.GlobalSettings?.Clone();
            GenerateTree(NewSettings, SettingsTreeView.Nodes); ;
            SettingsTreeView.AfterSelect += UpdatePanel;
            SettingsTreeView.ExpandAll();
        }

        private void UpdatePanel(object sender, TreeViewEventArgs e)
        {
            Type panelType = null;
            Control toya;
            var panels = ContentPanel.Controls?.Cast<Control>()?.ToList();
            panels?.ForEach(p => p.Visible = false);
            
            try
            {
                panelType = Assembly.GetExecutingAssembly().GetTypes().Where(m => ((OpyumSettingsPanelElementAttribute)m.GetCustomAttribute(typeof(OpyumSettingsPanelElementAttribute)))?.Type == e.Node?.Tag?.GetType())?.FirstOrDefault();
                if (panelType != null && (toya = panels?.Where(p => panelType.IsAssignableFrom(p.GetType()))?.FirstOrDefault()) != null)
                {
                    toya.Visible = true;
                }
                else if (typeof(Control).IsAssignableFrom(panelType))
                {
                    var control = (Control)Activator.CreateInstance(panelType);
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    control.Dock = DockStyle.Fill;
                    ContentPanel.Controls.Add((Control)((ISettingsPanel)control).LoadElements());
                    if (control is IUndoRedoable)
                    {
                        this.KeyDown += ((ISettingsPanel)control).KeyPressResolve;
                    }
                }
            }
            catch (Exception ef)
            {

            }
        }

        protected void GenerateTree(object data, TreeNodeCollection parent)
        {
            if (data == null)
            {
                return;
            }
            //get all properties from the given object
            var props = new List<PropertyInfo>(data?.GetType()?.GetProperties());
            //find if the object is put in a group of objects
            new List<Attribute>(props?.Select(i => i.GetCustomAttribute(typeof(OpyumSettingsGroupAttribute))))?.Where(g => g != null)?.Select(f => ((OpyumSettingsGroupAttribute)f)?.Group)?.Distinct()?.ToList().ForEach(z => parent.Add(text: z, key: z));
            

            props.ForEach(x =>
            {
                var type = data.GetType();
                if (typeof(IEnumerable).IsAssignableFrom(type) || x.Name == "Item")
                {
                    return;
                }
                var node = new TreeNode(x.Name) { Tag = x.GetValue(data) };

                OpyumSettingsGroupAttribute group = (OpyumSettingsGroupAttribute)x.GetCustomAttribute(typeof(OpyumSettingsGroupAttribute));
                if (group != null)
                {
                     parent[group.Group]?.Nodes.Add(node);
                }
                else
                {
                    parent.Add(node); 
                }
                
                GenerateTree(node.Tag, node.Nodes);
            });
        }

    }
}
