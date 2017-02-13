using DF_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DF_DAL
{
  public  class PayAction
    {
       private Random rd= new Random();
       private PayModel pm ;
       private  string[] nomast = new string[] { "discountable_amount", "discount_coupon", "body",
            "detail","attach","openid"}; 
        public PayModel PaySet(string fdbs,string lshh,string auth_code, double je,string pay_type) {
            pm = new PayModel();
            try
            {
                pm.Fdbs = fdbs;
                pm.Djbh = lshh;
                pm.Auth_code = auth_code;
                pm.Total_amount = (int)(je * 100); //保留到分 传进来是元
                pm.Pay_type = pay_type;
                pm.Discountable_amount = 0; //保留到分 传进来是元
                pm.Discount_coupon = "";
                pm.Body = "药品消费";
                pm.Attach = "消费单号：" + lshh;
                pm.Nonce_str = this.getNonce_str();
                pm.Out_trade_no = lshh+DateTime.Now.ToString("hhmmss");
                pm.Version = ConfigurationManager.AppSettings["haifu_version"].ToString();
                this.getappfd(pm);
            }
            catch(Exception e)
            {
                Trace.WriteLine("设置交易信息失败："+e.Message);
            }
            //hf1197842184J81A  4aTawCcmFlmmUHvnpch4yNIdmGIO567A
            return pm;
        }
        public string PaySubmit(PayModel pm) {
            string subreturn = this.PostWebRequest(pm.Suburl, getpara(pm), Encoding.UTF8);
            Trace.WriteLine("交易信息返回结果：" + subreturn);
            return subreturn;
        }
        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            Trace.WriteLine("设置交易：" + postUrl + "参数：" + paramData);
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("提交交易信息失败：" + ex.Message);
            }
            return ret;
        }
        private string getpara(PayModel pm) {
            string ssign="";
            //判断非必填参数值为空或为0则不传递

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //利用反射把属性和属性值写入集合
            System.Reflection.PropertyInfo[] properties = pm.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length > 0)
            {
                foreach (System.Reflection.PropertyInfo item in properties)
                {
                    string name = item.Name.ToLower();
                    string value = item.GetValue(pm, null)==null ?"": item.GetValue(pm, null).ToString();
                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        if (nomast.Contains(name) && (string.IsNullOrEmpty(value) || value == "0"))
                        { }
                        else
                            dic.Add(name, value);
                    }
                }
            }
            dic.Remove("fdbs"); //无用参数
            dic.Remove("djbh"); //无用参数
            dic.Remove("appsecret");//秘钥添加在最后 先移除最后加
            dic.Remove("suburl");//秘钥添加在最后 先移除最后加
            dic.Remove("sign");//MD5添加在最后 先移除最后加
            //按key排序
            dic = dic.OrderBy(o =>o.Key).ToDictionary(o => o.Key, p => p.Value);
            string  ssignA = "";
            foreach (KeyValuePair<string, string> pair in dic)
            {
                ssignA += "&"+pair.Key+"="+pair.Value;
            }
          //  Trace.WriteLine("参数排序结果生成:" + ssignA);
            ssignA = ssignA.Substring(1, ssignA.Length-1);
           string  signmd5 = ssignA + "&appsecret=" + pm.Appsecret; //添加秘钥参数生成加密
          //  Trace.WriteLine("加密前字符串:" + signmd5);
            ssign=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(signmd5, "MD5").ToString();
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(signmd5));
            //foreach (byte b in result)
            //{
            //    ssign += b.ToString("X");
            //}
          //  ssign =System.Text.Encoding.UTF8.GetString(result);
            ssignA= ssignA + "&sign=" + ssign;//添加MD5参数
         //   Trace.WriteLine("最后参数结果:" + ssignA);
            return ssignA;
        }
        private void getappfd(PayModel pm) {
            string sql = "select fdbs,fdname,appid,appsecret,openid,suburl from df_wpay where fdbs='" + pm.Fdbs + "'";
            DataSet ds = Pubmethod.GetDataSet(sql);
            if (ds != null && ds.Tables[0] != null) {
                pm.Appid = ds.Tables[0].Rows[0]["appid"].ToString();
                pm.Appsecret= ds.Tables[0].Rows[0]["appsecret"].ToString();
                pm.Openid = ds.Tables[0].Rows[0]["openid"].ToString();
                pm.Suburl = ds.Tables[0].Rows[0]["suburl"].ToString();
                pm.Detail="购买门店："+ ds.Tables[0].Rows[0]["fdname"].ToString()+",谢谢光临！";
            }
        }
        /// <summary>
        /// 得到32位随机数
        /// </summary>
        /// <returns></returns>
        private  string getNonce_str() {
            int i = 0;
            string Nonce = "";
            while (i< 32) {
                Nonce += rd.Next(10,500).ToString();
                if (Nonce.Length >= 32) {
                    break;
                }
                i++;
               }
            return Nonce.Substring(0,32);
        }
    }
}
