using System.Collections.Generic;
using RPGStats.Characters;

namespace RPGStats.Samples
{
    public class Party
    {
        private readonly List<Character> _characters = new();

        public void ReRollParty()
        {
            _characters.Clear();
            
            _characters.Add(Character.Create<Knight>("Trevor"));
            _characters.Add(Character.Create<Wizard>("Guntelf"));
        }
    }
}