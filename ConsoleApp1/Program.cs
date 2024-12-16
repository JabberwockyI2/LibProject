using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace MyLibrary
{
    class Library
    {
        static List<Book> books = new List<Book>();
        static List<Magazine> magazines = new List<Magazine>();

        static void Main(string[] args)
        {

            InitializeBooks();
            InitializeMagazines();

            static void PrintYellow(string text)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(text);
                Console.ResetColor();
            }

            static void ShowMainMenu()
            {
                Console.Clear();
                Console.WriteLine("Library Management System");
                Console.WriteLine();

                PrintYellow("S");
                Console.WriteLine(" - Search by Title");
                Console.WriteLine();

                PrintYellow("1");
                Console.WriteLine(" - Books List");

                PrintYellow("2");
                Console.WriteLine(" - Borrow a Book");

                PrintYellow("3");
                Console.WriteLine(" - Return a Book");
                Console.WriteLine();

                PrintYellow("4");
                Console.WriteLine(" - Magazines List");

                PrintYellow("5");
                Console.WriteLine(" - Borrow a Magazine");

                PrintYellow("6");
                Console.WriteLine(" - Return a Magazine");
                Console.WriteLine();

                PrintYellow("0");
                Console.WriteLine(" - Exit");
                Console.WriteLine();

                Console.Write("Choose an ");
                PrintYellow("option");
                Console.Write(":");
            }

            while (true)
            {
                ShowMainMenu();

                string choice = Console.ReadLine()?.ToLower();

                switch (choice)
                {
                    case "s":
                        SearchByTitle();
                        break;
                    case "1":
                        ShowBooksList();
                        break;
                    case "2":
                        BorrowBook();
                        break;
                    case "3":
                        ReturnBook();
                        break;
                    case "4":
                        ShowMagazinesList();
                        break;
                    case "5":
                        BorrowMagazine();
                        break;
                    case "6":
                        ReturnMagazine();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void SearchByTitle()
        {
            Console.Clear();
            Console.Write("Enter the title to search: ");
            string searchTerm = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Search cannot be empty. Returning to the main menu...");
                Console.ReadKey();
                return;
            }

            var matchingBooks = books.
                Where(book => book.Title.
                ToLower().Contains(searchTerm)).ToList();

            var matchingMagazines = magazines.
                Where(magazines => magazines.Title.
                ToLower().Contains(searchTerm)).ToList();

            Console.WriteLine("\nSearch Results:");
            if (matchingBooks.Count == 0 && matchingMagazines.Count == 0)
            {
                Console.WriteLine("No matches found");
            }
            else
            {
                Console.WriteLine("\nBooks:");
                foreach (var book in matchingBooks)
                {
                    Console.ForegroundColor = book.IsBorrowed ? ConsoleColor.Red : ConsoleColor.White;
                    Console.WriteLine($"{book.ID}: {book.Title} by {book.Author}");
                }

                Console.ResetColor();

                Console.WriteLine("\nMagazines:");
                foreach (var magazine in matchingMagazines)
                {
                    Console.WriteLine($"{magazine.ID}: {magazine.Title}, {magazine.Issue}");
                }
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
        static void InitializeBooks()
        {
            books.Add(new Book
            {
                ID = 1,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                IsBorrowed = false
            });

            books.Add(new Book
            {
                ID = 2,
                Title = "1984",
                Author = "George Orwell",
                IsBorrowed = false
            });

            books.Add(new Book
            {
                ID = 3,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                IsBorrowed = false
            });
        }
        static void InitializeMagazines()
        {
            magazines.Add(new Magazine
            {
                ID = 1,
                Title = "National Geographic",
                Issue = "October 2024",
                IsBorrowed = false
            });

            magazines.Add(new Magazine
            {
                ID = 2,
                Title = "Time",
                Issue = "December 2024",
                IsBorrowed = false
            });

            magazines.Add(new Magazine
            {
                ID = 3,
                Title = "Popular Science",
                Issue = "November 2024",
                IsBorrowed = false
            });
        }
        static void ShowBooksList(bool pauseAfterDisplay = true)
        {
            Console.Clear();
            Console.WriteLine("Books List:");
            Console.WriteLine();

            foreach (var book in books)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{book.ID}: ");

                Console.ForegroundColor = book.IsBorrowed ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine($"{book.Title} by {book.Author}");
            }

            Console.ResetColor();

            if (pauseAfterDisplay)
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
        }
        static void BorrowBook()
        {
            Console.Clear();
            Console.WriteLine("Borrow a Book:");
            ShowBooksList(false);

            bool bookBorrowed = false;
            while (!bookBorrowed)
            {
                Console.Write("\nEnter the ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID");
                Console.ResetColor();
                Console.Write(" of the book you want to borrow, or press '0' to cancel: ");
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        break;
                    }

                    Book bookToBorrow = books.Find(book => book.ID == id);

                    if (bookToBorrow != null && !bookToBorrow.IsBorrowed)
                    {
                        bookToBorrow.IsBorrowed = true;
                        Console.WriteLine($"You have successfully borrowed " + $"\"{bookToBorrow.Title}\". Press any key to return to the main menu...");
                        Console.ReadKey();
                        bookBorrowed = true;
                    }
                    else if (bookToBorrow != null && bookToBorrow.IsBorrowed)
                    {
                        Console.Beep(1000, 500);
                        Console.Write("This book is already borrowed. Select one that is not in ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("RED");
                        Console.ResetColor();
                        Console.WriteLine(".");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Beep(1000, 500);
                        Console.WriteLine("Book not found. Please try again.");
                    }
                }
                else
                {
                    Console.Beep(1000, 500);
                    Console.Write("Invalid ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ID.");
                    Console.ResetColor();
                    Console.Write(" Please enter a numeric value.");
                    Console.WriteLine();
                }
            }
        }
        static void ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("Return a Book:");
            ShowBooksList(false);

            bool bookReturned = false;
            while (!bookReturned)
            {
                Console.Write("\nEnter the ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID");
                Console.ResetColor();
                Console.Write(" of the book you want to return, or press '0' to cancel: ");
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        break;
                    }

                    Book bookToReturn = books.Find(book => book.ID == id);

                    if (bookToReturn != null && bookToReturn.IsBorrowed)
                    {
                        bookToReturn.IsBorrowed = false;
                        Console.WriteLine($"You have successfully returned " + $"\"{bookToReturn.Title}\". Press any key to return to the main menu...");
                        Console.ReadKey();
                        bookReturned = true;
                    }
                    else if (bookToReturn != null && !bookToReturn.IsBorrowed)
                    {
                        Console.Beep(1000, 500);
                        Console.Write("This book was not borrowed. Please return one in ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("RED");
                        Console.ResetColor();
                        Console.Write(" only.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Beep(1000, 500);
                        Console.WriteLine("Book not found. Please try again.");
                    }
                }
                else
                {
                    Console.Beep(1000, 500);
                    Console.Write("Invalid ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ID.");
                    Console.ResetColor();
                    Console.Write(" Please enter a numeric value.");
                    Console.WriteLine();
                }
            }
        }
        static void ShowMagazinesList(bool pauseAfterDisplay = true)
        {
            Console.Clear();
            Console.WriteLine("Magazines List:");
            Console.WriteLine();

            foreach (var magazines in magazines)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{magazines.ID}: ");

                Console.ForegroundColor = magazines.IsBorrowed ? ConsoleColor.Red : ConsoleColor.White;

                Console.WriteLine($"{magazines.Title} (Issue: {magazines.Issue})");
            }

            Console.ResetColor();

            if (pauseAfterDisplay)
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
        }
        static void BorrowMagazine()
        {
            Console.Clear();
            Console.WriteLine("Borrow a Magazine:");
            ShowMagazinesList(false);

            bool magazineBorrowed = false;
            while (!magazineBorrowed)
            {
                Console.Write("\nEnter the ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID");
                Console.ResetColor();
                Console.Write(" of the magazine you want to borrow, or press '0' to cancel: ");
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        break;
                    }

                    Magazine magazineToBorrow = magazines.Find(magazine => magazine.ID == id);

                    if (magazineToBorrow != null && !magazineToBorrow.IsBorrowed)
                    {
                        magazineToBorrow.IsBorrowed = true;
                        Console.WriteLine($"You have successfully borrowed " + $"\"{magazineToBorrow.Title}\". Press any key to return to the main menu...");
                        Console.ReadKey();
                        magazineBorrowed = true;
                    }
                    else if (magazineToBorrow != null && magazineToBorrow.IsBorrowed)
                    {
                        Console.Beep(1000, 500);
                        Console.Write("This magazine is already borrowed. Select one that is not in ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("RED");
                        Console.ResetColor();
                        Console.WriteLine(".");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Beep(1000, 500);
                        Console.WriteLine("Magazine not found. Please try again.");
                    }
                }
                else
                {
                    Console.Beep(1000, 500);
                    Console.Write("Invalid ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ID.");
                    Console.ResetColor();
                    Console.Write(" Please enter a numeric value.");
                    Console.WriteLine();
                }
            }
        }
        static void ReturnMagazine()
        {
            Console.Clear();
            Console.WriteLine("Return a Magazine:");
            ShowMagazinesList(false);

            bool magazineReturned = false;
            while (!magazineReturned)
            {
                Console.Write("\nEnter the ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID");
                Console.ResetColor();
                Console.Write(" of the magazine you want to borrow, or press '0' to cancel: ");
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (id == 0)
                    {
                        break;
                    }

                    Magazine magazineToReturn = magazines.Find(magazine => magazine.ID == id);

                    if (magazineToReturn != null && magazineToReturn.IsBorrowed)
                    {
                        magazineToReturn.IsBorrowed = false;
                        Console.WriteLine($"You have successfully returned " + $"\"{magazineToReturn.Title}\". Press any key to return to the main menu...");
                        Console.ReadKey();

                        magazineReturned = true;
                    }
                    else if (magazineToReturn != null && !magazineToReturn.IsBorrowed)
                    {
                        Console.Beep(1000, 500);
                        Console.Write("This magazine was not borrowed. Please return one in ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("RED");
                        Console.ResetColor();
                        Console.Write(" only.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Beep(1000, 500);
                        Console.WriteLine("Magazine not found. Please try again.");
                    }
                }
                else
                {
                    Console.Beep(1000, 500);
                    Console.Write("Invalid ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("ID.");
                    Console.ResetColor();
                    Console.Write(" Please enter a numeric value.");
                    Console.WriteLine();
                }
            }
        }
        class Book
        {
            private int id;
            private string title;
            private string author;
            private bool isBorrowed;

            public int ID
            {
                get { return id; }
                set { id = value; }
            }
            public string Title
            {
                get { return title; }
                set { title = value; }
            }
            public string Author
            {
                get { return author; }
                set { author = value; }
            }
            public bool IsBorrowed
            {
                get { return isBorrowed; }
                set { isBorrowed = value; }
            }
            public override string ToString()
            {
                return $"{ID}: {Title} by {Author}" + (IsBorrowed ? " (Borrowed)" : "");
            }
        }
        class Magazine
        {
            private int id;
            private string title;
            private string issue;
            private bool isBorrowed;

            public int ID
            {
                get { return id; }
                set { id = value; }
            }
            public string Title
            {
                get { return title; }
                set { title = value; }
            }
            public string Issue
            {
                get { return issue; }
                set { issue = value; }
            }
            public bool IsBorrowed
            {
                get { return isBorrowed; }
                set { isBorrowed = value; }
            }
        }
    }
}


