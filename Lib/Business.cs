
using Storage.Lib.Entity;
using Storage.Lib.DataAccess;

namespace Storage.Lib.Bussiness
{
    public class Create
    {
        public static TheItem Item(string ID, string Name, int Quantity, double Buying , double Selling , string Supplier)
        {
            TheItem item = new TheItem();
            item.ItemID = ID;
            item.ItemName = Name;
            item.ItemQuantity = Quantity;
            item.ItemBuying = Buying;
            item.ItemSelling = Selling;
            item.Supplier = Supplier;
            return item;
        }
        public static TheSupplier Supplier(string Name, TheItem[]? ItemsProve, int NumProvde = 0)
        {
            TheSupplier supplier = new TheSupplier();
            supplier.SupplierName = Name;
            supplier.NumProvde = NumProvde;
            supplier.ItemsProve = ItemsProve;
            return supplier;
        }
        public static TheAccount Account(string Account, string Password , string Name)
        {
            TheAccount ac = new TheAccount();
            ac.Account = Account;
            ac.Password = Password;
            ac.Name = Name;
            return ac;
        }
        public static void History(string mode, TheItem[] Items, string mem, string time)
        {   
            string Name = string.Join("___", mode.ToUpper() , mem, time);
            string[] names = {Name};
            string[] lines = new string[Items.Length];

            for (int i = 0; i < Items.Length; i ++)
            {
                lines[i] = string.Join("|", Items[i].ItemID, Items[i].ItemName, Items[i].ItemQuantity, Items[i].ItemBuying, Items[i].ItemSelling, Items[i].Supplier);
            }
            InOut.SetHistoryPaths(names);
            InOut.SetHistory(Name, lines);         
        }
        public static string Time()
        {
            string Time = DateTime.Now.ToString();
            Time = Time.Replace("/", "-");
            Time = Time.Replace(":", "-");
            return Time;
        }
        public static TheStatistic Statistic(int Goods, double Sum, TheItem max, TheItem min)
        {
            TheStatistic statistic = new TheStatistic();
            statistic.NumGoods = Goods;
            statistic.Sum = Sum;
            statistic.max = max;
            statistic.min = min;
            return statistic;
        }
    }
    public class Check
    {
        // kiểm tra hợp lệ tài khoản
        public static bool[] Account(string Account, string Password)
        {
            bool[] check = {false, false};

            TheAccount[] acList = InOut.GetAccount();
            
            for ( int i = 0 ; i < acList.Length ; i++)
            {
                if (acList[i].Account == Account && acList[i].Password == Password)
                {
                    check[0] = true;
                    check[1] = true;
                    break;
                }
                if (acList[i].Account == Account)
                {
                    check[0] = true;
                }
            }
        return check;            
        }
        // kiểm tra tồn tại của item
        public static int Item(TheItem[] checkList,TheItem checkItem )
        {
            int count = 0;
            foreach (TheItem item in checkList)
            {
                if (Equals(item.ItemID, checkItem.ItemID))
                    return count;
                count ++;
            }
            return -1;
        }
        // kiểm tra tồn tại của supplier
        public static int Supplier(List<TheSupplier> checkList, TheSupplier checkSupp)
        {
            int count = 0;
            foreach (TheSupplier supp in checkList)
            {
                if (Equals(supp.SupplierName, checkSupp.SupplierName))
                    return count;
                count ++;
            }
            return -1;
        }
        // Xác định his
        public static bool History(string hispath)
        {
            string[] lines = InOut.GetHistoryPaths();
            foreach (string line in lines)
            {
                if (line == hispath)
                {
                    return true;
                }
            }
            return false;
        }
        // kiểm tra hợp lệ ngày
        public static bool Day(int y, int m, int d)
        {
            if (d > 0){
                switch (m){
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (d <= 31){
                        return true;
                    }
                    else return false;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (d < 31){
                        return true;
                    }
                    else return false;
                case 2:
                    if ((y % 4 == 0 && y % 100 != 0) || y % 400 == 0){
                        if (d < 30){
                            return true;
                        }
                        else return false;
                    }
                    else {
                        if (d < 29){
                            return true;
                        }
                        else return false;
                    }
                }
            }
            return false;
        }
    }
    public class Buss
    {
        public static void QuantityDown(ref TheItem item, int quan)
        {
            item.ItemQuantity -= quan;
        }
        public static void QuantityUp(ref TheItem item, int addQuantity)
        {
            item.ItemQuantity += addQuantity;
        }
        public static void ItemChange(ref TheItem Item, TheItem newItem)
        {
            Item.ItemID = newItem.ItemID;
            Item.ItemName = newItem.ItemName;
            Item.ItemQuantity = newItem.ItemQuantity;
            Item.ItemBuying = newItem.ItemBuying;
            Item.ItemSelling = newItem.ItemSelling;
            Item.Supplier = newItem.Supplier;
        }
    }
    public class Data 
    {
        // Nhập tài khoản vào Account
        public static void AccountIn(TheAccount account)
        {
            InOut.SetAccount(account);
        }
        // Lấy thông tin Account cố tài khoản là ac 
        public static TheAccount AccountOut(string ac)
        {
            foreach (TheAccount account in InOut.GetAccount())
            {
                if (ac == account.Account)
                {
                    return account;
                }
            }
            return Create.Account("","","Unknown");
        }
        // Lấy mảng các Item chứa xâu s trong ID hoặc Name -- > Tìm kiếm vs siêu tìm kiếm
        public static TheItem[] ItemContainsS(string s , bool full = false)
        {
            TheItem[] lines = InOut.GetItemsInStorage();
            TheItem[] Items = new TheItem[0];
            TheItem item = new TheItem();
            string l = "";
            // Tìm kiếm tương đối
            if (full)
            {
                foreach (TheItem line in lines)
                {   
                    l = string.Join("|", line.ItemID, line.ItemName, line.ItemQuantity, line.ItemBuying, line.ItemSelling, line.Supplier);
                    if (l.ToUpper().Contains(s.ToUpper()))
                    {
                        Array.Resize(ref Items, Items.Length + 1);
                        Items[Items.Length - 1] = line;
                    }
                }   
            }
            // Tìm kiếm tuyệt đối
            else {
                foreach (TheItem line in lines)
                {
                    if (line.ItemID.Contains(s) || line.ItemName.Contains(s))
                    {
                        Array.Resize(ref Items, Items.Length + 1);
                        Items[Items.Length - 1] = line;
                    }
                }
            }

            return Items;
        }
        // Truyền item vào trong Storage, nếu trong kho có mặt hàng cùng ID thì tăng số lượng
        public static void ItemsIn(TheItem[] Items)
        {
            int u;
            TheItem[] ItemsInStorage = {};
            try 
            {
                ItemsInStorage = InOut.GetItemsInStorage();
                foreach (TheItem item in Items)
                {
                    u = Check.Item(ItemsInStorage, item);
                    if (u != -1)
                    {
                        Buss.QuantityUp(ref ItemsInStorage[u] ,item.ItemQuantity);
                    }
                    else 
                    {
                        Array.Resize(ref ItemsInStorage, ItemsInStorage.Length + 1);
                        ItemsInStorage[ItemsInStorage.Length - 1] = item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            InOut.SetItemsInStorage(ItemsInStorage);
        }
        // Lấy item trong kho ra
        public static void ItemsOut(TheItem[] Items)
        {
            TheItem[] ItemsInStorage = InOut.GetItemsInStorage();
            foreach (var item in Items)
            {   
                for (int i = 0 ; i < ItemsInStorage.Length; i ++)
                {
                    if (ItemsInStorage[i].ItemID == item.ItemID )
                    {
                        Buss.QuantityDown(ref ItemsInStorage[i], item.ItemQuantity);
                    }
                }
            }
            InOut.SetItemsInStorage(ItemsInStorage);
        }
        // Thay thế item
        public static void ReplaceItem(TheItem oItem, TheItem nItem)
        {
            TheItem[] itemlts = InOut.GetItemsInStorage();

            for (int i = 0; i < itemlts.Length; i++)
            {
                if (Equals(itemlts[i].ItemID,oItem.ItemID))
                {
                    itemlts[i] = nItem;
                    break;
                }
            }
            InOut.SetItemsInStorage(itemlts);
        }        
        // DelItem
        public static void DeleteItem(TheItem dItem)
        {
            TheItem[] itemlts = InOut.GetItemsInStorage();
            List<TheItem> nlst = new List<TheItem>();
            for (int i = 0; i < itemlts.Length; i++)
            {
                if (Equals(itemlts[i].ItemID,dItem.ItemID) == false)
                {
                    nlst.Add(itemlts[i]);
                }
            }
            InOut.SetItemsInStorage(nlst.ToArray());
        }
        // Lấy his path chứa xâu s --> tìm kiếm
        public static List<string[]> GetHistotyPaths(string s = "")
        {   
            string[] lines = InOut.GetHistoryPaths();
            List<string[]> Hispaths = new List<string[]>();

            for (int i = 0; i < lines.Length ; i++)
            {
                if (lines[i].ToUpper().Contains(s.ToUpper()))
                {
                    Hispaths.Add(lines[i].Split("___"));
                }
            }

            return Hispaths;
        }
        // đọc các dòng trong his - mỗi dòng là thông tin 1 sản phẩm
        public static TheItem[] GetHistory(string path)
        {
            string[] lines = InOut.GetHistory(path);
            string[] line ;
            TheItem[] Items = new TheItem[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split("|");
                Items[i] = Create.Item(line[0], line[1], int.Parse(line[2]), double.Parse(line[3]), double.Parse(line[4]), line[5]);
            }
            return Items;
        }
    }
    public class Statistical
    {
        // Trả về mảng nhập hàng , xuất hàng
        static void Import_Export(string mode, string[] mdy, List<string[]> paths, ref string[] ImportBill, ref string[] ExportBill)
        {
            // mdy [ month , day , year]
            string[] day = new string[3];

            foreach(var path in paths)  //  mode - staff - time (day + time)
            {
                day = path[2].Split(" ")[0].Split("-");    // month + day + year

                if 
                (  (Equals(mode.ToLower(),"y") && Equals(day[2], mdy[2]))
                || (Equals(mode.ToLower(),"m") && Equals(day[2], mdy[2]) && Equals(day[0], mdy[0]))
                || (Equals(mode.ToLower(),"d") && Equals(day[2], mdy[2]) && Equals(day[1], mdy[1]) && Equals(day[0], mdy[0])))
                {
                    if (Equals(path[0], "IMPORT"))
                    {
                        Array.Resize(ref ImportBill, ImportBill.Length + 1);
                        ImportBill[ImportBill.Length - 1] = string.Join("___", path);
                    }
                    else if (Equals(path[0], "EXPORT"))
                    {
                        Array.Resize(ref ExportBill, ExportBill.Length + 1);
                        ExportBill[ExportBill.Length - 1] = string.Join("___", path);
                    }
                }
            }
        }
        // tính toán thống kê | mode = y / m / d
        public static void GetItemImEx(string mode, string[] mdy, out TheStatistic import, out TheStatistic export, out double Income)
        {
            List<string[]> paths = Data.GetHistotyPaths();
            string[] ImportBill = new string[0];
            string[] ExportBill = new string[0];
            
            Import_Export(mode, mdy, paths, ref ImportBill, ref ExportBill);

            // Tính tổng giá trị hàng nhập
            import = Calculate("import", ImportBill);
            export = Calculate("export", ExportBill);

            Income = export.Sum - import.Sum;

        }
        // Tính thống kê : tổng số tiền, tổng số hàng, hàng xuất nhiều nhất và ít nhất
        static TheStatistic Calculate(string mode ,string[] Bills)
        {
            string[] lines, line;

            double Sum = 0;
            int AllGoods = 0;
            TheItem Item = new TheItem();
            TheItem[] Items = new TheItem[0];

            // chia thanh item
            foreach (var bill in Bills)
            {
                int i ;
                lines = InOut.GetHistory(bill);
                foreach (var l in lines)
                {
                    line = l.Split("|");    // 0 id - 1 name - 2 quan - 3 buy - 4 sell - 5 supp
                    Item = Create.Item(line[0], line[1], int.Parse(line[2]), double.Parse(line[3]), double.Parse(line[4]), line[5]);

                    i = Check.Item(Items, Item);
                    if (i != -1)
                    {
                        Buss.QuantityUp(ref Items[i], Item.ItemQuantity);
                    }
                    else 
                    {
                        Array.Resize(ref Items, Items.Length + 1);
                        Items[Items.Length - 1] = Item;
                    }
                }
            }

            // tinh tong tien va tong hang
            if (mode.ToLower() == "import")
            {
                foreach (var item in Items)
                {
                    AllGoods += item.ItemQuantity;
                    Sum += item.ItemQuantity * item.ItemBuying;
                }
            }
            else
            {
                foreach (var item in Items)
                {
                    AllGoods += item.ItemQuantity;
                    Sum += item.ItemQuantity * item.ItemSelling;
                }
            }

            TheItem max = new TheItem();
            TheItem min = new TheItem();
            min = Items[0];
            foreach (var item in Items)
            {
                if (string.IsNullOrEmpty( item.ItemID ) == false)
                {
                    if (item.ItemQuantity > max.ItemQuantity)
                    {
                        max = item;
                    }
                    if (item.ItemQuantity < min.ItemQuantity)
                    {
                        min = item;
                    }
                }
            }

        return Create.Statistic(AllGoods, Sum, max, min);
        }
        // Thống kê hàng hoa
        public static TheStatistic Goods()
        {
            TheStatistic sta = new TheStatistic();

            sta.High = new List<TheItem>();
            sta.Low = new List<TheItem>();
            
            TheItem[] items = InOut.GetItemsInStorage();

            sta.BestExpensive = new TheItem();
            sta.Cheapest = new TheItem();

            try
            {
                sta.BestExpensive = items[0];
                sta.Cheapest = items[0];
            }
            catch
            {}

            foreach(var item in items)
            {
                //  tổng hàng
                sta.NumGoods += item.ItemQuantity;
                // tổng giá trị
                sta.Sum += item.ItemQuantity * item.ItemSelling;
                // hàng nhiều
                if (item.ItemQuantity > 100)
                {
                    sta.High.Add(item);
                }
                // hàng ít
                if (item.ItemQuantity < 10)
                {
                    sta.Low.Add(item);
                }
                // đắt nhất
                if (sta.BestExpensive.ItemSelling < item.ItemSelling)
                {
                    sta.BestExpensive = item;
                }
                // rẻ nhất
                if (sta.Cheapest.ItemSelling > item.ItemSelling)
                {
                    sta.Cheapest = item;
                }
            }
            return sta;
        }
        // supp -- > thông tin các supp
        public static List<TheSupplier> Supp()
        {
            TheItem[] Items = InOut.GetItemsInStorage();
            List<TheSupplier> SuppList = new List<TheSupplier>();
            bool exist = false;
            TheItem[] s;

            foreach (var item in Items)
            {
                for (int i = 0; i < SuppList.Count; i++)
                {
                    if (Equals(item.Supplier, SuppList[i].SupplierName))
                    {
                        SuppList[i].NumProvde ++;
                        exist = true;
                    }
                }
                if (exist)
                {
                    exist = false;
                }
                else 
                {
                    s = new TheItem[] {item};
                    SuppList.Add(Create.Supplier(item.Supplier,s, 1));
                }
            }
            return SuppList;
        }
        
    }
}
