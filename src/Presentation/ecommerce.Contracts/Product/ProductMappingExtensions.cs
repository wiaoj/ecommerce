﻿using ecommerce.Application.Features.Products.Commands.CreateProduct;
using static ecommerce.Contracts.External;

namespace ecommerce.Contracts.Product;
public static class CreateProductExtensions {
    public static CreatedProductResponse ToResponse(this CreateProductCommandResponse response) {
        return new(response.Id, response.CategoryId);
    }
}