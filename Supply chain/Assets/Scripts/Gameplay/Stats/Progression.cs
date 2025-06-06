using UnityEngine;

namespace SCG.Stats
{
    // This script creates a chart for all the stats of each character throughout the game
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]

    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        // Loops through the chart and returns the health value for each level based on what character type it is (Player or Robot)
        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass == characterClass)
                {
                    // It is level - 1 because level 1 is stored as Element 0 in the Progression chart
                    return progressionClass.health[level - 1];
                }
            }
            return 0;
        }

        // Creates the set of fields in the Progression chart
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}
