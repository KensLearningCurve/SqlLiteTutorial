using Moq;
using MovieDatabase.Data.Domain.Entities;
using MovieDatabase.Data.Domain.Interfaces;

namespace MovieDatabase.Business.Tests.MovieServiceTests;

public class Delete
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

    public Delete()
    {
        repositoryMock = new();
        repositoryMock.Setup(x=>x.GetAll()).Returns(movies.AsQueryable());

        movieService = new(repositoryMock.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-13)]
    public async Task Should_ThrowException_When_IdIsInvalid(int id)
    {
        // Arrange
        Func<Task> deleteMethod = async () => await movieService.Delete(id);

        // Act
        Exception result = await Assert.ThrowsAsync<Exception>(deleteMethod);

        // Assert
        Assert.Equal("Id is invalid.", result.Message);
    }

    [Fact]
    public async Task Should_ThrowException_When_MovieIsNotFound()
    {
        // Arrange
        int id = 745;
        Func<Task> deleteMethod = async () => await movieService.Delete(id);

        // Act
        Exception result = await Assert.ThrowsAsync<Exception>(deleteMethod);

        // Assert
        Assert.Equal($"Movie with id {id} not found.", result.Message);
    }

    [Fact]
    public async Task Should_DeleteMovie()
    {
        // Act
        await movieService.Delete(1);

        // Assert
        repositoryMock.Verify(x=>x.Delete(It.IsAny<Movie>()), Times.Once);
    }
}
