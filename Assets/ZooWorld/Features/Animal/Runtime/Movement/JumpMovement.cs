using UnityEngine;
using ZooWorld.Features.Animals.Runtime.Movement.Contracts;
using ZooWorld.Shared.Runtime;

namespace ZooWorld.Features.Animals.Runtime.Movement
{
    public class JumpMovement : IAnimalMovement
    {
        private readonly Rigidbody _body;
        private readonly PlayArea _playArea;
        private readonly float _jumpDistance;
        private readonly float _jumpDuration;
        private readonly float _jumpInterval;
        private readonly float _maxTurnAngle;

        private Vector3 _direction;
        private float _timeUntilNextJump;

        public JumpMovement(Rigidbody body, PlayArea playArea, float jumpDistance, float jumpDuration, float jumpInterval, float maxTurnAngle)
        {
            _body = body;
            _playArea = playArea;
            _jumpDistance = jumpDistance;
            _jumpDuration = jumpDuration;
            _jumpInterval = jumpInterval;
            _maxTurnAngle = maxTurnAngle;

            var direction = Random.insideUnitCircle.normalized;
            _direction = new Vector3(direction.x, 0f, direction.y);
        }

        public void FixedTick(float deltaTime)
        {
            _timeUntilNextJump -= deltaTime;

            if (_timeUntilNextJump > 0f)
                return;

            Jump();

            _timeUntilNextJump = _jumpInterval;
        }

        private void Jump()
        {
            var turnAngle = Random.Range(-_maxTurnAngle, _maxTurnAngle);
            _direction = Quaternion.AngleAxis(turnAngle, Vector3.up) * _direction;
            _direction = _playArea.GetMovementDirection(_body.position, _direction);
            var horizontalSpeed = _jumpDistance / _jumpDuration;
            var verticalSpeed = -Physics.gravity.y * _jumpDuration * 0.5f;

            _body.MoveRotation(Quaternion.LookRotation(_direction));
            _body.linearVelocity = new Vector3(_direction.x * horizontalSpeed, verticalSpeed, _direction.z * horizontalSpeed);
        }
    }
}
