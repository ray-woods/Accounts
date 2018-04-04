using Accounts.Core.Interfaces.Repositories;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Accounts.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AccountsEntities _ctx;
        protected DbSet<T> _set;

        public Repository(AccountsEntities context)
        {
            _ctx = context;
            _set = _ctx.Set<T>();
        }

        public virtual T Find(int id)
        {
            return _set.Find(id);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _set.FirstOrDefault(predicate);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query.FirstOrDefault(predicate);
        }

        public virtual IEnumerable<T> All()
        {
            return _set;
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query;
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);

            return query.Where(predicate);
        }

        public virtual IQueryable<T> WhereQ(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);
            return query.AsExpandable().Where(predicate);
        }


        public virtual int AllCount()
        {
            return _set.Count();
        }

        public virtual IEnumerable<T> All(int pageNo, int pageSize, Expression<Func<T, object>> orderBy)
        {
            return _set.OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public virtual IEnumerable<T> AllIncluding(int pageNo, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _set;
            foreach (var prop in includeProperties)
                query = query.Include(prop);

            return query.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> predicate, int pageNo, int pageSize)
        {
            return _set.Where(predicate).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }


        public virtual int InsertUpdate(T entity)
        {
            if (_ctx.Entry(entity).State == EntityState.Detached)
            {
                _set.Add(entity);
            }

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            var entry = ((IObjectContextAdapter)_ctx).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            if (entry != null && entry.EntityKey.EntityKeyValues != null)
                return (int)entry.EntityKey.EntityKeyValues[0].Value;
            else
                return 0;

        }

        public virtual int InsertUpdate(T entity, int key)
        {
            if (_ctx.Entry(entity).State == EntityState.Detached)
            {
                if (key == 0)
                    _set.Add(entity);
                else
                {
                    T original = _set.Find(key);
                    if (original != null)
                    {
                        _ctx.Entry(original).CurrentValues.SetValues(entity);
                        entity = original;
                    }
                    else
                        throw new InvalidOperationException("There was a problem updating the entity, Could not find original object");
                }
            }

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            var entry = ((IObjectContextAdapter)_ctx).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            if (entry != null && entry.EntityKey.EntityKeyValues != null)
                return (int)entry.EntityKey.EntityKeyValues[0].Value;
            else
                return 0;

        }

        public virtual void Delete(int id)
        {
            T entity = _set.Find(id);
            _set.Remove(entity);
        }
    }
}
