using MsToDo.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using WordStorage.Utils;

namespace MsToDo.ViewModels
{
    public class MsToDoViewModel : Notifier
    {
        private MsToDoList _toDoList;
        private ICollectionView _activeTasksView;
        private ICollectionView _comletedTasksView;
        private string _newTaskNameField;
        private RelayCommand _createTaskCommand;

        public MsToDoViewModel()
        {
            _toDoList = MsToDoList.Instance;

            _newTaskNameField = "Add a task";

            _activeTasksView = new CollectionViewSource { Source = _toDoList }.View;
            _activeTasksView.Filter = t => !((ToDoTask)t).IsCompleted;

            _comletedTasksView = new CollectionViewSource { Source = _toDoList }.View;
            _comletedTasksView.Filter = t => ((ToDoTask)t).IsCompleted;
        }

        public ICollectionView ActiveTasksView => _activeTasksView;

        public ICollectionView CompletedTasksView => _comletedTasksView;

        public ushort CompletedTasksCount => (ushort)_toDoList.Count(t => t.IsCompleted);

        public string NewTaskNameField
        {
            get { return _newTaskNameField; }
            set
            {
                _newTaskNameField = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand CreateTaskCommand
        {
            get => _createTaskCommand ??= new RelayCommand(
                execute: _ =>
            {
                CreateTask(NewTaskNameField);
                WhipeNewTaskNameField();
            },
                canExecute: _ => NewTaskNameField != string.Empty);
        }

        private void CreateTask(string taskName)
        {
            var task = new ToDoTask(taskName);

            task.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(ToDoTask.IsCompleted))
                {
                    _activeTasksView.Refresh();
                    _comletedTasksView.Refresh();
                    NotifyPropertyChanged(nameof(CompletedTasksCount));
                }
            };

            _toDoList.Add(task);
        }

        private void WhipeNewTaskNameField()
        {
            NewTaskNameField = string.Empty;
        }
    }
}
