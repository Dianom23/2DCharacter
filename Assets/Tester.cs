using System;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    private IHealable _healable;
    private IDamagable _damagable;

    private void Awake()
    {
        _healable = _playerHealth.GetComponent<IHealable>();
        _damagable = _playerHealth.GetComponent<IDamagable>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _healable.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            _damagable.TakeDamage(15);
        }
    }
}
