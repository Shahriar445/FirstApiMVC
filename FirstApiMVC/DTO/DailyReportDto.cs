namespace FirstApiMVC.DTO
{
    public class DailyReportDto
    {
        public string ItemName { get; set; }
        public int PurchasedQuantity { get; set; }
        public int SoldQuantity { get; set; }
        public DateTime Date { get; set; }
    }
}
