using System;
using System.Diagnostics;
using System.IO;

namespace DF_DAL
{
    public class DF_Listener : TraceListener
    {
        public string FilePath { get; private set; }
        public DF_Listener(string filePath)
         {
             FilePath = filePath;
              dellog();
         }
        public override void Write(string message)
           {
              File.AppendAllText(FilePath, message);
          }
           public override void WriteLine(string message)
          {
              File.AppendAllText(FilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss||") + message + Environment.NewLine);
          }
           public override void Write(object o, string category)
           {
               string message = string.Empty;
             if (!string.IsNullOrEmpty(category))
               {
                  message = category + ":";
             }
              if (o is Exception)//如果参数对象o是与Exception类兼容,输出异常消息+堆栈,否则输出o.ToString()
              {
                 var ex = (Exception)o;
                  message += ex.Message + Environment.NewLine;
                  message += ex.StackTrace;
              }
            else if(null != o)
             {
                 message += o.ToString();
             }
  
             WriteLine(message);
          }
        private void dellog() {
        int n = 1000;
        if (File.Exists(FilePath)) { 
        string[] lines = System.IO.File.ReadAllLines(FilePath);
            //MessageBox.Show(lines.Length.ToString());
        if (lines.Length > 5000)
       System.IO.File.WriteAllText(FilePath, string.Join(Environment.NewLine, lines, n, lines.Length - n));
           }
        }
    }
}
