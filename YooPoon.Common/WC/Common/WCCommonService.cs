namespace YooPoon.Common.WC.Common
{
    public class WCCommonService:IWCCommonService
    {
        public string AccessToken { get; private set; }
        public string JsAPITicket { get; private set; }
        public string AppId { get; private set; }
        public string Secret { get; private set; }
        public void RefreshToken()
        {
            throw new System.NotImplementedException();
        }
    }
}