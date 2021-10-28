using MsToDo.Models;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using WordStorage.Utils;

namespace MsToDo.ViewModels
{
    public class MsToDoViewModel : Notifier
    {
        private MsToDoModel _msToDo;
        private ICollectionView _activeTasksView;
        private ICollectionView _comletedTasksView;
        private string _addTaskInputField;
        private RelayCommand _addTaskCommand;
        private RelayCommand _deleteTaskCommand;
        private RelayCommand _changeCompleteStatusCommand;

        public MsToDoViewModel()
        {
            _msToDo = MsToDoModel.Instance;

            _addTaskInputField = string.Empty;
            
            _activeTasksView = new CollectionViewSource { Source = _msToDo.Tasks }.View;
            _activeTasksView.Filter = t => !((ToDoTask)t).IsCompleted;

            _comletedTasksView = new CollectionViewSource { Source = _msToDo.Tasks }.View;
            _comletedTasksView.Filter = t => ((ToDoTask)t).IsCompleted;

            // Set task's PropertyChanged event
            foreach (var task in _msToDo.Tasks) task.PropertyChanged += Task_PropertyChangedEventHandler;
        }

        public ICollectionView ActiveTasksView => _activeTasksView;

        public ICollectionView CompletedTasksView => _comletedTasksView;

        public ushort CompletedTasksCount => (ushort)_msToDo.Tasks.Count(t => t.IsCompleted);

        public string AddTaskInputField
        {
            get { return _addTaskInputField; }
            set
            {
                _addTaskInputField = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand AddTaskCommand
        {
            get => _addTaskCommand ??= new RelayCommand(
                execute: _ =>
                {
                    AddToDoTaskAsync(AddTaskInputField);
                    AddTaskInputField = string.Empty;  // Whipe input field after creating task
                },
                canExecute: _ => AddTaskInputField != string.Empty);
        }

        public RelayCommand DeleteTaskCommand
        {
            get => _deleteTaskCommand ??= new RelayCommand(
                execute: taskObj => _msToDo.DeleteTaskAsync((IToDoTask)taskObj));
        }

        // Command is use in ContextMenu
        public RelayCommand ChangeCompleteStatusCommand
        {
            get => _changeCompleteStatusCommand ??= new RelayCommand(
                execute: taskObj => ((IToDoTask)taskObj).IsCompleted = !((IToDoTask)taskObj).IsCompleted);
        }

        private async Task AddToDoTaskAsync(string taskName)
        {
            var task = await _msToDo.AddTaskAsync(taskName);
            task.PropertyChanged += Task_PropertyChangedEventHandler;
        }

        private void Task_PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ToDoTask.IsCompleted))
            {
                _activeTasksView.Refresh();
                _comletedTasksView.Refresh();
                NotifyPropertyChanged(nameof(CompletedTasksCount));
            }
        }
    }
}
