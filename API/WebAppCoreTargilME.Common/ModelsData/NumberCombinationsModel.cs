using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreTargilME.Common.BaseModel;

namespace WebAppCoreTargilME.Common.ModelsData
{
    public class NumberCombinationsModel
    {
        public List<int> Combination { get; set; }
        public long? CombinationsTotal { get; set; } = 0;
        public List<List<int>>? Combinations { get; set; }
        public PaginationModel<List<List<int>>>? CombinationsPage { get; set; }
    }
}
