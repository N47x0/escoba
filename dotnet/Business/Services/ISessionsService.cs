using System.Collections.Generic;
using Business.Models;

namespace Business.Services
{
    public interface ISessionsService
    {
        IReadOnlyCollection<ClientSession> GetAll();

        ClientSession GetByServiceId(long serviceId);

        ClientSession Update(ClientSession clientSession);

        ClientSession Add(ClientSession clientSession);
    }
}