using InventoryManagement_api.Data;
using InventoryManagement_api.Models;
using InventoryManagement_api.Models.Design;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace InventoryManagement_api.Controllers
{
    public class ProductController : Controller
    {
        private readonly InventoryDbContext inventoryDbContext;
        public ProductController(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> products(Productdto products)
        {
            var prod = new Products()
            {
                Productname = products.Productname,
                size = products.size,
                status = "On stock",
                Description = products.Description,
                timestamp = DateTime.Now,
                weight = products.weight,
                price = products.price
            };
            await inventoryDbContext.products.AddAsync(prod);
            await inventoryDbContext.SaveChangesAsync();
            return Ok(prod);
        }

        [HttpGet("products")]
        public async Task<IActionResult> viewProducts()
        {
            var Vprod = await inventoryDbContext.products.ToListAsync();
            return Ok(Vprod);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> ViewOneProd(int id)
        {
            var V1prod = await inventoryDbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(V1prod);
        }

        [HttpPost("updateproduct")]
        public async Task<IActionResult> updateproducts(int id,UpdateProductdto products)
        {
            var prod = await inventoryDbContext.products.FindAsync(id);
            if(prod != null) 
            {
                prod.Productname = products.Productname;
                prod.size = products.size;
                prod.status = products.status;
                prod.Description = products.Description;
                prod.timestamp = DateTime.Now;
                prod.weight = products.weight;
                prod.price = products.price;
            };
            await inventoryDbContext.SaveChangesAsync();
            return Ok(prod);
        }

        [HttpDelete("product")]
        public async Task<IActionResult> remove(int id)
        {
            var prod= await inventoryDbContext.products.FindAsync(id);
            if (prod != null)
            {
                inventoryDbContext.products.Remove(prod);
                inventoryDbContext.SaveChanges();
                return Ok("Product deleted");
            }
            return NotFound("not found");

        }

    }

}
