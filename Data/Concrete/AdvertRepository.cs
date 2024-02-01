using karavancidan.Data.Abstract.Advert;
using karavancidan.Model.Middleware.Exceptions;
using karavancidan.Model.Results;
using System.Linq.Expressions;

namespace karavancidan.Data.Concrete.Advert
{
    public class AdvertRepository : EntityRepository<Core.Entity.Advert>, IAdvertRepository
    {
        private readonly karavancidanDBContext _context;
        public AdvertRepository(karavancidanDBContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteAdvert(Guid ID)
        {
            var Advert = GetAdvertQuery(w => w.ID == ID).FirstOrDefault();

            if (Advert == null)
            {
                throw new CustomException(nameof(Advert));
            }
            Advert.IsDeleted = true;

            _context.SaveChanges();
        }

        public void FullDeleteAdvert(Guid ID)
        {
            var Advert = GetAdvertQuery(w => w.ID == ID).FirstOrDefault();

            Delete(Advert);

            _context.SaveChanges();
        }

        public IQueryable<Core.Entity.Advert> GetAdvertQuery(Expression<Func<Core.Entity.Advert, bool>> filter = null)
        {
            var data = filter == null ? Query() : Query().Where(filter);

            return data;

        }

        public Guid InsertAdvert(Core.Entity.Advert model)
        {
            Insert(model);

            _context.SaveChanges();

            return model.ID;
        }

        public Guid UpdateAdvert(Core.Entity.Advert model)
        {
            Update(model);

            _context.SaveChanges();

            return model.ID;
        }
    }
}
