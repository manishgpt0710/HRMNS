namespace HRMS.Data.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime JoiningDate { get; set; }
}
