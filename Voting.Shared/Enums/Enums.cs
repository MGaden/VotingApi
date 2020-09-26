using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Shared.Enums
{
    public static class Enums
    {
        public enum NotificationChannelType
        {
            SMS=1,
            Email,
            Push
        };

        public enum NotificationType
        {
            Registration=1,
            PriceAlert,
            News
        };
    }
}
