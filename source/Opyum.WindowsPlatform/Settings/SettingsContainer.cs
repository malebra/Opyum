using System;
using System.Collections.Generic;
using Opyum.Structures.Attributes;

namespace Opyum.WindowsPlatform.Settings
{
    public class SettingsContainer : IDisposable
    {
        [OpyumSettingsGroup("General")]
        public List<ShortcutKeyBinding> Shortcuts { get; set; } = null;
        [OpyumSettingsGroup("Appearance")]
        public List<ColorContainer> Colors { get; set; } = null;




        ~SettingsContainer()
        {
            this.Dispose();
        }

        /// <summary>
        /// Get the value of a property by giving the properties name
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public object this[string property]
        {
            get => this.GetType().GetProperty(property)?.GetValue(this);
            set { if (value?.GetType() == this.GetType().GetProperty(property).PropertyType || value == null) this.GetType().GetProperty(property).SetValue(this, value); }
        }


        #region Disposing

        public void Dispose()
        {
            Dispose(true);
            GC.Collect();
        }

        private void Dispose(bool disposing)
        {
            if (disposing && Shortcuts != null)
            {
                foreach (var item in Shortcuts)
                {
                    item?.Dispose();
                }
                Shortcuts = null;
            }
        } 
        #endregion
    }

}
