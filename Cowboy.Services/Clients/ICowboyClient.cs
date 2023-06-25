using Cowboy.Repository.Entities;
using Cowboy.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Services.Clients
{
    public interface ICowboyClient
    {
        Task<CowboyEntity?> GetCowboyAsync();
    }
}
