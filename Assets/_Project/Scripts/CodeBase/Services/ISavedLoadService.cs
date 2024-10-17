using CodeBase.Data;

namespace CodeBase.Services.SaveLoad
{
    public interface ISavedLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}