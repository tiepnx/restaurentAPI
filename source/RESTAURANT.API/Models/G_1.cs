using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAURANT.API.Models
{
    public class G_1
    {
        public string RESERVATION_INTERFACE_ID { get; set; }
        public string RESERVATION_BATCH_ID { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_EXPLANATION { get; set; }
        public string ROW_STATUS_CODE { get; set; }
        public string ORDER_NUMBER { get; set; }
        public string ORDER_LINE { get; set; }
        public string LOT_NUMBER { get; set; }
    }
}