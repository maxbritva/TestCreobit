using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}