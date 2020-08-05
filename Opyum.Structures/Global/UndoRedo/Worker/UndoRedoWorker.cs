using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Opyum.Structures.Global
{
    public abstract class UndoRedoWorker<T> : IUndoRedoWorker<T>, IUndoRedoWorker
    {
        protected virtual UndoRedoStack _stack { get; set; }
        public virtual T Item { get; protected set; }

        public virtual T PreviousValue { get; protected set; }

        object IUndoRedoWorker.Item { get => Item; }

        object IUndoRedoWorker.PreviousValue { get => PreviousValue; }


        protected UndoRedoWorker() { }
        public UndoRedoWorker(T item, T newValue) : this()
        {
            //OldItem = oldItem;
            //NewItem = newItem;
        }

        public abstract void Do(T oldItem, T newItem);

        public abstract void Undo();

        public abstract void Redo();

        void IUndoRedoWorker.Do(object oldItem, object newItem)
        {
            Do((T)oldItem, (T)newItem);
        }
    }
}
