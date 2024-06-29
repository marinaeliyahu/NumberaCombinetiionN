using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreTargilME.Common.BaseModel;

namespace WebAppCoreTargilME.Common.QueryData
{
    public record QueryDataModel<Q>  
    {
        public Q DataQuery { get; set; }
    }
    public record QueryPaginationDataModel<Q> : PaginationModel
    {
        public Q DataQuery { get; set; }
    }
}
