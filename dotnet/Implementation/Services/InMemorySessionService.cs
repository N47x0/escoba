using System;
using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Business.Services;

namespace Implementation.Services
{
    public class InMemorySessionsService : ISessionsService
    {
        private readonly List<ClientSession> _clientSessions = new List<ClientSession>();
        private long _currentId = 0;
        
        public IReadOnlyCollection<ClientSession> GetAll()
        {
            return _clientSessions.AsReadOnly();
        }

        public ClientSession GetByServiceId(long id)
        {
            return _clientSessions.SingleOrDefault(g => g.ServiceId == id);
        }

        public ClientSession Update(ClientSession clientSession)
        {
            var toUpdate = _clientSessions.SingleOrDefault(cs => cs.ServiceId == clientSession.ServiceId);

            if (toUpdate == null)
            {
                return null;
            }

            // toUpdate.Name = clientSession.Name;
            return toUpdate;
        }

        public ClientSession Add(ClientSession clientSession)
        {
            clientSession.ServiceId = ++_currentId;
            _clientSessions.Add(clientSession);
            Console.WriteLine("#### adding client session from in memory service ####");
            Console.WriteLine(clientSession.ServiceId);
            return clientSession;
        }
    }
}