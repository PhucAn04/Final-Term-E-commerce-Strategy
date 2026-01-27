namespace Ecommerce.ViewModels
{
    public class TopSellingProductViewModel
    {
        public string MaSp { get; set; } = null!;
        public string TenSp { get; set; } = null!;
        public string? AnhDaiDien { get; set; }
        public decimal GiaLonNhat { get; set; }
        public int TotalSold { get; set; }
    }
}
