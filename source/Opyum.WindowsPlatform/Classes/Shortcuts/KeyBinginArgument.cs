using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Opyum.WindowsPlatform
{
    public class KeyBindingArgument : IKeyBindingArgument
    {
        public static List<IKeyBindingArgument> All { get; private set; } = new List<IKeyBindingArgument>();



        public string Command { get; private set; }

        public Keys Shortcut { get; private set; } = 0;

        public string ShortcutString { get; private set; } = string.Empty;

        public delegate void DELL();
        private DELL Function;



        public KeyBindingArgument()
        {
            All.Add(this);
            All = All.OrderBy(t => t.Command).ToList();
        }


        public bool Match(KeyEventArgs e)
        {
            return e.KeyData == Shortcut ? true : false;
        }

        public void UpdateFromXML(XmlNode node)
        {
            string n = string.Empty;
            if (node.Name == "Shortcut")
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "Command")
                    {
                        n = childNode.InnerText;
                    }
                    else if (childNode.Name == "Keys" && n == this.Command)
                    {
                        //Sets the keys
                        KeysConverter kc = new System.Windows.Forms.KeysConverter();
                        Shortcut = 0;

                        string text = childNode.InnerText.Contains("++") ? childNode.InnerText.Replace("++", "+\\plus") : childNode.InnerText;

                        foreach (string key in text.Split('+'))
                        {
                            try
                            {
                                ShortcutString += ShortcutString == string.Empty ? key : $" + {key}";
                                Shortcut |= ((System.Windows.Forms.Keys)kc.ConvertFromString(key == "\\plus" ? "+" : key));
                            }
                            catch
                            {

                            }
                        }
                    }
                }


            }
        }
        
        public void AddFunction(Delegate func)
        {
            Function = (DELL)func;
            
        }

        public void AddFunction(DELL func)
        {
            Function = func;
            
        }

        public void Run()
        {
            Function?.Invoke();
        }

        public void DefaultShortcut()
        {
            Shortcut = Keys.None;
            ShortcutString = string.Empty;
        }



        #region Disposable

        public void Destroy()
        {
            All.Remove(this);
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Shortcut = Keys.None;
                this.ShortcutString = null;
                this.Command = null;

                return;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion




        #region Static functions

        public static IKeyBindingArgument CreateFromXML(XmlNode node)
        {
            var temp = new KeyBindingArgument();
            //Checks if the given node is valid
            if (node.Name == "Shortcut")
            {
                //Finds the Keys & Command nodes inside the Shortcut node
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "Command")
                    {

                        //Checks if the KeyBingingArgument akready exists, and if it does, it updates it and breaks out of the function
                        IKeyBindingArgument P = KeyBindingArgument.FindByCommand(childNode.InnerText);
                        if (P != null)
                        {
                            //Disposes of the temp Argument and updates already existing one
                            temp.Destroy();
                            P.UpdateFromXML(node);
                            return P;
                        }

                        //Updates the Command field of the Argument
                        temp.Command = childNode.InnerText;
                    }
                    else if (childNode.Name == "Keys")
                    {
                        //Sets the keys
                        KeysConverter kc = new System.Windows.Forms.KeysConverter();
                        temp.Shortcut = 0;


                        string text = childNode.InnerText.Contains("++") ? childNode.InnerText.Replace("++", "+\\plus") : childNode.InnerText;

                        foreach (string key in text.Split('+'))
                        {
                            try
                            {
                                temp.ShortcutString += temp.ShortcutString == string.Empty ? key : $" + {key}";
                                temp.Shortcut |= (System.Windows.Forms.Keys)kc.ConvertFromString(key == "\\plus" ? "+" : key);
                            }
                            catch
                            {

                            }
                        }
                    }
                }


            }
            return temp;
        }

        public static IKeyBindingArgument FindByCommand(string command) => KeyBindingArgument.All.Where(x => x.Command == command).FirstOrDefault();

        public static void AllBindingsSetup(XmlNode temp)
        {
            KeyBindingArgument.All.ForEach(kb => kb.DefaultShortcut());

            XmlNode node = null;

            temp = temp.FirstChild;

            if (temp.Name == "Settings")
            {
                foreach (XmlNode n in temp.ChildNodes)
                {
                    if (n.Name == "Shortcuts")
                    {
                        node = n;
                    }
                }
            }

            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name == "Shortcut")
                {
                    KeyBindingArgument.CreateFromXML(n);
                }
            }
            
        }
        

        #endregion
    }
}
