﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Itacometragem.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ItacometragemContext _context;
        public DbSet<T> _dbSet;
        private int? _count;
        public int Count => _count ?? _dbSet.Count();

        public Repository(ItacometragemContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.AsNoTracking<T>().ToList();
        }

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasWhere)
            {
                foreach (var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }
                _count = query.Count();
            }
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                {
                    query = query.OrderBy(options.OrderBy);
                }
                else
                {
                    query = query.OrderByDescending(options.OrderBy);
                }
            }
            if (options.HasPaging)
            {
                query = query.PageBy(options.PageNumber, options.PageSize);
            }
            return query;
        }

        public IEnumerable<T> List()
        {
            return _dbSet.AsNoTracking<T>().ToList();
        }

        public virtual T Get(int id) => _dbSet.Find(id);
        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);
            return query.FirstOrDefault();
        }
        public virtual void Insert(T entity) => _dbSet.Add(entity);
        public virtual void Update(T entity) => _dbSet.Update(entity);
        public virtual void Delete(T entity) => _dbSet.Remove(entity);
        public virtual void Save() => _context.SaveChanges();
    }
}
