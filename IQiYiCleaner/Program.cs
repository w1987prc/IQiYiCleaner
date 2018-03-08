using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace IQiYiCleaner
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool cleancache = false;
            bool cleanproc;

            //清理缓存
            try
            {
                var cachefiles = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Packages\0C72C7CD.Beta_atj5cpqqdhzyp\LocalState\QiYi\QiyiHCDN\Config",
                    "iqiyi-0*.pgf");
                if (cachefiles.Length != 0)
                {
                    cleancache = true;
                    foreach (string cachefile in cachefiles)
                    {
                        File.Delete(cachefile);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("清理缓存失败！");
                return;
            }

            //清理进程
            try
            {
                var QyKernelproc = Process.GetProcessesByName("QyKernel");
                var QyProxyproc = Process.GetProcessesByName("QyProxy");
                if (QyKernelproc.Length == 0 && QyProxyproc.Length == 0)
                {
                    cleanproc = false;
                }
                else
                {
                    cleanproc = true;
                    if (QyKernelproc.Length >= 1)
                    {
                        QyKernelproc[0].Kill();
                    }
                    if (QyProxyproc.Length >= 1)
                    {
                        QyProxyproc[0].Kill();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("清理进程失败！");
                return;
            }

            string result;
            var restr1 = "未发现缓存文件！";
            var restr2 = "未发现相关进程！";
            if(cleancache && cleanproc)
            {
                result = "爱奇艺缓存和进程清理完毕！";
            }
            else if(cleancache && !cleanproc)
            {
                result = "爱奇艺缓存清理完毕！" + "\n" + restr2;
            }
            else if(!cleancache && cleanproc)
            {
                result= "爱奇艺进程清理完毕！" + "\n" + restr1;
            }
            else
            {
                result = restr1 + "\n" + restr2;
            }
            MessageBox.Show(result);
        }
    }
}
