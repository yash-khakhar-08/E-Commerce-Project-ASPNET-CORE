using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;

namespace MarketMatrix.Service
{
    public class AddressService : AddressRepo
    {
        ApplicationDbContext _db;
        public AddressService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Address entity)
        {
            _db.Address.Add(entity);
        }

        public void Delete(Address entity)
        {
            _db.Address.Remove(entity);
        }

        public List<Address> GetAll()
        {
            return _db.Address.ToList();
        }

        public Address GetById(int id)
        {
            return _db.Address.FirstOrDefault(x => x.Id == id);    
        }

        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(Address entity)
        {
            _db.Address.Update(entity);
        }
    }
}
