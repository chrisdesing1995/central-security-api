
namespace CentralSecurity.Infrastructure.Constant
{
    public class Const
    {
        public static class Accions
        {
            public const string INSERT = "INSERT";
            public const string UPDATE = "UPDATE";
            public const string DELETED = "DELETED";
        }

        public static class Entity
        {
            public const string Users = "User";
            public const string Roles = "Role";
            public const string Menus = "Menu";
            public const string AuditLogs = "AuditLog";
        }

        public static class CodeParameter
        {
            public const string TypeDocument = "TIPODOCUMENTO";
        }

        public static class StoreProcedure
        {
            public const string SP_INSERT_AUDITLOG = "Sp_Insert_AuditLog";
            public const string SP_GET_ALL_USER_LOGIN = "Sp_Get_Users_Login";

            public const string SP_GET_ALL_MENU = "Sp_Get_Menus";
            public const string SP_GET_ALL_MENU_USER = "Sp_Get_Menus_User";
            public const string SP_INSERT_UPDATE_MENU = "Sp_Insert_Update_Menu";

            public const string SP_GET_ALL_USERS = "Sp_Get_Users";
            public const string SP_GET_ALL_USER_ID = "Sp_Get_Users_By_Id";
            public const string SP_INSERT_UPDATE_USER = "Sp_Insert_Update_User";

            public const string SP_GET_ALL_ROLES = "Sp_Get_Roles";
            public const string SP_GET_ALL_ROL_ID = "Sp_Get_Roles_By_Id";
            public const string SP_INSERT_UPDATE_ROL = "Sp_Insert_Update_Role";

        }
    }
}
