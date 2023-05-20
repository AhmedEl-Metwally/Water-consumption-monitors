using Water_consumption_monitors.Date;
using Water_consumption_monitors.Interface;

namespace Water_consumption_monitors.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IInvoice Invoice { get; set; }
        public ISlidedistribution Slidedistribution { get; set; }
        public ISubscriber Subscriber { get; set; }
        public ISubscription Subscription { get; set; }
        public ITypesOfRealEstate TypesOfRealEstate { get; set; }

        public UnitOfWork(ApplicationDbContext context )
        {
            _context = context; 
            Invoice = new InvoiceServicescs(_context);
            Slidedistribution = new SlidedistributionServicescs(_context);
            Subscriber = new SubscriberServicescs(_context);
            Subscription = new SubscriptionServicescs(_context);
            TypesOfRealEstate = new TypesOfRealEstateServicescs(_context);

        }

        public int Compelete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
