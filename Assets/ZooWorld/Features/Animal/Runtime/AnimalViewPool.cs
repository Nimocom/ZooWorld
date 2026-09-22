using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;
using ZooWorld.Features.Animals.Runtime.Definitions;

namespace ZooWorld.Features.Animals.Runtime
{
    public class AnimalViewPool : IDisposable
    {
        private readonly IObjectResolver _resolver;
        private readonly Dictionary<AnimalDefinition, Queue<AnimalActorView>> _pools = new();
        private readonly List<AsyncOperationHandle<GameObject>> _handles = new();

        public AnimalViewPool(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public async UniTask<AnimalActorView> Get(AnimalDefinition definition, Vector3 position)
        {
            var pool = GetPool(definition);

            if (pool.Count > 0)
            {
                var pooledView = pool.Dequeue();

                pooledView.transform.SetPositionAndRotation(position, Quaternion.identity);
                pooledView.gameObject.SetActive(true);

                return pooledView;
            }

            var handle = Addressables.InstantiateAsync(definition.Prefab, position, Quaternion.identity);
            var gameObject = await handle.ToUniTask();

            _handles.Add(handle);
            _resolver.InjectGameObject(gameObject);

            var view = gameObject.GetComponent<AnimalActorView>();

            return view;
        }

        public void Release(AnimalDefinition definition, AnimalActorView view)
        {
            view.Unbind();

            view.Body.linearVelocity = Vector3.zero;
            view.Body.angularVelocity = Vector3.zero;
            view.gameObject.SetActive(false);

            GetPool(definition).Enqueue(view);
        }

        private Queue<AnimalActorView> GetPool(AnimalDefinition definition)
        {
            if (_pools.TryGetValue(definition, out var pool))
                return pool;

            pool = new Queue<AnimalActorView>();
            _pools.Add(definition, pool);
            return pool;
        }

        public void Dispose()
        {
            foreach (var handle in _handles)
                Addressables.ReleaseInstance(handle);
        }
    }
}
