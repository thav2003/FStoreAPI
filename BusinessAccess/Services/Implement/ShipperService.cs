using BusinessAccess.Repository.Interface;
using BusinessAccess.Services.Interface;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implement
{
    public class ShipperService : IShipperService
    {

        private readonly IRepository<Shipper> _shipperRepository;
        public ShipperService(IRepository<Shipper> shipperRepository)
        {
            _shipperRepository = shipperRepository;

        }
        public async Task<Shipper> getOwnOrder(User user)
        {
            return await _shipperRepository.FindOne(o => o.UserId == user.Id);
        }
    }
}
