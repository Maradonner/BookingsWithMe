namespace BookingsWithMe.Exceptions;

public class EmailAlreadyExists : Exception
{
    public EmailAlreadyExists() : base("Email is already exists")
    {
        
    }
}
