using Voting.DAL.SQLLite.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.DAL.SQLLite.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        bool Unsubscribe(long userId);
    }
}
