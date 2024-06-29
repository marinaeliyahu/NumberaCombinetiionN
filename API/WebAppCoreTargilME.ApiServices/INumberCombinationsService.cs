using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCoreTargilME.Common.BaseModel;
using WebAppCoreTargilME.Common.ModelsData;
using WebAppCoreTargilME.Common.QueryData;

namespace WebAppCoreTargilME.ApiServices
{
    public interface INumberCombinationsService
    {
        Task<BaseViewModel<NumberCombinationsModel>> GetAllAPI(QueryPaginationDataModel<NQuery> query);
        Task<BaseViewModel<NumberCombinationsModel>> GetNextApi(QueryDataModel<GetNextApiQuery> query);
        Task<BaseViewModel<StartApiModel>> StartApi(QueryDataModel<NQuery> query);
        Task<BaseViewModel<NumberCombinationsModel>> GetAllNextApiByCurrent(QueryPaginationDataModel<GetNextApiQuery> query);
    }
}
