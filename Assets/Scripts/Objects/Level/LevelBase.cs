using UnityEngine;

namespace Game.Object
{
    public class LevelBase : CustomBehaviour, ILevel
    {
        public override void Initialize()
        {
            Debug.Log("Initialize Level");
        }

        public void OnSpawnLevel()
        {
            Debug.Log("On Spawn Level");
        }
    }
}