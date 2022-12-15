using System;
using System.Windows;
using Company.Database.Models;
using Company.Database.Modules;

namespace Company.ModalWindows;

public partial class TaskWindow : Window{
    private readonly Task _task;
    private readonly TaskDbModule _taskDbModule = new TaskDbModule();
    public TaskWindow(Task task){
        InitializeComponent();

        _task = task;
    }

    private void TaskWindow_OnLoaded(object sender, RoutedEventArgs e){
        TaskIdTxtBlock.Text = _task.TaskId.ToString();
        CreateDateTxtBlock.Text = _task.CreateDate.ToString();
        ExecuteDateTxtBlock.Text = _task.ExecuteDate.ToString();
        CompletionDateTxtBlock.Text = _task.CompletionDate.ToString();
        ContactParsonNameTxtBlock.Text = _task.ContactParsonName;
        OrganizationNameTxtBlock.Text = _task.OrganizationName;
        PriorityTxtBlock.Text = _task.Priority;
        ReceiptTxtBlock.Text = _task.Receipt;
        StatusTxtBlock.Text = _task.Status;
        ExecutorNameTxtBlock.Text = _task.ExecutorName;

        if (_task.Status.Equals("выполнено")){
            CompleteTaskBtn.IsEnabled = false;
            CompleteTaskBtn.Content = "выполнено";
        }
        else if  (_task.Status.Equals("в процессе")){
            CompleteTaskBtn.IsEnabled = true;
            CompleteTaskBtn.Content = "выполнить";
        }
        else{
            CompleteTaskBtn.IsEnabled = true;
            CompleteTaskBtn.Content = "начать делать";
        }
    }

    private void CompleteTaskBtn_OnClick(object sender, RoutedEventArgs e){
        if (_task.Status.Equals("не выполнено")){
            _taskDbModule.UpdateTaskStatus(_task.TaskId, 2);
            StatusTxtBlock.Text = "в процессе";
            CompleteTaskBtn.Content = "выполнить";
        }

        else if (_task.Status.Equals("в процессе") || StatusTxtBlock.Text == "в процессе"){
            _taskDbModule.UpdateTaskStatus(_task.TaskId, 3);
            StatusTxtBlock.Text = "выполнено";
            CompleteTaskBtn.Content = "выполнено";
            CompleteTaskBtn.IsEnabled = false;
            CompletionDateTxtBlock.Text = DateTime.Now.ToString();
        }
    }

    private void CloseBtn_OnClick(object sender, RoutedEventArgs e){
        this.DialogResult = true;
    }
}