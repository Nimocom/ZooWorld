using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Definitions;

namespace ZooWorld.Features.Spawning.Runtime
{
    [CreateAssetMenu(fileName = "AnimalCatalog", menuName = "ZooWorld/Animals/Animal Catalog")]
    public class AnimalCatalog : ScriptableObject
    {
        [SerializeField] private AnimalDefinition[] _animals;

        public AnimalDefinition GetRandom()
        {
            return _animals[Random.Range(0, _animals.Length)];
        }
    }
}
