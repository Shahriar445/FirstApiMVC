namespace FirstApiMVC.DTO
{
    public class ItemCreateDto
    {
       
            public string ?ItemName { get; set; }
            public int NumStockQuantity { get; set; }
            public bool IsActive { get; set; }
            public IFormFile? ImageFile { get; set; }
        

    }
}
