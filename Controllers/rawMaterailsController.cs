using InventoryManagement_api.Data;
using InventoryManagement_api.Models;
using InventoryManagement_api.Models.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement_api.Controllers
{
    public class rawMaterailsController : Controller
    {
        private readonly InventoryDbContext inventoryDbContext;

        public rawMaterailsController(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("addRawMaterial")]
        public async Task<IActionResult> addMaterial(rawmaterialsDto rawmaterialsDto)
        {
            var rawmat = new rawMaterials()
            {
                Materialname = rawmaterialsDto.Materialname,
                price = rawmaterialsDto.price,
                status = "On stock",
                weight = rawmaterialsDto.weight,
            };
            await inventoryDbContext.rawMaterials.AddAsync(rawmat);
            await inventoryDbContext.SaveChangesAsync();
            return Ok(rawmat);
        }

        [HttpGet("getRawMaterials")]
        public async Task<IActionResult> getMaterials()
        {
            var mat = await inventoryDbContext.rawMaterials.ToListAsync();
            return Ok(mat);
        }

        [HttpGet("getRawMaterial/{id}")]
        public async Task<IActionResult> getMaterial(int id)
        {
            var mat = await inventoryDbContext.rawMaterials.FirstOrDefaultAsync(x => x.id ==id);
            return Ok(mat);
        }

        [HttpPost("updateMaterials")]
        public async Task<IActionResult> updateMaterials(UpdaterawmaterialsDto updaterawmaterialsDto,int id)
        {
            var material = await inventoryDbContext.rawMaterials.FirstOrDefaultAsync(x=>x.id ==id);
            
            material.weight = updaterawmaterialsDto.weight;
            material.price = updaterawmaterialsDto.price;
            material.status = updaterawmaterialsDto.status;
            material.Materialname = updaterawmaterialsDto.Materialname;

            await inventoryDbContext.SaveChangesAsync();
            return Ok(material);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> deleteMateials(int id)
        {
            var deleteMaterial = await inventoryDbContext.rawMaterials.FindAsync(id);
            if(deleteMaterial != null)
            {
                 inventoryDbContext.rawMaterials.Remove(deleteMaterial);
                 await inventoryDbContext.SaveChangesAsync();
                return Ok("delted");
            }
            return BadRequest("item not found");
        }

    }
}
