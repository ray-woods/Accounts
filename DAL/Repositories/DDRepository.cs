using Accounts.Core.Interfaces.Repositories;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Accounts.DAL.Repositories
{
    public class DDRepository<T> : Repository<T> where T : class, IDeleteDate
    {
        public DDRepository(AccountsEntities context)
            : base(context)
        { }

        public override T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(e => e.Deleted == null).AsExpandable().FirstOrDefault(predicate);
        }

        public override T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query.Where(e => e.Deleted == null).AsExpandable().FirstOrDefault(predicate);
        }

        public override IEnumerable<T> All()
        {
            return _set.Where(e => e.Deleted == null);
        }

        public override IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query.AsExpandable().Where(e => e.Deleted == null);
        }

        public override IEnumerable<T> Where(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);

            return query.AsExpandable().Where(e => e.Deleted == null).Where(predicate);
        }

        public override IQueryable<T> WhereQ(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query.AsExpandable().Where(e => e.Deleted == null).Where(predicate);
        }

        public override int AllCount()
        {
            return _set.Count(e => e.Deleted == null);
        }

        public override IEnumerable<T> All(int pageNo, int pageSize, Expression<Func<T, object>> orderBy)
        {
            return _set.Where(e => e.Deleted == null).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public override IEnumerable<T> AllIncluding(int pageNo, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);

            return query.Where(e => e.Deleted == null).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public override IEnumerable<T> Where(Expression<Func<T, bool>> predicate, int pageNo, int pageSize)
        {
            return _set.Where(e => e.Deleted == null).Where(predicate).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public override void Delete(int id)
        {
            T entity = _set.Find(id);
            entity.Deleted = DateTime.Now;
            _ctx.SaveChanges();
        }
    }
}
