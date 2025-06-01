using MarketMatrix_Models;

namespace MarketMatrix.Repository
{
    public interface MyCartRepo
    {
        void Save();

        MyCart GetById(int id);

        void Add(MyCart entity);

        void Update(MyCart entity);

        void Delete(MyCart entity);

        List<MyCart> GetAll(string userId);

        MyCart GetByProductId(int productId, string userId);

    }
}
