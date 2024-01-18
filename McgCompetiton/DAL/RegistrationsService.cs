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
  ///Registrations数据访问类
  ///<summary>
  public class RegistrationsService
  {
        public RegistrationsService() {
            SQLHelper.GetConnString(ConfigurationManager.AppSettings["McgConnect"]);
        }
        public bool GetRegistration(dynamic dynamic,dynamic UserId)
        {
            return SQLHelper.GetDataSet($"select*from Registrations where CompetitionId={dynamic} and UserId={UserId}").Tables[0].Rows.Count > 0;
        }
  public int DeleteMethod(Registrations model){
 string sql="delete from Registrations where RegistrationId=@RegistrationId";
 SqlParameter[]param=new SqlParameter[]
{
       new SqlParameter("@RegistrationId",model.RegistrationId)
};
try
{
 return SQLHelper.Update(sql,param);
}
catch(SqlException ex)
{
if(ex.Number == 547)
{
 throw new Exception("您要删除的数据行主键值"+model.RegistrationId+"已经被其他表引用，不能直接删除！");
}
else 
 throw ex;
}
 catch(Exception ex)
{
 throw ex;
}
}

 public List<Registrations>GetAllRegistrations (){
       
 string sql= "SELECT   dbo.Registrations.CompetitionId, dbo.Categorys.CategoryId, dbo.Categorys.CategoryName, dbo.Competitions.Comdate, \r\n                dbo.Competitions.ComAddress, dbo.Competitions.CompetitionName, dbo.Competitions.Comdateil, \r\n                dbo.Competitions.ComStartus, dbo.Users.UserId , dbo.Users.UseName, dbo.Registrations.UserId AS Expr, \r\n                dbo.Registrations.RegistrationId\r\nFROM      dbo.Registrations INNER JOIN\r\n                dbo.Competitions ON dbo.Registrations.CompetitionId = dbo.Competitions.CompetitionId INNER JOIN\r\n                dbo.Users ON dbo.Registrations.UserId = dbo.Users.UserId CROSS JOIN\r\n                dbo.Categorys";
  SqlDataReader reader=SQLHelper.GetReader(sql);
List<Registrations>readerList=new List<Registrations>();
while(reader.Read())
{
  readerList.Add(new Registrations()
{
      Expr = new UsersService().GetUser(reader["Expr"]),
      UseName = reader["UseName"] is DBNull ? "" : reader["UseName"].ToString(),
      CategoryName = reader["CategoryName"] is DBNull ? "" : reader["CategoryName"].ToString(),
      Comdate = reader["Comdate"] is DBNull ? "" : reader["Comdate"].ToString(),
      ComAddress = reader["ComAddress"] is DBNull ? "" : reader["ComAddress"].ToString(),
      CompetitionName = reader["CompetitionName"] is DBNull ? "" : reader["CompetitionName"].ToString(),
      Comdateil = reader["Comdateil"] is DBNull ? "" : reader["Comdateil"].ToString(),
      ComStartus = reader["ComStartus"] is DBNull ? "" : reader["ComStartus"].ToString(),
      
      RegistrationId =reader["RegistrationId"] is DBNull ? 0 : (int)reader["RegistrationId"],
  CompetitionId= reader["CompetitionId"] is DBNull ? 0 : (int)reader["CompetitionId"],
  UserId=reader["UserId"] is DBNull ? 0 : (int)reader["UserId"]
});
}
     reader.Close();
     return readerList;
}

    public int UpdateMethod(Registrations model){
 string sql="update  Registrations set CompetitionId=@CompetitionId,UserId=@UserId where RegistrationId=@RegistrationId";
    SqlParameter[]param=new SqlParameter[]
{
 new SqlParameter("@RegistrationId",model.RegistrationId),
 new SqlParameter("@CompetitionId",model.CompetitionId),
 new SqlParameter("@UserId",model.UserId)
};
return SQLHelper.Update(sql,param);
}

 public int InsertMethod(Registrations model){
 string sql="insert into Registrations(CompetitionId,UserId)values(@CompetitionId,@UserId)";
 SqlParameter[]param=new SqlParameter[]
{
   
   new SqlParameter("@CompetitionId",model.CompetitionId),
 new SqlParameter("@UserId",model.UserId)
};
return SQLHelper.Update(sql,param);
      }

    }
}
