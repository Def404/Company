using System.Windows;
using System.Windows.Controls;
using Company.Database.Modules;

namespace Company.Pages;

public partial class StatisticPage : UserControl{
    private readonly StatisticDbModule _statisticDbModule = new StatisticDbModule();
    public StatisticPage(){
        InitializeComponent();
    }

    private void StatisticPage_OnLoaded(object sender, RoutedEventArgs e){
        var statistics = _statisticDbModule.GetStatisticList();

        AllTasksCntTxtBlock.Text = statistics.AllTasksCnt.ToString();
        TasksCmpltOnTimeCntTxtBlock.Text = statistics.TasksCmpltOnTimeCnt.ToString();
        TasksCmpltOutTimeCntTxtBlock.Text = statistics.TasksCmpltOutTimeCnt.ToString();
        TasksNotCmpltOnTimeCntTxtBlock.Text = statistics.TasksNotCmpltOnTimeCnt.ToString();
        TasksNotCmpltOutTimeCntTxtBlock.Text = statistics.TasksNotCmpltOutTimeCnt.ToString();
    }
}