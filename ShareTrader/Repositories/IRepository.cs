using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//not using it at the moment. Will have to implement interfaces later on
namespace ShareTrader.Repositories
{
    public interface IRepostiory<T> : IDisposable where T : class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        int SaveChanges();
    }
}