using AutoMapper;
using Contracts = ENSE483Group3Fall2017.PetTracking.Contracts.V1;
using MessageProcessingWebJob.Features.Shared;
using System;


namespace MessageProcessingWebJob.Features.PetTracking
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<(Contracts.TrackingBatch batch, Location location), TrackingRecord>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dst => dst.BatchId, opt => opt.MapFrom(src => src.batch.Id))
                .ForMember(dst => dst.FrameStartTime, opt => opt.MapFrom(src => src.batch.FrameStart))
                .ForMember(dst => dst.FrameEndTime, opt => opt.MapFrom(src => src.batch.FrameEnd))
                .ForMember(dst => dst.GpsCoordinates, opt => opt.MapFrom(src => src.batch.GpsCoordinates))
                .ForMember(dst => dst.GeoReversedAddress, opt => opt.MapFrom(src => src.location.Address))
                .ForMember(dts => dts.MinProximityInFrame, opt => opt.Ignore())
                .ForMember(dts => dts.MaxProximityInFrame, opt => opt.Ignore())
                .ForMember(dts => dts.MinProximityTime, opt => opt.Ignore())
                .ForMember(dts => dts.MaxProximityTime, opt => opt.Ignore());
        }
    }
}
