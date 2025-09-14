namespace SLHDotNetTrainingInPersonBatch1.WebApi.Controllers
{
    // Dto - Data Transfer Object
    // Model

    public class ProductModel
    {
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
