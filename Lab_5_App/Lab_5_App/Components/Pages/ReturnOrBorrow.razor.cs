using Lab_5_App.Models;
using Lab_5_App.Services;
using Microsoft.AspNetCore.Components;

namespace Lab_5_App.Components.Pages
{
    public partial class ReturnOrBorrow
    {
        [Inject]
        ILibraryService? LibServ { get; set; }

        private string erro = string.Empty;
        private string borrow_u_id = string.Empty;
        private string borrow_b_id = string.Empty;
        private string r_u_id = string.Empty;
        private string r_b_id = string.Empty;
        private string v_id = string.Empty;
        private bool viewBorrowed = false;
        private User borrowUser;
        private List<Book> bookList = new List<Book>();

        private void HandleBorrowBook()
        {
            if (borrow_b_id == string.Empty || borrow_u_id == string.Empty)
            {
                erro = "PLEASE ENSURE ALL FIELDS FOR BORROWING A BOOK ARE FILLED OUT...";
            }
            else if (!int.TryParse(borrow_u_id, out int u_id) || !int.TryParse(borrow_b_id, out int b_id))
            {
                erro = "PLEASE ENSURE PROPERLY FORMATTED NUBMERS ARE ENTERED FOR THE ID'S FOR BORROWING...";
            }
            else
            {
                if (LibServ.BorrowBook(u_id, b_id) == 1)
                {
                    erro = "SUCCESS! BOOK BORROWED...";
                    borrow_b_id = string.Empty;
                    borrow_u_id = string.Empty;
                }
                else
                {
                    erro = "ERROR BORROWING BOOK...";
                }
            }
        }

        private void HandleReturnBook()
        {
            if (r_b_id == string.Empty || r_u_id == string.Empty)
            {
                erro = "PLEASE ENSURE ALL FIELDS FOR RETURNING A BOOK ARE FILLED OUT...";
            }
            else if (!int.TryParse(r_u_id, out int u_id) || !int.TryParse(r_b_id, out int b_id))
            {
                erro = "PLEASE ENSURE PROPERLY FORMATTED NUBMERS ARE ENTERED FOR THE ID'S FOR RETURNING...";
            }
            else
            {
                if (LibServ.ReturnBook(u_id, b_id) == 1)
                {
                    erro = "SUCCESS! BOOK SUCCESSFULLY RETURNED...";
                    r_u_id = string.Empty;
                    r_b_id = string.Empty;
                }
                else
                {
                    erro = "ERROR RETURNING BOOK...";
                }
            }
        }

        private void HandleViewBorrowed()
        {
            if (v_id == string.Empty)
            {
                erro = "PLEASE ENTER USER ID TO VIEW BORROWED BOOKS...";
            }
            else if (!int.TryParse(v_id, out int u_id))
            {
                erro = "PLEASE ENSURE PROPERLY FORMATTED NUBMERS ARE ENTERED FOR THE ID'S FOR RETURNING...";
            }
            else
            {
                if (LibServ.ViewBorrowedBooks(u_id) == 1)
                {
                    borrowUser = LibServ.users.FirstOrDefault(u => u.Id == u_id);
                    if (!LibServ.borrowedBooks.ContainsKey(borrowUser))
                    {
                        erro = "USER HAS NOT CHECKED OUT ANY BOOKS...";
                        v_id = string.Empty;
                        viewBorrowed = false;
                        return;
                    }
                    bookList = LibServ.borrowedBooks[borrowUser];
                    viewBorrowed = true;
                }
                else
                {
                    erro = "ERROR VIEWING BORROWED BOOKS; USER DOES NOT EXIST...";
                }
            }
        }
    }
}
