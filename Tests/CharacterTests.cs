using NUnit.Framework;
using RPGStats.Characters;

namespace Tests
{
    public class CharacterTests
    {
        private const string Name = "name";
        
        [Test]
        public void CanInitWithDefaultHealth()
        {
            Character character = Character.Create<Character>(Name);
            Assert.AreEqual(100, character.GetMaxHealth()); 
        }
        
        [Test]
        public void CanInitWithDefaultDefense()
        {
            Character character = Character.Create<Character>(Name);
            Assert.AreEqual(100, character.GetDefense()); 
        }
        
        [Test]
        public void CanInitWithDefaultAttack()
        {
            Character character = Character.Create<Character>(Name);
            Assert.AreEqual(100, character.GetAttack()); 
        }

        [Test]
        public void CanCreateCharacterWithName()
        {
            Character character = Character.Create<Character>(Name);
            Assert.AreEqual(Name, character.GetName());
        }
        
        [Test]
        public void CustomCharacterCanAddStats()
        {
            Character character = Character.Create<TestCustomCharacter>(Name);
            
            Assert.AreEqual(100, character.GetMaxHealth());
        }



        private class TestCustomCharacter : Character
        {
            protected TestCustomCharacter(string name) : base(name)
            {
            }

            protected override void CreateStats()
            {
                _stats.AddStat("Health", 100, 5.0);
            }
        }
    }
}