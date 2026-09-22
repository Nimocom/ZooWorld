using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ZooWorld.Shared.Runtime.Updates
{
    public class FixedUpdateService : IFixedTickable
    {
        private readonly List<IFixedUpdatable> _items = new();

        public void Add(IFixedUpdatable item)
        {
            _items.Add(item);
        }

        public void Remove(IFixedUpdatable item)
        {
            _items.Remove(item);
        }

        public void FixedTick()
        {
            var deltaTime = Time.fixedDeltaTime;

            for (var i = 0; i < _items.Count; i++)
                _items[i].FixedTick(deltaTime);
        }
    }
}
