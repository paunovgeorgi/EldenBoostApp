namespace EldenBoost.Core.Models.Order
{
	public class OrderCountDataModel
	{
		public int Pending { get; set; }
		public int Working { get; set; }
		public int Completed { get; set; }
		public int Total { get; set; }
	}
}
