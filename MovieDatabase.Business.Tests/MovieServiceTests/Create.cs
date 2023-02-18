using Moq;
using MovieDatabase.Data.Domain.Entities;
using MovieDatabase.Data.Domain.Interfaces;

namespace MovieDatabase.Business.Tests.MovieServiceTests;

public class Create
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

    public Create()
    {
        repositoryMock = new();
        movieService = new(repositoryMock.Object);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Should_ThrowException_When_TitleIsInvalid(string title)
    {
        // Arrange
        Func<Task<Movie>> createMethod = async () => await movieService.Create(new Movie { Id = 1, Rating = 1, Seen = false, Title = title });

        // Act
        Exception result = await Assert.ThrowsAsync<Exception>(createMethod);

        // Assert
        Assert.Equal("Title cannot be empty.", result.Message);
    }

    [Fact]
    public async Task Should_ThrowException_When_IdIsNotZero()
    {
        // Arrange
        Func<Task<Movie>> createMethod = async () => await movieService.Create(new Movie { Id = 1, Rating = 1, Seen = false, Title = "Hello world" });

        // Act
        Exception result = await Assert.ThrowsAsync<Exception>(createMethod);

        // Assert
        Assert.Equal("The identifier needs to be 0.", result.Message);
    }

    [Fact]
    public async Task Should_CreateMovie()
    {
        // Act
        Movie result = await movieService.Create(new Movie()
        {
            Rating = 0,
            Seen = false,
            Title = "Black Adam"
        });

        // Assert
        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Movie>()), Times.Once);
    }
}
