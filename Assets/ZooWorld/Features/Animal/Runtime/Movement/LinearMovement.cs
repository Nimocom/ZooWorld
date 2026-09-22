using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Animals.Runtime.Movement
{
    public class LinearMovement : IAnimalMovement
    {
        private readonly Rigidbody _body;
        private readonly PlayArea _playArea;
        private readonly float _speed;
        private Vector3 _direction;

        public LinearMovement(Rigidbody body, PlayArea playArea, float speed)
        {
            _body = body;
            _playArea = playArea;
            _speed = speed;

            var direction = Random.insideUnitCircle.normalized;
            _direction = new Vector3(direction.x, 0f, direction.y);
        }

        public void FixedTick(float deltaTime)
        {
            _direction = _playArea.GetMovementDirection(_body.position, _direction);
            _body.MoveRotation(Quaternion.LookRotation(_direction));

            var currentVelocity = _body.linearVelocity;
            var desiredVelocity = _direction * _speed;

            _body.linearVelocity = new Vector3(desiredVelocity.x, currentVelocity.y, desiredVelocity.z);
        }
    }
}
