using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Demo.Guvi;
using MinimalAPI.Demo.Guvi.Data;
using MinimalAPI.Demo.Guvi.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var supportedLanguages = new[] { "en-us", "fr-FR"};
//app.UseRequestLocalization(new RequestLocalizationOptions
//{
//    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
//    SupportedCultures= supportedLanguages,



//}) ;
;
app.MapGet("/api/product", () =>
{
    return Results.Ok(ProductStore.ProductList);
}).WithName("GetAllProducts").Produces<IEnumerable<Product>>(200).Produces<Product>(400);
//get by id
app.MapGet("/api/product/{id:int}", (int id) =>
{
    return Results.Ok(ProductStore.ProductList.FirstOrDefault(x => x.Id == id));
}).WithName("GetProductById").Produces<Product>(200);

app.MapPost("/api/product", async (IMapper _mapper,IValidator<ProductCreateDTO> _validation,[FromBody] ProductCreateDTO productCreateDTO) =>
{
    var validationResult=await _validation.ValidateAsync(productCreateDTO);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());
    }
    //if (string.IsNullOrEmpty(productCreateDTO.Name))
    //{
    //    return Results.BadRequest("invalid product name");
    //}
    //if (ProductStore.ProductList.FirstOrDefault(u => u.Name.ToLower() == productCreateDTO.Name.ToLower()) != null)
    //{
    //    return Results.BadRequest("product is already exists");
    //}
    //Product product = new()
    //{
    //    Id = (ProductStore.ProductList.OrderByDescending(u => u.Id).FirstOrDefault().Id) + 1,
    //    Name = productCreateDTO.Name,
    //    Price = productCreateDTO.Price,
    //    DisplayOrder = productCreateDTO.DisplayOrder,
    //    IsActvie = true,
    //    Created = DateTime.Now
    //};
    Product product=_mapper.Map<Product>(productCreateDTO);
    //int lastProdctCount = 
    //product.Id = ++lastProdctCount;
    ProductStore.ProductList.Add(product);
    //ProductDTO productDTO = new()
    //{
    //    Id = product.Id,
    //    Name = product.Name,
    //    Price = product.Price,
    //    DisplayOrder = product.DisplayOrder,
    //    IsActvie = product.IsActvie,
    //    Created = product.Created
    //};
    ProductDTO productDTO=_mapper.Map<ProductDTO>(product);
    //return Results.Ok(product);


    return Results.Created($"/api/product/{productDTO.Id}", productDTO);

});


app.Run();

