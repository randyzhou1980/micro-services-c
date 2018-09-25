using System.Threading.Tasks;

namespace EventBus.Base.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
