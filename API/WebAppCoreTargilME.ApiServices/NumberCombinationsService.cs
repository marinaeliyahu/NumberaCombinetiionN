using AutoMapper;
using Microsoft.Extensions.Logging;
using WebAppCoreTargilME.BusinessLogic;
using WebAppCoreTargilME.Common.BaseModel;
using WebAppCoreTargilME.Common.ModelsData;
using WebAppCoreTargilME.Common.QueryData;

namespace WebAppCoreTargilME.ApiServices
{
    public class NumberCombinationsService : INumberCombinationsService
    {
        private ILogger<NumberCombinationsService> logger;

        //  private IDigitalArrayDynamicMetaDataContext _context;
        private IMapper mapper;
        private INumberCombinationsBL numberCombinationsBL;

        public NumberCombinationsService(ILogger<NumberCombinationsService> Logger, IMapper Mapper, INumberCombinationsBL NumberCombinationsBL)
        {
            logger = Logger;
            mapper = Mapper;
            numberCombinationsBL = NumberCombinationsBL;
        }

        public Task<BaseViewModel<NumberCombinationsModel>> GetAllAPI(QueryPaginationDataModel<NQuery> query)
        {
            return numberCombinationsBL.GetAllAPI(query);
        }

        public Task<BaseViewModel<NumberCombinationsModel>> GetAllNextApiByCurrent(QueryPaginationDataModel<GetNextApiQuery> query)
        {
            return numberCombinationsBL.GetAllNextApiByCurrent(query);
        }

        public Task<BaseViewModel<NumberCombinationsModel>> GetNextApi(QueryDataModel<GetNextApiQuery> query)
        {
            return numberCombinationsBL.GetNextApi(query);
        }

        public Task<BaseViewModel<StartApiModel>> StartApi(QueryDataModel<NQuery> query)
        {
            return numberCombinationsBL.StartApi(query);
        }


    }
}