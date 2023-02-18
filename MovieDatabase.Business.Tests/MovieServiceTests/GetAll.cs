using Moq;
using MovieDatabase.Business;
using MovieDatabase.Data.Domain.Entities;
using MovieDatabase.Data.Domain.Interfaces;

namespace MovieServiceTests;

public class GetAll
{
    private readonly Mock<IRepository<Movie>> repositoryMock;
    private readonly MovieService movieService;

    private readonly List<Movie> movies = new()
    {
        new Movie()
        {
            Id = 1,
            Rating = 0,
            Seen = false,
            Title = "Shrek"
        },
        new Movie()
        {
            Id = 2,
            Rating = 1,
            Seen = true,
            Title = "Inception"
        },
        new Movie()
        {
            Id = 3,
            Rating = 2,
            Seen = true,
            Title = "The Green Latern"
        },
        new Movie()
        {
            Id = 4,
            Rating = 5,
            Seen = true,
            Title = "The Matrix"
        },
        new Movie()
        {
            Id = 5,
            Rating = 5,
            Seen = true,
            Title = "The Muppets"
        }
    };

    public GetAll()
    {
        repositoryMock = new();
        movieService = new(repositoryMock.Object);
    }

    [Fact]
    public void Should_ReturnFiveMovies_When_FilterIsNull()
    {
        // Arrange
        repositoryMock.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

        // Act
        List<Movie> result = movieService.GetAll(null).ToList();

        // Assert
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Should_MoviesSortedOnTitle_When_FilterIsNull()
    {
        // Arrange
        repositoryMock.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

        // Act
        List<Movie> result = movieService.GetAll(null).ToList();

        // Assert
        Assert.Equal("Inception", result[0].Title);
        Assert.Equal("Shrek", result[1].Title);
        Assert.Equal("The Green Latern", result[2].Title);
        Assert.Equal("The Matrix", result[3].Title);
        Assert.Equal("The Muppets", result[4].Title);
    }

    [Theory]
    [InlineData(false, 1)]
    [InlineData(true, 4)]
    public void Should_ReturnMoviesAccordingToFilterSeen(bool filterSeen, int numberOfMovies)
    {
        // Arrange
        repositoryMock.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

        // Act
        List<Movie> result = movieService.GetAll(new() {  Seen = filterSeen }).ToList();

        // Assert
        Assert.Equal(numberOfMovies, result.Count);
    }

    [Theory]
    [InlineData("Green", 1)]
    [InlineData("a", 2)]
    public void Should_ReturnMoviesAccordingToFilterQuery(string query, int numberOfMovies)
    {
        // Arrange
        repositoryMock.Setup(x => x.GetAll()).Returns(movies.AsQueryable());

        // Act
        List<Movie> result = movieService.GetAll(new() { Query = query }).ToList();

        // Assert
        Assert.Equal(numberOfMovies, result.Count);
    }

    [Fact]
    public void Should_ReturnEmptyList_When_NoMoviesAreFound()
    {
        // Act
        List<Movie> result = movieService.GetAll(null).ToList();

        // Assert
        Assert.Empty(result);
    }
}
