using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.Features.TastyFeedback.Runtime
{
    public class TastyLabelPool : IDisposable
    {
        private readonly TastyLabelView _prefab;
        private readonly RectTransform _root;
        private readonly Queue<TastyLabelView> _pool = new();
        private readonly List<TastyLabelView> _instances = new();

        public TastyLabelPool(TastyLabelView prefab, RectTransform root)
        {
            _prefab = prefab;
            _root = root;
        }

        public TastyLabelView Get()
        {
            if (_pool.Count > 0)
                return _pool.Dequeue();

            var view = UnityEngine.Object.Instantiate(_prefab, _root);
            _instances.Add(view);

            return view;
        }

        public void Release(TastyLabelView view)
        {
            if (view == null)
                return;

            view.Hide();
            _pool.Enqueue(view);
        }

        public void Dispose()
        {
            foreach (var view in _instances)
                if (view)
                    UnityEngine.Object.Destroy(view.gameObject);
        }
    }
}
