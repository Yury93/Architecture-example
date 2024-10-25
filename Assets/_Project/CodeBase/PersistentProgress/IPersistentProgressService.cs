using CodeBase.Data;

namespace CodeBase.Services.PersistantProgress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress Progress { get; set; }
    }
}