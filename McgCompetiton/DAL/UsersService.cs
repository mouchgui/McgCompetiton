using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using  McgCompetiton.Models;
using MCGLib;
using System.Configuration;

namespace McgCompetiton.DAL
{
  ///<summary>
  ///Users数据访问类
  ///<summary>
  public class UsersService
  {
        public UsersService() { SQLHelper.GetConnString(ConfigurationManager.AppSettings["McgConnect"]); }
      public dynamic GetUser(dynamic dynamic)
        {
            return SQLHelper.GetDataSet($"select UseName from Users where UserId={dynamic}").Tables[0].Rows[0][0];
        }
  public int DeleteMethod(Users model){
 string sql="delete from Users where UserId=@UserId";
 SqlParameter[]param=new SqlParameter[]
{
       new SqlParameter("@UserId",model.UserId)
};
try
{
 return SQLHelper.Update(sql,param);
}
catch(SqlException ex)
{
if(ex.Number == 547)
{
 throw new Exception("您要删除的数据行主键值"+model.UserId+"已经被其他表引用，不能直接删除！");
}
else 
 throw ex;
}
 catch(Exception ex)
{
 throw ex;
}
}
public Users Login(dynamic UserName,dynamic Pwd)
        {
            Users users = null;
            string sql = $"select*from Users where UseName='{UserName}' and Pwd='{Pwd}'";
            SqlDataReader reader = SQLHelper.GetReader($"select*from Users where UseName='{UserName}' and Pwd='{Pwd}'");
            if (reader.Read())
            {
                users = new Users() { 
                UserId = Convert.ToInt32(reader["UserId"]),
                    UseName = reader["UseName"].ToString(),
                    Pwd = reader["Pwd"].ToString(),
                    Roles = reader["Roles"].ToString()
                };
            }
            return users;
        }
 public List<Users>GetAllUsers (){
 string sql="select UserId,UseName,Pwd,Roles from Users";
  SqlDataReader reader=SQLHelper.GetReader(sql);
List<Users>readerList=new List<Users>();
while(reader.Read())
{
  readerList.Add(new Users()
{
  UserId=(int)reader["UserId"],
  UseName=(System.String)reader["UseName"].ToString(),
  Pwd=(System.String)reader["Pwd"].ToString(),
  Roles=(System.String)reader["Roles"].ToString()
});
}
     reader.Close();
     return readerList;
}

    public int UpdateMethod(Users model){
 string sql="update  Users set UseName=@UseName,Pwd=@Pwd where UserId=@UserId";
    SqlParameter[]param=new SqlParameter[]
{
 new SqlParameter("@UserId",model.UserId),
 new SqlParameter("@UseName",model.UseName),
 new SqlParameter("@Pwd",model.Pwd)
};
return SQLHelper.Update(sql,param);
}

 public int InsertMethod(Users model){
 string sql="insert into Users(UseName,Pwd,Roles)values(@UseName,@Pwd,@Roles)";
 SqlParameter[]param=new SqlParameter[]
{

   new SqlParameter("@UseName",model.UseName),
   new SqlParameter("@Pwd",model.Pwd),
 new SqlParameter("@Roles",model.Roles)
};
return SQLHelper.Update(sql,param);
      }

    }
}
