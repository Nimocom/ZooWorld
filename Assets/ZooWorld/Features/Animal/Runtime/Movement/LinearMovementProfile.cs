using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Animals.Runtime.Movement
{
    [CreateAssetMenu(fileName = "LinearMovementProfile", menuName = "ZooWorld/Animals/Movement/Linear Movement")]
    public sealed class LinearMovementProfile : MovementProfile
    {
        [SerializeField] private float _speed = 3f;

        public override IAnimalMovement CreateRuntime(Rigidbody body, PlayArea playArea)
        {
            return new LinearMovement(body, playArea, _speed);
        }
    }
}
