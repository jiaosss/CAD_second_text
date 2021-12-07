using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//使用CAD命名空间
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Colors;

namespace LoadX
{
    public class LocdX
    {
        private Action cmd;

        //1.CAD中直接输入DD执行命令即可
        [CommandMethod("DD")]
        public void ReloadX()
        {
            #region CmdHello
            //string dllName = "CAD_second_text";
            //string className = "CAD_second_text";
            //string methodName = "CmdHello";
            #endregion

            #region CmdSum
            //string dllName = "CAD_second_text.dll";
            //string className = "CAD_second_text.Cmd";
            //string methodName = "CmdSum";
            #endregion

            //json.NET
            #region Json.NET
            CmdInfo cmdInfo = new CmdInfo();
            string jsonfile = @"D:\3D练习\.NET+CAD\CAD_second_text\Json.json";
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    cmdInfo.DllName = o["DllName"].ToString();
                    cmdInfo.ClassName = o["ClassName"].ToString();
                    cmdInfo.MethodName = o["MethodName"].ToString();
                }
            }
            string dllName = cmdInfo.DllName;
            string className = cmdInfo.ClassName;
            string methodName = cmdInfo.MethodName;

            #endregion

            //2.套路不动
            #region Don't Change this Code
            var adapterFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var targetFilePath = Path.Combine(adapterFileInfo.DirectoryName,dllName);
            var targetAssembly = Assembly.Load(File.ReadAllBytes(targetFilePath));
            var targetType = targetAssembly.GetType(className);
            var targetMethod = targetType.GetMethod(methodName);
            var targetObject = Activator.CreateInstance(targetType);
            cmd = () => targetMethod.Invoke(targetObject, null);

            try
            {
                cmd?.Invoke();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Tips");
            };
            #endregion
        }
    }
    public class CmdInfo
    {
        public string DllName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
    }
}
