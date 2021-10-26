using WordStorage.Utils;

namespace MsToDo.Models
{
    public class ToDoTask : Notifier
    {
        private bool _isComplite;
        private string _name;

        public ToDoTask(string taskName, bool isCompleted = false)
        {
            IsCompleted = isCompleted;
            Name = taskName;
        }

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
