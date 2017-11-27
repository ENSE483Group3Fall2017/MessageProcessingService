using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking.DAL
{
    [Table("TrackingInfos")]
    public class TrackingInfo
    {
        [Key]
        public Guid ID { get; set; }

        public Guid BatchID { get; set; }

        public string BeaconID { get; set; }

        public string GpsCoordinates { get; set; }

        public string GeoReversedAddress { get; set; }

        public DateTime FrameStartTime { get; set; }

        public DateTime FrameEndTime { get; set; }

        public int MinProximityInFrame { get; set; }

        public DateTime MinProximityTime { get; set; }

        public int MaxProximityInFrame { get; set; }

        public DateTime MaxProximityTime { get; set; }
    }
}