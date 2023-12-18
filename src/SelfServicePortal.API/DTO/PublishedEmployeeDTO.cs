using MassTransit;
using Shared;

namespace SelfServicePortal.API.DTO
{
    public sealed class PublishedEmployeeDTO : IConsumer<CreatedEvent>
    {
       public async Task Consume(ConsumeContext<CreatedEvent> context)
        {
            List<CreatedEvent> Employees = new List<CreatedEvent>();

            Employees.Add(context.Message);
        }
    }
}
