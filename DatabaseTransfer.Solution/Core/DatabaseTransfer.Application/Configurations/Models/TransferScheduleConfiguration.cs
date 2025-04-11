using System;
using System.Collections.Generic;
using DatabaseTransfer.Application.Clients.Models;

namespace DatabaseTransfer.Application.Configurations.Models
{
    public class TransferScheduleConfiguration
    {
        public TransferScheduleConfiguration()
        {
            TableSchemas = new List<TableSchemaConfiguration>();

            TransferSynchronousType = TransferSynchronousTypes.Snapshot;
            TransferOffset = null;

            TransferScheduleType = TransferScheduleTypes.Daily;
            TransferScheduleStartTime = DateTime.Now;
        }

        public string Name { get; set; }

        public List<TableSchemaConfiguration> TableSchemas { get; set; }

        public TransferSynchronousTypes TransferSynchronousType { get; set; }

        public int? TransferOffset { get; set; }

        public TransferScheduleTypes TransferScheduleType { get; set; }

        public DateTime TransferScheduleStartTime { get; set; }

        public DateTime? ServiceStartDateTime { get; set; }

        public DateTime? ServiceCompleteDateTime { get; set; }

        /// <summary>
        ///     Is the current date within the schedule type
        /// </summary>
        /// <returns></returns>
        public bool IsScheduled()
        {
            if (!ServiceStartDateTime.HasValue)
            {
                return true;
            }

            switch (TransferScheduleType)
            {
                case TransferScheduleTypes.Daily:
                    return IsScheduledTime(new TimeSpan(1, 0, 0, 0));

                case TransferScheduleTypes.Weekly:
                    return IsScheduledTime(new TimeSpan(7, 0, 0, 0));

                case TransferScheduleTypes.Monthly:
                    return IsScheduledTime(new TimeSpan(30, 0, 0, 0));

                case TransferScheduleTypes.Yearly:
                    return IsScheduledTime(new TimeSpan(365, 0, 0, 0));
            }

            return false;
        }

        /// <summary>
        ///     Is the current datetime within a minute of schedule start time
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private bool IsScheduledTime(TimeSpan timeSpan)
        {
            var timeSpanTotalDays = (DateTime.Now.Date - ServiceStartDateTime.Value.Date).TotalDays;

            if (!(timeSpanTotalDays >= timeSpan.TotalDays))
            {
                return false;
            }

            var startTimeSpan = new TimeSpan(TransferScheduleStartTime.Hour, TransferScheduleStartTime.Minute - 1, 0);
            var endTimeSpan = new TimeSpan(TransferScheduleStartTime.Hour, TransferScheduleStartTime.Minute + 60, 0);

            return DateTime.Now.TimeOfDay > startTimeSpan && DateTime.Now.TimeOfDay < endTimeSpan;
        }
    }
}