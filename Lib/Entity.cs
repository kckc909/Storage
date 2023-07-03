
namespace Storage.Lib.Entity
{
    public class TheItem
    {
        public string ItemID {get;set;} = string.Empty; //  Mã mặt hàng
        public string ItemName {get;set;} = string.Empty;   // Tên mặt hàng 
        public int ItemQuantity {get;set;}  // Số lượng mặt hàng 
        public double ItemBuying {get;set;} // Giá nhập của mặt hàng
        public double ItemSelling {get;set;}    // Giá bán của mặt hàng
        public string Supplier {get;set;} = string.Empty; // Nhà cung cấp
        public string properties()
        {
            return string.Join("|", ItemID, ItemName, ItemQuantity, ItemBuying, ItemSelling, Supplier);
        }
    }
    public class TheSupplier
    {   
        public string SupplierName {get;set;} = string.Empty;
        public int NumProvde {get;set;}
        public TheItem[]? ItemsProve {get;set;}
    } 
    public class TheAccount
    {
        public string Account {set;get;} = string.Empty;
        public string Password {get;set;} = string.Empty;
        public string Name {get;set;} = string.Empty;
        public string SepLine = "-";
    }
    public class TheStatistic
    {
        public int NumGoods{get;set;}   // tổng hàng giao dịch / Số lượng
        public double Sum{get;set;}     // tổng tiền giao dịch / Giá trị
        public TheItem? max {get;set;}  // Hàng giao dịch nhiều nhất 
        public TheItem? min{get;set;}   // hàng giao dịch ít nhất 
        public TheItem? BestExpensive{get;set;}     // hàng đắt nhất
        public TheItem? Cheapest{get;set;}  // rẻ nhất
        public List<TheItem>? High {get;set;}   // hàng còn nhiều
        public List<TheItem>? Low {get;set;}    // hàng sắp hết hoặc đã hết
    }
}