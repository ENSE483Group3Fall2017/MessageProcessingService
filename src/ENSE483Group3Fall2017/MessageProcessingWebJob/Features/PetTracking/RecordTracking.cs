using MediatR;
using MessageProcessingWebJob.Features.Shared;
using System;
using System.Collections.Generic;

namespace MessageProcessingWebJob.Features.PetTracking
{
    public class RecordTracking
    {
        public class Command : IRequest
        {
            public Command(IEnumerable<TrackingRecord> trackingRecords)
            {
                TrackingRecords = trackingRecords ?? throw new ArgumentNullException(nameof(trackingRecords));
            }

            public IEnumerable<TrackingRecord> TrackingRecords { get; }
        }
    }
}
