using System;
using System.Linq.Expressions;
using VD.Data.Base;
using VD.Data.Entity;
using VD.Data.Entity.MF;
using VD.Data.Paging;
using VD.Lib.DTO;

namespace VD.Data.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        PG<Product> GetQueryPaging(product_sp paging = null, Expression<Func<Product, bool>> where = null);
    }
}
