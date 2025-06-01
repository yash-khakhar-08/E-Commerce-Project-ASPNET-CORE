using MarketMatrix_Models;

namespace MarketMatrix.Repository
{
    public interface OrdersRepo
    {
        void Save();

        List<Orders> GetAll(string userId);

        List<Orders> GetAllWithUser(string status);

        Orders GetById(int id);

        void Add(Orders entity);

        void Update(Orders entity);

        void Delete(Orders entity);
    }
}
