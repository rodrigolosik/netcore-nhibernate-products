using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netcore_nhibernate.Models;
using NHibernate;

namespace netcore_nhibernate.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private ISession _session;
        public ProductRepository(ISession session) => _session = session;

        public async Task Add(Product item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public IEnumerable<Product> FindAll() => _session.Query<Product>().ToList();

        public async Task<Product> FindById(int id) => await _session.GetAsync<Product>(id);

        public async Task Remove(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Product>(id);
                await _session.DeleteAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Product item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
                throw new HibernateException(ex);
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}