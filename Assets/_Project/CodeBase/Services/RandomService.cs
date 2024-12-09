 

namespace CodeBase.Services
{
    public class RandomService : IRandomService
    {
        public int Next(int lootMin, int lootMax)
        {
            System.Random rnd = new System.Random();
            int result = rnd.Next(lootMin, lootMax);
            return result;
        }
    }
}