using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Email.Dtos
{
    public class EmailMessageDto
    {
		public EmailMessageDto()
		{

		}

		//public List<EmailAddressDto> ToAddresses { get; set; }
		//public List<EmailAddressDto> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string FromName { get; set; }
		public string FromEmail { get; set; }
		public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string ToMailBox { get; set; }

        public List<string> AttachemntFiles { get; set; }
    }

	//public class EmailAddressDto
	//{
	//	public string Name { get; set; }
	//	public string Address { get; set; }
	//}
}
