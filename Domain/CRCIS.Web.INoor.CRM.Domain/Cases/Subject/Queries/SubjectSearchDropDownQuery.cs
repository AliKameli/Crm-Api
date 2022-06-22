using Noor.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Queries
{
    public class SubjectSearchDropDownQuery
    {
        public string SearchWord { get; private set; }
        public int? ProductId { get; set; }
        public SubjectSearchDropDownQuery(string searchWord, int? productId)
        {
            searchWord = string.IsNullOrEmpty(searchWord) ? string.Empty : searchWord;
            SearchWord = searchWord.CrmSampleText();
            ProductId = productId;
        }
    }
}
