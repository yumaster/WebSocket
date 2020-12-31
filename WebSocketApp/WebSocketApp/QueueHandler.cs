using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketApp
{
    public class QueueHandler: WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                QueueParameter parameter = JsonConvert.DeserializeObject<QueueParameter>(e.Data);
                if (parameter == null) SendMsg("error", "参数错误");
                SendMsg("success", parameter.content);
                if (FrmMain.instance != null)
                {
                    FrmMain.instance.Invoke(new Action(() =>
                    {
                        //new PrintController(parameter).Print();
                        Debug.WriteLine("执行排队方法");
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void SendMsg(string state, string content)
        {
            string result = "hello world_" + state + "_" + content;//string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}");
            this.Send(result);
        }
    }
    public class QueueParameter
    {
        /// <summary>
        /// 参数
        /// </summary>
        public string content { get; set; }
    }
}
