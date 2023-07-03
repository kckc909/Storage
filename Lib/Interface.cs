
using Storage.Lib.Bussiness;
using Storage.Lib.Entity;
using System.Text;

namespace Storage.Lib.Interface
{   
    public class Menu
    {
        // ► ╬ ║ ═ ╔ ╗ ╚ ╝ ╩ ╦ ╠ ╣ │┌┐└┘─
        // static string rim;
        // Giới thiệu
        public static void Introduce()
        {
            Console.Clear();
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.Write("╔");     Cover("═",118,0,false);     Console.WriteLine("╗");
            Console.WriteLine($"{"║",-40}{"Trường Đại học Sư phạm Kĩ thuật Hưng Yên", -60}{"║",20}");
            Console.WriteLine($"{"║",-48}{"Khoa Công nghệ Thông tin", -52}{"║",20}");
            Console.WriteLine($"{"║",-100}{"║",20}");
            Console.WriteLine($"{"║",-40}{"Bài tập lớn môn Cơ sở kĩ thuật lập trình", -60}{"║",20}");
            Console.WriteLine($"{"║",-100}{"║",20}");
            Console.WriteLine($"{"║", -36}{"Đề tài              : Quản lý nhập xuất kho hàng", -64}{"║",20}");
            Console.WriteLine($"{"║", -36}{"Tác giả             : Chu Đức Minh", -64}{"║",20}");
            Console.WriteLine($"{"║", -36}{"Giáo viên hướng dẫn : Ngô Thanh Huyền", -64}{"║",20}");
            Console.Write("╚");     Cover("═",118,0,false);     Console.WriteLine("╝");

            Console.ReadKey(true);
        }
        // ly
        static void Cover(string p, int num, int ly = 0, bool nl = true)
        {
            for (int i = 0; i < ly; i++)    {Console.Write(" ");}
            for (int i = 0; i < num; i++)   {Console.Write(p);}
            if (nl)                         {Console.WriteLine();}
        }
        // Giao diện chung 
        static void Screen(string title, int NeL, string[] lines, int row, int col, int step = 1, bool newsc = true)
        {
            if (newsc) {Console.Clear();}

            Console.Write("╔");     Cover("═",118,0,false);     Console.WriteLine("╗");
            Console.Write($"{"║", -50}"); ChangeColor($"{title,-50}", "green"); Console.WriteLine($"{"║",20}");
            for (int i = 0 ; i < NeL ; i++)
            {
                Console.WriteLine($"{"║",-60}{"║",60}");
            }
            Console.Write("╚");     Cover("═",118,0,false);     Console.WriteLine("╝");

            foreach (var l in lines)
            {   
                Console.SetCursorPosition(col, row);
                Console.Write($"{l}");
                row += step;
            }
        }
        // Màn hình đăng ký đăng nhập
        public static void StorageLoginScreen()
        {
            string[] lines = {"Sign Up", "Log In", "Exit"};
            Screen("Manage Storage", 10, lines, 3, 54 , 2);            

            int l = MoveCursorUpDown(3, 7, 52, 2);
            if (l == -1 || l == 7)
            {
                Console.Clear();
                Environment.Exit(0);
            }
            else
            {
                if (l == 3)         {SignUp();}
                else if (l == 5)    {Login();}
            }
        }
        // Khai báo tài khoản
        static TheAccount staff = Create.Account("","","Unknown");
        // Đăng ký 
        static void SignUp(){
            // variable
            string Account = "" , Password = "", Name = "";
            int c ;
            string[] lines = {"Account   : ", "Password  : ", "Name      : "};

            // show
            Screen("Sign Up", 9, lines, 3, 28, 2);

            // Data in
            for (;;){
                Console.SetCursorPosition(40, 3);
                c = InputField(ref Account, 40, false, true);
                if (c == -1)    {StorageLoginScreen();}
                if (c == 0 && Account != "" && Password != "" && Name != "")     {break;}

                // Hiển thị nếu tài khoản đã tồn tại
                if (Check.Account(Account, Password)[0])
                {
                    Console.SetCursorPosition(40, 4);
                    ChangeColor("Account already existed", "red");
                }
                else
                {
                    Console.SetCursorPosition(40, 4);
                    Console.Write("                       ");
                }

                Console.SetCursorPosition(40, 5);
                c = InputField(ref Password, 40, false, true, true);
                if (c == -1)    {StorageLoginScreen();}
                if (c == 0 && Account != "" && Password != "" && Name != "")     {break;}

                Console.SetCursorPosition(40, 7);
                c = InputField(ref Name, 40, false,false);
                if (c == -1)    {StorageLoginScreen();}
                if (c == 0 && Account != "" && Password != "" && Name != "")     {break;}

            }
            
            // Data Check
            string[] line;
            if (Check.Account(Account, Password)[0])
            {
                line = new string[] {"Account already existed", "Please use other account"};
                Screen("Sign Up", 5, line, 3,40, 2);
                Console.ReadKey();
                SignUp();
                Environment.Exit(0);
            }
            line = new string[] {"Sign Up successful"};
            Screen("Sign Up", 5, line, 3, 45);
            Console.ReadKey();

            // Get Data
            Data.AccountIn(Create.Account(Account,Password,Name));
            Menu.StorageLoginScreen();
        }
        // Đăng nhập
        static void Login(){
            // varialbe 
            string Account = "", Password = "";
            int c;
            string[] lines = {"Account  :", "Password :"};
            Screen("Log In", 7, lines, 3, 27, 2);

            // Data in
            for (;;){
                Console.SetCursorPosition(38, 3);
                c = InputField(ref Account, 40, false, true );
                if (c == -1)    {StorageLoginScreen();}
                if (c == 0 && Account != "" && Password != "")     {break;}

                Console.SetCursorPosition(38, 5);
                c = InputField(ref Password, 40, false, true    , true);
                if (c == -1)    {StorageLoginScreen();}
                if (c == 0 && Account != "" && Password != "")     {break;}
            }
        
            // data check
            if (Check.Account(Account, Password)[0] && Check.Account(Account, Password)[1]){
                staff = Data.AccountOut(Account);
                string[] ls = {"Login successful !"};
                Screen("Log In", 5, ls, 3, 45);
                Console.ReadKey(true);
            }
            else 
            {
                string[] ls = {"Invalid account or password !"};
                Screen("Log In", 5, ls ,3, 45);
                Console.ReadKey();          
                Login();
            }
        
            MainMenu();
        }
        // Đổi màu chuỗi S hiển thị ra màn hình
        static void ChangeColor(string s, string color){
            color = color.ToUpper();
            if (color == "RED")
                {
                    Console.ForegroundColor = ConsoleColor.Red;     // Warning
                }
            if (color == "YELLOW")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;  // Instruct
                }
            if (color == "GREEN")
            {
                Console.ForegroundColor = ConsoleColor.Green;       // Congratulation
            }
            Console.Write(s);
            Console.ResetColor();
        }
        // Menu
        static void MainMenu()
        {
            // Variable
            string[] lines = {"Management" ,"Search", "Report", "Turtorial"};
            Screen("MainMenu", lines.Length * 2 + 3, lines, 3, 50, 2);
            int l = MoveCursorUpDown(3, 9, 48, 2);
            if (l == -1)
            {
                Screen("MainMenu", 3, new string[] {"Press Esc to sign out!"}, 3, 45);
                if (Console.ReadKey(true).KeyChar == 27)
                {
                    StorageLoginScreen();
                }
                MainMenu();
            }
            else if (l == 3)
            {
                Module_Goods();
            }
            else if (l == 5)
            {
                Search();
            }
            else if (l == 7)
            {
                Module_Report();
            }
            else if (l == 9)
            {
                Turtorial();
            }              
        }
        // module mặt hàng
        static void Module_Goods()
        {
            string[] lines = new string[] {"Storage", "Import Items", "Export Items"};
            Screen("Management", lines.Length * 2 + 3, lines, 3, 50, 2);
            int l = MoveCursorUpDown(3, 7, 48, 2);
            if (l == -1)
            {
                MainMenu();
            }
            else if (l == 3)
            {
                Storage();
            }
            else if (l == 5)
            {
                Import_Items();
            }
            else if (l == 7)
            {
                Export_Items();
            }
        }
        static void Module_Report()
        {
            string[] lines = new string[] {
                "History",
                "Statistic"
            };
            Screen("Report", lines.Length * 2 + 3, lines, 3, 50, 2);
            int l = MoveCursorUpDown(3, 5, 48, 2);
            if (l == -1)
            {
                MainMenu();
            }   
            else if (l == 3)
            {
                History();
            }   
            else if (l == 5)
            {
                Statistic();
            }
        }
        // Giao diện sửa ? xóa ?
        static void Storage_2(TheItem cItem)
        {
            string[] lines = {"Fix Item", "Delete Item"};
            Screen("Storage", lines.Length * 2 + 3, lines, 3, 30, 2);
            int i = MoveCursorUpDown(3, 5, 27, 2);
            if (i == -1)
            {
                Storage();
            }
            else 
            {
                i = (i - 3)/2;
                if (i == 0)
                {
                    FixGoods(cItem);
                }
                else 
                {
                    DeleteGoods(cItem);
                }
            }
        } 
        // DEl item
        static void DeleteGoods(TheItem dItem)
        {
            string[] lines = {
                "Do you want to delete this goods ?",
                "  Yes                         No"
            };
            Screen("Delete Item", lines.Length * 2 + 3, lines, 4, 42, 2);
            int a = 42;
            for(;;)
            {
                Console.SetCursorPosition(a, 6);
                if (Console.ReadKey(true).KeyChar == 13)
                {
                    if (a == 42)   {
                        Data.DeleteItem(dItem);
                        TheItem nu = new TheItem(){ItemID = "Null", ItemName = "Delete Item"};
                        TheItem[] on = {dItem, nu};
                        string time = Create.Time();
                        Create.History("Del", on, staff.Name, time);
                        Storage();
                    } 
                    else            
                        Storage();
                }
                else 
                {
                    a = (a == 42) ? 70 : 42; 
                }
            }
        }
        // Kiểm tra kho hàng
        static void Storage(){
            string s = "";
            char c;
            TheItem[] items = Data.ItemContainsS("");
            string[] lines = {"Search : "};
            int cl = items.Count();
            Console.Clear();
            Screen("Storage", 5, lines, 3, 15);
            
            Console.SetCursorPosition(0,9);
            ChangeColor($"{"Items", 58}", "green");
            Console.WriteLine();
            Console.WriteLine($"    {"ID", -20} {"Name", -40} {"Quantity", -16} {"Buying Price", -16} {"Selling Price", -16} {"Supplier"}");
            Console.WriteLine();

            for (;;)
            {
                Console.SetCursorPosition(0,12);
                for (int i = 0; i < cl; i++)
                {
                    Console.WriteLine($"{"",150}");
                }
                
                Console.SetCursorPosition(0,12);
                items = Data.ItemContainsS(s);
                foreach(TheItem item in items)
                {
                    Console.Write($"    {item.ItemID,-20} {item.ItemName, -40} {item.ItemQuantity, -16}");
                    Console.Write($" {item.ItemBuying, -16} {item.ItemSelling, -16} {item.Supplier}");
                    Console.WriteLine();
                }
           
                Console.SetCursorPosition(25 + s.Length, 3);
                c = Console.ReadKey(true).KeyChar;
                if (c == 8 && s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    Console.SetCursorPosition(25 + s.Length, 3);
                    Console.Write(" ");
                    Console.SetCursorPosition(25 + s.Length, 3);
                }
                else if (c == 13)
                {
                    int l = MoveCursorUpDown(12, 11 + items.Length, 2);
                    if (l == -1)
                    {
                        Storage();
                    }
                    else
                    {
                        l = l - 12;
                        try
                        {
                            Storage_2(items[l]);
                            Storage();
                        }
                        catch 
                        {
                            Console.SetCursorPosition(40, 14);
                            ChangeColor("No found Goods !", "red");
                            Console.ReadKey();
                            Console.SetCursorPosition(40, 14);
                            Console.Write("                ");
                        }
                    }             
                }
                else if (c == 27)
                {   
                    Module_Goods();
                    Environment.Exit(0);
                }
                else if (c > 31 && c < 127)
                {
                    s += c;
                    Console.Write(c);
                }
            }
        }
        // Sửa mặt hàng
        static void FixGoods(TheItem rpitem)
        {
            string id = rpitem.ItemID, name = rpitem.ItemName, quan = rpitem.ItemQuantity.ToString(), buy = rpitem.ItemBuying.ToString();
            string sell = rpitem.ItemSelling.ToString(), supp = rpitem.Supplier;
            int c ;
            TheItem[] items = Data.ItemContainsS("");
            int check = -1;
            string[] lines = {
            $"{"ID              : " + id}", 
            $"{"Name            : " + name}", 
            $"{"Quantity        : " + quan}",
            $"{"Buying Price    : " + buy}", 
            $"{"Selling Price   : " + sell}", 
            $"{"Supplier        : " + supp}"
            };
            
            Screen("Old Goods", 14, lines, 3, 15, 2);
            Console.SetCursorPosition(0, 17);
            Screen("New Goods", 14, lines, 20, 15, 2,false);
            
            for (;;){
                Console.SetCursorPosition(33, 20);
                c = InputField(ref id, 30);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
                TheItem it = new TheItem() {ItemID = id};
                Console.SetCursorPosition(33, 21);
                check = Check.Item(items, it);
                if (check != -1)
                {
                    ChangeColor("Invalid ID, please use other ID", "red");
                }
                else 
                {
                    Console.Write("                               ");
                }
                Console.SetCursorPosition(33, 22);
                c = InputField(ref name, 40);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
                Console.SetCursorPosition(33, 24);
                c = InputField(ref quan, 20, true);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
                Console.SetCursorPosition(33, 26);
                c = InputField(ref buy, 20, true);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
                Console.SetCursorPosition(33, 28);
                c = InputField(ref sell, 20, true);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
                Console.SetCursorPosition(33, 30);
                c = InputField(ref supp, 40);
                if (c == -1)    {Storage();}
                else if(c==0 && id != "" && name != "" && quan != "" && buy != "" && sell != "" && supp != "" && check == -1) {break;}
            }
            
            TheItem item = Create.Item(id, name, int.Parse(quan), int.Parse(buy), int.Parse(sell), supp);

            TheItem[] on = {rpitem, item};
            string time = Create.Time();
            Create.History("Fix", on, staff.Name, time);
            Data.ReplaceItem(rpitem, item);
        }
        // Input field
        static int InputField(ref string s , int maxlength = 20, bool numonly = false, bool acpw = false, bool hide = false)
        {
            (int a, int b) = Console.GetCursorPosition();
            char c;
            
            Console.SetCursorPosition(a + s.Length, b);

            for(;;)
            {
                c = Console.ReadKey(true).KeyChar;
                
                if (c == 8)
                {
                    if (s.Length > 0)
                    {
                        s = s.Substring(0, s.Length - 1);
                        Console.SetCursorPosition(a + s.Length, b);
                        Console.Write(" ");
                        Console.SetCursorPosition(a + s.Length, b);
                    }
                }   
                else if (c == 9)
                {
                    return 1;   
                }
                else if (c == 13)
                {
                    return 0;
                }
                else if (c == 27)
                {
                    return -1;
                }
                if (acpw)
                {
                    if (hide && c > 31 && c < 127 && s.Length < maxlength)
                    {
                        s += c;
                        Console.Write("*");
                    }
                    else if (c > 31 && c < 127 && s.Length < maxlength)
                    {
                        s += c;
                        Console.Write(c);
                    }
                }
                else if (acpw == false){
                    if (numonly == false && c > 31 && s.Length < maxlength)
                    {
                        s += c;
                        Console.Write(c);
                    }
                    else if (numonly && c > 47 && c < 58 && s.Length < maxlength)
                    {
                        s += c;
                        Console.Write(c);
                    }
                }
            }
        }
        // Nhập hàng
        static void Import_Items()
        {
            string id = "", name = "", quan = "", buypr = "" , supp = "";
            char c ;
            TheItem goods;
            TheItem[] ItemsImport = new TheItem[0];
            string time;
            string[] lines = 
            {" Item's ID       : "
            ," Item's Name     : "
            ," Item's Quantity : "
            ," Item's Buying   : "
            ," Item's Supplier : "
            ,""};
            start : int l = 3;

            Screen("Import Goods", lines.Length * 2 + 1, lines, 3, 12, 2);
            Console.WriteLine();
            Console.WriteLine(); // warning line
            Console.WriteLine();
            ChangeColor($"{"", 50} Goods list", "green"); Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{"",5}{"ID", -20} {"Name", -40} {"Quantity", -15} {"Buying Price", -15} {"Supplier"}");
            Console.WriteLine();
            foreach(TheItem item in ItemsImport)
            {
                Console.Write($"{"",5}{item.ItemID, -20} {item.ItemName, -40}");
                Console.Write($" {item.ItemQuantity, -15} {item.ItemBuying, -15}");
                Console.Write($" {item.Supplier}");
                Console.WriteLine();
            }

            Console.SetCursorPosition(31, l + id.Length);
            for (;;)
            {
                c = Console.ReadKey(true).KeyChar;
                if (c == 8)     // xóa
                {
                    if (id.Length > 0 && l == 3)
                    {
                        id = id.Substring(0, id.Length - 1);
                        Console.SetCursorPosition(31 + id.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(31 + id.Length, l);
                    }
                    else if (name.Length > 0 && l == 5)
                    {
                        name = name.Substring(0, name.Length - 1);
                        Console.SetCursorPosition(31 + name.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(31 + name.Length, l);
                    }
                    else if (quan.Length > 0 && l == 7)
                    {
                        quan = quan.Substring(0, quan.Length - 1);
                        Console.SetCursorPosition(31 + quan.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(31 + quan.Length, l);
                    }
                    else if (buypr.Length > 0 && l == 9)
                    {
                        buypr = buypr.Substring(0, buypr.Length - 1);
                        Console.SetCursorPosition(31 + buypr.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(31 + buypr.Length, l);
                    }
                    else if (supp.Length > 0 && l == 11)
                    {
                        supp = supp.Substring(0, supp.Length - 1);
                        Console.SetCursorPosition(31 + supp.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(31 + supp.Length, l);
                    }
                }
                else if (c == 9)    // chuyển dòng 
                {
                    if (l == 3)
                    {
                        l += 2;
                        Console.SetCursorPosition(31 + name.Length, l);
                    }
                    else if (l == 5)
                    {
                        l += 2;
                        Console.SetCursorPosition(31 + quan.Length, l);
                    }
                    else if (l == 7)
                    {
                        l += 2;
                        Console.SetCursorPosition(31 + buypr.Length, l);
                    }
                    else if (l == 9)
                    {
                        l += 2;
                        Console.SetCursorPosition(31 + supp.Length, l);
                    }
                    else 
                    {
                        l = 3;
                        Console.SetCursorPosition(31 + id.Length, l);
                    }
                }
                else if (c == 13)   // nhập hàng vào kho và xuất hóa đơn
                {
                    int i;
                    if (id != "" && name != "" && quan != "" && buypr != "" && supp != "")
                    {   
                        double sellpr = int.Parse(buypr) * 12 / 10;

                        goods = Create.Item(id, name , int.Parse(quan), double.Parse(buypr), sellpr, supp);
                        i = Check.Item(ItemsImport, goods);
                        if (i != -1)
                        {
                            Buss.QuantityUp(ref ItemsImport[i], goods.ItemQuantity);
                        }
                        else
                        {
                            Array.Resize(ref ItemsImport, ItemsImport.Length + 1);
                            ItemsImport[ItemsImport.Length - 1] = (goods);
                        }
                        id = ""; name = ""; quan = ""; buypr = ""; supp = "";
                        goto start;
                    }
                    else if (id == "" && name == "" && quan == "" && buypr == "" && supp == "") 
                    {
                        if (ItemsImport.Length == 0)
                        {
                            string[] lns = {"Do not have any items to import into the storage !"};
                            Console.Clear();
                            Screen("Stroage", lns.Length*2 + 1, lns, lns.Length*2 + 1, 30);
                            Console.ReadKey(true);
                            goto start;
                        }
                        else 
                        {
                            time = DateTime.Now.ToString();
                            time = time.Replace("/", "-");
                            time = time.Replace(":", "-");
                            Data.ItemsIn(ItemsImport);
                            BillScreen("import", ItemsImport, staff.Name, time);
                            Create.History("import", ItemsImport, staff.Name , time);
                            MainMenu();
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(5, 13);
                        ChangeColor ($"{"", 25} Some attributes are empty !","yellow");
                        Console.ReadKey(true);
                        Console.SetCursorPosition(5, 13);
                        Console.Write($"{"",25}                            ");
                        if (id == "")
                        {
                            l = 3;
                            Console.SetCursorPosition(31, l);
                        }
                        else if (name == "")
                        {
                            l = 5;
                            Console.SetCursorPosition(31, l);
                        }
                        else if (quan == "")
                        {
                            l = 7;
                            Console.SetCursorPosition(31, l);
                        }
                        else if (buypr == "")
                        {
                            l = 9;
                            Console.SetCursorPosition(31, l);
                        }
                        else if (supp == "")
                        {
                            l = 11;
                            Console.SetCursorPosition(31,l);
                        }
                    }       
                }
                else if (c == 27)   // quay lại MainMenu
                {
                    if (ItemsImport.Count() == 0)
                    {
                        Module_Goods();
                        Environment.Exit(0);
                    }
                    WarningScreen();
                    c = Console.ReadKey(true).KeyChar;
                    if (c == 27)
                    {
                        Module_Goods();
                        Environment.Exit(0);
                    }
                    else {
                        goto start;
                    }
                }
                else    // nhập
                {   
                    if (l == 3 && id.Length < 40)
                    {
                        id += c;
                        Console.Write(c);
                    }
                    else if (l == 5 && name.Length < 40)
                    {
                        name += c;
                        Console.Write(c);
                    }
                    else if (l == 7 && c > 47 && c < 58 && quan.Length < 20)
                    {
                        quan += c;
                        Console.Write(c);
                    }
                    else if (l == 9 && c > 47 && c < 58 && buypr.Length < 20)
                    {
                        buypr += c;
                        Console.Write(c);
                    }
                    else if (l == 11 && supp.Length < 21 && supp.Length < 40)
                    {
                        supp += c;
                        Console.Write(c);
                    }
                }
            }
        }
        // Xuất hàng
        static void Export_Items()
        {
            string s = "";
            int a;
            char c ;
            int lin ;
            TheItem[] items;
            TheItem choseitem = new TheItem();
            TheItem[] ExportItems = new TheItem[0];
            string[] line = {"Search : "};
            
            start : Console.Clear();
            Screen("Export Items",3,line, 3, 10);
            Console.SetCursorPosition(0,6);
            ChangeColor($"{"", 50} Export List", "green"); Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{"", 5}{"ID", -20} {"Name", -40} {"Quantity", -20} {"Selling Price", -20}");
            Console.WriteLine();
            
            foreach (var item in ExportItems)
            {
                Console.WriteLine($"{"", 5}{item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -20} {item.ItemSelling, -20}");
            }
            
            Console.WriteLine();
            ChangeColor($"{"", 50} Find List", "green"); Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{"", 5}{"ID", -20} {"Name", -40} {"Quantity",-20} {"Selling Price"}");
            Console.WriteLine();
            
            lin = 14 ;
            a =  Data.ItemContainsS(s).Length ;
            for (;;)
            {
                Console.SetCursorPosition(0,14);    
                for (int i = 0; i < a; i++ )
                {
                    Console.WriteLine($"{" ", -100}");
                }

                Console.SetCursorPosition(0, lin);
                items =  Data.ItemContainsS(s);
                foreach (var item in items) 
                {
                    Console.WriteLine($"{"", 5}{item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -20} {item.ItemSelling, -20}");
                }
    
                Console.SetCursorPosition(20 + s.Length, 3);
                c = Console.ReadKey(true).KeyChar;        
            
                if (c == 8 && s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    Console.SetCursorPosition(20 + s.Length, 3);
                    Console.Write(" ");
                    Console.SetCursorPosition(20 + s.Length, 3);
                }   
                else if (c == 13)   //  Chọn mặt hàng và số lượng hàng xuất ra
                {
                    Console.Clear();
                    Screen("Exprot Item", 0, new string[0], 2, 60);
                    Console.WriteLine($"{"", 5}{"ID", -20} {"Name", -40} {"Quantity",-20} {"Selling Price"}");
                    Console.WriteLine();
                    
                    foreach (var item in items) 
                    {
                        Console.WriteLine($"{"", 5}{item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -20} {item.ItemSelling, -20}");
                    }
                    
                    int l = MoveCursorUpDown(5, 5 - 1 + items.Length,3);
                    if (l == -1)
                    {
                        goto start;
                    }
                    else    // Chọn số lượng xuất và lưu vào mảng
                    {
                        string quan = "";
                        int between ;
                        l -= 5;
                        try
                        {
                            choseitem = items[l];
                        }
                        catch
                        {
                            Console.SetCursorPosition(40, 12);
                            ChangeColor("No found goods !", "red");
                            Console.ReadKey();
                            Export_Items();
                        }
                        
                        string l1 = ($"{"", 5}{"ID", -20} {"Name", -40} {"Quantity", -20} {"Selling Price"}");
                        string l2 = ($"{"", 5}{choseitem.ItemID,-21}{choseitem.ItemName,-41}{choseitem.ItemQuantity, -20} {choseitem.ItemSelling}");
                        string l3 = ($"{"", 9} Quantity : ");
                        string[] l4 = {l1, l2,l3};
                        Screen("Export Items", 8,l4 ,3, 10, 2);
                        for (;;)
                        {
                            c = Console.ReadKey(true).KeyChar;
                            
                            if (c == 8 && quan.Length > 0)
                            {
                                quan = quan.Substring(0, quan.Length - 1);
                                Console.SetCursorPosition(31 + quan.Length , 7);
                                Console.Write(" ");
                                Console.SetCursorPosition(31 + quan.Length, 7);
                            }
                            else if (c == 13)
                            {
                                if (quan.Length == 0 )      // nếu không nhập số thì 
                                {   
                                    Console.SetCursorPosition(31, 8);
                                    ChangeColor("Please enter quantity !", "yellow");
                                    Console.ReadKey(true);
                                    Console.SetCursorPosition(31, 8);
                                    Console.Write("                       ");
                                    Console.SetCursorPosition(31 + quan.Length, 7);
                                }
                                else 
                                {
                                    if (int.Parse(quan) <= choseitem.ItemQuantity)      
                                    {
                                        // Số lượng xuất
                                        TheItem epitem = Create.Item(choseitem.ItemID, choseitem.ItemName, int.Parse(quan), choseitem.ItemBuying, choseitem.ItemSelling, choseitem.Supplier);
                                        // Nếu mặt hàng đã tồn tại
                                        int i = Check.Item(ExportItems, epitem);
                                        if (i != -1)
                                        {
                                            if (ExportItems[i].ItemID == epitem.ItemID) 
                                            {
                                                between = ExportItems[i].ItemQuantity + epitem.ItemQuantity;
                                                if (between <= choseitem.ItemQuantity)
                                                {
                                                    Buss.QuantityUp(ref ExportItems[i], epitem.ItemQuantity);
                                                }
                                                else 
                                                {
                                                    Console.SetCursorPosition(30 ,8);
                                                    ChangeColor("Not enough goods", "red");
                                                    Console.SetCursorPosition(30, 9);
                                                    ChangeColor("Goods remaining : " + (choseitem.ItemQuantity - ExportItems[i].ItemQuantity) , "yellow");
                                                    Console.ReadKey(true);
                                                    Console.SetCursorPosition(30 ,8);
                                                    Console.WriteLine("                                    ");
                                                    Console.SetCursorPosition(30 ,9);
                                                    Console.WriteLine("                                    ");
                                                    Console.SetCursorPosition(31 + quan.Length , 7);
                                                }
                                            }
                                        }
                                        // Nếu mặt hàng chưa tồn tại
                                        else {
                                            Array.Resize(ref ExportItems, ExportItems.Length + 1);
                                            ExportItems[ExportItems.Length - 1] = epitem;
                                            lin ++;
                                        }
                                        // Hỏi xuất nhập
                                        Console.Clear();
                                        string[] lns = {$"Press Esc to export goods {"", -20} Press Anykey to continue"};
                                        Screen("Export Item", lns.Length * 2 + 1, lns, lns.Length * 2 + 1, 24);
                                        if (Console.ReadKey(true).KeyChar == 27)
                                        {
                                            string time = DateTime.Now.ToString();
                                            time = time.Replace("/", "-");
                                            time = time.Replace(":", "-");
                                            Data.ItemsOut(ExportItems);
                                            Create.History("export", ExportItems, staff.Name , time);
                                            BillScreen("export", ExportItems, staff.Name , time);
                                            Create.History("Import", ExportItems, staff.Name, time);
                                            MainMenu();
                                        }
                                        else 
                                        {
                                            goto start;
                                        }
                                    }
                                    else 
                                    {
                                        Console.SetCursorPosition(30, 9);
                                        string report = "The quantity want to export is more than the quantity in storage !";
                                        ChangeColor(report, "yellow");
                                        Console.ReadKey(true);
                                        Console.SetCursorPosition(30, 9);
                                        Cover(" ", report.Length, 0, false);
                                        Console.SetCursorPosition(31 + quan.Length, 7);
                                    }
                                }
                            }
                            else if (c == 27)
                            {
                                goto start;
                            }
                            else if (c > 47 && c < 58) 
                            {
                                quan += c;
                                Console.Write(c);
                            }
                        }
                    }             
                } 
                else if (c == 27)   // Out ra MainMenu
                {
                    if (ExportItems.Count() != 0)
                    {
                        WarningScreen();
                        if (Console.ReadKey(true).KeyChar == 27)
                        {
                            Module_Goods();
                        }
                        else 
                        {
                            goto start;
                        }
                    }
                    else
                    {
                        Module_Goods();
                    }
                    if (Console.ReadKey(true).KeyChar == 27)
                    {
                        Module_Goods();
                    }
                    else 
                    {
                        goto start;
                    }
                }
                else if (c > 31 && c < 127)
                {
                    s += c;
                    Console.Write(c);
                }
            }       
        }
        // Hiển thị lịch sử
        static void History()
        {
            string s = "";
            char c ;
            int nm;
            string[] line = {$"{"Search : "}"};

            Screen("History", 3, line, 3, 10);

            List<string[]> paths = Data.GetHistotyPaths();
            nm = paths.Count;
            for (;;){

                Console.SetCursorPosition(0,7);
                paths = Data.GetHistotyPaths(s);
                // clear search
                for(int m = 0; m < nm ; m++)
                {
                    Console.WriteLine($"{"",100}");
                }
                
                // write search
                Console.SetCursorPosition(0,7);                
                foreach(var p in paths)     // 0_Mode 1_staff 2_time
                {
                    Console.WriteLine($"{"",5}{p[0], -20}{p[1], -40}{p[2], -20}");
                }
                
                Console.SetCursorPosition(20 + s.Length, 3);
                c = Console.ReadKey(true).KeyChar;
                if (c == 27)
                {
                    Module_Report();
                }
                else if (c == 13)
                {
                    break;
                }
                else if (c == 8 && s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    Console.SetCursorPosition(20 + s.Length ,3);
                    Console.Write(" ");
                    Console.SetCursorPosition(20 + s.Length, 3);
                }
                else if (c > 31 && c < 127)
                {
                    Console.Write(c);
                    s += c;
                }
            }

            int l = MoveCursorUpDown(7, 7 - 1 + paths.Count, 3);
            if (l == -1)
            {
                History();
            }
            else 
            {
                l -= 7;
                try
                {
                    TheItem[] items = Data.GetHistory(string.Join("___", paths[l]));
                    BillScreen(paths[l][0], items, paths[l][1], paths[l][2]);
                }
                catch
                {
                    Console.SetCursorPosition(30, 9);
                    ChangeColor("No found History Path !", "red");
                    Console.ReadKey();
                }   
                finally
                {
                    History();
                }
            }
        }
        // Thống kê
        static void Statistic()
        {
            string[] lines = {"By Year", "By Month", "By Day", "By Goods", "By Suppliers"};
            Screen("Statistic", lines.Length * 2 + 3, lines, 3, 50, 2);
            int l = MoveCursorUpDown(3, 11, 48, 2);
            
            if (l == -1)
            {
                Module_Report();
            }
            else if (l == 3)
            {
                ByYear();
                Statistic();
            }
            else if (l == 5)
            {
                ByMonth();
                Statistic();
            }
            else if (l == 7)
            {
                ByDay();
                Statistic();
            }
            else if (l == 9)
            {
                ByGoods();
                Statistic();
            }
            else if (l == 11)
            {
                BySupp();
                Statistic();
            }
            Environment.Exit(0);
        }
        // thống kê theo năm -- > 
        static void ByYear()
        {
            string y = "";
            int year;
            char c;

            string [] lines = {"Year  :"};
            Screen("Statistic by year", 7, lines , 3, 22, 2);

            Console.SetCursorPosition(30 + y.Length, 3);
            for (;;){
                c = Console.ReadKey(true).KeyChar;
                if (c == 27)
                {
                    Statistic();
                }
                else if (c == 13 && int.TryParse(y, out year))
                {
                    break;
                }
                else if (c == 8)
                {
                    y = y.Substring(0, y.Length - 1);
                    Console.SetCursorPosition(30 + y.Length, 3);
                    Console.Write(" ");
                    Console.SetCursorPosition(30 + y.Length, 3);
                }
                else if (c > 47 && c < 58)
                {
                    y +=c ;
                    Console.Write(c);
                }
            }

            TheStatistic import;
            TheStatistic export;
            string[] time = {"","",y};
            double Income;

            Statistical.GetItemImEx("y", time, out import, out export, out Income);

            ShowStatstic("Year", import, export, Income);
            Console.Clear();
        }
        // thông kê theo tháng
        static void ByMonth()
        {
            string y = "" , m = "";
            int l = 3;
            char c;
            string [] lines = {"Year  :","Month :" };
            Screen("Statistic by month", 7, lines , 3, 22, 2);
            Console.SetCursorPosition(30 + y.Length, l);
            for (;;)
            {
                c = Console.ReadKey(true).KeyChar;
                if (c == 8)
                {
                    if (l == 3 && y.Length > 0)
                    {
                        y = y.Substring(0, y.Length - 1);
                        Console.SetCursorPosition(30 + y.Length, 3);
                        Console.Write(" ");
                        Console.SetCursorPosition(30 + y.Length, 3);
                    }
                    else if (l == 5 && m.Length > 0)
                    {
                        m = m.Substring(0, m.Length - 1);
                        Console.SetCursorPosition(30 + m.Length, 3);
                        Console.Write(" ");
                        Console.SetCursorPosition(30 + m.Length, 3);
                    }
                }
                else if (c == 9)
                {
                    if (l == 5)
                    {
                        l = 3;
                        Console.SetCursorPosition(30 + y.Length, l);
                    }
                    else
                    {
                        l = 5;
                        Console.SetCursorPosition(30 + m.Length, l);
                    }
                }
                else if (c == 13 )
                {
                    if (string.IsNullOrEmpty(y) == false && string.IsNullOrEmpty(m) == false)
                    {
                        if (int.Parse(m) > 0 && int.Parse(m) < 13)
                        {
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(30, 7);
                            ChangeColor("Invalid month !", "red");
                            Console.ReadKey();
                            Console.SetCursorPosition(30, 7);
                            Console.WriteLine("                          ");
                            l = 5;
                            Console.SetCursorPosition(30 + m.Length, l);
                            continue;
                        }
                    }
                    else if (l == 3)
                    {
                        l = 5;
                        Console.SetCursorPosition(30 + m.Length, l);
                    }
                    else if (l == 5)
                    {
                        l = 3;
                        Console.SetCursorPosition(30 + y.Length, l);
                    }
                }
                else if (c == 27)
                {
                    Statistic();
                    Environment.Exit(0);
                }
                else if (c > 47 && c < 58)
                {
                    if (l == 3)
                    {
                        y += c;
                        Console.Write(c);
                    }
                    else if (l == 5)
                    {
                        m += c;
                        Console.Write(c);
                    }
                }
            }

            TheStatistic import;
            TheStatistic export;
            string[] time = {m, "", y};
            double Income;

            Statistical.GetItemImEx("m", time, out import, out export, out Income);

            ShowStatstic("Month", import, export, Income);

            Statistic();
        }
        // Thống kê theo ngày
        static void ByDay()
        {
            string y = "" , m = "", d = "";
            int l = 3;
            char c;

            string [] lines = {"Year  :","Month :","Day   :" };
            Screen("Statistic by day", 7, lines , 3, 22, 2);

            Console.SetCursorPosition(30 + y.Length, l);
            for (;;)
            {
                c = Console.ReadKey(true).KeyChar;
                if (c == 8)
                {
                    if (l == 3 && y.Length > 0)
                    {
                        y = y.Substring(0, y.Length - 1);
                        Console.SetCursorPosition(30 + y.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(30 + y.Length, l);
                    }
                    else if (l == 5 && m.Length > 0)
                    {
                        m = m.Substring(0, m.Length - 1);
                        Console.SetCursorPosition(30 + m.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(30 + m.Length, l);
                    }
                    else if (l == 7 && d.Length > 0)
                    {
                        d = d.Substring(0, d.Length - 1);
                        Console.SetCursorPosition(30 + d.Length, l);
                        Console.Write(" ");
                        Console.SetCursorPosition(30 + d.Length, l);  
                    }
                }
                else if (c == 9)
                {
                    if (l == 7)
                    {
                        l = 3;
                        Console.SetCursorPosition(30 + y.Length, l);
                    }
                    else if (l == 5)
                    {
                        l = 7;
                        Console.SetCursorPosition(30 + d.Length, l);
                    }
                    else if (l == 3)
                    {
                        l = 5;
                        Console.SetCursorPosition(30 + m.Length, l);
                    }
                }
                else if (c == 13 )
                {
                    if (string.IsNullOrEmpty(y) == false && string.IsNullOrEmpty(m) == false && string.IsNullOrEmpty(d) == false)
                    {
                        if (int.Parse(m) > 0 && int.Parse(m) < 13)
                        {
                            if (Check.Day(int.Parse(y), int.Parse(m), int.Parse(d)))
                            {
                                break;
                            }
                            else    // báo lỗi ngày nếu ngày lỗi
                            {
                                Console.SetCursorPosition(30, 9);
                                ChangeColor("Invalid day !", "red");
                                Console.ReadKey();
                                Console.SetCursorPosition(30, 9);
                                Console.WriteLine("                          ");
                                l = 7;
                                Console.SetCursorPosition(30 + d.Length, l);
                                continue;
                            }
                        }
                        else    // báo lỗi nếu tháng lỗi
                        {
                            Console.SetCursorPosition(30, 9);
                            ChangeColor("Invalid month !", "red");
                            Console.ReadKey();
                            Console.SetCursorPosition(30, 9);
                            Console.WriteLine("                          ");
                            l = 5;
                            Console.SetCursorPosition(30 + m.Length, l);
                            continue;
                        }
                    }
                    else 
                    {
                        if (l == 7)
                        {
                            l = 3;
                            Console.SetCursorPosition(30 + y.Length, l);
                        }
                        else if (l == 5)
                        {
                            l = 7;
                            Console.SetCursorPosition(30 + d.Length, l);
                        }
                         else if (l == 3)
                        {
                            l = 5;
                            Console.SetCursorPosition(30 + m.Length, l);
                        }
                    }
                }
                else if (c == 27)
                {
                    Statistic();
                    Environment.Exit(0);
                }
                else if (c > 47 && c < 58)
                {
                    if (l == 3)
                    {
                        y += c;
                        Console.Write(c);
                    }
                    else if (l == 5)
                    {
                        m += c;
                        Console.Write(c);
                    }
                    else if (l == 7)
                    {
                        d += c;
                        Console.Write(c);
                    }
                }
            }

            TheStatistic import;
            TheStatistic export;
            string[] time = {m,d,y};
            double Income;

            Statistical.GetItemImEx("d", time, out import, out export, out Income);

            ShowStatstic("Day", import, export, Income);

            Statistic();
        }
        // Hiển thị thống kê doanh thu
        static void ShowStatstic(string mode, TheStatistic import, TheStatistic export, double Income)
        {   
            string[] lines = 
            {"" // 3
            ,$"{"The most Imported goods     : " + import.max?.ItemName}"
            ,$"{"The least Imported goods    : " + import.min?.ItemName}"
            ,$"{"Total Imported goods        : " + import.NumGoods}"
            ,$"{"Total Imported goods's Price: " + import.Sum}"
            ,"" // 13
            ,$"{"The most Exported goods     : " + export.max?.ItemName}"
            ,$"{"The least Exported goods    : " + export.min?.ItemName}"
            ,$"{"Total Exported goods        : " + export.NumGoods}"
            ,$"{"Total Exported goods's Price: " + export.Sum}"
            ,"" 
            ,$"{"Total income : ", 30}" + Income};

            Screen($"Statistic by {mode}",27, lines, 3, 20, 2);
            Console.ReadKey();

        }
        // Thống kê hàng hóa
        static void ByGoods()
        {
            TheStatistic sta = Statistical.Goods();
            Console.Clear();
            Console.WriteLine();
            ChangeColor($"{"Statistic by goods", 40}", "green"); Console.WriteLine();
            
            // more
            Console.WriteLine();
            ChangeColor($"{"Goods in lagre quantity : ", -20}", "green"); Console.WriteLine();

            if (sta.High != null && sta.High.Count != 0)
            {
                Console.WriteLine($"{"", 24} {"ID", -20} {"Name", -40} {"Quantity", -20}");
                foreach(var item in sta.High!)
                {
                    Console.WriteLine($"{"", 24} {item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -20}");
                }
            }
            else 
            {
                ChangeColor($"{"", 24}Have no goods is too much in storage !", "yellow"); Console.WriteLine();
            }
            
            // less
            Console.WriteLine();
            ChangeColor($"{"Goods in small quantity : ", -20}", "green"); Console.WriteLine();
            if (sta.Low != null && sta.Low.Count != 0)
            {
                Console.WriteLine($"{"", 24} {"ID", -20} {"Name", -40} {"Quantity", -20}");
                foreach(var item in sta.Low!)
                {
                    Console.WriteLine($"{"", 24} {item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -20}");
                }
            }
            else 
            {
                ChangeColor($"{"", 24}Have no goods is too low in storage !", "yellow"); Console.WriteLine();
            }

            // expensive
            Console.WriteLine();
            ChangeColor($"{"The best expensive goods:", -20}", "green");   Console.WriteLine();
            Console.WriteLine($"{"", 24} {sta.BestExpensive!.ItemID, -20} {sta.BestExpensive.ItemName, -40} {sta.BestExpensive.ItemQuantity, -20}");
            Console.WriteLine();
            ChangeColor($"{"The cheapest goods : ", -20}", "green");    Console.WriteLine();
            Console.WriteLine($"{"", 24} {sta.Cheapest!.ItemID, -20} {sta.Cheapest.ItemName, -40} {sta.Cheapest.ItemQuantity, -20}");
            Console.WriteLine();
            ChangeColor($"{"Total quantity : ", -20}", "green" ); Console.WriteLine(sta.NumGoods);
            Console.WriteLine();
            ChangeColor($"{"Total price  : ", -20}", "green"); Console.WriteLine(sta.Sum);

            Console.ReadKey();
            
        }
        // Thống kê nhà cung cấp | hiển thị các nhà cung cấp - > truy cập vào nhà cung cấp sẽ hiện ra các mặt hàng của nhà cung cấp đó
        static void BySupp()
        {
            List<TheSupplier> SuppList = Statistical.Supp();
            string[] lines = {};
            Screen("Statstic by supplier", 2, lines , 0 , 0);
            Console.WriteLine();
            Console.WriteLine($"{"", 5}{"Name", -30}{"Prove",-10}");
            Console.WriteLine();
            foreach(var supp in SuppList)
            {
                Console.WriteLine($"{"", 5}{supp.SupplierName, -30}{supp.NumProvde,-10}Items");
            }

            int l = MoveCursorUpDown(8, 8 + SuppList.Count - 1,2);
            if (l == -1)
            {
                Statistic();
            }
            else 
            {
                TheSupplier supp = new TheSupplier();
                l -= 8;
                try
                {
                    supp = SuppList[l];
                }
                catch
                {
                    Console.SetCursorPosition(40, 5);
                    Console.Write("Some thing wrong");
                    Console.ReadKey(true);
                    
                }
                lines = new string[] {$"{"Supplier : ", 30}" + supp.SupplierName};
                Screen("Statstic by supplier", 2, lines , 2 , 2);
                Console.SetCursorPosition(0, 5);
                Console.WriteLine();
                Console.WriteLine($"{"",5}{"ID", -20}{"Name", -35}{"Quantity", -20}{"Buying Price",-20}{"Selling Price", -20}");
                foreach(var item in supp.ItemsProve!)
                {
                    Console.WriteLine($"{"",5}{item.ItemID,-20}{item.ItemName,-35}{item.ItemQuantity,-20}{item.ItemBuying,-20}{item.ItemSelling}");
                }
            }
            Console.ReadKey();
        }
        // Tìm kiếm -> chỉ đọc nhưng là siêu tìm kiếm
        static void Search()
        {
            string s = "";
            TheItem[] items;
            char c;
            string[] line = {"Search Bar : "};
            Screen("Search", 3, line, 3, 10);
            Console.SetCursorPosition(0,9);
            Console.WriteLine($"    {"ID", -20} {"Name", -40} {"Quantity", -20} {"Buying Price", -20} {"Selling Price", -20} {"Supplier"}");

            for(;;){
                Console.SetCursorPosition(0,12);
                items = Data.ItemContainsS(s , true);
                // clear
                for (int i = 0; i < Data.ItemContainsS("").Length; i++)
                {
                    Console.WriteLine($"{"",150}");
                }
                Console.SetCursorPosition(0,12);
                // print
                foreach(TheItem item in items)
                {
                    Console.Write($"    {item.ItemID,-20} {item.ItemName, -40} {item.ItemQuantity, -20}");
                    Console.Write($" {item.ItemBuying, -20} {item.ItemSelling, -20} {item.Supplier}");
                    Console.WriteLine();
                }

                Console.SetCursorPosition(23 + s.Length, 3);
                c= Console.ReadKey(true).KeyChar;
                // Control
                if (c == 8 && s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    Console.SetCursorPosition(23 + s.Length, 3);
                    Console.Write(" ");
                    Console.SetCursorPosition(23 + s.Length, 3);
                }  
                else if (c == 27)
                {
                    MainMenu();
                }
                else if (c > 31 && c < 127)
                {
                    s += c;
                    Console.Write(c);
                }
            }
        }
        // Hướng dẫn căn bản
        static void Turtorial()
        {
            string [] lines = {
                "Press Tab to chose other function",
                "Press Enter to select or execute the function",
                "Press Esc to return to the previous function or cancel the current function",
                "Now press Esc to return to MainMenu"
            };
            Screen("Tutorial", 9, lines, 3, 24, 2);
            for(;;){if (Console.ReadKey(true).KeyChar == 27) break;}
            MainMenu();
        }
        // Cảnh báo không lưu tác vụ
        public static void WarningScreen()
        {
            Console.Clear();
            Console.Write("╔");     Cover("═",118,0,false);     Console.WriteLine("╗");
            Console.Write($"{"║", -50}"); ChangeColor($"{"",-50}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -50}"); ChangeColor($"{"Warning",-50}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -50}"); ChangeColor($"{"",-50}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -35}"); ChangeColor($"{"The action taken will not be saved !",-65}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -50}"); ChangeColor($"{"",-50}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -20}"); ChangeColor($"{$"{"Press Esc to exit to MainMenu       Press Anykey to continue "}",-80}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write($"{"║", -50}"); ChangeColor($"{"",-50}", "red"); Console.WriteLine($"{"║",20}");
            Console.Write("╚");     Cover("═",118,0,false);     Console.WriteLine("╝");            
            Console.SetCursorPosition(52, 6);
        }
        // Hiển thị hóa đơn
        public static void BillScreen(string mode, TheItem[] items, string mem, string time)
        {
            List<string> lines = new List<string>(); 
            double sum = 0;
            Console.Clear();
            // Có thể thêm Heading cho bill
            if (mode.ToLower() == "import")
            {             
                lines.Add($"{"ID", -20} {"Name", -40} {"Quantity", -15} {"Buying Price", -15} {"Supplier"}");
                foreach(TheItem item in items)
                {
                    lines.Add($"{item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -15} {item.ItemBuying, -15} {item.Supplier}");
                    sum = sum + item.ItemBuying * item.ItemQuantity;
                }               
                lines.Add($"{"Total : " + sum, 80}");
                lines.Add($"{"", 5}{"Staff : " + mem, -50}{"Time : " + time}");
                Screen("Import Items", lines.Count * 2 + 1, lines.ToArray(), 3, 5, 2);               
            }
            else if (mode.ToLower() == "export")
            {            
                lines.Add($"{"ID", -20} {"Name", -40} {"Quantity", -15} {"Selling Price", -15} {"Supplier"}");
                foreach(TheItem item in items)
                {
                    lines.Add($"{item.ItemID, -20} {item.ItemName, -40} {item.ItemQuantity, -15} {item.ItemSelling, -15} {item.Supplier}");
                    sum = sum + item.ItemSelling * item.ItemQuantity;
                }
                lines.Add($"{"Total : " + sum, 80}");
                lines.Add($"{"", 5}{"Staff : " + mem, -50}{"Time : " + time}");
                Screen("Export Items", lines.Count * 2 + 1, lines.ToArray(), 3, 5, 2);
            } 
            else if (mode.ToLower() == "fix")
            {
                lines.Add("Old : ");
                lines.Add($"{"ID", -20} {"Name", -40} {"Quantity", -15} {"Buying Price", -15} {"Selling Price", -15} {"Supplier"}");
                lines.Add($"{items[0].ItemID, -20} {items[0].ItemName, -40} {items[0].ItemQuantity, -15} {items[0].ItemBuying, -15} {items[0].ItemSelling, -15} {items[0].Supplier}");
                lines.Add("New : ");
                lines.Add($"{"ID", -20} {"Name", -40} {"Quantity", -15} {"Buying Price", -15} {"Selling Price", -15} {"Supplier"}");
                lines.Add($"{items[1].ItemID, -20} {items[1].ItemName, -40} {items[1].ItemQuantity, -15} {items[1].ItemBuying, -15} {items[1].ItemSelling, -15} {items[1].Supplier}");
                Screen("Fix Items", lines.Count * 2 + 1, lines.ToArray(), 3, 5, 2);
            }
            Console.ReadKey(true);
        }
        // di chuyển con trỏ  
        static int MoveCursorUpDown(int start , int end , int col, int sep = 1 , string cursor = "►")
        {
            char c;
            int l = start;
            string del = "";

            for (int i = 0; i < cursor.Length; i ++)
            {
                del += " ";
            }

            Console.SetCursorPosition(col , l);
            Console.Write(cursor);

            for (;;)
            {
                c = Console.ReadKey(true).KeyChar;
                if (c == 9)
                {
                    Console.SetCursorPosition(col, l);
                    Console.Write(del);
                    if (l >= end)
                    {
                        l = start;
                    }
                    else 
                    {
                        l += sep;
                    }
                    Console.SetCursorPosition(col, l);
                    Console.Write(cursor);
                }
                else if (c == 13)
                {
                    break;
                }
                else if (c == 27)
                {
                    l = -1;
                    break;
                }
                else 
                {
                    string tb = "Tab to move        Enter to chose";
                    Console.SetCursorPosition(col - tb.Length/2 + 4, end + 2);
                    ChangeColor(tb , "Yellow");
                    Console.SetCursorPosition(col + 3,end + 2);
                    Console.ReadKey(true);
                    Console.SetCursorPosition(col - tb.Length/2 + 4, end + 2);
                    Cover(" ", tb.Length, 0, false);
                    Console.SetCursorPosition(col,l);
                }
            }
            return l;
        }
        static void ShowItem(List<TheItem> lstItem)
        {
            Console.WriteLine($"    {"ID", -20} {"Name", -40} {"Quantity", -20} {"Buying Price", -20} {"Selling Price", -20} {"Supplier"}");
            foreach (var item in lstItem)
            {
                Console.WriteLine($"{"",5}{item.ItemID,-20}{item.ItemName,-40}{item.ItemQuantity,-20}{item.ItemBuying,-20}{item.ItemSelling, -20}{item.Supplier}");
            }
        }
    }
}
