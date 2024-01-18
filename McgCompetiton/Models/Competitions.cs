using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace McgCompetiton.Models
{
  ///<summary>
  ///Competitions实体类
  ///<summary>
  [Serializable]
  public class Competitions:Users
  {
        public bool IsRegistratition { get; set; }
     public int CompetitionId{get;set;}
     public string Comdate{get;set;}
     public string ComAddress{get;set;}
     public string CompetitionName{get;set;}
     public string Comdateil{get;set;}
     public string ComStartus{get;set;}
     public int CategoryId{get;set;}
        public string CategoryName { get;set;}
       
  }
}
