using System; 
using System.Globalization;
namespace BookManagementSystem
{
     
    class BookManagement
    {

        List<BookDetails> bookDetails = new List<BookDetails>() {
            new BookDetails() {BookId =1,BookName="C#",PublishedDate="22/02/2019",Author="Bowsky",price=199.99 },
            new BookDetails() {BookId =1,BookName="Dot net",PublishedDate="22/02/2016",Author="resky",price=859.99 },
            new BookDetails() {BookId =2,BookName="HTML",PublishedDate="11/08/2012",Author="hinch",price=1600.01 },
            new BookDetails() {BookId =3,BookName="CSS",PublishedDate="12/11/2010",Author="Usopp",price=1100.99 },
            new BookDetails() {BookId =4,BookName="Jquery",PublishedDate="12/11/2010",Author="Xorro",price=2200.99 },
            new BookDetails() {BookId =5,BookName="GOT",PublishedDate="12/11/2010",Author="Xorro",price=2200.99 },
            new BookDetails() {BookId =6,BookName="HOTD",PublishedDate="12/11/2020",Author="Xorro",price=2200.99 },
            new BookDetails() {BookId =7,BookName="MoneyHeist",PublishedDate="12/11/2010",Author="Xorro",price=2200.99 },
        };
        List<Stocks> stocks;
        public BookManagement()
        {
            stocks = new List<Stocks>();
            stocks.Add(new Stocks() { Id = 1, BookId = 4, Quantity = 11 });
            stocks.Add(new Stocks() { Id = 2, BookId = 3, Quantity = 45 });
            stocks.Add(new Stocks() { Id = 3, BookId = 2, Quantity = 7 });
            stocks.Add(new Stocks() { Id = 4, BookId = 1, Quantity = 89 });
            stocks.Add(new Stocks() { Id = 5, BookId = 8, Quantity = 79 });
            stocks.Add(new Stocks() { Id = 6, BookId = 9, Quantity = 99 });
            stocks.Add(new Stocks() { Id = 7, BookId = 10, Quantity = 45 });
        }

        public void RemoveDuplicates()
        {
            bookDetails = bookDetails.GroupBy(x => x.BookId).Select(x => x.First()).ToList();
        }
        public void AfterDateFilter(string date)
        {
            DateTime checkDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var newList = bookDetails.Where(x => DateTime.ParseExact(x.PublishedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) > checkDate).ToList();
            Display(newList);
        }
        public void DisplayOnlyBooks()
        {
            foreach (var book in bookDetails)
            {
                Console.WriteLine($"{book.BookId}\t{book.BookName}\t{book.PublishedDate}\t{book.Author}\t{book.price}");
            }
        }
        public void FindBookQty(int id)
        {
            
            Stocks bk = stocks.FirstOrDefault(x => x.BookId == id);
            if(bk == null)
            {
                Console.WriteLine("No book stock found");
                return;
            }
            Console.WriteLine($"book id :  {bk.BookId}\t Quantity : {bk.Quantity}");
        }
        public void Display()
        {
            var records = (from book in bookDetails
                           join stock in stocks on book.BookId equals stock.BookId
                           select new
                           {
                               BookId = book.BookId,
                               BookName = book.BookName,
                               PublishedDate = book.PublishedDate,
                               Author = book.Author,
                               price = book.price,
                               quantity = stock.Quantity
                           }).ToList();
            foreach (var book in records)
            {
                Console.WriteLine($"{book.BookId}\t{book.BookName}\t{book.PublishedDate}\t{book.Author}\t{book.price}\t{book.quantity}");
            }
        }
        public void Display(List<BookDetails> list) 
        {
            var records = (from book in list
                           join stock in stocks on book.BookId equals stock.BookId
                           select new
                           {
                               BookId = book.BookId,
                               BookName = book.BookName,
                               PublishedDate = book.PublishedDate,
                               Author = book.Author,
                               price = book.price,
                               quantity = stock.Quantity
                           }).ToList();
            foreach (var book in records)
            {
                Console.WriteLine($"{book.BookId}\t{book.BookName}\t{book.PublishedDate}\t{book.Author}\t{book.price}");
            }
        }
        public void LeftJoin()
        {
            var records = from book in bookDetails
                          join stock in stocks
                          on book.BookId equals stock.BookId
                          into rec
                          from rc in rec.DefaultIfEmpty(new Stocks())
                          select new
                          {
                              bookId = book.BookId,
                              bookName = book.BookName,
                              publishedDate = book.PublishedDate,
                              author = book.Author,
                              price = book.price,
                              stockId = rc.Id == null ? 0: rc.Id,
                              Qty = rc.Quantity == null ? 0 : rc.Quantity,
                          };
            foreach (var book in records)
            {
                Console.WriteLine($"{book.bookId}\t{book.bookName}\t{book.publishedDate}\t{book.author}\t{book.price}\t{book.stockId}\t{book.Qty}");
            }

        }
        public void RightJoin()
        {
            var records = from stock in stocks
                          join book in bookDetails
                          on stock.BookId equals book.BookId
                          into rec
                          from rc in rec.DefaultIfEmpty(new BookDetails())
                          select new
                          {
                              id = stock.Id,
                              bookId = stock.BookId,
                              Qty = stock.Quantity,
                              bookName = rc.BookName == null ? "No Data" : rc.BookName,
                              publishedDate = rc.PublishedDate == null ? "No Data" : rc.PublishedDate,
                              author = rc.Author == null ? "No Data" : rc.Author,
                              price = rc.price == null ? 0 : rc.price,
                          };
            foreach (var book in records)
            {
                Console.WriteLine($"{book.id}\t{book.bookId}\t{book.bookName}\t{book.publishedDate}\t{book.author}\t{book.price}\t{book.Qty}");
            }

        }
        public static void Main(string[] args)
        {
            Console.WriteLine("\n\tWelcome\n\tPress Any Key To Continue");
            Console.ReadKey();
            var obj = new BookManagement();
            bool exit = false;
            while(exit == false)
            {
                Console.Clear();
                Console.WriteLine("\n\t1. Display all Books\n\t2. Remove Duplicates \n\t3. Find Quantity \n\t4. Find after a specific date\n\t5. Left Join \n\t6. Right Join\n\t 7. Exit");
                Console.WriteLine("\n\tPlease Enter a choice no.");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: Console.Clear();
                        Console.WriteLine("\tDisplaying All Books");
                        obj.Display();
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 2: Console.Clear();
                        Console.WriteLine("\n\tRemoving Duplicates....");
                        obj.RemoveDuplicates();
                        Console.WriteLine("\n\tRemoving Duplicates Done..");
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 3: Console.Clear();
                        obj.DisplayOnlyBooks();
                        Console.WriteLine("\n\tPlease Enter a book id to find qty");
                        obj.FindBookQty(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 4: Console.Clear();
                        Console.WriteLine("\n\tEnter Date to filter books that published after (dd/MM/yyyy)");
                        obj.AfterDateFilter(Console.ReadLine());
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 5:Console.Clear();
                        Console.WriteLine("Left Join");
                        obj.LeftJoin();
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 6: Console.Clear();
                        obj.RightJoin();
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break;
                    case 7:Console.Clear();
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\n\tInvalid Choice");
                        Console.WriteLine("\n\tPress enter to continue...");
                        Console.ReadLine();
                        break ;

                }
            }



        }
    }
}