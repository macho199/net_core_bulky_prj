using System;
namespace BulkyBook.Utility
{
	public static class SD
	{
		public static string GetConnectionString(string type = "")
		{
			switch(type)
			{
				default:
					return "Host=localhost;Uid=postgres;Pwd=1234;Database=bulky;";
			}
		}
	}
}

