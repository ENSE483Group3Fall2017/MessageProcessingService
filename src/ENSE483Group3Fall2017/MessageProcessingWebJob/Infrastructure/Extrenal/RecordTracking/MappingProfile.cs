using AutoMapper;
using MessageProcessingWebJob.Features.Shared;
using MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking.DAL;

namespace MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrackingRecord, TrackingInfo>();
        }
    }
}
