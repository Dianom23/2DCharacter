using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth.HealthChangeEvent.AddListener(UpdateView);
    }

    private void UpdateView(int value)
    {
        float valueInPersent = (float)value / 100;
        _healthSlider.value = valueInPersent;
        print($"{value} --- {valueInPersent}%");
    }
}
