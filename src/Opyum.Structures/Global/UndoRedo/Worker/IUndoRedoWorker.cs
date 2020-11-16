using System.Collections.Generic;

namespace Opyum.Structures.Global
{
    public interface IUndoRedoWorker<T> : IUndoRedoable
    {
        void Do(T item, T newValue);
        T Item { get; }
        T PreviousValue { get; }
    }

    public interface IUndoRedoWorker : IUndoRedoable
    {
        void Do(object item, object newValue);


        object Item { get; }
        object PreviousValue { get; }
    }
}