using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZooWorld.Features.TastyFeedback.Runtime
{
    public class TastyLabelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _visibleDuration;
        [SerializeField] private float _fadeDuration;

        private Sequence _sequence;

        public RectTransform RectTransform => (RectTransform)transform;

        public void Show(Action completed)
        {
            _sequence?.Kill();
            gameObject.SetActive(true);
            _label.alpha = 1f;
            transform.localScale = Vector3.zero;

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(Vector3.one, _scaleDuration).SetEase(Ease.OutBack));
            _sequence.AppendInterval(_visibleDuration);
            _sequence.Append(DOTween.To(() => _label.alpha, value => _label.alpha = value, 0f, _fadeDuration));
            _sequence.OnComplete(() => completed());
        }

        public void Hide()
        {
            _sequence?.Kill();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
