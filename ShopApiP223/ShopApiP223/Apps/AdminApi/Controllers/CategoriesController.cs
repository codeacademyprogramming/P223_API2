using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApiP223.Apps.AdminApi.DTOs;
using ShopApiP223.Apps.AdminApi.DTOs.CategoryDtos;
using ShopApiP223.Data.DAL;
using ShopApiP223.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApiP223.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] CategoryPostDto catregoryDto)
        {
            if (_context.Categories.Any(x => x.Name.ToUpper() == catregoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);

            Category category = new Category
            {
                Name = catregoryDto.Name,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();


            return StatusCode(201, category);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            
            if (category == null) return NotFound();

            CategoryGetDto categoryDto = new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                ModifiedAt = category.ModifiedAt
            };

            return Ok(categoryDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Categories.Where(x => !x.IsDeleted);

            ListDto<CategoryListItemDto> listDto = new ListDto<CategoryListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page-1)*8).Take(8).Select(x => new CategoryListItemDto { Id = x.Id, Name = x.Name }).ToList()
            };

            return Ok(listDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryPostDto categoryDto)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            if (_context.Categories.Any(x => x.Id!=id && x.Name.ToUpper() == categoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);

            category.Name = categoryDto.Name;
            category.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            category.IsDeleted = true;
            category.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
