using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using DF_DAL;
using System.Diagnostics;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace DF_DAL
{
    public class JsonTools
    {

        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            string jsonString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            // byte[] dataBytes = new byte[stream.Length];
            // stream.Position = 0;
            //  stream.Read(dataBytes, 0, (int)stream.Length);
            // rjson=Encoding.UTF8.GetString(dataBytes);
            //替换Json的Date字符串
            string p = @"\\/Date(\d+)\+\d+\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }
        public static string DsToJson(DataSet ds,string tbname) {
            DataTable dt = ds.Tables[tbname];
         //   var jssettint = new JsonSerializerSettings();
            string js = JsonConvert.SerializeObject(dt).Replace(" ", "");
            return js;
        }
        public static string DsToJson(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            //   var jssettint = new JsonSerializerSettings();
            string js = JsonConvert.SerializeObject(dt).Replace(" ","");
            return js;
        }
        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
 {
 string result = string.Empty;
 DateTime dt = new DateTime(1970, 1, 1);
 dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
 dt = dt.ToLocalTime();
 result = dt.ToString("yyyy-MM-dd HH:mm:ss");
 return result;
 }

/// <summary>
 /// 将时间字符串转为Json时间
 /// </summary>
 private static string ConvertDateStringToJsonDate(Match m)
 {
 string result = string.Empty;
 DateTime dt = DateTime.Parse(m.Groups[0].Value);
 dt = dt.ToUniversalTime();
 TimeSpan ts = dt - DateTime.Parse("1970-01-01");
 result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
 return result;
 }
// 从一个Json串生成对象信息
public static MyDic<string,string> JsonToObject(string jsonString)
        {
           // DataContractJsonSerializer serializer = new DataContractJsonSerializer(object);
          //  MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var js = JsonConvert.DeserializeObject<MyDic<string, string>>(jsonString);
            // return serializer.ReadObject(mStream);
            return js;
        }
        /// <summary>
        /// Json解析
        /// </summary>
        /// <param name="jsons">客户端发送的Json参数</param>
        /// <returns>JsonModel实例</returns>
public static JsonModel JsonToDicList(string jsons) {
        // string str = @"{'pos_mast':[{'lshh':'sssssss'}]}";
        // string str1 = @"{'lshh':'ddddd'}";
        //string s = File.ReadAllText("D:\\0au.txt", Encoding.Default);
       // var jser = new JavaScriptSerializer();
            // var js = JsonConvert.DeserializeObject<Dictionary<string, string>>(str1);
            //Console.WriteLine(js["lshh"]);
            JsonModel JsM = new JsonModel();
      try { 
         JsM.Jsons = JsonConvert.DeserializeObject<MyDic<string, List<MyDic<string, string>>>>(jsons);
                foreach (string k in JsM.JsonKeys())
                {
                    Trace.WriteLine("Json解析成功,要上传表::" + k + "::数据条目::" + JsM.Jsons[k].Count);
                }
           }
       catch{
        string ErrorPath = ConfigurationManager.AppSettings["ErrorJson"].ToString();
                Console.WriteLine(ErrorPath);
        string ErrorJsonName = DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "")+".txt";
                if (!Directory.Exists(ErrorPath))
                {
                    Directory.CreateDirectory(ErrorPath);
                }                
                 File.AppendAllText(ErrorPath + "\\" + ErrorJsonName,jsons);
                Trace.WriteLine("本次解析的Json数据失败,已将错误数据存入::"+ ErrorPath + "\\" + ErrorJsonName);
            }
            return JsM;
            }
        public static bool ListContainsKey(List<string> lis,string value) {
            bool b = false;
            foreach (string s in lis) {
                if (value.ToUpper().Equals(s.ToUpper()))
                { b = true; } 
            }
            return b;
        }
    }
}