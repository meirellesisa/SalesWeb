using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;


namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) 
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        
        public async Task InsertAsync(Seller seller)
        {
            _context.Seller.Add(seller);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller
                .Include(x => x.Departments)
                .FirstOrDefaultAsync(x => x.Id == id)
                ;  
            
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await FindByIdAsync(id);
            _context.Seller.Remove(seller);
            _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            var objExisteNoBanco = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!objExisteNoBanco)
            {
               throw new NotFoundException("Id not found");
            }
            try
            {
               _context.Update(seller);
               await _context.SaveChangesAsync();
            }catch (DbConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
