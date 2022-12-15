namespace Company.Database.Models;

public class TaskReceipt{
    public int Id{ get; }
    public string Name{ get; }

    public TaskReceipt(int id, string name){
        Id = id;
        Name = name;
    }
}