using AutoMapper.Configuration.Annotations;
using Noor.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Domain.Cases.Subject.Commands
{
    public class SubjectCreateCommand
    {
        public string Title { get;private set; }
        public int? ParentId { get;private set; }
        public bool IsActive { get;private set; }
        public int? Priority { get;private set; }
        public int? Code { get;private set; }
        public DateTime CreateAt { get;private set; }
        /// <summary>
        /// برای عنوان و کد
        /// </summary>
        public string SampleWord { get;private set; }

        public SubjectCreateCommand(string title, int? parentId, bool isActive, int? priority, int? code)
        {
            Title = title;
            ParentId = parentId;
            IsActive = isActive; 
            Priority = priority;
            CreateAt = DateTime.Now;
            Code = code;
            SampleWord = $"{title} - {code?.ToString()}".CrmSampleText();
        }
    }
}
