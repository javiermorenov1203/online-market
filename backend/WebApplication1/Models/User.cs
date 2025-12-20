
public class User
{
    public User() { }

    public User(UserDto userDto)
    {
        this.Email = userDto.Email;
        this.Password = userDto.Password;
    }

    public int Id { get; set; }    
    public string Email { get; set; }
    public string Password { get; set; }
}

