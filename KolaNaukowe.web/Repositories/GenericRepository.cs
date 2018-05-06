using KolaNaukowe.web.Data;
using KolaNaukowe.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public TEntity Get(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            }

            var item  = dbQuery
                    .AsNoTracking() 
                    .FirstOrDefault(where); 
            
            return item;
        }


        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();           
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            }        
            return dbQuery;
        }

        public void Remove(int id)
        {
            var item = _context.Set<TEntity>().Find(id);
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
