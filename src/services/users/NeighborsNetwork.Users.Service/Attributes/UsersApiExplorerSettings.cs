using System;
using Microsoft.AspNetCore.Mvc;

namespace NeighborsNetwork.Users.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UsersApiExplorerSettingsAttribute : ApiExplorerSettingsAttribute
    {
        public UsersApiExplorerSettingsAttribute()
        {
            GroupName = Constants.ServiceSlug;
        }
    }
}
