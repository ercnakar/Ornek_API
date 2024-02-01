using karavancidan.Core.Enums;
using karavancidan.Model.Results;
using karavancidan.Model.ViewModel.Helper;
using karavancidan.Model.ViewModel.Users;
using karavancidan.Services.Abstract.Brand;
using karavancidan.Services.Default;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace karavancidan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CaravanTypeController : ControllerBase
    {
        public CaravanTypeController(IBrandServices IBrandServices)
        {

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListObjectViewModel>>> CaravanType()
        {
            List<ListObjectViewModel> models = new List<ListObjectViewModel>();

            var alkovenResponse = new { Alkovenli = "Alkovenli", Id = CaravanTypeEnum.Alkoven };
            var panelvanResponse = new { Panelvan = "Panelvan", Id = CaravanTypeEnum.Panelvan };
            var minibusResponse = new { Minibüs = "Minibüs Karavan", Id = CaravanTypeEnum.Minibus };
            var otobusResponse = new { Otobüs = "Otobüs Karavan", Id = CaravanTypeEnum.Otobus };
            var pickupResponse = new { Pickup = "Pickup", Id = CaravanTypeEnum.Pickup };
            var tirResponse = new { Tır = "Tır Karavan", Id = CaravanTypeEnum.Tir };

            var response = new object[] { alkovenResponse, panelvanResponse, minibusResponse, otobusResponse, pickupResponse, tirResponse };

            foreach (var item in response)
            {
                var model = new ListObjectViewModel();
                var typeProp = item.GetType().GetProperties()[0]; 
                model.ObjectName = typeProp.Name;

                var idProp = item.GetType().GetProperties()[1]; 
                model.ObjectID = Guid.Parse(idProp.GetValue(item).ToString());

                models.Add(model);
            }
            return Ok(new SuccessDataListResult<List<ListObjectViewModel>>(models, models.Count(), DefaultServices.TransactionSuccessful));
        }

    }
}
