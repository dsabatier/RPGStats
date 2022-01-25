using NUnit.Framework;
using RPGStats.SimpleStateMachine;

namespace Tests
{
    public class SimpleStateMachineTests
    {
        [Test]
        public void CanCreateSimpleStateMachine()
        {
            Assert.DoesNotThrow(() =>
            {
                var fsm = new SimpleStateMachine();
            });
        }

        [Test]
        public void CanAddState()
        {
            IState state = new TestState();
            var fsm = new SimpleStateMachine();
            
            Assert.DoesNotThrow(() =>
            {
                fsm.Change(state);
            });

        }

        private class TestState : IState
        {
            
        }
    }
}