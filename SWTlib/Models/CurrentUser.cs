public class CurrentUserObject
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }

    public CurrentUserObject(string name, int userID, string email)
    {
        Name = name;
        Id = userID;
        Email = email;
    }
}
