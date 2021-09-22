using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YuanNewWebsite.Helpers.FileHelpers
{
    /// <summary>
    /// (讀取/寫入)檔案(Txt)最後修改日的 Helper
    /// 建立日期：2016/7/1 by Gelis.
    /// </summary>
    public class DateTextFileHelper
    {
        /// <summary>
        /// 從文字檔中寫入日期字串
        /// </summary>
        /// <param name="serverFolder">Server 完整絕對路徑</param>
        public void SetLastCreateDateTxt(string serverFolder)
        {
            FileStream fs = null;
            try
            {
                lock (this)
                {
                    fs = new FileStream(Path.Combine(serverFolder, "FileCreateDate.txt"), FileMode.Create, FileAccess.Write);

                    StreamWriter sw = null;
                    try
                    {
                        sw = new StreamWriter(fs);
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                    catch 
                    {
                      //讀不到或是出現錯誤不做任何處理
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        /// <summary>
        /// 從文字檔中讀取日期字串
        /// </summary>
        /// <param name="serverFolder">Server 完整絕對路徑</param>
        /// <returns></returns>
        public string GetLastCreateDateTxt(string serverFolder)
        {

            FileStream fs = null;
            string result = string.Empty;

            lock (this)
            {
                if (File.Exists(Path.Combine(serverFolder, "FileCreateDate.txt")))
                {
                    try
                    {
                        fs = new FileStream(Path.Combine(serverFolder, "FileCreateDate.txt"), FileMode.Open, FileAccess.Read);

                        StreamReader sr = null;
                        try
                        {
                            sr = new StreamReader(fs);
                            result = sr.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            //不處理此txt寫入失敗所引發的錯誤
                        }
                        finally
                        {
                            sr.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                        //不處理此txt寫入失敗所引發的錯誤
                    }
                    finally
                    {
                        fs.Close();
                    }

                }

            }



            return result;
        }
    }
}
