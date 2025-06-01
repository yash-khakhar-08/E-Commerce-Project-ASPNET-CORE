using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix.Service
{
    public class OrdersService : OrdersRepo
    {
        private readonly ApplicationDbContext _db;
        public OrdersService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Orders entity)
        {
            _db.Orders.Add(entity);
        }

        public void Delete(Orders entity)
        {
            _db.Orders.Remove(entity);
        }

        public List<Orders> GetAll(string userId)
        {
            return _db.Orders.Include(o => o.Products).Where(x => x.UserId == userId).OrderByDescending(x => x.DateTime).ToList();
        }

        public Orders GetById(int id)
        {
            return _db.Orders.Include(o => o.Products).Include(u => u.User).FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Orders entity)
        {
            _db.Orders.Update(entity);
        }

        public List<Orders> GetAllWithUser(string status) {

            return _db.Orders.Include(o => o.Products).Include(u => u.User).Where(x => x.status == status).OrderByDescending(x => x.DateTime).ToList();
        }
    }
}
