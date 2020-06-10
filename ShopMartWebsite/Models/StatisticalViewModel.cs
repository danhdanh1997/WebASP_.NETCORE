using ShopMartWebsite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Models
{
    public class StatisticalViewModel
    {
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string option { get; set; }
        public int monthSearch { get; set; }
        public int yearSearch { get; set; }
        public List<Statistical> statisticals { get; set; }
    }
}
