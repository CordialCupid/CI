using Lab_5_App.Services;
using Microsoft.AspNetCore.Components;

namespace Lab_5_App.Components.Pages
{
    public partial class UserManager
    {
        [Inject]
        ILibraryService? LibServ { get; set; }

        private string u_name { get; set; } = string.Empty;
        private string u_email { get; set; } = string.Empty;
        private string ed_name { get; set; } = string.Empty;
        private string ed_email { get; set; } = string.Empty;
        private string erro = string.Empty;
        private string del_id = string.Empty;
        private string ed_id = string.Empty;

        public void OnInitializedAsync()
        {
            if (LibServ != null)
            {
                LibServ.ReadUsers(LibServ._userFilePath);
            }
        }

        private void HandleAddUser()
        {
            if (u_name == string.Empty || u_email == string.Empty)
            {
                erro = "PLEASE ENSURE EACH FIELD FOR ADDING A USER IS FILLED...";
            }
            else
            {
                if (LibServ != null)
                {
                    LibServ.AddUser(u_name, u_email, LibServ._userFilePath);
                    erro = "SUCCESS! USER ADDED...";
                    u_name = string.Empty;
                    u_email = string.Empty;
                }
            }
        }

        private void HandleUserDelete()
        {
            if (del_id == string.Empty)
            {
                erro = "PLEASE ENSURE USER ID VALUE IS ENTERED BEFORE ATTEMPTING TO DELETE...";
            }
            else if (!int.TryParse(del_id, out int to_delete))
            {
                erro = "PLEASE ENSURE THE ENTERED VALUE IS A VALID NUMBER...";
            }
            else
            {
                if (LibServ != null)
                {
                    erro = LibServ.DeleteUser(to_delete, LibServ._userFilePath) == -1 ? "USER TO DELETE NOT FOUND" : "SUCCESS! USER DELETED...";
                    del_id = string.Empty;
                }
            }
        }

        private void HandleUserEdit()
        {
            if (ed_id == string.Empty || ed_name == string.Empty || ed_email == string.Empty)
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
                    erro = LibServ.EditUser(to_edit, ed_name, ed_email, LibServ._userFilePath) == -1 ? "COULD NOT FIND USER MATCHING ENTERED ID..." : "SUCCESS! USER EDITED...";
                    ed_id = string.Empty;
                    ed_name = string.Empty;
                    ed_email = string.Empty;
                }
            }
        }
    }
}
