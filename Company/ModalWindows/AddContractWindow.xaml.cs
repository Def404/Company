using System.Windows;
using System.Windows.Controls;
using Company.Database.Models;
using Company.Database.Modules;

namespace Company.ModalWindows;

public partial class AddContractWindow : Window{
    private readonly OrganizationDbModule _organizationDbModule = new OrganizationDbModule();
    private readonly ContactPersonDbModule _contactPersonDbModule = new ContactPersonDbModule();
    private readonly ContractTypeDbModule _contractTypeDbModule = new ContractTypeDbModule();
    private readonly ContractDbModule _contractDbModule = new ContractDbModule();
    
    public AddContractWindow(){
        InitializeComponent();
    }

    private void AddContractWindow_OnLoaded(object sender, RoutedEventArgs e){
        OrganizationComboBox.ItemsSource = _organizationDbModule.GetOrganizationList();
        TypeContractComboBox.ItemsSource = _contractTypeDbModule.GetContractTypeList();
    }

    private void OrganizationComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e){
        var organization = (Organization)OrganizationComboBox.SelectedItem;
        if (organization is not null){
            var organizationId = organization.Id;
            ContactParsonNameComboBox.ItemsSource = _contactPersonDbModule.GetContactPersonList(organizationId);
        }
    }

    private void AddContractBtn_OnClick(object sender, RoutedEventArgs e){
        if (OrganizationComboBox.SelectedIndex == -1 || ContactParsonNameComboBox.SelectedIndex == -1 ||
            TypeContractComboBox.SelectedIndex == -1){
            MessageBox.Show("не все поля заполнены");
            return;
        }

        string? info;
        info = InfoTxtBlock.Text.Trim() == "" ? null : InfoTxtBlock.Text.Trim();

        var typeId = ((ContractType)TypeContractComboBox.SelectedItem).Id;
        var personId = ((ContactPerson)ContactParsonNameComboBox.SelectedItem).Id;
        
        _contractDbModule.SetContract(info, typeId, personId);
        this.DialogResult = true;
    }
}