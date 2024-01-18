using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace McgCompetiton.Models
{
  ///<summary>
  ///Users实体类
  ///<summary>
  [Serializable]
  public class Users
  {
     public int UserId{get;set;}
     public string UseName{get;set;}
     public string Pwd{get;set;}
     public string Roles{get;set;}
  }
}
