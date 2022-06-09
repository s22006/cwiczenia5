using Microsoft.EntityFrameworkCore;
using zd7.Models;
using zd7.Models.Dtos;
using System.Linq;
using zd7.Services.Interfaces;

namespace zd7.Services
{
    public class TripService : ITripService
    {
        private readonly zd7Context _dbContext;
        private readonly IClientService _clientService;

        public TripService(zd7Context dbContext, IClientService clientService)
        {
            _dbContext = dbContext;
            _clientService = clientService;
        }

        public async Task AssignClientToTrip(AssignClientToTripDto request)
        {
            await CheckDoesTripExcist(request);

            var client = await _clientService.GetClientByPeselAsync(request.Pesel);

            if (client == null)
                client = await AddNewClient(request, client);

            if (client.ClientTrips.Select(x => x.IdTrip).Contains(request.IdTrip))
                throw new Exception("Error: Client assigned to this trip already!");

            await _dbContext.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = request.IdTrip,
                RegisteredAt = DateTime.Now
            });

            await _dbContext.SaveChangesAsync();
        }

        private async Task CheckDoesTripExcist(AssignClientToTripDto request)
        {
            if (!_dbContext.Trips.Any(x => x.IdTrip == request.IdTrip))
                throw new Exception("Error: Trip does not exist");
        }
 
        public async Task<IEnumerable<ListOfTripsDto>> GetAllTripsAsync()
        {
            var trips = await _dbContext.Trips
                .Include(x => x.ClientTrips)
                .Include(x => x.IdCountries)
                .Select(x => new ListOfTripsDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    MaxPeople = x.MaxPeople,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    Countries = x.IdCountries.Select(c => new TripCountriesDto { Name = c.Name }).ToList(),
                    Clients = x.ClientTrips.Select(c => new TripClientsDto { FirstName = c.IdClientNavigation.FirstName, LastName = c.IdClientNavigation.LastName}).ToList()
                })
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();

            return trips;
        }

        private async Task<Client> AddNewClient(AssignClientToTripDto request, Client? client)
        {
            client = await _clientService.CreateNewClient(new CreateClientDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Pesel = request.Pesel,
                Telephone = request.Telephone,
            });

            return client;
        }
    }
}
