using UnityEngine;
using UnityEngine.AddressableAssets;
using ZooWorld.Features.Animals.Runtime.Enums;
using ZooWorld.Features.Animals.Runtime.Movement;

namespace ZooWorld.Features.Animals.Runtime.Definitions
{
    [CreateAssetMenu(fileName = "AnimalDefinition", menuName = "ZooWorld/Animals/Animal Definition")]
    public class AnimalDefinition : ScriptableObject
    {
        [field: SerializeField] public string SpeciesId { get; private set; }

        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
        [field: SerializeField] public AnimalRole Role { get; private set; }

        [field: SerializeField] public MovementProfile Movement { get; private set; }

        [field: SerializeField] public int Power { get; private set; }
    }
}
