using System;

namespace TableRelations.Application.Infrastructure.Entities
{
    public class BaseHistoryEntity : BaseEntity
    {
        public int HistoryId { get; set; }

        public int HistoryStateId { get; set; }

        public int ReleaseVersionId { get; set; }

        public int HistoryUserId { get; set; }

        public DateTime HistoryDateTime { get; set; }
    }
}