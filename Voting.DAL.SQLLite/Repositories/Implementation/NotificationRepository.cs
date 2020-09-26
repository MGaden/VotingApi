using Voting.DAL.SQLLite.Data;
using Voting.DAL.SQLLite.Entities;
using Voting.DAL.SQLLite.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voting.DAL.SQLLite.Repositories.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        VotingDbContext context;
        public NotificationRepository(VotingDbContext _context)
        {
            context = _context;
        }

        public bool Unsubscribe(long userId)
        {
            return true;
        }

        
    }
}
