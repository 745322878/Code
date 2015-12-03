using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerSmallItem.DAL
{
      class CustomerDAL
    {
        //从数据库读取的DBNULL值在界面显示是null
        private  static object   FromDBValue(object value)
        {
            if(value==DBNull.Value )
                return  null ;
            else 
                return value;
        }
        private static   Customer ToCustomer(DataRow row)
        {
            Customer customer = new Customer();
            customer.Id = (long)row["Id"];
            customer.Name = (string)row["Name"];
            customer.Class = (string)row["Class"];
            customer.Number = (string)row["Number"];
            customer.TelPhone = (string)row["TelPhone"];
            customer.School = (string)row["School"];
            customer.Hobbies = (string)FromDBValue(row["Hobbies"]);
            customer.Score = (int)row["Score"];
            
            return customer;
        }
          //得到全部信息
        public   static Customer[] GetAll()
        {
            DataTable dt = SqlHelper.ExecuteDataTable("select * from T_Student");
            Customer[] cs = new Customer[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                Customer customer = new Customer();
                customer = ToCustomer(row);

                cs[i] = customer;
            }
            return cs;
        }
          //根据ID删除
         public  static int  DeleteById(long id)
         {
             return SqlHelper.ExecuteNonQuery("delete from T_Student where Id=@id",new SqlParameter("@id",id));
         }
          //根据ID获取
         public   Customer  GetById(long id)
         {
             DataTable dt= SqlHelper.ExecuteDataTable("select * from T_Student where Id=@id", new SqlParameter("@id", id));
             return ToCustomer(dt.Rows[0]);
         }
          //根据Name获取
         public static Customer[] GetByName(string name)
         {
             DataTable dt = SqlHelper.ExecuteDataTable("select * from T_Student where Name=@name",new  SqlParameter("@name",name));
             Customer[] cs = new Customer[dt.Rows.Count];
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 DataRow row = dt.Rows[i];
                 Customer customer = new Customer();
                 customer = ToCustomer(row);
                 cs[i] = customer;
                 //cs[i] = ToCustomer(dt.Rows[i]);
             }
             return cs;
         }
          //增加信息
         public static int Insert(Customer customer)
         {
             return  SqlHelper.ExecuteNonQuery("insert into T_Student(Name,Class,Number,TelPhone,School,Hobbies,Score) values(@name,@class,@number,@telphone,@school,@hobbies,@score)", new SqlParameter("@name",customer.Name), new SqlParameter("@class",customer.Class), new SqlParameter("@number", customer.Number), new SqlParameter("@telphone", customer.TelPhone), new SqlParameter("@school",customer.School), new SqlParameter("@hobbies", customer.Hobbies), new SqlParameter("@score", customer.Score));
         }
          //编辑信息
         public static int Modify(Customer customer)
         {
             return SqlHelper.ExecuteNonQuery("UPDATE T_Student SET Name=@name,Class=@class,Number=@number,TelPhone=@telphone,School=@school ,Hobbies=@hobbies,Score=@score WHERE Id=@id", new SqlParameter("@name", customer.Name), new SqlParameter("@class", customer.Class), new SqlParameter("@number", customer.Number), new SqlParameter("@telphone", customer.TelPhone), new SqlParameter("@school", customer.School), new SqlParameter("@hobbies", customer.Hobbies), new SqlParameter("@score", customer.Score),new SqlParameter("@id",customer.Id));
         }
    }
}
