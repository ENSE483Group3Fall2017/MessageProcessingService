using System;

namespace MessageProcessingWebJob.Features.Shared
{
    public class TrackingRecord
    {
        public Guid Id { get; set; }

        public Guid BatchId { get; set; }

        public string BeaconId { get; set; }

        public string GpsCoordinates { get; set; }

        public string GeoReversedAddress { get; set; }

        public DateTime FrameStartTime { get; set; }

        public DateTime FrameEndTime { get; internal set; }

        public decimal MinProximityInFrame { get; set; }

        public DateTime MinProximityTime { get; set; }

        public decimal MaxProximityInFrame { get; set; }

        public DateTime MaxProximityTime { get; set; }
    }
}