using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "InPersonBatch1MiniPOS",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            using (IDbConnection db = new SqlConnection(_stringBuilder.ConnectionString))
            {
                db.Open();

                string query = @"SELECT [ProductId]
                                      ,[ProductCode]
                                      ,[ProductName]
                                      ,[Price]
                                      ,[Quantity]
                                      ,[DeleteFlag]
                                  FROM [dbo].[Tbl_Product]";
                var lst = db.Query<ProductDto>(query).ToList();
                return Ok(lst);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto request)
        {
            using (IDbConnection db = new SqlConnection(_stringBuilder.ConnectionString))
            {
                db.Open();

                request.ProductId = Guid.NewGuid().ToString();
                string query = @"INSERT INTO [dbo].[Tbl_Product]
                                       ([ProductId]
                                       ,[ProductCode]
                                       ,[ProductName]
                                       ,[Price]
                                       ,[Quantity]
                                       ,[DeleteFlag])
                                 VALUES
                                       (@ProductId
                                       ,@ProductCode
                                       ,@ProductName
                                       ,@Price
                                       ,@Quantity
                                       ,@DeleteFlag)";
                var result = db.Execute(query, request);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                ProductResponseDto response = new ProductResponseDto
                {
                    IsSuccess = result > 0,
                    Message = message
                };
                return Ok(response);
            }
        }

        [HttpGet("id/{id}")]
        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            using (IDbConnection db = new SqlConnection(_stringBuilder.ConnectionString))
            {
                db.Open();

                string query = @"SELECT [ProductId]
                                      ,[ProductCode]
                                      ,[ProductName]
                                      ,[Price]
                                      ,[Quantity]
                                      ,[DeleteFlag]
                                  FROM [dbo].[Tbl_Product] Where ProductId = @ProductId";
                var item = db.QueryFirstOrDefault<ProductDto>(query, new { ProductId = id });
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
                    Data = item
                });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] ProductDto request)
        {
            using (IDbConnection db = new SqlConnection(_stringBuilder.ConnectionString))
            {
                db.Open();
                //string query = @"SELECT [ProductId]
                //                      ,[ProductCode]
                //                      ,[ProductName]
                //                      ,[Price]
                //                      ,[Quantity]
                //                      ,[DeleteFlag]
                //                  FROM [dbo].[Tbl_Product] Where ProductId = @ProductId";
                //var item = db.QueryFirstOrDefault<ProductDto>(query, new { ProductId = id });

                //if (item is null)
                //{
                //    return NotFound(new ProductResponseDto
                //    {
                //        IsSuccess = false,
                //        Message = "No data found."
                //    });
                //}

                string conditions = "";
                if (!string.IsNullOrEmpty(request.ProductCode))
                {
                    conditions += "[ProductCode] = @ProductCode, ";
                }
                if (!string.IsNullOrEmpty(request.ProductName))
                {
                    conditions += "[ProductName] = @ProductName, ";
                }
                if (request.Price > 0)
                {
                    conditions += "[Price] = @Price, ";
                }
                if (request.Quantity > 0)
                {
                    conditions += "[Quantity] = @Quantity, ";
                }

                if (conditions.Length == 0)
                {
                    return BadRequest(new ProductResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid Data. Please add some field to update."
                    });
                }
                //10
                //0, 8(10-2)
                conditions = conditions.Substring(0, conditions.Length - 2);

                //[ProductCode] = @ProductCode
                //  ,[ProductName] = @ProductName
                //  ,[Price] = @Price
                //  ,[Quantity] = @Quantity[ProductCode] = @ProductCode
                //  ,[ProductName] = @ProductName
                //  ,[Price] = @Price
                //  ,[Quantity] = @Quantity

                string updateQuery = $@"
            UPDATE [dbo].[Tbl_Product]
               SET {conditions}
             WHERE [ProductId] = @ProductId";

                request.ProductId = id;
                var result = db.Execute(updateQuery, request);
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                return Ok(new ProductResponseDto
                {
                    IsSuccess = true,
                    Message = "Success."
                });
            }
        }
    }

    public class ProductDto
    {
        public string? ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool DeleteFlag { get; set; }
    }

    public class ProductResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ProductDto Data { get; set; }
    }
}
