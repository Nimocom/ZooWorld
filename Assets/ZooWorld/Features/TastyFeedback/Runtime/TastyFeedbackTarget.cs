using UnityEngine;

namespace ZooWorld.Features.TastyFeedback.Runtime
{
    public class TastyFeedbackTarget : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;

        public Vector2 Offset => _offset;
    }
}
