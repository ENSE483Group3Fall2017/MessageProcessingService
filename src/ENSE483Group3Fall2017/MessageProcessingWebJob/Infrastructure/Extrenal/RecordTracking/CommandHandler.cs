using AutoMapper;
using MediatR;
using MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecordTrackingCommand = MessageProcessingWebJob.Features.PetTracking.RecordTracking.Command;

namespace MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking
{
    public class CommandHandler : IAsyncRequestHandler<RecordTrackingCommand>
    {
        private readonly IMapper _mapper;

        public CommandHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(RecordTrackingCommand command)
        {
            command = command ?? throw new ArgumentNullException(nameof(command));

            var trackingInfos = _mapper.Map<IEnumerable<TrackingInfo>>(command.TrackingRecords);
            using (var ctx = new PetTrackingContext())
            {
                ctx.TrackingInfos.AddRange(trackingInfos);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
