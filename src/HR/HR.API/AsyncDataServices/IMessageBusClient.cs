namespace HR.API.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewEmployee(PublishEmployeeDTO publishEmployee);
    }
}
