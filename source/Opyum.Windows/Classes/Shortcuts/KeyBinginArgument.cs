using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Opyum.Windows
{
    public class KeyBindingArgument : IKeyBindingArgument
    {
        public static List<IKeyBindingArgument> All { get; private set; } = new List<IKeyBindingArgument>();
        


        public string Command { get; private set; }

        public Keys Shortcut { get; private set; } = 0;

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
                                Shortcut |= ((System.Windows.Forms.Keys)kc.ConvertFromString(key == "\\plus" ? "+" : key));
                            }
                            catch
                            {

                            }
                        }


                        //List<string> KeyList = node.InnerText.Split('+').ToList();

                        //KeysConverter kc = new System.Windows.Forms.KeysConverter();

                        //KeyList.ForEach((x) =>
                        //{
                        //    try
                        //    {
                        //        Keys.Add((System.Windows.Forms.Keys)kc.ConvertFromString(x));
                        //    }
                        //    catch
                        //    {

                        //    }
                        //});
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



        #region Disposable

        public void Destroy()
        {
            All.Remove(this);
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {

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
