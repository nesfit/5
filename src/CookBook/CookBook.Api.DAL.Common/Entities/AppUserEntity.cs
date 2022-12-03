using System;
using CookBook.Api.DAL.Common.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Api.DAL.Common.Entities;

public class AppUserEntity : IdentityUser<Guid>, IEntity
{
    public AppUserEntity()
    {
        Id = Guid.NewGuid();
    }
}
