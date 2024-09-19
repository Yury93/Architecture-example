using CodeBase.Data;

namespace CodeBase.Services.PersistantProgress
{
    public class PersistentProgressService : IPersistentProgressService, IService
    {
        public PlayerProgress Progress { get; set; }
    }
}