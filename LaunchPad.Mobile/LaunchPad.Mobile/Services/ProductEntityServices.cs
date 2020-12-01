using LaunchPad.Repository;
using LaunchPad.Repository.LocalDbModels;
namespace LaunchPad.Mobile.Services
{
    public class ProductEntityServices : EntityService<Product>
    {
        public ProductEntityServices() : base(App.DbConnection)
        {
        }
    }
}
