using WordStorage.Utils;

namespace MsToDo.Models
{
    /// <summary>
    /// Represent task entity
    /// </summary>
    internal class ToDoTask : Notifier, IToDoTask
    {
        private bool _isComplite;
        private string _name;

        /// <summary>
        /// Do not create manually.
        /// Objects of this class should be creating by MsToDoModel
        /// </summary>
        public ToDoTask() { }

        /// <summary>
        /// Do not create manually.
        /// Objects of this class should be creating by MsToDoModel
        /// </summary>
        public ToDoTask(string taskName, bool isCompleted = false)
        {
            IsCompleted = isCompleted;
            Name = taskName;
        }

        public int Id { get; set; }

        public bool IsCompleted
        {
            get { return _isComplite; }
            set
            {
                _isComplite = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
