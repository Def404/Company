namespace Company.Database.Models;

public class ContactPerson{
    public int Id{ get; }
    public string FullName{ get; }

    public ContactPerson(int id, string fullName){
        Id = id;
        FullName = fullName;
    }
}