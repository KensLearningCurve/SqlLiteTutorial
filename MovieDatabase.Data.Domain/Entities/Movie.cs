namespace MovieDatabase.Data.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Rating { get; set; }
    public bool Seen { get; set; }
}