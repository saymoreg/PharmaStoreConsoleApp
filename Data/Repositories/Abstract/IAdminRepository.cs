using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
    public interface IAdminRepository
    {
        Admin GetUsernameAndPassword(string username, string password);
    }
}

