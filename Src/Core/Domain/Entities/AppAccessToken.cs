using System;
using Domain.Common;

namespace Domain.Entities
{
    public class AppAccessToken  : BaseEntity
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Token {get; set;} = string.Empty;
        public DateTime ExpiresOn {get; set;} 
        public DateTime CreatedOn {get; set;}
        public DateTime UpdatedOn {get; set;}
    }
}