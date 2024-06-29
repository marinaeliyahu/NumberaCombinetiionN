using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebAppCoreTargilME.Common.BaseModel;
using WebAppCoreTargilME.Common.ModelsData;
using WebAppCoreTargilME.Common.QueryData;


namespace WebAppCoreTargilME.BusinessLogic
{
    public class NumberCombinationsBL : INumberCombinationsBL
    {
        private ILogger<NumberCombinationsBL> _logger;

        
        private IMapper _mapper;

        public NumberCombinationsBL(ILogger<NumberCombinationsBL> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<BaseViewModel<NumberCombinationsModel>> GetAllAPI(QueryPaginationDataModel<NQuery> query)
        {
            var vm = new BaseViewModel<NumberCombinationsModel>();
            try
            {
                int N = query.DataQuery.N;
                long pageNumber = query.PageNumber ?? 1;
                int pageSize = query.PageSize ?? 10;

                var totalRecords = CountCombinations(N);
                var totalPages = totalRecords / (long)pageSize;

                var pageCombinations = CombinationsPage(N, pageNumber, pageSize);
                var json = JsonSerializer.Serialize<List<List<int>>>(pageCombinations.ToList());
                vm.Data = new NumberCombinationsModel()
                {
                    CombinationsPage = new PaginationModel<List<List<int>>>()
                    {
                        DataPage = pageCombinations.ToList(),
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalPages = totalPages,
                        TotalRecords = totalRecords
                    },
                    CombinationsTotal = totalRecords
                };
            }
            catch (Exception e)
            {
                vm.Data = null;
                vm.Status = System.Net.HttpStatusCode.BadRequest;
                vm.Messages.Add(new Message { LogLevel = Common.BaseModel.LogLevel.Error, Text = e.Message });
                _logger.LogError(e.Message);
            }
            return vm;
        }
        #region  GetAllAPI Business Logic

        IEnumerable<List<int>> CombinationsPage(int n, long pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
                yield break;

            long totalCombinations = Factorial(n);
            if (totalCombinations < pageSize * (pageNumber - 1))
                yield break;

            var arr = Enumerable.Range(1, n).ToArray();
            var indexes = Enumerable.Range(0, n).ToArray();
             
            long startIndex = pageSize * (pageNumber - 1);

           
            long endIndex = Math.Min(startIndex + pageSize, totalCombinations);
             
            for (long i = 0; i < startIndex; i++)
            {
                NextCombination(indexes);
            }
 
            for (long i = startIndex; i < endIndex; i++)
            {
                var combination = indexes.Select(idx => arr[idx]).ToList();
                yield return combination;
 
                NextCombination(indexes);
            }
        }



        #endregion

        public async Task<BaseViewModel<NumberCombinationsModel>> GetNextApi(QueryDataModel<GetNextApiQuery> query)
        {
            var currentCombination = query.DataQuery.CurrentCombination;

            var vm = new BaseViewModel<NumberCombinationsModel>();
            try
            {
                var nextCombination = GetNextCombination(currentCombination);
                vm.Data = new NumberCombinationsModel()
                { Combination = nextCombination };

            }
            catch (Exception e)
            {
                vm.Data = null;
                vm.Status = System.Net.HttpStatusCode.BadRequest;
                vm.Messages.Add(new Message { LogLevel = Common.BaseModel.LogLevel.Error, Text = e.Message });
                _logger.LogError(e.Message);
            }
            return vm;
        }
        #region GetNextApi Business logic
        List<int> GetNextCombination(List<int> arr)
        {
            var next = NextCombinationByCurrent(arr);
            return next.Any() ? next.ToList() : new List<int>();
        }
        IEnumerable<T> NextCombinationByCurrent<T>(IEnumerable<T> source)
        {
            var array = source.ToArray();
            int i = array.Length - 2;
            while (i >= 0 && Comparer<T>.Default.Compare(array[i], array[i + 1]) >= 0)
                i--;

            if (i < 0)
                return Enumerable.Empty<T>();

            int j = array.Length - 1;
            while (Comparer<T>.Default.Compare(array[j], array[i]) <= 0)
                j--;

            Swap(ref array[i], ref array[j]);
            Array.Reverse(array, i + 1, array.Length - i - 1);
            return array;
        }
        #endregion

        public async Task<BaseViewModel<StartApiModel>> StartApi(QueryDataModel<NQuery> query)
        {
            var vm = new BaseViewModel<StartApiModel>();
            try
            {
                var N = query.DataQuery.N;
                var count = CountCombinations(N);
                var firstCombanation = GenerateFirstCombination(N);

                vm.Data = new StartApiModel { AllPossibleCombinationsCount = count, StartCombination = firstCombanation.ToList() };
            }
            catch (Exception e)
            {
                vm.Data = null;
                vm.Status = System.Net.HttpStatusCode.BadRequest;
                vm.Messages.Add(new Message { LogLevel = Common.BaseModel.LogLevel.Error, Text = e.Message });
                _logger.LogError(e.Message);
            }
            return vm;
        }
        #region StartApi Business logic
        long CountCombinations(int N) => Factorial(N);

        List<int> GenerateFirstCombination(int n)
        {
            var numbers = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }
        #endregion


        public async Task<BaseViewModel<NumberCombinationsModel>> GetAllNextApiByCurrent(QueryPaginationDataModel<GetNextApiQuery> query)
        {
            var vm = new BaseViewModel<NumberCombinationsModel>();
            try
            {
                long pageNumber = query.PageNumber ?? 1;
                int pageSize = query.PageSize ?? 10;
               
                var currentCombination = query.DataQuery.CurrentCombination;
                var count = CountCombinations(currentCombination.Count);
                var nextCombinations = NextCombinations(currentCombination, pageSize);
                vm.Data = new NumberCombinationsModel()
                { Combinations = nextCombinations.ToList(), CombinationsTotal = count };
            }
            catch (Exception e)
            {
                vm.Data = null;
                vm.Status = System.Net.HttpStatusCode.BadRequest;
                vm.Messages.Add(new Message { LogLevel = Common.BaseModel.LogLevel.Error, Text = e.Message });
                _logger.LogError(e.Message);
            }
            return vm;
        }

        #region GetAllNextApiByCurrent
        IEnumerable<List<int>> NextCombinations(IEnumerable<int> combination, int pageSize)
        {
            var indexes = combination.ToArray();

            for (int i = 0; i < pageSize; i++)
            {
                if (!NextCombination(indexes))
                    yield break;

                yield return indexes.ToList();
            }
        }
        #endregion

        #region Reusable Functions 
        long Factorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        bool NextCombination(int[] indexes)
        {
            int i = indexes.Length - 1;
            while (i > 0 && indexes[i - 1] >= indexes[i])
                i--;

            if (i <= 0)
                return false;

            int j = indexes.Length - 1;
            while (indexes[j] <= indexes[i - 1])
                j--;

            Swap<int>(ref indexes[i - 1], ref indexes[j]);

            j = indexes.Length - 1;
            while (i < j)
            {
                Swap<int>(ref indexes[i], ref indexes[j]);
                i++;
                j--;
            }
            return true;
        }
        #endregion

    }
}