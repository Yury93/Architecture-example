
using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHp;
        public int MaxHp;
        public void ResetHp()
        {
            CurrentHp = MaxHp;
        }
    }
}