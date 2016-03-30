using System.Linq;
using Infrastructure.Interface;
using PhotoMaps.Domain;
using PhotoMaps.Domain.IRepository;

namespace DDD2Try.Application
{
    public class ProductApplication
    {
        public static readonly ProductApplication Instance = ApplicationFactory.Create<ProductApplication>();
        private IUnitOfWork _unitOfWork;
        private IProductRepository _productRepository;

        public ProductApplication(IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            this._unitOfWork = unitOfWork;
            this._productRepository = productRepository;
        }
        
    }
}