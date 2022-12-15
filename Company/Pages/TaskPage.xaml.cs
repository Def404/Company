using System.Windows;
using System.Windows.Controls;
using Company.Database.Models;
using Company.Database.Modules;
using Company.ModalWindows;

namespace Company.Pages;

public partial class TaskPage : UserControl{
    private readonly TaskDbModule _taskDbModule = new TaskDbModule();
    
    public TaskPage(){
        InitializeComponent();
    }

    private void TaskPage_OnLoaded(object sender, RoutedEventArgs e){
        if (MainWindow._employee.PositionName.Equals("worker")){
            AddTaskBtn.Visibility = Visibility.Hidden;
        }
        TasksDataGrid.ItemsSource = _taskDbModule.GetTasks();
    }

    private void InfoTaskBtn_OnClick(object sender, RoutedEventArgs e){
        if (TasksDataGrid.SelectedIndex != -1){
            var task = (Task)TasksDataGrid.SelectedItem;

            TaskWindow taskWindow = new TaskWindow(task);
            if (taskWindow.ShowDialog() == true){
                TasksDataGrid.ItemsSource = _taskDbModule.GetTasks();
            }
        }
    }

    private void AddTaskBtn_OnClick(object sender, RoutedEventArgs e){
        AddTaskWindow addTaskWindow = new AddTaskWindow();
        if (addTaskWindow.ShowDialog() == true){
            TasksDataGrid.ItemsSource = _taskDbModule.GetTasks();
        }
    }
}