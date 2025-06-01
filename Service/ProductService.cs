using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix.Service
{
    public class ProductService :  ProductRepo
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

		public void Add(Products entity)
		{
			_db.Products.Add(entity);
		}

		public void Delete(Products entity)
		{
			_db.Products.Remove(entity);
		}

		public List<Products> GetAll(string? includeProperties = null)
		{
			List<Products> list = _db.Products.ToList();

			if (!string.IsNullOrEmpty(includeProperties))
			{

				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{

					_db.Products.Include(includeProp);
				}
			}
			return list;
		}

		public Products GetById(int id)
		{
			return _db.Products.Find(id);
		}

        public void Save()
        {
            _db.SaveChanges();
        }

		public void Update(Products entity)
		{
			_db.Products.Update(entity);
		}

        public List<Products> GetBySearchQuery(string query, int minPrice, int maxPrice)
        {
            var products = _db.Products.Where(p => p.Price >= minPrice && p.Price <= (maxPrice > 0 ? maxPrice:1000) ).ToList();

			List<string> commonAttr = new List<string>{ "under", "below","between","above","and","for","is","more","than","less","the","rupees","rs","money","item","product" };

            // Split query into individual words
            var keywords = query.ToLower().
				Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Except(commonAttr);

            // LINQ query to search across product_name, product_desc, and color
            var results = products
                .Where(p => keywords.All(k =>
					p.Name.ToLower().Contains(k) ||
					p.Description.ToLower().Contains(k) ||
					p.Color.ToLower().Contains(k)))
				.ToList();


            return results;


        }

		public List<Products> GetByCategoryId(int catId) {

			return _db.Products.Where(p => p.CategoryId == catId).OrderBy(x=> EF.Functions.Random()).Take(4).ToList();
			
		} 

    }
}
