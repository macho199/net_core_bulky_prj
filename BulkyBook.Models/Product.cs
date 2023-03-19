using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public double ListPrice { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Price50 { get; set; }
        [Required]
        public double Price100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public string CategoryName { get; set; }
        [ValidateNever]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public string CoverTypeName { get; set; }

        public string ToWhereString()
        {
            List<string> condition = new List<string>();
            if (Id > 0) condition.Add("id = @Id");
            if (!string.IsNullOrEmpty(Title)) condition.Add("title = @Title");

            return (condition.Count() > 0 ? " WHERE " : "") + string.Join(" AND ", condition);
        }
    }
}

