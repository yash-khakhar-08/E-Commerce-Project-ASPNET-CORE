using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix.Service
{
    public class CategoryService :  CategoryRepo
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db) 
        {
            _db = db;
        }

		public void Add(Category entity)
		{
			_db.Categories.Add(entity);
		}

		public void Delete(Category entity)
		{
			_db.Categories.Remove(entity);
		}

        public List<Category> GetAll(string? includeProperties = null)
        {
            IQueryable<Category> query = _db.Categories; // Start with the base query

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp); // Apply Include() properly
                }
            }

            return query.ToList(); // Execute the query and return the result
        }


        public Category GetById(int id)
		{
			return _db.Categories.FirstOrDefault(x => x.Id == id);
		}

        public void Save()
        {
            _db.SaveChanges();
        }

		public void Update(Category entity)
		{
			_db.Categories.Update(entity);	
		}

	}
}
