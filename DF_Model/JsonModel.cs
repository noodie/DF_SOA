using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DF_DAL
{
   public  class JsonModel
    {
        private MyDic<string,List<MyDic<string, string>>> jsons;
        private List<MyDic<string, string>> tb_list;
        private MyDic<string, string> tb_dic;
        private List<string> lshh_list;

        public MyDic<string, List<MyDic<string, string>>> Jsons
        {
            get
            {
                return jsons;
            }

            set
            {
                jsons = value;
            }
        }

        public List<MyDic<string, string>> Tb_list
        {
            get
            {
                return tb_list;
            }

            set
            {
                tb_list = value;
            }
        }

        public MyDic<string, string> Tb_dic
        {
            get
            {
                return tb_dic;
            }

            set
            {
                tb_dic = value;
            }
        }

        public List<string> Lshh_list
        {
            get
            {
                if (lshh_list == null)
                    return new List<string>();
                else
                    return lshh_list;
            }

            set
            {
                lshh_list = value;
            }
        }

        public List<string> JsonKeys() {
            return new List<string>(jsons.Keys); 
        }
        public List<MyDic<string, string>> JsonDic(string tablename) {    
            if (jsons.ContainsKey(tablename))
            {
                return jsons[tablename];
            }
            return null;
        }
        public void SetData(JsonModel JM, Dictionary<string,string> sd) {
            try
            {
                string data = JM.Tb_dic[sd["D_name"]].Trim();
                int len = 0;
                bool databool = false;
                if (sd["D_len"]!=string.Empty)
                 len=int.Parse(sd["D_len"]);
                if (sd["D_type"] == "decimal")
                 len = int.Parse(sd["D_SCALE"]);
                //当数据为空时 字符字符串类型数据不处理，其它数据要处理 
                if (data == string.Empty || data.Equals("") || data.Length == 0)
                    databool = true;
                switch (sd["D_type"])
                {
                    case "char":
                        data = data.Length > len ? data.Substring(0, len) : data;
                        break;
                    case "varchar":
                        data = data.Length > len ? data.Substring(0, len) : data;
                        break;
                    case "int":
                        data= databool? "0" : ((int)Convert.ToSingle(data)).ToString();
                        break;
                    case "decimal":
                        data = databool ? "0": Math.Round(Convert.ToDouble(data),len).ToString();
                        break;
                    default:
                        break;
                }
                JM.Tb_dic[sd["D_name"]] = data;
            }
            catch(Exception ex) {
                string s = string.Format("字段{0},值{1},类型{2}转换失败,自动转为默认值{3}", sd["D_name"], JM.Tb_dic[sd["D_name"]], sd["D_type"], sd["D_def"]);
                JM.Tb_dic[sd["D_name"]] = sd["D_def"];
                Trace.TraceError(s+"::" + ex.Message); 
            }
        }
    }
}
