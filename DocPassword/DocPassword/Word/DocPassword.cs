using DocPassword.Interface;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocPassword.Word
{
    public class DocPassword : IDocPassword
    {
        private string FileName { get; set; }
        private string SaveAsFilePath { get; set; }

        private Application wordApp = new Microsoft.Office.Interop.Word.Application();
        private object nullobj = System.Reflection.Missing.Value;
        private object pwd;        
        private Document aDoc = null;

        public DocPassword(string fileAndPath)
        {
            FileName = fileAndPath;            
        }
        
        public bool AddPassword(string password, out string status)
        {
            if (!CheckFile())
            {
                status = "File not found.";
                return false;
            }

            try
            {               
                object FName = FileName;
                pwd = password;
                
                wordApp.Visible = false;
                aDoc = wordApp.Documents.Open(FileName, PasswordDocument: password);
                aDoc.Password = password;                
                aDoc.SaveAs(ref FName, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
                aDoc.Close(ref nullobj, ref nullobj, ref nullobj);                
            }
            catch (Exception e)
            {
                status = e.Message;
                try
                {
                    aDoc.Close(ref nullobj, ref nullobj, ref nullobj);
                }
                catch { }
                return false;
            }
            finally
            {
                wordApp.Quit();
                wordApp = null;
            }

            status = "Password Added Successfully!";
            return true;
        }

        public bool RemovePassword(string password, out string status)
        {
            if (!CheckFile())
            {
                status = "File not found.";
                return false;
            }

            try
            {                
                pwd = password;
                string saveAsFileName = DateTime.Now.Ticks.ToString();
                
                wordApp.Visible = false;
                aDoc = wordApp.Documents.Open(FileName, PasswordDocument: password);                

                if (aDoc.HasPassword)
                    aDoc.Password = null;
                if (aDoc.ProtectionType != WdProtectionType.wdNoProtection)
                    aDoc.Unprotect(ref pwd);

                FileInfo fileInfo = new FileInfo(FileName);
                string ext = fileInfo.Extension;
                SaveAsFilePath = fileInfo.DirectoryName;
                string saveAs = SaveAsFilePath + "\\" + saveAsFileName + ext;
                object saveAsObject = saveAs;
                
                aDoc.SaveAs(ref saveAsObject, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
                aDoc.Close(ref nullobj, ref nullobj, ref nullobj);

                File.Delete(FileName);
                File.Copy(saveAs, FileName);
                File.Delete(saveAs);               
            }
            catch (Exception e)
            {
                status = e.Message;  
                try
                { 
                    aDoc.Close(ref nullobj, ref nullobj, ref nullobj);
                }
                catch { }
                return false;
            }
            finally
            {                
                wordApp.Quit();
                wordApp = null;
            }
           
            status = "Password Removed Successfully!";
            return true;
        }

        private bool CheckFile()
        {
            return File.Exists(FileName);
        }
    }
}
