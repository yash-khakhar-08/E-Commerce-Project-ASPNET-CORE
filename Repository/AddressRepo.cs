using MarketMatrix_Models;

namespace MarketMatrix.Repository
{
    public interface AddressRepo
    {
        void Save();

        List<Address> GetAll();

        Address GetById(int id);

        void Add(Address entity);

        void Update(Address entity);

        void Delete(Address entity);
    }
}
