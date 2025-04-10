using Data;

namespace Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameProgress GameProgress { get; set; }
    }
}