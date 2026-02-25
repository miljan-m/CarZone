using System.ComponentModel.DataAnnotations;

namespace CarZone.Domain.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<Model>? Models { get; set; } = [];
        public Brand()
        {
        }

        public Brand(int BrandId, string BrandName, List<Model> Models)
        {
            if (BrandId < 0)
                throw new ArgumentException("BrandId cannot be negative.");

            if (string.IsNullOrWhiteSpace(BrandName))
                throw new ArgumentException("Brand name is required.");
            this.BrandId = BrandId;
            this.BrandName = BrandName;
            this.Models = Models;
        }
    }
}