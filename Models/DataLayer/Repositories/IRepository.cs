using System.Collections.Generic;

namespace Itacometragem.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);
        IEnumerable<T> List();
        T Get(int id);
        T Get(QueryOptions<T> options);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        public int Count { get; }

    }
}
