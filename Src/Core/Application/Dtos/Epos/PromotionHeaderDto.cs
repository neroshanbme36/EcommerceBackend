using System.Collections.Generic;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class PromotionHeaderDto
    {
       public int Id {get; set;}
       public string Description {get; set;} = string.Empty;
       public IReadOnlyList<PromotionLineDto> PromotionLines {get; set;} = new List<PromotionLineDto>();
    }
}