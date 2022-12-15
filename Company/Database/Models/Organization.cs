namespace Company.Database.Models;

public class Organization{
    public int Id{ get; }
    public string Name{ get; }

    public Organization(int id, string name){
        Id = id;
        Name = name;
    }
}