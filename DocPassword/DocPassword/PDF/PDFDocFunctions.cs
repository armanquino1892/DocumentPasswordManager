using DocPassword.Interface;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocPassword.PDF
{
    public class PDFDocFunctions : IPDFPassword
    {
        PdfDocument pdfDocument = new PdfDocument();
        private string Filepath { get; set; }

        public PDFDocFunctions(string fileAndPath)
        {
            Filepath = fileAndPath;
        }

        public bool AddPDFPassword(string password, out string status)
        {

            bool isvalid = ValidationChecker(password, Filepath, out string message);
            if (!isvalid)
            {
                status = message;
                return false;
            }

            try
            {
                HasPass hasPass = HasPassword(Filepath);
                if (!hasPass.haspassword)
                {
                    PdfSecuritySettings securitySettings = pdfDocument.SecuritySettings;
                    
                    // Setting one of the passwords automatically sets the security level to 
                    // PdfDocumentSecurityLevel.Encrypted128Bit.
                    securitySettings.UserPassword = password;
                    securitySettings.OwnerPassword = password;

                    // Don't use 40 bit encryption unless needed for compatibility
                    //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

                    // Restrict some rights.
                    //securitySettings.PermitAccessibilityExtractContent = false;
                    //securitySettings.PermitAnnotations = false;
                    //securitySettings.PermitAssembleDocument = false;
                    //securitySettings.PermitExtractContent = false;
                    //securitySettings.PermitFormsFill = true;
                    //securitySettings.PermitFullQualityPrint = false;
                    //securitySettings.PermitModifyDocument = true;
                    //securitySettings.PermitPrint = false;

                    // Save the document...
                    pdfDocument.Save(Filepath);
                    status = "Password Added Successfully!";
                    return true;
                }
                else
                {
                    status = hasPass.status;
                    return false;
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }

        private HasPass HasPassword(string filepath)
        {
            //Check if opening the file requires password
            //Return True if File requires password else false
            HasPass hp = new HasPass();
            try
            {
                pdfDocument = PdfReader.Open(filepath);
                hp.status = "No Password";
                hp.haspassword = false;
                return hp; //has no password
            }
            catch(Exception ex)
            {
                hp.status = "File already has password";
                hp.haspassword = true; 
                return hp; //has password
            }
        }

        public bool RemovePDFPassword(string password, out string status)
        {
            bool isvalid = ValidationChecker(password, Filepath, out string message);
            if (!isvalid)
            {
                status = message;
                return false;
            }

            try
            {

                PdfDocument pdfDocument;

                pdfDocument = PdfReader.Open(Filepath, password);
                if(pdfDocument != null)
                {
                    
                    bool hasOwnerAccess = pdfDocument.SecuritySettings.HasOwnerPermissions;

                    hasOwnerAccess = pdfDocument.SecuritySettings.HasOwnerPermissions;

                    PdfDocumentSecurityLevel level = pdfDocument.SecuritySettings.DocumentSecurityLevel;

                    pdfDocument.Save(Filepath);
                }
                else
                {
                    status = "Failed";
                    return false;
                }

                status = "Password Removed Successfully!";
                return true;
            }
            catch(Exception ex)
            {
                status = ex.Message;
                return false;
            }
        }

        private bool ValidationChecker(string password, string filepath, out string status)
        {
            status = string.Empty;
            if (string.IsNullOrEmpty(password))
            {
                status = "password field required";
                return false;
            }

            if (string.IsNullOrEmpty(Filepath))
            {
                status = "file path is empty";
                return false;
            }
            else if (Path.GetExtension(Filepath).ToLower() != ".pdf")
            {
                status = "invalid file type";
                return false;
            }

            return true;
        }

        public class HasPass
        {
            public bool haspassword { get; set; }
            public string status { get; set; }
        }

    }
}
