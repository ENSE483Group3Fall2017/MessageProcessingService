using AutoMapper;
using MediatR;
using MessageProcessingWebJob.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Contracts = ENSE483Group3Fall2017.PetTracking.Contracts.V1;
using System.Threading.Tasks;

namespace MessageProcessingWebJob.Features.PetTracking
{
    public class SquashTrackingData
    {
        public class Query : IRequest<IEnumerable<TrackingRecord>>
        {
            public Query(Contracts.TrackingBatch trackingBatch, Location location)
            {
                TrackingBatch = trackingBatch ?? throw new ArgumentNullException(nameof(trackingBatch));
                Location = location ?? throw new ArgumentNullException(nameof(location));
            }

            public Contracts.TrackingBatch TrackingBatch { get; }

            public Location Location { get; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, IEnumerable<TrackingRecord>>
        {
            private readonly IMapper _mapper;

            public QueryHandler(IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public Task<IEnumerable<TrackingRecord>> Handle(Query message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));

                var batch = message.TrackingBatch;
                var items = message.TrackingBatch.Items;
                var location = message.Location;

                var beaconIds = message.TrackingBatch.Items.Select(x => x.BeaconId).Distinct();

                var result = items.GroupBy(x => x.BeaconId)
                                  .Select(x => MapTrackingDataIntoRecord(batch, location, x))
                                  .ToArray().AsEnumerable();

                return Task.FromResult(result);
            }

            private TrackingRecord MapTrackingDataIntoRecord(Contracts.TrackingBatch batch, Location location,
                                                             IEnumerable<Contracts.TrackingItem> beaconTrackings)
            {
                var trackingRecord = _mapper.Map<TrackingRecord>((batch, location));

                var orderedByProximity = beaconTrackings.OrderBy(x => x.Proximity).ToArray();
                var minRecord = orderedByProximity.First();
                var maxRecord = orderedByProximity.Last();

                trackingRecord.BeaconId = minRecord.BeaconId;
                trackingRecord.MinProximityInFrame = minRecord.Proximity;
                trackingRecord.MinProximityTime = minRecord.Created;
                trackingRecord.MaxProximityInFrame = maxRecord.Proximity;
                trackingRecord.MaxProximityTime = maxRecord.Created;

                return trackingRecord;
            }
        }
    }
}