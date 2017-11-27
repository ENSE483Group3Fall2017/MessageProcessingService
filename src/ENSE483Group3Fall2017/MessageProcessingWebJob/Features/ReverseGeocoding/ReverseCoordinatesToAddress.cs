using MediatR;
using MessageProcessingWebJob.Features.Shared;
using System;
using System.Threading.Tasks;

namespace MessageProcessingWebJob.Features.PetTracking
{
    public class ReverseCoordinatesToAddress
    {
        public class Query : IRequest<Location>
        {
            public Query(string gpsCoordinates)
            {
                if (string.IsNullOrWhiteSpace(gpsCoordinates)) throw new ArgumentNullException(nameof(gpsCoordinates));
                GpsCoordinates = gpsCoordinates;
            }

            public string GpsCoordinates { get; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Location>
        {
            public Task<Location> Handle(Query message) =>
                Task.FromResult(new Location { Address = "Canada, SK, Regina, University of Regina" } );
        }
    }
}
