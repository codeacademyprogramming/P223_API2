using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApiP223.Apps.AdminApi.DTOs;
using ShopApiP223.Apps.AdminApi.DTOs.ProductDtos;
using ShopApiP223.Data.DAL;
using ShopApiP223.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApiP223.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }
        
        //[Route("{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(x=>x.Id == id && !x.IsDeleted);

            if (product == null) return NotFound();

            //return StatusCode(200, product);

            ProductGetDto productDto = new ProductGetDto
            {
                Id = product.Id,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
                Name = product.Name,
                DisplayStatus = product.DisplayStatus,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt
            };


            return Ok(productDto);
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll(int page=1,string search=null)
        {
            var query = _context.Products.Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));

            ListDto<ProductListItemDto> listDto = new ListDto<ProductListItemDto>
            {
                Items = query.Skip((page-1)*8).Take(8).Select(x =>
                new ProductListItemDto
                { Id = x.Id, Name = x.Name,
                    SalePrice = x.SalePrice,
                    CostPrice = x.CostPrice,
                    DisplayStatus = x.DisplayStatus }).ToList(),
                TotalCount = query.Count()
            };

            //foreach (var item in products)
            //{
            //    ProductListItemDto productListItemDto = new ProductListItemDto
            //    {
            //        Id = item.Id,
            //        CostPrice = item.CostPrice,
            //        Name = item.Name,
            //        SalePrice = item.SalePrice,
            //        DisplayStatus = item.DisplayStatus
            //    };
            //    listDto.Items.Add(productListItemDto);
            //}


            return Ok(listDto);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(ProductPostDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                SalePrice = productDto.SalePrice,
                CostPrice = productDto.CostPrice,
                DisplayStatus = productDto.DisplayStatus
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return StatusCode(201,product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,ProductPostDto productDto)
        {
            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == id);

            if (existProduct == null)
                return NotFound();

            existProduct.Name = productDto.Name;
            existProduct.SalePrice = productDto.SalePrice;
            existProduct.CostPrice = productDto.CostPrice;
            existProduct.DisplayStatus = productDto.DisplayStatus;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();


            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ChangeStatus(int id,bool status)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            product.DisplayStatus = status;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
