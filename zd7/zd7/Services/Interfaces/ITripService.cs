using zd7.Models.Dtos;

namespace zd7.Services.Interfaces
{
    public interface ITripService
    {
        Task<IEnumerable<ListOfTripsDto>> GetAllTripsAsync();

        Task AssignClientToTrip(AssignClientToTripDto request);
    }
}
