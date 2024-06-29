using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreTargilME.Common.BaseModel;

namespace WebAppCoreTargilME.Common.QueryData
{
    public record GetNextApiQuery
    {
        [Required]
        public List<int> CurrentCombination { get; set; }

    }
}
