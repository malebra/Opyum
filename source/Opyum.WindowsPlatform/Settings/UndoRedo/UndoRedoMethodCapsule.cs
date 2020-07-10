using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.WindowsPlatform.Settings
{
    public class UndoRedoMethodCapsule
    {
        public UndoRedoMethodCapsule(UndoRedoDelegate method, object editObject, object args, object caller)
        {
            Method = method;
            EditObject = editObject;
            Args = args;
            CallerObject = caller;
        }

        public delegate object UndoRedoDelegate(object obj, object args);
        public object Args { get; set; }
        public object EditObject { get; set; }
        public UndoRedoDelegate Method { get; set; }

        public object CallerObject { get; set; }
    }


}
