using MessagePipe;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using ZooWorld.Features.Animals.Runtime.Messages;

namespace ZooWorld.Features.TastyFeedback.Runtime
{
    public class TastyFeedbackPresenter : IStartable, ILateTickable, IDisposable
    {
        private readonly ISubscriber<AnimalAteMessage> _animalAteSubscriber;
        private readonly TastyLabelPool _pool;
        private readonly Camera _camera;
        private readonly RectTransform _root;
        private readonly List<TastyLabelView> _activeViews = new();
        private readonly Dictionary<TastyLabelView, TastyFeedbackTarget> _targets = new();

        private IDisposable _subscription;

        public TastyFeedbackPresenter(ISubscriber<AnimalAteMessage> animalAteSubscriber, TastyLabelPool pool, Camera camera, RectTransform root)
        {
            _animalAteSubscriber = animalAteSubscriber;
            _pool = pool;
            _camera = camera;
            _root = root;
        }

        public void Start()
        {
            _subscription = _animalAteSubscriber.Subscribe(OnAnimalAte);
        }

        public void LateTick()
        {
            for (var i = _activeViews.Count - 1; i >= 0; i--)
            {
                var view = _activeViews[i];
                var target = _targets[view];

                if (!target.gameObject.activeInHierarchy)
                {
                    Release(view);
                    continue;
                }

                UpdatePosition(view, target);
            }
        }



        private void OnAnimalAte(AnimalAteMessage message)
        {
            var target = message.Predator.View.GetComponent<TastyFeedbackTarget>();
            var view = _pool.Get();

            _activeViews.Add(view);
            _targets.Add(view, target);

            UpdatePosition(view, target);

            view.Show(() => Release(view));
        }

        private void UpdatePosition(TastyLabelView view, TastyFeedbackTarget target)
        {
            var screenPosition = _camera.WorldToScreenPoint(target.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_root, screenPosition, null, out var position);
            view.RectTransform.anchoredPosition = position + target.Offset;
        }

        private void Release(TastyLabelView view)
        {
            _activeViews.Remove(view);
            _targets.Remove(view);
            _pool.Release(view);
        }

        public void Dispose()
        {
            _subscription.Dispose();

            for (var i = _activeViews.Count - 1; i >= 0; i--)
                _pool.Release(_activeViews[i]);

            _activeViews.Clear();
            _targets.Clear();
        }
    }
}
