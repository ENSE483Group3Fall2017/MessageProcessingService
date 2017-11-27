using System.Data.Entity;

namespace MessageProcessingWebJob.Infrastructure.Extrenal.RecordTracking.DAL
{
    public class PetTrackingContext : DbContext
    {
        public PetTrackingContext()
            : base("PetTrackingDatabase")
        {
        }

        public DbSet<TrackingInfo> TrackingInfos { get; set; }
    }
}
