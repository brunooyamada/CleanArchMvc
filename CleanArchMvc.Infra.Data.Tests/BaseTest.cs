using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.Data.Tests
{
    public class BaseTest
    {
        protected readonly ApplicationDbContext _context;

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            ServiceProvider service = serviceCollection.BuildServiceProvider();
            _context = service.GetService<ApplicationDbContext>();
        }
    }
}
