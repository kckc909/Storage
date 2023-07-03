
using Storage.Lib.Bussiness;
using Storage.Lib.Entity;

namespace Storage.Lib.DataAccess
{
    public class InOut
    {
        // Lấy các Items trong Storage
        public static TheItem[] GetItemsInStorage()
        {
            string FilePath = "ItemsInStorage.txt";
            string[] readFile = {}, line;
            TheItem[] ItemsInStorage = new TheItem[0];
            TheItem item = new TheItem();
            try
            {
                readFile = File.ReadAllLines(FilePath);
                for(int i = 0; i < readFile.Length; i ++)
                {
                    if (string.IsNullOrEmpty(readFile[i]) == false)
                    {
                        line = readFile[i].Split("|");
                        item = Create.Item(line[0],line[1],int.Parse(line[2]), double.Parse(line[3]), double.Parse(line[4]),line[5]);
                        Array.Resize(ref ItemsInStorage, ItemsInStorage.Length + 1);
                        ItemsInStorage[ItemsInStorage.Length - 1] = item;
                    }
                }
            }
            catch (Exception e)
            {   
                Console.WriteLine(e);
                FixFileNotExists("ItemsInStorage.txt");   
            }
            return ItemsInStorage;
        }
        // Viết lại các Items trong Storage
        public static void SetItemsInStorage(TheItem[] items)
        {
            string FilePath = "ItemsInStorage.txt";
            try 
            {
            string[] lines = new string[0];
            foreach (TheItem item in items)
            {
                if (string.IsNullOrEmpty(item.ItemID) == false)
                    {
                    Array.Resize(ref lines, lines.Length + 1 );
                    lines[lines.Length - 1] = string.Join("|", item.ItemID, item.ItemName, item.ItemQuantity, item.ItemBuying, item.ItemSelling, item.Supplier);
                }
            }
            
            File.WriteAllText(FilePath, "");
            File.AppendAllLines(FilePath, lines);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                FixFileNotExists(FilePath) ;  
            }
        }
        // Lấy His Path
        public static string[] GetHistoryPaths()
        {
            string[] readFile = new string[0];
            string HisPath = @"History\History.txt";
            try
            { 
                foreach (string line in File.ReadAllLines(HisPath))
                {
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        Array.Resize(ref readFile, readFile.Length + 1);
                        readFile[readFile.Length - 1] = line;
                    }
                }
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);   
                FixFileNotExists(HisPath);
            }
            return readFile;
        }
        // Thêm His Path
        public static void SetHistoryPaths(string[] newLine)
        {   
            string HisPath = @"History\History.txt";
            try
            {
                File.AppendAllLines(HisPath, newLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);   
                FixFileNotExists(HisPath);
            }
        }
        // Thêm His có các dòng là nội dung lines tên là Name
        public static void SetHistory(string Name, string[] lines)
        {
            string fpath = @"History";
            try
            {
                fpath = Path.Combine(fpath, Name);
                StreamWriter writer = new StreamWriter(fpath);
                foreach(string line in lines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);   
                FixFileNotExists(fpath);
            }
        }
        // Lấy các dòng của his có tên Name
        public static string[] GetHistory(string path)
        {
            string [] readFile = {};
            string fpath = @$"History\{path}";
            try 
            {
                readFile = File.ReadAllLines(fpath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);   
                FixFileNotExists(fpath);                
            }
            return readFile;
        }
        // New Account 
        public static void SetAccount(TheAccount account)
        {
            string[] ac = { account.Account , account.Password, account.Name, account.SepLine};
            File.AppendAllLines("Account.txt", ac);
        }
        // Lấy Account
        public static TheAccount[] GetAccount()
        {
            string[] lines = {};
            TheAccount[] aclist = {};
            try
            {
                lines = File.ReadAllLines("Account.txt");
                aclist = new TheAccount[lines.Length/4];
                int count = 0;
                for (int i = 0; i < lines.Length; i += 4)
                {
                    if (string.IsNullOrEmpty(lines[i]) == false)
                    {    
                        aclist[count] = Create.Account(lines[i], lines[i + 1], lines[i + 2]);               
                        count ++;
                    }    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                FixFileNotExists("Account.txt");
            }

            return aclist;
        }
        // fix
        static void FixFileNotExists (string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName, true);
            writer.Write("");
            writer.Close();
        }

    }
}