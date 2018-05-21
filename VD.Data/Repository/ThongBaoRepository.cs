using System;
using System.Linq;
using System.Linq.Expressions;
using VD.Data.Base;
using VD.Data.Entity.BQC;
using VD.Data.Repository;
using VD.Data.Base;
using VD.Data.Entity;
using VD.Data.Paging;

using VD.Data.Temp;
using VD.Lib;
using VD.Lib.DTO;
using VD.Lib.Encode;

using VD.Data.IRepository;
namespace VD.Data.Repository
{
    public class ThongBaoRepository : RepositoryBase<ThongBao>, IThongBaoRepository
    {

        public ThongBaoRepository(IUnitOfWork uOW)
            : base(uOW)
        {

        }


        
        public PG<ThongBao> GetQueryPaging(smartpaging paging = null, Expression<Func<ThongBao, bool>> where = null)
        {
            IQueryable<ThongBao> query;
            //PHÂN QUYỀN ĐỒ
            query = GetList(where);
           
            //SORTING
            query = query.OrderByField(paging.tentruongsort, paging.giatrisort == "asc");

            //FILTERING
            if (paging.bolocs != null)
            {
                foreach (var loc in paging.bolocs)
                {
                    if (loc.tentruongloc.Equals("full_text_search"))
                    {
                        boloc loc1 = loc;
                        query = query.FullTextSearch(loc1.giatriloc, true);
                    }
                    else
                    {/*
                        if (loc.tentruongloc.Equals("ThongBaoname"))
                        {
                            boloc loc1 = loc;
                            query = query.Where(q => q.ThongBaoname.Contains(loc1.giatriloc));
                        }*/

                    }
                }
            }
            return (new PGQuery<ThongBao>(query)).GetPG(paging);
        }


      
    }
}
