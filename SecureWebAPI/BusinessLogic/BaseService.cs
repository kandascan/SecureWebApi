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

        public BaseService(ILoggerManager logger)
        {
            this._logger = logger;
        }

        protected void RunCode(string methodName, BaseRequest request, BaseResponse response, Action<UnitOfWork> action)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    _logger.LogInfo($"Call method: {methodName} at {DateTime.Now}");
                    action(uow);
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