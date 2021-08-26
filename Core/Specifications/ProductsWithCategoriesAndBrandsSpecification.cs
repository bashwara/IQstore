using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithCategoriesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithCategoriesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductCategory);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithCategoriesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductCategory);
            AddInclude(x => x.ProductBrand);
        }
    }
}