namespace RPGStats.SimpleStateMachine
{
    public class SimpleStateMachine
    {
        private IState _currentState;
        
        public void Change(IState state)
        {
            _currentState = state;
        }
    }
}