using System;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
	public class CoverType
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

