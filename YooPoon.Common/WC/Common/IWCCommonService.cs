﻿using YooPoon.Core.Autofac;

namespace YooPoon.Common.WC.Common
{
    public interface IWCCommonService:ISingletonDependency
    {
        string AccessToken { get; }
        string JsAPITicket { get; }
        string AppId { get; }

        string AppSecret { get; }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        void RefreshToken();
    }
}