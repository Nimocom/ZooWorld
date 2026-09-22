using TMPro;
using UnityEngine;

namespace ZooWorld.Features.DeathCounter.Runtime
{
    public class DeathCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;

        public void Show(int deadPrey, int deadPredators)
        {
            _label.text = $"Prey: {deadPrey}\nPredators: {deadPredators}";
        }
    }
}
