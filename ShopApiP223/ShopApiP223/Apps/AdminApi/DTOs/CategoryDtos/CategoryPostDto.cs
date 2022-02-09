using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApiP223.Apps.AdminApi.DTOs.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name uzunlugu 20-den boyuk ola bilmez!")
                .NotEmpty().WithMessage("Name mecburidir!");



        }
    }
}
