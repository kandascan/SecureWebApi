using LoggerService;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic
{
    public abstract class BaseService
    {
        protected readonly ILoggerManager _logger;
        protected readonly IUnitOfWork _uow;

        public BaseService(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _uow = unitOfWork;
        }

        protected void RunCode(string methodName, BaseRequest request, BaseResponse response, Action<IUnitOfWork> action)
        {
            try
            {
                _logger.LogInfo($"Call method: {methodName}");
                action(_uow);
                _logger.LogInfo($"Finished method: {methodName}");
                if (response.Errors.Count != 0)
                {
                    foreach (var err in response.Errors)
                    {
                        _logger.LogError($"Response Errors in method: {methodName}, \nError message: Key: {err.Key} \t Value: {err.Value}");
                    }
                }
                else
                    response.Success = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add("System Exception", ex.Message);
                _logger.LogError($"Exception method: {methodName}, \nException message: {ex.Message + ex.StackTrace}");
            }
        }

        protected async Task RunCodeAsync(string methodName, BaseRequest request, BaseResponse response, Func<UnitOfWork, Task> action)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    _logger.LogInfo($"Call method: {methodName} at {DateTime.Now}");
                    await action(uow);
                    _logger.LogInfo($"Finished method: {methodName} at {DateTime.Now}");
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Errors.Add("System Exception", ex.Message);
                    _logger.LogError($"Exception method: {methodName} at {DateTime.Now}, \nException message: {ex.Message + ex.StackTrace}");
                }
            }
        }
    }
}