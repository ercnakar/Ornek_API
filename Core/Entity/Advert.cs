using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karavancidan.Core.Entity
{
    /// Featured = One çıkan
    public class Advert : EntityBase
    {
        [MaxLength(15)]
        public string? AdvertNo { get; set; }
        public Nullable<DateTime> AdvertApprovedDate { get; set; }
        public Nullable<Guid> ApprovedStatusID { get; set; } //Onaylandı, Bekliyor
        public Nullable<Guid> UserID { get; set; }

    }
}
