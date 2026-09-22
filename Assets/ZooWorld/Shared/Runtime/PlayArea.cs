using UnityEngine;

namespace ZooWorld.Shared.Runtime
{
    public class PlayArea
    {
        private readonly Camera _camera;
        private readonly Plane _ground;

        public PlayArea(Camera camera, float groundHeight)
        {
            _camera = camera;
            _ground = new Plane(Vector3.up, new Vector3(0f, groundHeight + .03f, 0f));
        }

        public Vector3 GetRandomPosition()
        {
            return GetWorldPosition(new Vector2(Random.value, Random.value));
        }

        public Vector3 GetMovementDirection(Vector3 position, Vector3 direction)
        {
            var viewportPosition = _camera.WorldToViewportPoint(position);

            if (viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f)
                return direction;

            var returnDirection = GetWorldPosition(new Vector2(0.5f, 0.5f)) - position;
            returnDirection.y = 0f;

            return returnDirection.normalized;
        }

        private Vector3 GetWorldPosition(Vector2 viewportPosition)
        {
            var ray = _camera.ViewportPointToRay(viewportPosition);
            _ground.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }
    }
}
