using zd7.Models;
using zd7.Models.Dtos;

namespace zd7.Services.Interfaces
{
    public interface IClientService
    {
        Task RemoveClientById(int id);
        Task<Client?> GetClientByPeselAsync(string pesel);

        Task<Client> CreateNewClient(CreateClientDto clientDto);
    }
}
