using System.Collections.Generic;

namespace Opyum.Structures.Global
{
    public interface IUndoRedoWorker<T> : IUndoRedoable
    {
        void Do(T oldItem, T newItem);
        T Item { get; }
        T PreviousValue { get; }
    }

    public interface IUndoRedoWorker : IUndoRedoable
    {
        void Do(object oldItem, object newItem);


        object Item { get; }
        object PreviousValue { get; }
    }
}