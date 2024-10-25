
namespace CentralSecurity.Infrastructure.Conts
{
    public class Conts
    {
        public static class Entity
        {
            public const string Users = "Users";
            public const string Roles = "Roles";
            public const string Menus = "Menus";
            public const string AuditLogs = "AuditLogs";
        }

        public static class CodeParameter
        {
            public const string TypeDocument = "TIPODOCUMENTO";
        }

        public static class StoreProcedure
        {
            public const string SP_GET_ALL_USERS = "Sp_Get_Users";
            public const string SP_GET_ALL_USER_ID = "Sp_Get_Users_Id";
            public const string SP_GET_ALL_USER_LOGIN = "Sp_Get_Users_Login";

            public const string SP_GET_ALL_ROLES = "Sp_Get_Roles";
            public const string SP_GET_ALL_MENU = "Sp_Get_Menus";
            public const string SP_GET_ALL_MENU_USER = "Sp_Get_Menus_User";
        }
    }
}
