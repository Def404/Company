namespace Company.Database.Models;

public class ContractType{
    public int Id{ get; }
    public string Name{ get; }

    public ContractType(int id, string name){
        Id = id;
        Name = name;
    }
}