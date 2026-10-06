using Lamazon.ViewModels.Models;

namespace Lamazon.Web.Models;

public class HomeIndexViewModel
{
    public List<ProductViewModel> Products { get; set; } = new();
    public List<ProductCategoryViewModel> ProductCategories { get; set; } = new();
    public int? SelectedCategoryId { get; set; }
    public string? SelectedCategoryName { get; set; }
}
