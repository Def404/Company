namespace Company.Database.Models;

public class Statistic{
    public int AllTasksCnt{ get; }
    public int TasksCmpltOnTimeCnt{ get; }
    public int TasksCmpltOutTimeCnt{ get; }
    public int TasksNotCmpltOutTimeCnt{ get; }
    public int TasksNotCmpltOnTimeCnt{ get; }

    public Statistic(int allTasksCnt, int tasksCmpltOnTimeCnt, int tasksCmpltOutTimeCnt, int tasksNotCmpltOutTimeCnt, int tasksNotCmpltOnTimeCnt){
        AllTasksCnt = allTasksCnt;
        TasksCmpltOnTimeCnt = tasksCmpltOnTimeCnt;
        TasksCmpltOutTimeCnt = tasksCmpltOutTimeCnt;
        TasksNotCmpltOutTimeCnt = tasksNotCmpltOutTimeCnt;
        TasksNotCmpltOnTimeCnt = tasksNotCmpltOnTimeCnt;
    }
}