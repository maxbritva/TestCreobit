using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public GameProgress GameProgress { get; set; }
    }
}