namespace Protoss.Models
{
    public class JsConfigModel
    {
        public bool debug { get; set; }

        public string appId { get; set; }

        public string nonceStr { get; set; }

        public string signature { get; set; }

        public string[] jsApiList { get; set; }

        public string timestamp { get; set; }
    }
}