using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Animals.Runtime.Movement
{
    public abstract class MovementProfile : ScriptableObject
    {
        public abstract IAnimalMovement CreateRuntime(Rigidbody body, PlayArea playArea);
    }
}
