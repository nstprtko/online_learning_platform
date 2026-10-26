
using EShopService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> Products = new();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(Products.Where(p => !p.Deleted));
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = _nextId++;
        product.CreatedAt = DateTime.UtcNow;

        Products.Add(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updatedProduct)
    {
        var product = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);

        if (product is null)
            return NotFound();

        product.Name = updatedProduct.Name;
        product.Ean = updatedProduct.Ean;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;
        product.Sku = updatedProduct.Sku;
        product.Category = updatedProduct.Category;
        product.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id && !p.Deleted);

        if (product is null)
            return NotFound();

        product.Deleted = true;
        product.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }
}
