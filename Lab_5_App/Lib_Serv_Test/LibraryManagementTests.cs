using Lab_5_App.Models;
using Lab_5_App.Services;
using Moq;

namespace Lib_Serv_Test
{
    [TestClass]
    public sealed class LibraryManagementTests
    {
        [TestMethod]
        [DataRow("./Data/Test_Books.csv")]
        public void TestReadBooks(string path)
        {
            //Arrange 
            var writer = new Mock<ICSVWriter>();
            var service = new LibraryService(writer.Object);

            //Act
            service.ReadBooks(path);

            //Assert
            Assert.IsNotNull(service.books);
        }

        [TestMethod]
        [DataRow("./Data/Test_Users.csv")]
        public void TestReadUsers(string path)
        {
            //Arrange
            var writer = new Mock<ICSVWriter>();
            var service = new LibraryService(writer.Object);

            //Act
            service.ReadUsers(path);

            //Assert
            Assert.IsNotNull(service.books);
        }

        [TestMethod]
        [DataRow(1, "TestTitle1", "TestAuthor1", "1234567", "./Data/Test_Books.csv")]
        [DataRow(2, "TestTitle2", "TestAuthor2", "8910111", "./Data/Test_Books.csv")]
        public void TestAddBook(int id, string title, string author, string isbn, string path)
        {
            //Arrange 
            var mockWriter = new Mock<ICSVWriter>();
            var service = new LibraryService(mockWriter.Object);

            //Act
            service.AddBook(title, author, isbn, path);

            //Assert
            Assert.IsTrue(service.books.Count() == 1);
            mockWriter.Verify(w => w.WriteToCSV(
            It.Is<Book>(b => b.Title == title && b.Author == author && b.ISBN == isbn),
            It.IsAny<string>()
            ), Times.Once);
        }

        [TestMethod]
        [DataRow("TestUser1", "TestEmail1", "./Data/Test_Books.csv")]
        [DataRow("TestUser2", "TestEmail2", "./Data/Test_Books.csv")]
        public void TestAddUser(string name, string email, string path)
        {
            //Arrange 
            var mockWriter = new Mock<ICSVWriter>();
            var service = new LibraryService(mockWriter.Object);

            //Act
            service.AddUser(name, email, path);

            //Assert
            Assert.IsTrue(service.users.Count() == 1);
            mockWriter.Verify(w => w.WriteToCSV(
            It.Is<User>(b => b.Name == name && b.Email == email),
            It.IsAny<string>()
            ), Times.Once);
        }


        [TestMethod]
        [DataRow(1, "TestEditTitle1", "TestEditAuthor1", "123456", "./Data/Test_Books.csv")]
        [DataRow(2, "TestEditTitle2", "TestEditAuthor2", "234567", "./Data/Test_Books.csv")]
        public void TestEditBook(int id, string title, string author, string isbn, string path)
        {
            //Arrange
            var mockWriter = new Mock<ICSVWriter>();
            LibraryService service = new LibraryService(mockWriter.Object);
            service.books.Add(new Book { Id = id, Author = "BeforeChange", Title = "BeforeTitle", ISBN = "Before" });

            //Act
            service.EditBook(id, title, author, isbn, path);

            //Assert
            Assert.AreEqual(service.books.Where(b => b.Id == id).First().Title, title);
            mockWriter.Verify(w => w.OverwriteCSV(It.Is<List<Book>>(b => b == service.books), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow(1, "TestEditName1", "TestEditEmail1", "./Data/Test_Books.csv")]
        [DataRow(2, "TestEditName2", "TestEditEmail2", "./Data/Test_Books.csv")]
        public void TestEditUser(int id, string name, string email, string path)
        {
            //Arrange
            var mockWriter = new Mock<ICSVWriter>();
            LibraryService service = new LibraryService(mockWriter.Object);
            service.users.Add(new User { Name = name, Id = id, Email = email});

            //Act
            service.EditUser(id, name, email, path);

            //Assert
            Assert.AreEqual(service.users.Where(u => u.Id == id).First().Name, name);
            mockWriter.Verify(w => w.OverwriteCSV(
                It.Is<List<User>>(b => b == service.users),
                It.IsAny<string>()), Times.Once);
        }


        [TestMethod]
        [DataRow(1, "./Data/Test_Books.csv")]
        [DataRow(2, "./Data/Test_Books.csv")]
        public void TestDeleteBook(int id, string path)
        {
            //Arrange 
            var writer = new Mock<ICSVWriter>();
            var service = new LibraryService(writer.Object);
            service.books.Add(new Book { Id = id , Title = "test", Author = "Test", ISBN = "test"});

            //Act
            service.DeleteBook(id, path);

            //Assert
            Assert.IsTrue(service.books.Count() == 0);
            writer.Verify(w => w.OverwriteCSV(
                It.Is<List<Book>>(b => b == service.books),
                It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [DataRow(1, "./Data/Test_Books.csv")]
        [DataRow(2, "./Data/Test_Books.csv")]
        public void TestDeleteUser(int id, string path)
        {
            //Arrange 
            var writer = new Mock<ICSVWriter>();
            var service = new LibraryService(writer.Object);
            service.users.Add(new User { Id = id, Name = "Test", Email = "test" });

            //Act
            service.DeleteUser(id, path);

            //Assert
            Assert.IsTrue(service.users.Count() == 0);
            writer.Verify(w => w.OverwriteCSV(
                It.Is<List<User>>(b => b == service.users),
                It.IsAny<string>()), Times.Once);
        }
    }
}
