using System;

namespace Company.Database.Models;

public class Task{
    public int TaskId{ get; }
    public DateTime CreateDate{ get; }
    public DateTime? ExecuteDate{ get; }
    public DateTime? CompletionDate{ get; }
    public string ContactParsonName{ get; }
    public string OrganizationName{ get; }
    public string Priority{ get; }
    public string Receipt{ get; }
    public string Status{ get; }
    public string? ExecutorName{ get; }

    public Task(int taskId, DateTime createDate, DateTime? executeDate, DateTime? completionDate,
        string contactParsonName, string organizationName, string priority,
        string receipt, string status, string? executorName){
        
        TaskId = taskId;
        CreateDate = createDate;
        ExecuteDate = executeDate;
        CompletionDate = completionDate;
        ContactParsonName = contactParsonName;
        OrganizationName = organizationName;
        Priority = priority;
        Receipt = receipt;
        Status = status;
        ExecutorName = executorName;
    }
}