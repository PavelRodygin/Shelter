using System;

namespace Core.Infrastructure
{
    public class BootstrapState : IState
    {
        public BootstrapState(GameStateMachine gameStateMachine)
        {
            
        }

        public void Enter()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            //Ну ебать, регистрация сервисов;
            //InputService = RegisterInputService;
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}