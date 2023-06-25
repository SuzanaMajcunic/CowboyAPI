using Cowboy.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Services
{
    public interface IFirearmService
    {
        Task<ServiceResponse<bool>> ShootTheGunAsync(int cowboyId);
        Task<ServiceResponse<bool>> ReloadTheGunAsync(int cowboyId);
    }
}
