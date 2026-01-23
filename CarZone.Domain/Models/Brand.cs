using System.ComponentModel.DataAnnotations;

namespace CarZone.Domain.Models
{
    public class Brand
    {
        [Key]
        public int BrandId{get;set;}
        public string BrandName{get;set;}

        public List<Model>? Models{get;set;}=[];
        public Brand()
        {
        }

         public Brand(int BrandId,string BrandName,List<Model> Models)
        {
            this.BrandId=BrandId;
            this.BrandName=BrandName;
            this.Models=Models;
        }
    }
}