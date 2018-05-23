using KolaNaukowe.web.Data;
using KolaNaukowe.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KolaNaukowe.web.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private KolaNaukoweDbContext _context;

        public GenericRepository(KolaNaukoweDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;      
        }

        public TEntity Add(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            _context.SaveChanges();
            return item;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(int id)
        {
            var item = Get(id);
            if(item != null)
            {
                _context.Set<TEntity>().Remove(item);
                _context.SaveChanges();
            }
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

     


    }
}
