using MarketMatrix_Models;

namespace MarketMatrix.Repository
{
    public interface ProductRepo
    {
        void Save();

        List<Products> GetAll(string? includeProperties = null);

        Products GetById(int id);

        void Add(Products entity);

        void Update(Products entity);

        void Delete(Products entity);

        List<Products> GetBySearchQuery(string query, int minPrice, int maxPrice);

        List<Products> GetByCategoryId(int catId);
    }
}
