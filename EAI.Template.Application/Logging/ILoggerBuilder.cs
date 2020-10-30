using $safeprojectname$.Dtos;

namespace $safeprojectname$.Logging
{
    public interface ILoggerBuilder
    {
        void AddApplicationId(int applicationId);

        void AddRequest(string request);

        void AddResponse(string response);

        LoggingModel GetLogging();

        void SetProperty(string propertyName, object value);
    }
}