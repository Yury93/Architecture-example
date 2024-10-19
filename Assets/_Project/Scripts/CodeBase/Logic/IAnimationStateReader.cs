
 
 

namespace CodeBase.Logic
{
    public enum AnimatorState
    {
        Idle, Attack, Move, Die, Unknow
    }
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState State { get; }
    }
}