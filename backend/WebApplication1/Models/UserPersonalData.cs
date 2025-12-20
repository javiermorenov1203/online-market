
using System.ComponentModel.DataAnnotations;

public class UserPersonalData
{
    public UserPersonalData() { }

    public UserPersonalData(UserPersonalDataDto data)
    {
        this.FirstName = data.FirstName;
        this.MiddleName = data.MiddleName;
        this.LastName = data.LastName;
        this.SecondLastName = data.SecondLastName;
    }

    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? SecondLastName { get; set; }
}

