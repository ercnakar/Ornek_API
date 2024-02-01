using karavancidan.Model.Results;
using karavancidan.Model.ViewModel.Advert;
using karavancidan.Model.ViewModel.Helper;
using karavancidan.Services.Helper;
using System.Linq.Expressions;

namespace karavancidan.Services.Abstract.Advert
{
    public interface IAdvertServices : IServiceBase
    {
        Task<IDataResult<List<AdvertViewModel>>> GetAdvertList(Expression<Func<Core.Entity.Advert, bool>> filter = null);
        Task<IDataResult<AdvertViewModel>> GetAdvertByID(Guid ID);
        Task<IDataResult<Guid>> InsertAdvert(AdvertViewModel model);
        Task<IDataResult<Guid>> UpdateAdvert(Core.Entity.Advert model);
        void DeleteAdvert(Guid ID);
    }
}
