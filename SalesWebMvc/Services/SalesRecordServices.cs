using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordServices
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordServices(SalesWebMvcContext context) 
        {
            _context = context;
        }  
        
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            var saleRecord =
              _context.SalesRecord
              .Include(sr => sr.Seller)
              .ThenInclude(sr => sr.Departments)
              .AsQueryable();

                 if (initial.HasValue)
                 {
                     saleRecord = saleRecord.Where(x => x.Date >= initial);
                 }

                 if (final.HasValue)
                 {
                     saleRecord = saleRecord.Where(x => x.Date <= final);
                 }
                 
                 return await saleRecord.OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime ? initial, DateTime? final) 
        {
            var saleRecord =
              _context.SalesRecord
              .Include(sr => sr.Seller)
              .ThenInclude(sr => sr.Departments)
              .AsQueryable();

            if (initial.HasValue)
            {
                saleRecord = saleRecord.Where(x => x.Date >= initial);
            }

            if (final.HasValue)
            {
                saleRecord = saleRecord.Where(x => x.Date <= final);
            }

            return  await saleRecord
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Departments)
                .ToListAsync();

        }
 
    }
}
