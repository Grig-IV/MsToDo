using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MsToDo.Models
{
    public class MsToDoList : IEnumerable<ToDoTask>, INotifyCollectionChanged
    {
        private ObservableCollection<ToDoTask> _taskCollection;

        private MsToDoList()
        {
            _taskCollection = new ObservableCollection<ToDoTask>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _taskCollection.CollectionChanged += value;
            remove => _taskCollection.CollectionChanged -= value;
        }

        public static MsToDoList Instance { get; } = new();

        public IReadOnlyCollection<ToDoTask> Tasks => _taskCollection;

        public void Add(ToDoTask toDoTask)
        {
            _taskCollection.Add(toDoTask);
        }
        public void Remove(ToDoTask toDoTask)
        {
            _taskCollection.Remove(toDoTask);
        }

        public IEnumerator<ToDoTask> GetEnumerator()
        {
            return _taskCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _taskCollection.GetEnumerator();
        }
    }
}
