using System;
using System.Windows.Forms;
using System.Xml;

namespace Opyum.WindowsPlatform
{
    public interface IKeyBindingArgument : IDisposable
    {
        string Command { get; }
        Keys Shortcut { get; }
        string ShortcutString { get; }

        void AddFunction(Delegate func);
        void AddFunction(KeyBindingArgument.DELL func);
        void Destroy();
        bool Match(KeyEventArgs e);
        void Run();
        void UpdateFromXML(XmlNode node);
        void DefaultShortcut();
    }
}