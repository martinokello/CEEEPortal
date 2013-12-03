using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CEEEServices.Interfaces;

namespace CEEEServices
{
    public class MailTemplateService : IMailTemplateService
    {
        public string LoadTemplateByName(string physicalFilePath)
        {
            StreamReader stream = null;
            String fileText = string.Empty;
            try
            {
                var fileInfo = new FileInfo(physicalFilePath);

               stream = fileInfo.OpenText();

                fileText= stream.ReadToEnd();
            }
            catch (Exception exception)
            {

            }
            finally
            {
               if (stream != null)
               {
                   stream.Close();
               }
            }
            return fileText;
        }
    }
}
