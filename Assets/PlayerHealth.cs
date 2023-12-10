using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable, IHealable
{
    [HideInInspector] public UnityEvent<int> TakeDamageEvent = new UnityEvent<int>();
    [HideInInspector] public UnityEvent<int> HealthChangeEvent = new UnityEvent<int>();

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;

    private void Awake()
    {
        _health = _maxHealth;
        HealthChangeEvent?.Invoke(_health);
    }

    public void TakeDamage(int damage)
    {
        if(damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        _health -= damage;
        if(_health < 0)
            _health = 0;

        HealthChangeEvent?.Invoke(_health);
    }

    public void Heal(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        _health += value;
        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChangeEvent?.Invoke(_health);
    }
}
