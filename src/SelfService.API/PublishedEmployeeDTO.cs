using MassTransit;
using Shared;

namespace SelfService.API
{
    public class PublishedEmployeeDTO:IConsumer<CreatedEvent>
    {
        public async Task Consume(ConsumeContext<CreatedEvent> context)
        {
            List<CreatedEvent> Employees = new List<CreatedEvent>();

            Employees.Add(context.Message);
        }
    }
}
