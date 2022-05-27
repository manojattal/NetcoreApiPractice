using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInform.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "mailtoemail";
        private string _mailFrom = "mailfromemail";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"sending email with subject ${subject} to {_mailTo} from {_mailFrom}");
        }
    }
}
