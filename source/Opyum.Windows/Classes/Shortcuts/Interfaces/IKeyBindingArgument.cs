using System;
using System.Windows.Forms;
using System.Xml;

namespace Opyum.Windows
{
    public interface IKeyBindingArgument
    {
        string Command { get; }
        Keys Shortcut { get; }

        void AddFunction(Delegate func);
        void AddFunction(KeyBindingArgument.DELL func);
        void Destroy();
        void Dispose();
        bool Match(KeyEventArgs e);
        void Run();
        void UpdateFromXML(XmlNode node);
    }
}