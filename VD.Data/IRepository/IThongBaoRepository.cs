using System.Linq.Expressions;
using VD.Data.Base;
using VD.Data.Entity;
using VD.Data.Entity.BQC;
using VD.Data.Paging;
using VD.Data.Temp;
using VD.Lib.DTO;

namespace VD.Data.IRepository
{
    public interface IThongBaoRepository:IRepository<ThongBao>
    {
        PG<ThongBao> GetQueryPaging(smartpaging paging = null, Expression<System.Func<ThongBao, bool>> where = null);

      
    }
}
