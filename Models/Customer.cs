namespace LINQ.Models
{
	public class Customer
	{
		public string CustomerID { get; set; } = string.Empty;
		public string CustomerName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Region { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public Order[] Orders { get; set; } = Array.Empty<Order>();
		public override string ToString() => $"{CustomerID} {CustomerName}\n{Address}\n{City}, {Region} {PostalCode} {Country}\n{Phone}";
	}


}
