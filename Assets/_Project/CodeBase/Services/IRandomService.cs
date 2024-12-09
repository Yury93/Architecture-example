 
namespace CodeBase.Services
{
    public interface IRandomService : IService
    {
        int Next(int lootMin, int lootMax);
    }
}