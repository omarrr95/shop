using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class ProductsService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public ProductsService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public List<Product> GetAllProducts(int? pageNo = 1, int? recordSize = 0)
        {
            

            var products = _eCommerceContext.Products
                                    .Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted)
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                products = products.Skip(skip)
                                   .Take(recordSize.Value);
            }

            return products.ToList();
        }

        public List<Product> SearchFeaturedProducts(int? pageNo = 1, int? recordSize = 0, List<int> excludeProductIDs = null)
        {
            excludeProductIDs = excludeProductIDs ?? new List<int>();

            

            var products = _eCommerceContext.Products
                                    .Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted && x.isFeatured && !excludeProductIDs.Contains(x.ID))
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                products = products.Skip(skip)
                                       .Take(recordSize.Value);
            }

            return products.ToList();
        }

        public List<Product> SearchProducts(List<int> categoryIDs, string searchTerm, decimal? from, decimal? to, string sortby, int? pageNo, int recordSize, bool activeOnly, out int count, int? stockCheckCount = null)
        {
            

            var products = _eCommerceContext.Products
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = _eCommerceContext.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Price >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Price <= to.Value);
            }

            if(stockCheckCount.HasValue && stockCheckCount.Value > 0)
            {
                products = products.Where(x => x.StockQuantity <= stockCheckCount.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                if(string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Price);
                }
                else {
                    products = products.OrderBy(x => x.Price);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return products.Skip(skipCount).Take(recordSize).Include("Category.CategoryRecords").Include("ProductPictures.Picture").ToList();
        }


        public List<Product> GetProductWithLessStockQuantity(List<int> categoryIDs, string searchTerm, decimal? from, decimal? to, string sortby, bool activeOnly, int stockCount, out int count)
        {
            

            var products = _eCommerceContext.Products
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = _eCommerceContext.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Price >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Price <= to.Value);
            }

            products = products.Where(x => x.StockQuantity <= stockCount);

            if (!string.IsNullOrEmpty(sortby))
            {
                if (string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Price);
                }
                else
                {
                    products = products.OrderBy(x => x.Price);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            return products.Include("Category.CategoryRecords").ToList();
        }


        public Product GetProductByID(int ID, bool activeOnly = true)
        {
            

            var product = _eCommerceContext.Products.Include("Category.CategoryRecords").Include("ProductPictures.Picture").FirstOrDefault(x=>x.ID == ID);

            if(activeOnly)
            {
                return product != null && !product.IsDeleted && product.IsActive && !product.Category.IsDeleted ? product : null;
            }
            else return product != null && !product.IsDeleted && !product.Category.IsDeleted ? product : null;
        }

        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            

            return IDs.Select(id => _eCommerceContext.Products.Find(id)).Where(x=>!x.IsDeleted && x.IsActive && !x.Category.IsDeleted).OrderBy(x=>x.ID).ToList();
        }

        public ProductRecord GetProductRecordByID(int ID)
        {
            

            var productRecord = _eCommerceContext.ProductRecords.Find(ID);

            return productRecord != null && !productRecord.IsDeleted ? productRecord : null;
        }

        public decimal GetMaxProductPrice()
        {
            

            var products = _eCommerceContext.Products.Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted);

            return products.Count() > 0 ? products.Max(x => x.Price) : 0;
        }

        public bool SaveProduct(Product product)
        {
            

            _eCommerceContext.Products.Add(product);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool SaveProductRecord(ProductRecord productRecord)
        {
            

            _eCommerceContext.ProductRecords.Add(productRecord);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateProduct(Product product)
        {
            

            var existingProduct = _eCommerceContext.Products.Find(product.ID);

            _eCommerceContext.Entry(existingProduct).CurrentValues.SetValues(product);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateProductPictures(int productID, List<ProductPicture> newPictures)
        {
            

            var oldPictures = _eCommerceContext.ProductPictures.Where(p => p.ProductID == productID);

            _eCommerceContext.ProductPictures.RemoveRange(oldPictures);

            _eCommerceContext.ProductPictures.AddRange(newPictures);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateProductRecord(ProductRecord productRecord)
        {
            

            var existingRecord = _eCommerceContext.ProductRecords.Find(productRecord.ID);

            _eCommerceContext.Entry(existingRecord).CurrentValues.SetValues(productRecord);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateProductSpecifications(int productRecordID, List<ProductSpecification> newProductSpecification)
        {
            

            var oldProductSpecifications = _eCommerceContext.ProductSpecifications.Where(p => p.ProductRecordID == productRecordID);

            _eCommerceContext.ProductSpecifications.RemoveRange(oldProductSpecifications);

            _eCommerceContext.ProductSpecifications.AddRange(newProductSpecification);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool DeleteProduct(int ID)
        {
            

            var product = _eCommerceContext.Products.Find(ID);

            product.IsDeleted = true;

            _eCommerceContext.Entry(product).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }

        public void UpdateProductQuantities(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var dbProduct = _eCommerceContext.Products.Find(orderItem.ProductID);

                dbProduct.StockQuantity = dbProduct.StockQuantity - orderItem.Quantity;

                _eCommerceContext.Entry(dbProduct).State = EntityState.Modified;
            }

            _eCommerceContext.SaveChanges();
        }
    }
}
