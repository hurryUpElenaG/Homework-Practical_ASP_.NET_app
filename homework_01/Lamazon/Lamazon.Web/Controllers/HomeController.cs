using Lamazon.Services.Abstractions;
using Lamazon.ViewModels.Models;
using Lamazon.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lamazon.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductsService _productsService;
    private readonly IProductCategoriesService _productCategoriesService;

    public HomeController(IProductsService productsService, IProductCategoriesService productCategoriesService)
    {
        _productsService = productsService;
        _productCategoriesService = productCategoriesService;
    }

    public async Task<IActionResult> Index(int? categoryId, CancellationToken cancellationToken)
    {
        List<ProductViewModel> products;
        string? selectedCategoryName = null;

        if (categoryId.HasValue)
        {
            products = await _productsService.GetByCategoryAsync(categoryId.Value, cancellationToken);
            var cat = (await _productCategoriesService.GetAllAsync(cancellationToken)).FirstOrDefault(c => c.Id == categoryId.Value);
            selectedCategoryName = cat?.Name;
        }
        else
        {
            products = await _productsService.GetFeaturedAsync(cancellationToken);
        }

        List<ProductCategoryViewModel> categories = await _productCategoriesService.GetAllAsync(cancellationToken);

        var model = new HomeIndexViewModel
        {
            Products = products,
            ProductCategories = categories,
            SelectedCategoryId = categoryId,
            SelectedCategoryName = selectedCategoryName
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
