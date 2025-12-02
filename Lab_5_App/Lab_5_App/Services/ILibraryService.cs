using Lab_5_App.Models;

namespace Lab_5_App.Services
{
    public interface ILibraryService
    {
        public List<Book> books { get; }
        public List<User> users { get; }
        public Dictionary<User, List<Book>> borrowedBooks { get; }

        public string _bookFilePath { get; }
        public string _userFilePath { get; }
        public void ReadBooks(string path);

        public void ReadUsers(string path);
        public void AddBook(string title, string author, string isbn, string path);

        public int DeleteBook(int id, string path);

        public int EditBook(int id, string title, string author, string isbn, string path);

        public void AddUser(string name, string email, string path);

        public int DeleteUser(int id, string path);

        public int EditUser(int id, string name, string email, string path);

        public int BorrowBook(int u_id, int b_id);

        public int ReturnBook(int u_id, int b_id);

        public int ViewBorrowedBooks(int u_id);
    }
}
