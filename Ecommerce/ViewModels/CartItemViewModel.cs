namespace Ecommerce.ViewModels
{
    public class CartItemViewModel
    {
        public string MaSp { get; set; }
        public string hinh { get; set; }
        public string tenSp { get; set; }
        public double donGia { get; set; }
        public int soLuong { get; set; }
        public double thanhTien => soLuong * donGia;
    }
}
