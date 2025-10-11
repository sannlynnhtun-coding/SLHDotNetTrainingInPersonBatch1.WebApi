using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SLHDotNetTrainingInPersonBatch1.WebApi.Database.AppDbContextModels;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        private readonly AppDbContext db;
        public ProductV2Controller()
        {
            db = new AppDbContext();
        }

        //private IQueryable<TblProduct> ProductQuery()
        //{
        //    return db.TblProducts.Where(x => x.DeleteFlag == false);
        //}

        private IQueryable<TblProduct> ProductQuery =>
             db.TblProducts.Where(x => x.DeleteFlag == false);

        [HttpGet]
        public IActionResult GetProducts()
        {
            var result = ProductQuery.ToList();

            //List<ProductDto> lst = new List<ProductDto>();
            //foreach(TblProduct product in result)
            //{
            //    lst.Add(new ProductDto
            //    {
            //        ProductId = product.ProductId,
            //        ProductCode = product.ProductCode,
            //        ProductName = product.ProductName,
            //        Price = product.Price,
            //        Quantity = product.Quantity,
            //        DeleteFlag = product.DeleteFlag
            //    });
            //}

            var lst = result.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                DeleteFlag = product.DeleteFlag
            }).ToList();

            return Ok(new
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            var item = ProductQuery.FirstOrDefault(x => x.ProductId == id);
            if (item is null)
            {
                return NotFound(new ProductResponseDto
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }

            return Ok(new ProductResponseDto
            {
                IsSuccess = true,
                Message = "Success.",
                Data = new ProductDto
                {
                    ProductId = item.ProductId,
                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    DeleteFlag = item.DeleteFlag
                }
            });
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto request)
        {
            db.TblProducts.Add(new TblProduct
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductCode = request.ProductCode,
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                DeleteFlag = request.DeleteFlag
            });
            var result = db.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            ProductResponseDto response = new ProductResponseDto
            {
                IsSuccess = result > 0,
                Message = message
            };
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] ProductDto request)
        {
            var item = ProductQuery.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
            {
                return NotFound(new ProductResponseDto
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }

            if (!string.IsNullOrEmpty(request.ProductCode))
            {
                item.ProductCode = request.ProductCode;
            }

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                item.ProductName = request.ProductName;
            }

            if (request.Price > 0)
            {
                item.Price = request.Price;
            }

            if (request.Quantity > 0)
            {
                item.Quantity = request.Quantity;
            }

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(new ProductResponseDto
            {
                IsSuccess = true,
                Message = "Success."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var item = ProductQuery.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
            {
                return NotFound(new ProductResponseDto
                {
                    IsSuccess = false,
                    Message = "No data found."
                });
            }

            item.DeleteFlag = true;
            var result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(new ProductResponseDto
            {
                IsSuccess = true,
                Message = "Success."
            });
        }
    }
}
