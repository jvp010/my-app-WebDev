using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }
    [HttpGet("/products")]
    public async Task<IActionResult> Get()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Taran", Price = 20.50m, Description = "A keyboard for typing" },
            new Product { Id = 2, Name = "Frank", Price = 12.75m, Description = "A mouse for clicking" },
            new Product { Id = 3, Name = "Manu", Price = 199.99m, Description = "A monitor for viewing" }
        };
        return Ok(products);

    }
}