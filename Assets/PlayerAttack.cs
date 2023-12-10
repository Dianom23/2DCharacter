using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnAttackEvent = new UnityEvent();

    [SerializeField] private Transform _attackPositon;
    private Vector2 attackPositon;
    [SerializeField] private float _attackRadius = 0.3f;
    [SerializeField] private float _delayAfterAttack = 0.2f;

    private Player _player;
    private PlayerAnimation _playerAnimation;
    private float _timerDelayAttack;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        _timerDelayAttack += Time.deltaTime;
    }

    public void OnAttack()
    {
        if (_playerAnimation.isAttack || _playerAnimation.isDash || CanAttack() == false) return;

        //print()

        ResetTimerDelayAttack();
        SetAttackPosition2();
        OnAttackEvent?.Invoke();
    }

    private Vector2 SetAttackPosition2()
    {
        Vector2 newAttackPosition;

        if (_player.sr.flipX == true)
            newAttackPosition = new Vector2(transform.position.x - 0.5f, transform.position.y + 0.5f);
        else
            newAttackPosition = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);

        attackPositon = newAttackPosition;
        return newAttackPosition;

    }
    private void SetAttackPosition()
    {
        Vector2 newAttackPosition;

        if (_player.sr.flipX == true)
            newAttackPosition = new Vector3(-Mathf.Abs(_attackPositon.localPosition.x), _attackPositon.localPosition.y);
        else
            newAttackPosition = new Vector3(Mathf.Abs(_attackPositon.localPosition.x), _attackPositon.localPosition.y);

        _attackPositon.localPosition = newAttackPosition;

    }

    private bool CanAttack()
    {
        if(_timerDelayAttack >= _delayAfterAttack)
            return true;
        else
            return false;
    }

    private void ResetTimerDelayAttack()
    {
        _timerDelayAttack = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(_attackPositon.position, _attackRadius);
        Gizmos.DrawWireSphere(attackPositon, _attackRadius);

    }
}
