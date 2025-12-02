using Lab_5_App.Models;
using Lab_5_App.Services;
using Microsoft.AspNetCore.Components;
using static System.Reflection.Metadata.BlobBuilder;

namespace Lab_5_App.Components.Pages
{
    public partial class BookManager
    {
        [Inject]
        ILibraryService? LibServ { get; set; }
        private string b_title { get; set; } = string.Empty;
        private string b_auth { get; set; } = string.Empty;
        private string b_isbn { get; set; } = string.Empty;
        private string ed_title { get; set; } = string.Empty;
        private string ed_auth { get; set; } = string.Empty;
        private string ed_isbn { get; set; } = string.Empty;
        private string erro = string.Empty;
        private string succ = string.Empty;
        private string del_id = string.Empty;
        private string ed_id = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            if (LibServ != null)
            {
                LibServ.ReadBooks(LibServ._bookFilePath);                       
            }
        }
        private Dictionary<Book, int> bookGroups => LibServ.books.GroupBy(b => b.Id).ToDictionary(b => b.First(), b => b.Count());

        private void HandleAddBook()
        {
            if (b_title == string.Empty || b_auth == string.Empty || b_isbn == string.Empty)
            {
                erro = "PLEASE ENSURE EACH FIELD FOR ADDING A BOOK IS FILLED...";
            }
            else
            {
                if (LibServ != null)
                {
                    LibServ.AddBook(b_title, b_auth, b_isbn, LibServ._bookFilePath);
                    erro = "SUCCESS! BOOK ADDED...";
                    b_title = string.Empty;
                    b_auth = string.Empty;
                    b_isbn = string.Empty;
                    StateHasChanged();
                }
            }
        }

        private void HandleBookDelete()
        {
            if (del_id == string.Empty)
            {
                erro = "PLEASE ENSURE BOOK ID VALUE IS ENTERED BEFORE ATTEMPTING TO DELETE...";
            }
            else if (!int.TryParse(del_id, out int to_delete))
            {
                erro = "PLEASE ENSURE THE ENTERED VALUE IS A VALID NUMBER...";
            }
            else
            {
                if (LibServ != null)
                {
                    erro = LibServ.DeleteBook(to_delete, LibServ._bookFilePath) == -1 ? "BOOK TO DELETE NOT FOUND" : "SUCCESS! BOOK DELETED...";
                    del_id = string.Empty;
                }
            }
        }

        private void HandleBookEdit()
        {
            if (ed_id == string.Empty || ed_auth == string.Empty || ed_isbn == string.Empty || ed_title == string.Empty)
            {
                erro = "PLEASE ENSURE ALL FIELDS IN THE EDIT TEXT BOXES ARE FILLED...";
            }
            else if (!int.TryParse(ed_id, out int to_edit))
            {
                erro = "PLEASE ENSURE ENTERED ID IS A NUMBER...";
            }
            else
            {
                if (LibServ != null)
                {
                    erro = LibServ.EditBook(to_edit, ed_title, ed_auth, ed_isbn, LibServ._bookFilePath) == -1 ? "COULD NOT FIND BOOK MATCHING ENTERED ID..." : "SUCCESS! BOOK EDITED...";
                    ed_id = string.Empty;
                    ed_auth = string.Empty;
                    ed_isbn = string.Empty;
                    ed_title = string.Empty;
                }
            }
        }
    }
}
