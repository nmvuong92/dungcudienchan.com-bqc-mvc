using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using VD.Data.Base;
using VD.Data.Entity;
using VD.Data.Entity.MF;
using VD.Data.IRepository;
using VD.Data.Paging;
using VD.Lib.DTO;
using VD.Lib.Encode;

namespace VD.Data.Repository
{
    public class DonHangRepository : RepositoryBase<DonHang>, IDonHangRepository
    {
        public DonHangRepository(IUnitOfWork UOW) : base(UOW) { }

        public PG<DonHang> GetQueryPaging(donhang_sp paging = null, Expression<Func<DonHang, bool>> where = null)
        {
            IQueryable<DonHang> query;
            //PHÂN QUYỀN ĐỒ
            query = GetList(where);

         
            //SORTING
            query = query.OrderByField(paging.tentruongsort, paging.giatrisort == "asc");
            //FILTERING
            if (paging.bolocs != null)
            {
                foreach (var loc in paging.bolocs)
                {
                    //if (loc.tentruongloc.Equals("Username"))
                    //{
                    //    boloc loc1 = loc;
                    //    // query = query.Where(q => q.Username.Contains(loc1.giatriloc));
                    //}
                }
            }
            return (new PGQuery<DonHang>(query)).GetPG(paging);
        }
    }
}
