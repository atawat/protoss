using System.Collections.Generic;
using Protoss.Entity.Model;

namespace Protoss.Models
{
    public class CreateOrderModel
    {
        /// <summary>
        /// 送货地址
        /// </summary>
        public string DeliveryAddress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalPrice{get;set;}
        public string CounponNum { get; set; }

        public EnumOrderType Type { get; set; }

        public EnumPayType PayType { get; set; }

        public decimal LocationX { get; set; }

        public decimal LocationY { get; set; }

        public decimal Discount { get; set; }
        //public int ProductId { get; set; }

        //public decimal Count { get; set; }

        //public string ProductName { get; set; }

        //public string Remark { get; set; }

        public List<CreateOrderDetailModel> Details { get; set; }
    }

    public class CreateOrderDetailModel
    {
        public int ProductId { get; set; }

        public decimal Count { get; set; }

        public string ProductName { get; set; }

        public string Remark { get; set; }
    }
}