/**
 * Created by Yunjoy on 2015/6/20.
 */
app.service('wc', ['$http', '$state','$sessionStorage',function ($http, $state,$sessionStorage) {
        //alert("构建WCservice");
        var _appId;
        var _openId;
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.open("get", SETTING.APIURL + "wcpay/getInitParment?url=" + encodeURIComponent(location.href.split('#')[0]), false);
        //xmlhttp.withCredentials = true;
        xmlhttp.send();
        var data = angular.fromJson(xmlhttp.response);
        if (data) {
            _appId = data.appid;
            wx.config({
                debug: false,
                appId: data.appid,
                timestamp: data.timestamp,
                nonceStr: data.nonceStr,
                signature: data.signature,
                jsApiList: ["onMenuShareTimeline",
                    "onMenuShareAppMessage",
                    "onMenuShareQQ",
                    "onMenuShareWeibo",
                    "startRecord",
                    "stopRecord",
                    "onVoiceRecordEnd",
                    "playVoice",
                    "pauseVoice",
                    "stopVoice",
                    "onVoicePlayEnd",
                    "uploadVoice",
                    "downloadVoice",
                    "chooseImage",
                    "previewImage",
                    "uploadImage",
                    "downloadImage",
                    "translateVoice",
                    "getNetworkType",
                    "openLocation",
                    "getLocation",
                    "hideOptionMenu",
                    "showOptionMenu",
                    "hideMenuItems",
                    "showMenuItems",
                    "hideAllNonBaseMenuItem",
                    "showAllNonBaseMenuItem",
                    "closeWindow",
                    "scanQRCode",
                    "chooseWXPay",
                    "openProductSpecificView",
                    "addCard",
                    "chooseCard",
                    "openCard"] // 蹇呭～锛岄渶瑕佷娇鐢ㄧ殑JS鎺ュ彛鍒楄〃锛屾墍鏈塉S鎺ュ彛鍒楄〃瑙侀檮褰�2
            });
            wx.ready(function(){
                //wx.hideAllNonBaseMenuItem();
                //alert("看看是否hide了");
                wx.showMenuItems({
                    menuList: ['menuItem:refresh','menuItem:nightMode'] // 要显示的菜单项，所有menu项见附录3
                });
                wx.onMenuShareTimeline({
                    title: '乖宝来吃饭了', // 分享标题
                    link: SETTING.APIURL+'wx/', // 分享链接
                    imgUrl: SETTING.APIURL+'/wx/app/common/static/images/dahuofang.png', // 分享图标
                    success: function () {
                        alert("恭喜您分享成功！");
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数
                    }
                });
                wx.onMenuShareAppMessage({
                    title: '乖宝来吃饭了', // 分享标题
                    desc: '大伙房，为您提供爆款美食外卖！', // 分享描述
                    link: SETTING.APIURL+'wx/', // 分享链接
                    imgUrl:  SETTING.APIURL+'/wx/app/common/static/images/dahuofang.png', // 分享图标
                    type: '', // 分享类型,music、video或link，不填默认为link
                    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                    success: function () {
                        alert("恭喜您分享成功!");
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数
                    }
                });
                wx.onMenuShareQQ({
                    title: '乖宝来吃饭了', // 分享标题
                    desc: '大伙房，为您提供爆款美食外卖！', // 分享描述
                    link: SETTING.APIURL+'wx/', // 分享链接
                    imgUrl:  SETTING.APIURL+'/wx/app/common/static/images/dahuofang.png', // 分享图标
                    success: function () {
                        // 用户确认分享后执行的回调函数
                        alert("恭喜您分享成功!");
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数
                    }
                });
                wx.onMenuShareWeibo({
                    title: '乖宝来吃饭了', // 分享标题
                    desc: '大伙房，为您提供爆款美食外卖！', // 分享描述
                    link: SETTING.APIURL+'wx/', // 分享链接
                    imgUrl:  SETTING.APIURL+'/wx/app/common/static/images/dahuofang.png', // 分享图标
                    success: function () {
                        // 用户确认分享后执行的回调函数
                        alert("恭喜您分享成功!");
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数
                    }
                });
            });
        }
        //初始化service
        this.initWC = function () {
            if(!_appId) {
                _appId = this.getQueryStringByName("appId");
                if(!appId){
                    $http.get(SETTING.APIURL + 'wcpay/getAppId').success(function (res) {
                        _appId = res.appId
                    });
                }
            }
            this.getOpenId();
        };
        this.getOpenId = function () {
            if(_openId)
                return;
            var id = this.getQueryStringByName("openId");
            if(!id){
                id = $sessionStorage.openId;
                if(!id) {
                    window.location.href = "index.html";
                    return;
                }
            }
            $sessionStorage.openId = id;
            _openId = id;
        };
        this.sendPay = function (orderNo) {
            if (!_openId || _openId == 'null') {
                alert("无法获取openid");
                return;
            }
            window.location.href ="/wcpay/getPrepayId?showwxpaytitle=1&openId="+_openId+"&orderNo="+orderNo;
        };
        this.sendRechargePay = function(recordId){
            if (!_openId || _openId == 'null') {
                alert("无法获取openid");
                return;
            }
            window.location.href = SETTING.APIURL +"wcpay/getPayPage?showwxpaytitle=1&openId="+ _openId +"&recordId="+recordId;
        };
        this.getQueryStringByName = function (name) {
            var result = location.search == "" ? undefined : location.search.split('?')[1].split('&');
            if (!result)
                return "";
            var arry = [];
            for (var i = 0; i < result.length; i++) {
                var keyValue = result[i].split("=");
                arry[keyValue[0]] = keyValue[1];
            }
            return arry[name];
        };
        //alert("构建WCservice完成");
    }]
)
;