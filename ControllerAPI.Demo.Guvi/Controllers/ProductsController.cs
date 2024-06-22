using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllerAPI.Demo.Guvi.Data;
using ControllerAPI.Demo.Guvi.Models;
using Microsoft.AspNetCore.Authorization;
using ControllerAPI.Demo.Guvi.Extensions;
using Asp.Versioning;

namespace ControllerAPI.Demo.Guvi.Controllers
{
    //[Route("api/Products")]
    [Route("api/v{version:apiVersion}/Products")]

    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductsV1Controller : ControllerBase
    {
        private readonly ControllerAPIDemoGuviContext _context;

        public ProductsV1Controller(ControllerAPIDemoGuviContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct([FromQuery] ProductQueryParameters queryParameters)
        {
            //page
            //size
            IQueryable<APIDeprecationSettings> aPIDeprecationSettings = _context.aPIDeprecationSettings.Where(x => x.APIName == "ProductsV1" && x.MethodName == "GetProduct" && x.DeprecationDatetime <= DateTime.Now);
            if (aPIDeprecationSettings.Count() > 0)
            {
                IQueryable<Product> products = _context.Product.AsQueryable<Product>();
                if (!string.IsNullOrEmpty(queryParameters.ProductName))
                {
                    products = products.Where(x => x.Name == queryParameters.ProductName);
                }
                products = products.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);


                //sorting order either asending or desending order

                if (!string.IsNullOrEmpty(queryParameters.SortingOrder))
                {
                    if (queryParameters.SortingOrder.ToLower() == "asc")
                    {
                        products = products.OrderBy(x => x.Id);

                    }
                    else
                    {
                        products = products.OrderByDescending(x => x.Id);
                    }
                }

                return Ok(products);
            }
            else
            {
                return Content("This version of api got deprecated . you can another version to continue to get the data");
            }
            //return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }

    [Route("api/Products")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize]
    public class ProductsV2Controller : ControllerBase
    {
        private readonly ControllerAPIDemoGuviContext _context;

        public ProductsV2Controller(ControllerAPIDemoGuviContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct([FromQuery] ProductQueryParameters queryParameters)
        {
            //page
            //size
            IQueryable<Product> products = _context.Product.AsQueryable<Product>();
            if (!string.IsNullOrEmpty(queryParameters.ProductName))
            {
                products = products.Where(x => x.Name == queryParameters.ProductName);
            }
            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1)).Take(queryParameters.Size);


            //sorting order either asending or desending order

            if (!string.IsNullOrEmpty(queryParameters.SortingOrder))
            {
                if (queryParameters.SortingOrder.ToLower() == "asc")
                {
                    products = products.OrderBy(x => x.Id);

                }
                else
                {
                    products = products.OrderByDescending(x => x.Id);
                }
            }

            return Ok(products);
            //return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
