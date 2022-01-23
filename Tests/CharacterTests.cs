using NUnit.Framework;
using RPGStats.Characters;

namespace Tests
{
    public class CharacterTests
    {
        [Test]
        public void CanCreateNewCharacter()
        {
            Assert.DoesNotThrow(() =>
            {
                Character character = new Character();
            });
        }

        [Test]
        public void CanInitsWithDefaultHealth()
        {
            Character character = new Character();
            Assert.AreEqual(100, character.GetHealth()); 
        }
        
        [Test]
        public void CanInitsWithDefaultDefense()
        {
            Character character = new Character();
            Assert.AreEqual(100, character.GetDefense()); 
        }
    }
}