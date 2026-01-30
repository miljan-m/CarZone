using System.ComponentModel.DataAnnotations;

namespace CarZone.Domain.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }


        public Model()
        {
        }


        public Model(int ModelId, string ModelName, int BrandId, Brand Brand)
        {
            if (ModelId < 0)
                throw new ArgumentException("ModelId cannot be negative.");

            if (string.IsNullOrWhiteSpace(ModelName))
                throw new ArgumentException("Model name is required.");

            if (BrandId <= 0)
                throw new ArgumentException("BrandId must be a positive number.");

            if (Brand == null)
                throw new ArgumentNullException(nameof(Brand), "Brand is required.");
            this.ModelId = ModelId;
            this.ModelName = ModelName;
            this.BrandId = BrandId;
            this.Brand = Brand;
        }
    }
}