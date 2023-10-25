namespace MessagingEvents.Shared;

public class CustomerCreated
{
    public CustomerCreated() { }

    public CustomerCreated(int id, string fullName, string email, string phoneNumber, DateTime birthDate)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
}

