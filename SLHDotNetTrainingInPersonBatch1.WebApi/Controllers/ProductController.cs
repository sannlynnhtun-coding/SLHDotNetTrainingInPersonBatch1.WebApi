using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SLHDotNetTrainingInPersonBatch1.WebApi.Controllers
{
    // https://localhost:3000/api/Product
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
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
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            db.Open();

            var lst = db.Query<ProductModel>("select * from Tbl_Product").ToList();

            var response = new { Message = "PLM Success - GetProducts", Data = lst };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateProduct()
        {
            return Ok("PLM Success - CreateProduct");
        }

        [HttpPut]
        public IActionResult UpsertProduct() // Update + Insert
        {
            return Ok("PLM Success - UpsertProduct");
        }

        [HttpPatch]
        public IActionResult UpdateProduct()
        {
            return Ok("PLM Success - UpdateProduct");
        }

        [HttpDelete]
        public IActionResult DeleteProduct()
        {
            return Ok("PLM Success - DeleteProduct");
        }
    }
}
