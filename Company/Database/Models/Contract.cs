namespace Company.Database.Models;

public class Contract{
    public int Id{ get; }
    public string Info{ get; }
    public string Typename{ get; }

    public Contract(int id, string info, string typename){
        Id = id;
        Info = info;
        Typename = typename;
    }
}