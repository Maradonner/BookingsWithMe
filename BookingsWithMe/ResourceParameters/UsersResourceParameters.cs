namespace BookingsWithMe.ResourceParameters;

public class UsersResourceParameters
{
    private const int _maxPageSize = 50;
    private int _pageSize = 25;

    public int PageNumber { get; set; } = 1;
    public int PageSize 
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }
}
