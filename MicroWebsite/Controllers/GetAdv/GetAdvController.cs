using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using MicroWebsite.Models;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace MicroWebsite.Controllers.GetAdv
{
    public class GetAdvController : Controller
    {
        //
        // GET: /GetAdv/

        public ActionResult Index()
        {
            var code = Request.Params["code"];
            var appid = ConfigurationManager.AppSettings["AppID"];
            var secret = ConfigurationManager.AppSettings["AppSecret"];
            WxModel model = new WxModel();

            //var client = new WebClient();
            //client.Encoding = Encoding.UTF8;

            //var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            //var data = client.DownloadString(url);

            //var serializer = new JavaScriptSerializer();
            //var obj = serializer.Deserialize<Dictionary<string, string>>(data);
            //string accessToken;
            //if (!obj.TryGetValue("access_token", out accessToken))
            //    return View();

            //var opentid = obj["openid"];
            //url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", accessToken, opentid);
            //data = client.DownloadString(url);
            //var userInfo = serializer.Deserialize<Dictionary<string, object>>(data);
            var accessToken = OAuthApi.GetAccessToken(appid, secret, code);
            var ticket = JsApiTicketContainer.GetJsApiTicket(appid);
            var accessUserInfo = OAuthApi.GetUserInfo(accessToken.access_token, accessToken.openid);
            var noncestr = JSSDKHelper.GetNoncestr();
            var timeSpan = JSSDKHelper.GetTimestamp();
            var signature = JSSDKHelper.GetSignature(ticket, noncestr, timeSpan, Request.Url.AbsoluteUri);
            model.NickName = accessUserInfo.nickname;
            model.OpenId = accessUserInfo.openid;
            model.TimeStamp = timeSpan;
            model.Signature = signature;
            model.NonceStr = noncestr;
            return View(model);
        }

    }
}
