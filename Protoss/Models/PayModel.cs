namespace Protoss.Models
{
    public class PayModel
    {
        public string AppId { get;set; }
        public string TimeStamp { get;  set; }
        public string NonceStr { get; set; }
        public string Package { get; set; }
        public string SignType { get; set; }
        public string PaySign { get; set; }

        public decimal TotalFee { get; set; }
    }
}