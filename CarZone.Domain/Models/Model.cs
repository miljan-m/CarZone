using System.ComponentModel.DataAnnotations;

namespace CarZone.Domain.Models
{
    public class Model
    {
        [Key]
        public int ModelId{get;set;}
        public string ModelName{get;set;}

        public int BrandId{get;set;}
        public Brand Brand{get;set;}


        public Model()
        {
        }
    
        
        public Model(int ModelId,string ModelName,int BrandId,Brand Brand)
        {
            this.ModelId=ModelId;
            this.ModelName=ModelName;
            this.BrandId=BrandId;
            this.Brand=Brand;
        }
    }
}