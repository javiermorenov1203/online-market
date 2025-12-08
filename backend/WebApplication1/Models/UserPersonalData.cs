
using System.ComponentModel.DataAnnotations;

public class UserPersonalData
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? SecondLastName { get; set; }
}

