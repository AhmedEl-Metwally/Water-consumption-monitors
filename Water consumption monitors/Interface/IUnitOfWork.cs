using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IInvoice Invoice { get; set; }
        ISlidedistribution Slidedistribution { get; set; }
        ISubscriber Subscriber { get; set; }
        ISubscription Subscription { get; set; }
        ITypesOfRealEstate TypesOfRealEstate { get; set; }

        int Compelete();
    }
}
