using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithCategoriesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithCategoriesAndBrandsSpecification(ProductSpecParams productParams) 
            : base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                (!productParams.CategoryId.HasValue || x.ProductCategoryId == productParams.CategoryId)
            )
        {
            AddInclude(x => x.ProductCategory);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex-1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithCategoriesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductCategory);
            AddInclude(x => x.ProductBrand);
        }
    }
}