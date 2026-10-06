using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Repositories.Abstractions;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Repositories.Implementations;

public class ProductCategoriesRepository : BaseRepository<ProductCategory>, IProductCategoriesRepository
{
    public ProductCategoriesRepository(LamazonDbContext dbContext) : base(dbContext)
    {
    }

    private IQueryable<ProductCategory> ActiveCategories => Table
            .Where(c => c.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted);

    public async Task<List<ProductCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ActiveCategories
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await ActiveCategories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
