

using System;

namespace EbalitWebForms.BusinessLogicLayer.DTO
{
    [Serializable]
    public class WorkingReportFindDto
    {
        public int? ProjectId { get; set; }
        public string TaskGuid { get; set; }
        public int? ResourceId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}