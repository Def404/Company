namespace Company.Database.Models;

public class TaskPriority{
    public  int Id{ get; }
    public string Name{ get; }

    public TaskPriority(int id, string name){
        Id = id;
        Name = name;
    }
}