namespace CodeBase.Infrastructer.StateMachine
{
    public class GameLoopState : IState
    {
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        public GameLoopState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter()
        {
           
        }

        public void Exit()
        {
           
        }
    }
}
