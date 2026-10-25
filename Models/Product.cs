namespace LINQ.Models
{
	public class Product
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public decimal UnitPrice { get; set; }
		public int UnitsInStock { get; set; }
		public override string ToString() => $"ProductID={ProductID}, ProductName={ProductName}, Category={Category}, UnitPrice={UnitPrice:C2}, UnitsInStock={UnitsInStock}";
	}
}
