namespace Company.Database.Models;

public class HardDrive{
    public int SerialNumber{ get; }
    public string DriveName{ get; }
    public int DriveSize{ get; }
    public string DriveType{ get; }
    public string Interface{ get; }

    public HardDrive(int serialNumber, string driveName, int driveSize, string driveType, string @interface){
        SerialNumber = serialNumber;
        DriveName = driveName;
        DriveSize = driveSize;
        DriveType = driveType;
        Interface = @interface;
    }
}