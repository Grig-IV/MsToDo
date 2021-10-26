using MsToDo.ViewModels;
using System.Windows;

namespace MsToDo
{
    public partial class MsToDoView : Window
    {
        public MsToDoView()
        {
            InitializeComponent();
            DataContext = new MsToDoViewModel();
        }
    }
}
