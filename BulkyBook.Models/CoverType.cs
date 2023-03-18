using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models
{
	public class CoverType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public string ToWhereString()
		{
			List<string> condition = new List<string>();
			if (Id > 0) condition.Add("id = @Id");
			if (!string.IsNullOrEmpty(Name)) condition.Add("name = @Name");

            return (condition.Count() > 0 ? " WHERE " : "") + string.Join(" AND ", condition);
        }
	}
}

