﻿using Microsoft.EntityFrameworkCore;
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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        
        public void Insert(Seller seller)
        {
            _context.Seller.Add(seller);
            _context.SaveChanges();
            
        }

        public Seller FindById(int id)
        {
            return _context.Seller
                .Include(x => x.Departments)
                .FirstOrDefault(x => x.Id == id)
                ;  
            
        }

        public void Remove(int id)
        {
            var seller = FindById(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            var objExisteNoBanco = _context.Seller.Any(x => x.Id == seller.Id);
            if (!objExisteNoBanco)
            {
               throw new NotFoundException("Id not found");
            }
            try
            {
               _context.Update(seller);
               _context.SaveChanges();
            }catch (DbConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
