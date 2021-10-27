using System.ComponentModel;

namespace MsToDo.Models
{
    public interface IToDoTask
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsCompleted { get; set; }

        public string Name { get; set; }
    }
}