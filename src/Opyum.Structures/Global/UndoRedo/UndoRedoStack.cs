using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Structures.Global
{
    public class UndoRedoStack
    {
        public Stack<List<IUndoRedoWorker>> UndoStack { get; protected set; } = new Stack<List<IUndoRedoWorker>>();
        public Stack<List<IUndoRedoWorker>> RedoStack { get; protected set; } = new Stack<List<IUndoRedoWorker>>();

        /// <summary>
        /// Returns the number of elements in the undo stack
        /// </summary>
        public int UndoCount { get => UndoStack.Count; }
        /// <summary>
        /// Returns the number of elements in the redo stack
        /// </summary>
        public int RedoCount { get => RedoStack.Count; }

        //event called upon a change in the undo stack
        public event EventHandler Changed;


        public void Redo()
        {
            try
            {
                if (RedoStack.Count == 0)
                {
                    return;
                }
                var work = RedoStack.Pop();
                work.Reverse();
                foreach (var item in work)
                {
                    item.Redo();
                }
                UndoStack.Push(work);
                Changed?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Undo()
        {
            try
            {
                if (UndoStack.Count == 0)
                {
                    return;
                }
                var work = UndoStack.Pop();
                work.Reverse();
                foreach (var item in work)
                {
                    item.Undo();
                }
                RedoStack.Push(work);
                Changed?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Do(IUndoRedoWorker work)
        {
            try
            {
                if (RedoCount > 0)
                {
                    RedoStack.Clear();
                }
                UndoStack.Push(new List<IUndoRedoWorker>(new[] { work }));
                Changed?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void DoMany(List<IUndoRedoWorker> work)
        {
            try
            {
                if (RedoCount > 0)
                {
                    RedoStack.Clear();
                }
                UndoStack.Push(work);
                Changed?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
            Changed?.Invoke(this, new EventArgs());
        }
    }
}
