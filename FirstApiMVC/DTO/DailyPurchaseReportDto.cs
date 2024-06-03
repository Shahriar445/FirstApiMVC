namespace FirstApiMVC.DTO
{
    public class DailyPurchaseReportDto
    {
        public string DailyPurchase { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
