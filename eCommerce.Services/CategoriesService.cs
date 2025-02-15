using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;

public class CategoriesService
{
    #region Define as Singleton
    private readonly eCommerceContext _eCommerceContext;
    public CategoriesService(eCommerceContext eCommerceContext)
    {
        _eCommerceContext = eCommerceContext;
    }
    #endregion

    public List<Category> GetCategories(int? pageNo = 1, int? recordSize = 0)
    {
        var categories = _eCommerceContext.Categories
                                .Where(x=>!x.IsDeleted)
                                .OrderBy(x => x.ID)
                                .AsQueryable();
        
        if (recordSize.HasValue && recordSize.Value > 0)
        {
            pageNo = pageNo ?? 1;
            var skip = (pageNo.Value - 1) * recordSize.Value;

            categories = categories.Skip(skip)
                                   .Take(recordSize.Value);
        }

        return categories.ToList();
    }

    public List<Category> GetFeaturedCategories(int? pageNo = 1, int? recordSize = 0, bool includeProducts = false)
    {

        var categories = _eCommerceContext.Categories
                                .Where(x => !x.IsDeleted && x.isFeatured)
                                .OrderBy(x => x.ID)
                                .AsQueryable();

        if (recordSize.HasValue && recordSize.Value > 0)
        {
            pageNo = pageNo ?? 1;
            var skip = (pageNo.Value - 1) * recordSize.Value;

            categories = categories.Skip(skip)
                                   .Take(recordSize.Value);
        }

        if(includeProducts)
        {
            categories = categories.Include("Products.ProductRecords");
        }

        return categories.ToList();
    }

    public List<CategoryRecord> GetCategoriesRecordsByCategory(int categoryID, int? pageNo = 1, int? recordSize = 0)
    {

        var categoryRecords = _eCommerceContext.CategoryRecords
                                     .Where(x=>x.CategoryID == categoryID && !x.IsDeleted)
                                     .OrderBy(x => x.ID)
                                     .AsQueryable();

        if (recordSize.HasValue && recordSize.Value > 0)
        {
            pageNo = pageNo ?? 1;
            var skip = (pageNo.Value - 1) * recordSize.Value;

            categoryRecords = categoryRecords.Skip(skip)
                                             .Take(recordSize.Value);
        }

        return categoryRecords.ToList();
    }

    public List<CategoryRecord> GetCategoriesRecordsByLanguage(int languageID, int? pageNo = 1, int? recordSize = 0)
    {

        var categoryRecords = _eCommerceContext.CategoryRecords
                                     .Where(x => x.LanguageID == languageID && !x.IsDeleted)
                                     .OrderBy(x => x.ID)
                                     .AsQueryable();

        if (recordSize.HasValue && recordSize.Value > 0)
        {
            pageNo = pageNo ?? 1;
            var skip = (pageNo.Value - 1) * recordSize.Value;

            categoryRecords = categoryRecords.Skip(skip)
                                             .Take(recordSize.Value);
        }

        return categoryRecords.ToList();
    }

    public List<Category> GetAllTopLevelCategories(int? pageNo = 1, int? recordSize = 0)
    {
        var categories = _eCommerceContext.Categories
                                .Where(x => !x.ParentCategoryID.HasValue && !x.IsDeleted)
                                .OrderBy(x => x.ID)
                                .AsQueryable();

        if (recordSize.HasValue && recordSize.Value > 0)
        {
            pageNo = pageNo ?? 1;
            var skip = (pageNo.Value - 1) * recordSize.Value;

            categories = categories.Skip(skip)
                                   .Take(recordSize.Value);
        }

        return categories.ToList();
    }

    public Category GetCategoryByID(int ID)
    {

        var category = _eCommerceContext.Categories.Find(ID);

        return category != null && !category.IsDeleted ? category : null;
    }

    public CategoryRecord GetCategoryRecordByID(int ID)
    {

        var categoryRecord = _eCommerceContext.CategoryRecords.Find(ID);

        return categoryRecord != null && !categoryRecord.IsDeleted ? categoryRecord : null;
    }

    public Category GetCategoryByName(string sanitizedCategoryName)
    {

        var category = _eCommerceContext.Categories.FirstOrDefault(x => x.SanitizedName.Equals(sanitizedCategoryName) && !x.IsDeleted);

        return category != null ? category : null;
    }

    public bool SaveCategory(Category category)
    {

        _eCommerceContext.Categories.Add(category);

        return _eCommerceContext.SaveChanges() > 0;
    }

    public bool SaveCategoryRecord(CategoryRecord categoryRecord)
    {

        _eCommerceContext.CategoryRecords.Add(categoryRecord);

        return _eCommerceContext.SaveChanges() > 0;
    }

    public bool UpdateCategory(Category category)
    {

        var existingCategory = _eCommerceContext.Categories.Find(category.ID);

        _eCommerceContext.Entry(existingCategory).CurrentValues.SetValues(category);
        
        return _eCommerceContext.SaveChanges() > 0;
    }
    
    public bool UpdateCategoryRecord(CategoryRecord categoryRecord)
    {

        var existingRecord = _eCommerceContext.CategoryRecords.Find(categoryRecord.ID);

        _eCommerceContext.Entry(existingRecord).CurrentValues.SetValues(categoryRecord);

        return _eCommerceContext.SaveChanges() > 0;
    }

    public bool DeleteCategory(int ID)
    {

        var category = _eCommerceContext.Categories.Find(ID);

        category.IsDeleted = true;

        _eCommerceContext.Entry(category).State = EntityState.Modified;

        return _eCommerceContext.SaveChanges() > 0;
    }

    public List<Category> SearchCategories(int? parentCategoryID, string searchTerm, int? pageNo, int recordSize, out int count)
    {
        var categories = _eCommerceContext.Categories.Where(x => !x.IsDeleted).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            categories = _eCommerceContext.CategoryRecords
                                .Where(x => !x.Category.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                .Select(x => x.Category)
                                .AsQueryable();
        }

        if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
        {
            categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
        }

        count = categories.Count();
        
        pageNo = pageNo ?? 1;
        var skipCount = (pageNo.Value - 1) * recordSize;

        return categories.OrderBy(x => x.ID).Skip(skipCount).Take(recordSize).ToList();
    }
}
