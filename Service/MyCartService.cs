using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix.Service
{
    public class MyCartService : MyCartRepo
    {
        private ApplicationDbContext _db;

        public MyCartService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(MyCart entity)
        {
            _db.MyCart.Add(entity);
        }

        public void Delete(MyCart entity)
        {
            _db.MyCart.Remove(entity); 
        }

        public MyCart GetById(int id)
        {
            return _db.MyCart.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(MyCart entity)
        {
            _db.MyCart.Update(entity);
        }

        public List<MyCart> GetAll(string userId) { 
            return _db.MyCart.Include(x => x.Products).Where(x => x.UserId == userId).ToList();

        }

        public MyCart GetByProductId(int productId, string userId) {
            return _db.MyCart.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

        }

    }
}
