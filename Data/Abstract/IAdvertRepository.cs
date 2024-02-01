using System.Linq.Expressions;

namespace karavancidan.Data.Abstract.Advert
{
    public interface IAdvertRepository : IRepository<Core.Entity.Advert>
    {
        Guid InsertAdvert(Core.Entity.Advert model);
        Guid UpdateAdvert(Core.Entity.Advert model);
        void DeleteAdvert(Guid ID);
        void FullDeleteAdvert(Guid ID);
        IQueryable<Core.Entity.Advert> GetAdvertQuery(Expression<Func<Core.Entity.Advert, bool>> filter = null);
    }
}
