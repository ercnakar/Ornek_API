using karavancidan.Core.Entity;
using karavancidan.Core.Enums;
using karavancidan.Data.Abstract.Advert;
using karavancidan.Model.Middleware.Exceptions;
using karavancidan.Model.Results;
using karavancidan.Model.ViewModel.Advert;
using karavancidan.Model.ViewModel.Helper;
using karavancidan.Services.Abstract.Advert;
using karavancidan.Services.Default;
using karavancidan.Services.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace karavancidan.Services.Concrete.Advert
{
    public class AdvertServices : ServiceBase, IAdvertServices
    {
        private readonly IAdvertRepository _IAdvertRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdvertServices(IAdvertRepository IAdvertRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _IAdvertRepository = IAdvertRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IDataResult<List<AdvertViewModel>>> GetAdvertList(Expression<Func<Core.Entity.Advert, bool>> filter = null)
        {
            var _Advert = _IAdvertRepository.GetAdvertQuery();

            if (_Advert == null)
            {
                throw new CustomException(nameof(_Advert));
            }

            var count = _Advert.Count();

            var query = (from a in _Advert
                         select new AdvertViewModel
                         {
                             UserID = a.UserID,
                             ApprovedStatusID = a.ApprovedStatusID,
                             AdvertNo = a.AdvertNo,
                         });

            var dataResult = await query.ToListAsync();

            return new SuccessDataListResult<List<AdvertViewModel>>(dataResult, count, DefaultServices.TransactionSuccessful);
        }
        public async Task<IDataResult<AdvertViewModel>> GetAdvertByID(Guid ID)
        {
            Expression<Func<Core.Entity.Advert, bool>> filter = a => a.ID == ID;

            var advertList = await GetAdvertList(filter);

            return new SuccessDataResult<AdvertViewModel>(advertList.Data.First(), DefaultServices.TransactionSuccessful);
        }

        public async Task<IDataResult<Guid>> InsertAdvert(AdvertViewModel model)
        {

            var advertNo = generateAdvertNo();

            var _Advert = new Core.Entity.Advert()
            {
                AdvertNo = advertNo,
                ApprovedStatusID = Guid.Parse(AdvertApprovedStatusEnum.Waiting),
                UserID = model.UserID,
            };

            _IAdvertRepository.InsertAdvert(_Advert);

            return new SuccessDataResult<Guid>(_Advert.ID, DefaultServices.TransactionSuccessful);
        }

        private string generateAdvertNo()
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);

                int randomNum = BitConverter.ToInt32(randomNumber, 0);
                randomNum = Math.Abs(randomNum); // Negatif sayıları pozitife çevir

                return randomNum.ToString().Substring(0, 10);
            }
        }


        public async Task<IDataResult<Guid>> UpdateAdvert(Core.Entity.Advert model)
        {
            var _Advert = await _IAdvertRepository.GetAdvertQuery(x => x.ID == model.ID).FirstOrDefaultAsync();

            if (_Advert == null)
            {
                throw new CustomException(nameof(_Advert));
            }

            _Advert.AdvertApprovedDate = model.AdvertApprovedDate;
            _Advert.AdvertNo = model.AdvertNo;
            _Advert.ApprovedStatusID = model.ApprovedStatusID;
            _Advert.UserID = model.UserID;

            _IAdvertRepository.UpdateAdvert(_Advert);

            return new SuccessDataResult<Guid>(_Advert.ID, DefaultServices.TransactionSuccessful);
        }
        public void DeleteAdvert(Guid ID)
        {
            var _Advert = _IAdvertRepository.GetAdvertQuery(w => w.ID == ID).FirstOrDefault();

            if (_Advert == null)
            {
                throw new CustomException(nameof(_Advert));
            }

            _IAdvertRepository.DeleteAdvert(ID);

        }
    }
}
