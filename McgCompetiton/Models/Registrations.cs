using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace McgCompetiton.Models
{
  ///<summary>
  ///Registrations实体类
  ///<summary>
  [Serializable]
  public class Registrations:Competitions
  {
     public int RegistrationId{get;set;}
     public string Expr { get;set;}
   
  }
}
