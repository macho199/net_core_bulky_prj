using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BulkyBook.Models
{
    public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Range(0, 10000)]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		
		public string ToWhereString()
		{
			List<string> condition = new List<string>();
			if (Id > 0) condition.Add("id = @Id");
			if (!string.IsNullOrEmpty(Name)) condition.Add("name = @Name");
			if (DisplayOrder > 0 && DisplayOrder < 10000) condition.Add("display_order = @DisplayOrder");

			return (condition.Count() > 0 ? " WHERE " : "") + string.Join(" AND ", condition);
		}
	}
}

