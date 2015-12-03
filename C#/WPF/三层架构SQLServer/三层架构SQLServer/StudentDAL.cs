using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 三层架构SQLServer
{
    class StudentDAL
    {
        //和数据库有关的都放在DAL（数据访问层）中
        public static object ToDBValue(object value)
        {
            if (value == null)
            {
                return  DBNull.Value;
            }
            else
            {
                return value;
            }
        }
        public static void Insert(Student student)
        {
            SqlHelper.ExecuteNonQuery(@"insert into T_DBNULL(Name,Age,Height) values(@name,@age,@height)",new SqlParameter("@name",ToDBValue(student.Name)),new SqlParameter("@age",ToDBValue(student.Age)),new  SqlParameter("@height",student.Height));
        }
    }
}
