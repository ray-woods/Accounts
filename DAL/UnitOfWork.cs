using Accounts.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Accounts.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        AccountsEntities _ctx;
        Dictionary<string, IRepositoryBase> _repos;

        public UnitOfWork()
        {
            _ctx = new AccountsEntities();
            //cleared for lazy loading _ctx.Configuration.LazyLoadingEnabled = false;
            // _ctx.Configuration.ProxyCreationEnabled = false;
            _repos = new Dictionary<string, IRepositoryBase>();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            IRepositoryBase rst = null;
            if (!_repos.TryGetValue(typeof(T).Name, out rst))
            {
                if (typeof(IDeleteDate).IsAssignableFrom(typeof(T)))
                {
                    Type gen = typeof(Accounts.DAL.Repositories.DDRepository<>);
                    Type spec = gen.MakeGenericType(typeof(T));
                    ConstructorInfo ci = spec.GetConstructor(new Type[] { typeof(AccountsEntities) });
                    rst = ci.Invoke(new object[] { _ctx }) as IRepository<T>;
                }
                else
                    rst = new Accounts.DAL.Repositories.Repository<T>(_ctx);
                _repos.Add(typeof(T).Name, rst);
            }

            return rst as IRepository<T>;
        }

        //public void SetWorkflowStatusClaimSubmitted(int modelID)
        //{
        //    var rst = _ctx.SetWorkflowStatusClaimSubmitted(modelID);
        //}
    }
}
