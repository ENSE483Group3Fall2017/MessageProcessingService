using ENSE483Group3Fall2017.Messaging.Contracts;
using ENSE483Group3Fall2017.PetTracking.Contracts.V1;
using MediatR;
using MessageProcessingWebJob.Features.PetTracking;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MessageProcessingWebJob
{
    public class Functions
    {
        private readonly IMediator _mediator;

        public Functions(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task ProcessQueueMessage([ServiceBusTrigger("ense483group3fall2017.petstracking.messages.received")] string message, TextWriter log)
        {
            var envelop = JsonConvert.DeserializeObject<Envelop<TrackingBatch>>(message);
            var trackingBatch = envelop.Payload;
            var processCommand = new ProcessTracking.Command(trackingBatch);

            return _mediator.Send(processCommand);
        }
    }
}
