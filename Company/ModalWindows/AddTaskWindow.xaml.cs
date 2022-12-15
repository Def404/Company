using System;
using System.Windows;
using System.Windows.Controls;
using Company.Database.Models;
using Company.Database.Modules;

namespace Company.ModalWindows;

public partial class AddTaskWindow : Window{
    private readonly TaskStatusDbModule _taskStatusDbModule = new TaskStatusDbModule();
    private readonly OrganizationDbModule _organizationDbModule = new OrganizationDbModule();
    private readonly ContractDbModule _contractDbModule = new ContractDbModule();
    private readonly HardDriveDbModule _hardDriveDbModule = new HardDriveDbModule();
    private readonly ContactPersonDbModule _contactPersonDbModule = new ContactPersonDbModule();
    private readonly TaskReceiptDbModule _taskReceiptDbModule = new TaskReceiptDbModule();
    private readonly TaskPriorityDbModule _taskPriorityDbModule = new TaskPriorityDbModule();
    private readonly EmployeeDbModule _employeeDbModule = new EmployeeDbModule();
    private readonly TaskDbModule _taskDbModule = new TaskDbModule();
    public AddTaskWindow(){
        InitializeComponent();
    }

    private void AddTaskWindow_OnLoaded(object sender, RoutedEventArgs e){
        StatusComboBox.ItemsSource = _taskStatusDbModule.GetTaskStatusList();
        OrganizationComboBox.ItemsSource = _organizationDbModule.GetOrganizationList();
        ContractIdComboBox.ItemsSource = _contractDbModule.GetContractList();
        SerialNumberComboBox.ItemsSource = _hardDriveDbModule.GetHardDriveList();
        ReceiptComboBox.ItemsSource = _taskReceiptDbModule.GetTaskReceiptList();
        PriorityComboBox.ItemsSource = _taskPriorityDbModule.GetTaskPriorityList();
        ExecutorNameComboBox.ItemsSource = _employeeDbModule.GetEmployeeList();

    }

    private void OrganizationComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e){
        var organization = (Organization)OrganizationComboBox.SelectedItem;
        if (organization is not null){
            var organizationId = organization.Id;
            ContactParsonNameComboBox.ItemsSource = _contactPersonDbModule.GetContactPersonList(organizationId);
        }
    }

    private void AddTaskBtn_OnClick(object sender, RoutedEventArgs e){
        if (ContactParsonNameComboBox.SelectedIndex == -1 || PriorityComboBox.SelectedIndex == -1 ||
            ReceiptComboBox.SelectedIndex == -1 || StatusComboBox.SelectedIndex == -1){
            MessageBox.Show("Не все нужные поля заполнены");
            return;
        }

        if (ContractIdComboBox.SelectedIndex != -1 && SerialNumberComboBox.SelectedIndex == -1){
            MessageBox.Show("Выберите серийный номер устройства");
            return;
        }

        int executionPeriod = 0;
        if (ExecuteDateTxtBlock.Text.Trim() != ""){
            try{
                executionPeriod = Int32.Parse(ExecuteDateTxtBlock.Text.Trim());
            }
            catch (Exception exception){
                MessageBox.Show("период введен не правильно");
                return;
            }
        }
        else{
            executionPeriod = 0;
        }

        if (executionPeriod <= 0){
            executionPeriod = 0;
        }

        int? executorId;
        if (ExecutorNameComboBox.SelectedIndex != -1){
            executorId = ((Employee)ExecutorNameComboBox.SelectedItem).EmployeeId;
        }
        else{
            executorId = null;
        }

        var personId = ((ContactPerson)ContactParsonNameComboBox.SelectedItem).Id;
        var priorityId = ((TaskPriority)PriorityComboBox.SelectedItem).Id;
        var receiptId = ((TaskReceipt)ReceiptComboBox.SelectedItem).Id;
        var statusId = ((TaskStatus)StatusComboBox.SelectedItem).StatusId;

        int? contractId;
        if (ContractIdComboBox.SelectedIndex != -1){
            contractId = ((Contract)ContractIdComboBox.SelectedItem).Id;
        }
        else{
            contractId = null;
        }

        int? serialNum;
        if (SerialNumberComboBox.SelectedIndex != -1){
            serialNum = ((HardDrive)SerialNumberComboBox.SelectedItem).SerialNumber;
        }
        else{
            serialNum = null;
        }
        
        _taskDbModule.SetTask(executionPeriod, personId,  contractId, serialNum, priorityId, receiptId, statusId, executorId);
        this.DialogResult = true;
    }
}