using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opyum.WindowsPlatform.Settings
{
    public class UndoRedoStack
    {
        protected Stack<List<UndoRedoMethodCapsule>> UndoStack { get; set; } = new Stack<List<UndoRedoMethodCapsule>>();
        protected Stack<List<UndoRedoMethodCapsule>> RedoStack { get; set; } = new Stack<List<UndoRedoMethodCapsule>>();

        public void Undo()
        {
            if (UndoStack.Count > 0)
            {
                var last = UndoStack.Pop();
                if (last.Count > 0)
                {
                    RedoStack.Push(new List<UndoRedoMethodCapsule>(last?.Reverse<UndoRedoMethodCapsule>()?.Select(z => new UndoRedoMethodCapsule(z.Method, z.EditObject, ExecuteMethod(z), z.CallerObject)))); 
                }
            }

        }

        public void Redo()
        {
            if (RedoStack.Count > 0)
            {
                var last = UndoStack.Pop();
                if (last.Count > 0)
                {
                    UndoStack.Push(new List<UndoRedoMethodCapsule>(last?.Reverse<UndoRedoMethodCapsule>()?.Select(z => new UndoRedoMethodCapsule(z.Method, z.EditObject ,ExecuteMethod(z), z.CallerObject))));
                }
            }
        }

        public void ClearRedo()
        {
            RedoStack.Clear();
        }

        public void Do(UndoRedoMethodCapsule.UndoRedoDelegate method, object editObject, object args, object caller = null)
        {
            try
            {
                var list = new List<UndoRedoMethodCapsule>();
                list.Add(new UndoRedoMethodCapsule(method, editObject, ExecuteMethod(method, editObject, args, caller), caller));
                UndoStack.Push(list);
                if (RedoStack.Count > 0)
                {
                    ClearRedo();
                }
            }
            catch (Exception e)
            {

            }
        }
        public void Do(UndoRedoMethodCapsule work)
        {
            try
            {
                work.Args = ExecuteMethod(work);
                var list = new List<UndoRedoMethodCapsule>();
                list.Add(work);
                UndoStack.Push(list);
                if (RedoStack.Count > 0)
                {
                    ClearRedo();
                }
            }
            catch (Exception e)
            {

            }
        }


        public void DoMany(List<UndoRedoMethodCapsule> worklist)
        {
            try
            {
                if (worklist != null)
                {
                    UndoStack.Push(worklist.Select(a => new UndoRedoMethodCapsule(a.Method, a.EditObject, ExecuteMethod(a), a.CallerObject)).ToList());
                }
            }
            catch (Exception e)
            {

            }
        }

        private object ExecuteMethod(UndoRedoMethodCapsule.UndoRedoDelegate method, object editObject, object args, object caller = null)
        {
            return ((UndoRedoMethodCapsule.UndoRedoDelegate)Delegate.CreateDelegate(typeof(UndoRedoMethodCapsule.UndoRedoDelegate), caller, method.Method)).Invoke(editObject, args);
        }

        private object ExecuteMethod(UndoRedoMethodCapsule item)
        {
            return ExecuteMethod(item.Method, item.EditObject, item.Args, item.CallerObject);
        }

    }
}
