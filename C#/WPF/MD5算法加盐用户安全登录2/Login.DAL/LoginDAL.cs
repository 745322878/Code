using Login.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.DAL
{
    public class LoginDAL
    {
        private static  User ToUser(DataRow row)
        {
            User u = new User();
            u.Id = (Guid)row["Id"];
            u.UserName = (string)row["UserName"];
            u.Password = (string)row["Password"];
            u.ErrorTimes = Convert.ToInt32(row["ErrorTimes"]);
            return u;
        }
        //插入用户
        public static int Insert(string username, string password)
        {
            return SqlHelper.ExecuteNonQuery("insert into T_User(Id,UserName,Password,ErrorTimes) values(newid(),@name,@password,0)", new SqlParameter("@name", username), new SqlParameter("@password", password));
        }
        //删除用户
        public static int DeleteAll()
        {
            return SqlHelper.ExecuteNonQuery("delete from T_User");
        }
        //根据Name获得用户信息
        public static User GetByName(string username)
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from T_User where UserName=@name", new SqlParameter("@name", username));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("存在重名用户!!!");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToUser(row);
            }
        }
    }
}
