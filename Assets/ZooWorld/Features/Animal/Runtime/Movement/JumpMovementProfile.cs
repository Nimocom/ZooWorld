using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Animals.Runtime.Movement
{
    [CreateAssetMenu(fileName = "JumpMovementProfile", menuName = "ZooWorld/Animals/Movement/Jump Movement")]
    public class JumpMovementProfile : MovementProfile
    {
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpInterval;
        [SerializeField] private float _maxTurnAngle;

        public override IAnimalMovement CreateRuntime(Rigidbody body, PlayArea playArea)
        {
            return new JumpMovement(body, playArea, _jumpDistance, _jumpDuration, _jumpInterval, _maxTurnAngle);
        }
    }
}
