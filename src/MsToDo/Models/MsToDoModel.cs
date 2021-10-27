using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MsToDo.Models
{
    public class MsToDoModel : INotifyCollectionChanged
    {
        private ObservableCollection<ToDoTask> _taskCollection;
        private MsToDoContext _db;

        private MsToDoModel()
        {
            _db = new MsToDoContext("MsToDo.db");
            _taskCollection = new ObservableCollection<ToDoTask>(_db.Tasks);

            // Set task's PropertyChanged event
            foreach (var task in _taskCollection) task.PropertyChanged += Task_PropertyChangedEventHandler;
        }

        ~MsToDoModel()
        {
            _db.DisposeAsync();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _taskCollection.CollectionChanged += value;
            remove => _taskCollection.CollectionChanged -= value;
        }

        public static MsToDoModel Instance { get; } = new();

        public IReadOnlyCollection<IToDoTask> Tasks => _taskCollection;

        /// <summary>
        /// Create new ToDoTask and add it to Tasks collection
        /// </summary>
        /// <returns>IToDoTask of created task</returns>
        public async ValueTask<IToDoTask> AddTaskAsync(string taskName)
        {
            var task = new ToDoTask(taskName);
            task.PropertyChanged += Task_PropertyChangedEventHandler;

            _taskCollection.Add(task);

            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task DeleteTaskAsync(IToDoTask toDoTask)
        {
            var task = (ToDoTask)toDoTask;

            _taskCollection.Remove(task);

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        private void Task_PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e)
        {
            _db.SaveChangesAsync();
        }
    }
}
