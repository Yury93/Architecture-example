namespace CodeBase.Infrastructer.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
