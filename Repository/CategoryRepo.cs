using MarketMatrix_Models;

namespace MarketMatrix.Repository
{
    public interface CategoryRepo
    {
        void Save();

        List<Category> GetAll(string? includeProperties = null);

        Category GetById(int id);

        void Add(Category entity);

        void Update(Category entity);

        void Delete(Category entity);

    }
}
