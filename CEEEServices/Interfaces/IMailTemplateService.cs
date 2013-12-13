using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEEEServices.Interfaces
{
    public interface IMailTemplateService
    {
        string LoadTemplateByName(string templateName);
    }
}
