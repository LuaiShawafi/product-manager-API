using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.Data;
using ProductManagerAPI.Data.Entities;

namespace ProductManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<ProductDto> CreateProduct(CreateProductRequest createProductRequest)
        {
            var product = new Product
            {
                Name = createProductRequest.Name,
                Sku = createProductRequest.Sku,
                Description = createProductRequest.Description,
                ImageURL = createProductRequest.ImageURL,
                Price = createProductRequest.Price
            };

            context.Products.Add(product);
            context.SaveChanges();

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price

            };

            return Created("", productDto);
        }

        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = context.Products.ToList();

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Sku = p.Sku,
                Description = p.Description,
                ImageURL = p.ImageURL,
                Price = p.Price
            });

            return productsDto;
        }

        [HttpGet("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]

        public IEnumerable<ProductDto> GetProductsBySearch([FromQuery] string? name)
        {
            var products = string.IsNullOrWhiteSpace(name)
                ? context.Products.ToList()
                : context.Products.Where(p => p.Name.Contains(name)).ToList();


            var productsDto = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price
            }).ToList();

            return productsDto;

        }


        [HttpGet("{sku}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> GetProduct(int sku)
        {

            var product = context.Products.FirstOrDefault(p => p.Sku == sku);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Sku = product.Sku,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price
            };

            return productDto;
        }




        [HttpDelete("{sku}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteProduct(int sku)
        {
            var product = context.Products.FirstOrDefault(p => p.Sku == sku);

            if (product is null)
                return NotFound();

            context.Products.Remove(product);
            context.SaveChanges();

            return NoContent();
        }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sku { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Price { get; set; }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; }
        public int Sku { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Price { get; set; }
    }
}
