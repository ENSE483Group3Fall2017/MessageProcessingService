using MediatR;
using MessageProcessingWebJob.Features.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts = ENSE483Group3Fall2017.PetTracking.Contracts.V1;

namespace MessageProcessingWebJob.Features.PetTracking
{
    public class ProcessTracking
    {
        public class Command : IRequest
        {
            public Command(Contracts.TrackingBatch trackingBatch)
            {
                TrackingBatch = trackingBatch ?? throw new ArgumentNullException(nameof(trackingBatch));
            }

            public Contracts.TrackingBatch TrackingBatch { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly IMediator _mediator;

            public CommandHandler(IMediator mediator)
            {
                _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            }

            public async Task Handle(Command message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));

                var batch = message.TrackingBatch;
                var location = await GetLocationByGpsCoordinates(batch.GpsCoordinates);
                var trackingRecords = await SquashTrackingRecord(message, location);
                await SaveProcessedRecord(trackingRecords);
            }

            private Task<IEnumerable<TrackingRecord>> SquashTrackingRecord(Command message, Location location)
            {
                var squashRecords = new SquashTrackingData.Query(message.TrackingBatch, location);
                return _mediator.Send(squashRecords);
            }

            private Task<Location> GetLocationByGpsCoordinates(string gpsCoordinates)
            {
                var queryLocation = new ReverseCoordinatesToAddress.Query(gpsCoordinates);
                return _mediator.Send(queryLocation);
            }

            private Task SaveProcessedRecord(IEnumerable<TrackingRecord> records)
            {
                var saveCommand = new RecordTracking.Command(records);
                return _mediator.Send(saveCommand);
            }
        }
    }
}