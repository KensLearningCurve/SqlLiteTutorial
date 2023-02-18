using MovieDatabase.Business.Interfaces;
using MovieDatabase.Business.Models;
using MovieDatabase.Data.Domain.Entities;
using MovieDatabase.Data.Domain.Interfaces;

namespace MovieDatabase.Business;

public class MovieService : IMovieService
{
    private readonly IRepository<Movie> repository;

    public MovieService(IRepository<Movie> repository)
    {
        this.repository=repository;
    }

    public IEnumerable<Movie> GetAll(Filter? filter)
    {
        var movies = repository.GetAll();

        if (filter!=null && filter.Seen != null)
            movies = movies.Where(x => x.Seen == filter.Seen);

        if (filter != null && !string.IsNullOrEmpty(filter.Query))
            movies = movies.Where(x => x.Title.Contains(filter.Query));

        return movies.OrderBy(x => x.Title).ToList();
    }

    public async Task<Movie> Create(Movie movie)
    {
        if (string.IsNullOrEmpty(movie.Title))
            throw new Exception("Title cannot be empty.");

        if (movie.Id > 0)
            throw new Exception("The identifier needs to be 0.");

        Movie result = await repository.CreateAsync(movie);

        return result;
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new Exception("Id is invalid.");

        Movie? toDelete = 
            repository.GetAll().SingleOrDefault(x => x.Id == id) 
            ?? throw new Exception($"Movie with id {id} not found.");

        await repository.Delete(toDelete);
    }
}