using Lab_5_App.Models;
using System.Runtime.InteropServices.Marshalling;

namespace Lab_5_App.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ICSVWriter _writer;
        public List<Book> books { get; set; } = new();
        public List<User> users { get; set; } = new();
        public Dictionary<User, List<Book>> borrowedBooks { get; set; } = new();

        public string _userFilePath { get; } = "./Data/Users.csv";
        public string _bookFilePath { get; } = "./Data/Books.csv";

        public LibraryService(ICSVWriter writer)
        {
            _writer = writer;
        }

        public void ReadBooks(string path)
        {
            try
            {
                foreach (var line in File.ReadLines(path))
                {
                    var fields = line.Split(',');

                    if (fields.Length >= 4)
                    {
                        var book = new Book
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Title = fields[1].Trim(),
                            Author = fields[2].Trim(),
                            ISBN = fields[3].Trim()
                        };

                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void ReadUsers(string path)
        {
            try
            {
                foreach (var line in File.ReadLines(path))
                {
                    var fields = line.Split(',');

                    if (fields.Length >= 3) // Ensure there are enough fields
                    {
                        var user = new User
                        {
                            Id = int.Parse(fields[0].Trim()),
                            Name = fields[1].Trim(),
                            Email = fields[2].Trim()
                        };

                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void AddBook(string title, string author, string isbn, string path)
        {
            int id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            Book newB = new Book { Id = id, Title = title, Author = author, ISBN = isbn };
            books.Add(newB);

            _writer.WriteToCSV(newB, path);
        }

        public int DeleteBook(int id, string path)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                books.Remove(book);

                _writer.OverwriteCSV(books, path);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int EditBook(int id, string title, string author, string isbn, string path)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                book.Title = title;
                book.Author = author;
                book.ISBN = isbn;
                _writer.OverwriteCSV(books, path);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void AddUser(string name, string email, string path)
        {
            int id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            User newU = new User { Id = id, Name = name, Email = email };
            users.Add(newU);
            
            _writer.WriteToCSV(newU, path);
        }



        public int DeleteUser(int id, string path)
        {
            User user = users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                users.Remove(user);
                _writer.OverwriteCSV(users, path);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int EditUser(int id, string name, string email, string path)
        {
            User user = users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                _writer.OverwriteCSV(users, path);
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int BorrowBook(int u_id, int b_id)
        {
            User use = users.FirstOrDefault(u => u.Id == u_id);
            Book boo = books.FirstOrDefault(b => b.Id == b_id);

            if (use != null && boo != null)
            {
                if (!borrowedBooks.ContainsKey(use))
                {
                    borrowedBooks.Add(use, new List<Book>() { boo });
                    books.Remove(boo);
                    return 1;
                }
                else if (!borrowedBooks[use].Contains(boo))
                {
                    borrowedBooks[use].Add(boo);
                    books.Remove(boo);
                    return 1;
                }
                return -1; 
            }
            else
            {
                return -1;
            }
        }

        public int ReturnBook(int u_id, int b_id)
        {
            User use = users.FirstOrDefault(u => u.Id == u_id);

            if (use != null)
            {
                if (!borrowedBooks.ContainsKey(use))
                {
                    return -1;
                }

                Book bookRemove = borrowedBooks[use].First(b => b.Id == b_id);
                if (bookRemove != null)
                {
                    books.Add(bookRemove);
                    borrowedBooks[use].Remove(bookRemove);
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int ViewBorrowedBooks(int u_id)
        {
            User user = users.FirstOrDefault(u => u.Id == u_id);

            if (user != null)
            {

                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
