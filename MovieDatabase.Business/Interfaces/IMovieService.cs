using MovieDatabase.Business.Models;
using MovieDatabase.Data.Domain.Entities;

namespace MovieDatabase.Business.Interfaces;

public interface IMovieService
{
    IEnumerable<Movie> GetAll(Filter? filter);

    Task<Movie> Create(Movie movie);

    Task Delete(int id);
}