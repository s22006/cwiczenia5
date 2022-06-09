using Microsoft.EntityFrameworkCore;
using zd7.Models;
using zd7.Models.Dtos;
using zd7.Services.Interfaces;

namespace zd7.Services
{
    public class ClientService : IClientService
    {
        private readonly zd7Context _dbContext;

        public ClientService(zd7Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> CreateNewClient(CreateClientDto clientDto)
        {
            var client = new Client
            {
                Pesel = clientDto.Pesel,
                Email = clientDto.Email,
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Telephone = clientDto.Telephone
            };

            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }

        public async Task<Client?> GetClientByPeselAsync(string pesel)
        {
            var client = await _dbContext.Clients
                .Include(x => x.ClientTrips)
                .FirstOrDefaultAsync(x => x.Pesel == pesel);

            return client;
        }

        public async Task RemoveClientById(int id)
        {
            var client = _dbContext.Clients
                .Include(x => x.ClientTrips)
                .FirstOrDefault(x => x.IdClient == id);

            if(client == null)
                throw new Exception($"Error: Client with id {id}, does not exist.");

            if (client.ClientTrips.Any())
                throw new Exception("Error: Client has active trips. You can not delete him.");

            _dbContext.Remove(client);

            await _dbContext.SaveChangesAsync();
        }
    }
}
