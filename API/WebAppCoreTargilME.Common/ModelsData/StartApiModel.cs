using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreTargilME.Common.BaseModel;

namespace WebAppCoreTargilME.Common.ModelsData
{
    public class StartApiModel
    {
        public long? AllPossibleCombinationsCount { get; set; } 
        public List<int>? StartCombination { get; set; }

    }
}
