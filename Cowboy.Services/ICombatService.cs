using Cowboy.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Services
{
    public interface ICombatService
    {
        Task<ServiceResponse<string>> CombatAsync(int cowboyOneId, int cowboyTwoId);
    }
}
