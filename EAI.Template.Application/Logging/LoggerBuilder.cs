using $safeprojectname$.Dtos;
using System;
using System.Diagnostics;
using System.Reflection;

namespace $safeprojectname$.Logging
{
    public class LoggerBuilder : ILoggerBuilder
    {
        private LoggingModel loggingModel;
        private Stopwatch Stopwatch { get; }

        public LoggerBuilder()
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            loggingModel = new LoggingModel
            {
                ReferenceNumber = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now
            };
        }

        public void AddApplicationId(int applicationId)
        {
            loggingModel.ApplicationId = applicationId;
        }

        public void AddRequest(string request)
        {
            loggingModel.Request = request;
        }

        public void AddResponse(string response)
        {
            loggingModel.Response = response;
        }

        public void SetProperty(string propertyName, object value)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || value == null) return;

            PropertyInfo propInfo = loggingModel.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (null != propInfo && propInfo.CanWrite)
            {
                propInfo.SetValue(loggingModel, value, null);
            }
        }

        public LoggingModel GetLogging()
        {
            Stopwatch.Stop();
            loggingModel.ResponseTime = Stopwatch.ElapsedMilliseconds / 1000f;
            return loggingModel;
        }
    }
}