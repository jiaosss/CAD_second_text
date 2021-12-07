using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//使用CAD命名空间
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Colors;

namespace CAD_second_text
{
    public class Cmd
    {
        [CommandMethod("Cmdhello")]
        public void Cmdhello()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Hello World From VS C#");
            //System.Windows.Forms.MessageBox.Show("Hello World From VS C#(It's for Debugging)", "Info(Tips)");
        }

        [CommandMethod("CmdSum")]
        public void CmdSum()
        {
            int sum = 0;
            int max = 25;
            for (int i = 1; i <=max;i++)
            {
                sum += i;
            }

            double divisor = 13;
            double divide = sum / divisor;
            System.Windows.Forms.MessageBox.Show(sum.ToString() + "\n" + divide, "Info(Tips)");
        }
    }
}
