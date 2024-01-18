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
  ///Categorys数据访问类
  ///<summary>
  public class CategorysService
  {
        public CategorysService() {
            SQLHelper.GetConnString(ConfigurationManager.AppSettings["McgConnect"]);
        }
  public int DeleteMethod(Categorys model){
 string sql="delete from Categorys where CategoryId=@CategoryId";
 SqlParameter[]param=new SqlParameter[]
{
       new SqlParameter("@CategoryId",model.CategoryId)
};
try
{
 return SQLHelper.Update(sql,param);
}
catch(SqlException ex)
{
if(ex.Number == 547)
{
 throw new Exception("您要删除的数据行主键值"+model.CategoryId+"已经被其他表引用，不能直接删除！");
}
else 
 throw ex;
}
 catch(Exception ex)
{
 throw ex;
}
}

 public List<Categorys>GetAllCategorys (){
 string sql="select CategoryId,CategoryName from Categorys";
  SqlDataReader reader=SQLHelper.GetReader(sql);
List<Categorys>readerList=new List<Categorys>();
while(reader.Read())
{
  readerList.Add(new Categorys()
{
  CategoryId=(int)reader["CategoryId"],
  CategoryName=(System.String)reader["CategoryName"].ToString()
});
}
     reader.Close();
     return readerList;
}

    public int UpdateMethod(Categorys model){
 string sql="update  Categorys set CategoryName=@CategoryName where CategoryId=@CategoryId";
    SqlParameter[]param=new SqlParameter[]
{
 new SqlParameter("@CategoryId",model.CategoryId),
 new SqlParameter("@CategoryName",model.CategoryName)
};
return SQLHelper.Update(sql,param);
}

 public int InsertMethod(Categorys model){
 string sql="insert into Categorys(CategoryName)values(@CategoryName)";
 SqlParameter[]param=new SqlParameter[]
{
  
 new SqlParameter("@CategoryName",model.CategoryName)
};
return SQLHelper.Update(sql,param);
      }

    }
}
