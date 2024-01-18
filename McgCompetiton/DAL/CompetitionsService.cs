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
  ///Competitions数据访问类
  ///<summary>
  public class CompetitionsService
  {
    public CompetitionsService() {
            SQLHelper.GetConnString(ConfigurationManager.AppSettings["McgConnect"]);
        }    
        public int DeleteMethod(Competitions model){
 string sql="delete from Competitions where CompetitionId=@CompetitionId";
 SqlParameter[]param=new SqlParameter[]
{
       new SqlParameter("@CompetitionId",model.CompetitionId)
};
try
{
 return SQLHelper.Update(sql,param);
}
catch(SqlException ex)
{
if(ex.Number == 547)
{
 throw new Exception("您要删除的数据行主键值"+model.CompetitionId+"已经被其他表引用，不能直接删除！");
}
else 
 throw ex;
}
 catch(Exception ex)
{
 throw ex;
}
}

 public List<Competitions>GetAllCompetitions (int UserId=-1,string stua="",int uid=0){
 string sql= "select CompetitionId,UseName,Comdate,ComAddress,CompetitionName,Comdateil,ComStartus,CategoryName from Competitions inner join Users on Users.UserId=Competitions.UserId  inner join Categorys on Categorys.CategoryId=Competitions.CategoryId";
            if (UserId !=- 1&& string.IsNullOrEmpty(stua)) sql += $" where Competitions.UserId={UserId}";
            if (UserId == -1 && !string.IsNullOrEmpty(stua)) sql += $" where ComStartus='{stua}'";
  SqlDataReader reader=SQLHelper.GetReader(sql);
List<Competitions>readerList=new List<Competitions>();
while(reader.Read())
{
  readerList.Add(new Competitions()
{
  CompetitionId=(int)reader["CompetitionId"],
  Comdate=(System.String)reader["Comdate"].ToString(),
  ComAddress=(System.String)reader["ComAddress"].ToString(),
  CompetitionName=(System.String)reader["CompetitionName"].ToString(),
  Comdateil=(System.String)reader["Comdateil"].ToString(),
  ComStartus=(System.String)reader["ComStartus"].ToString(),
  CategoryName=reader["CategoryName"].ToString(),
   //   UserId =Convert.ToInt32( reader["UserId"]),
   IsRegistratition=new RegistrationsService().GetRegistration(reader["CompetitionId"], uid),
      UseName = reader["UseName"].ToString(),
  });
}
     reader.Close();
     return readerList;
}

    public int UpdateMethod(Competitions model){
 string sql="update  Competitions set Comdate=@Comdate,ComAddress=@ComAddress,CompetitionName=@CompetitionName,Comdateil=@Comdateil,ComStartus=@ComStartus,CategoryId=@CategoryId where CompetitionId=@CompetitionId";
    SqlParameter[]param=new SqlParameter[]
{
 new SqlParameter("@CompetitionId",model.CompetitionId),
 new SqlParameter("@Comdate",model.Comdate),
 new SqlParameter("@ComAddress",model.ComAddress),
 new SqlParameter("@CompetitionName",model.CompetitionName),
 new SqlParameter("@Comdateil",model.Comdateil),
 new SqlParameter("@ComStartus",model.ComStartus),
 new SqlParameter("@CategoryId",model.CategoryId)
};
return SQLHelper.Update(sql,param);
}
        public bool UpdateMethodById(Competitions model)
        {
            string sql = "update  Competitions set ComStartus=@ComStartus where CompetitionId=@CompetitionId";
            SqlParameter[] param = new SqlParameter[]
        {
 new SqlParameter("@CompetitionId",model.CompetitionId),

 new SqlParameter("@ComStartus",model.ComStartus),

        };
            return SQLHelper.Update(sql, param)>0;
        }
        public int InsertMethod(Competitions model){
 string sql= "insert into Competitions(Comdate,ComAddress,CompetitionName,Comdateil,ComStartus,CategoryId,UserId)values(@Comdate,@ComAddress,@CompetitionName,@Comdateil,@ComStartus,@CategoryId,@UserId)";
 SqlParameter[]param=new SqlParameter[]
{
   new SqlParameter("@Comdate",model.Comdate),
   new SqlParameter("@ComAddress",model.ComAddress),
   new SqlParameter("@CompetitionName",model.CompetitionName),
   new SqlParameter("@Comdateil",model.Comdateil),
   new SqlParameter("@ComStartus",model.ComStartus),
 new SqlParameter("@CategoryId",model.CategoryId),
 new SqlParameter("@UserId",model.UserId)
};
return SQLHelper.Update(sql,param);
      }

    }
}
