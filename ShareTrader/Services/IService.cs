using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShareTrader.Services
{
    public interface IService<T>
    {
        ICollection<T> GetAll();
        T GetById(int id);
        void Add(T entity);//use bull
        void Update(T entity);//use bool
    }
}