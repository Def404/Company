namespace Company.Database.Models;

public class TaskStatus{
    public int StatusId{ get; }
    public string StatusName{ get; }

    public TaskStatus(int statusId, string statusName){
        StatusId = statusId;
        StatusName = statusName;
    }
}