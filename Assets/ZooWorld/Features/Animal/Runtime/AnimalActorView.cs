using UnityEngine;

namespace ZooWorld.Features.Animals.Runtime
{
    public class AnimalActorView : MonoBehaviour
    {
        public Rigidbody Body => _body;
        public Collider Collider => _collider;
        public AnimalActor Actor => _actor;

        [SerializeField] private Rigidbody _body;
        [SerializeField] private Collider _collider;

        private AnimalActor _actor;

        public void Bind(AnimalActor actor)
        {
            _actor = actor;
        }

        public void Unbind()
        {
            _actor = null;
        }
    }
}
