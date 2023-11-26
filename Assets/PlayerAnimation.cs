using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _isAttack;
    private bool _isDash;
    private Player _player;
    private PlayerAttack _playerAttack;

    public Animator animator => _animator;
    public bool isAttack => _isAttack;
    public bool isDash => _isDash;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _playerAttack = GetComponent<PlayerAttack>();

        _player.OnJumpEvent.AddListener(SetJump);
        _player.OnDashEvent.AddListener(StartDash);
        _playerAttack.OnAttackEvent.AddListener(StartAttack);
    }

    private void Update()
    {
        SetWalk();

        SetFall(_player.isFalling);
    }

    public void StartDash()
    {
        if (_isDash == true || _player.inputMove == 0 || _isAttack == true) return;

        StartCoroutine(DashRoutine());
    }

    public void StartAttack()
    {
        _isAttack = true;
        _animator.SetInteger("NumberAttack", Random.Range(0, 2));
        //_animator.SetTrigger("Attack");
        _animator.SetBool("IsAttack", true);
    }

    public void SetWalk()
    {
        if(_player.inputMove != 0)
            _animator.SetBool("Walk", true);
        else
            _animator.SetBool("Walk", false);
    }

    public void EndAttack()
    {
        _isAttack = false;
        _animator.SetBool("IsAttack", false);
    }

    public void EndDash()
    {
        _isDash = false;
    }

    public void SetFall(bool isFall)
    {
        _animator.SetBool("Fall", isFall);
    }

    public void EndFall()
    {
        _animator.SetBool("Fall", false);
    }

    private IEnumerator DashRoutine()
    {
        _isDash = true;
        _animator.SetTrigger("Dash");
        _player.SetSpeed(_player.dashSpeed);
        yield return new WaitWhile(() => _isDash == true);
        _player.SetSpeed(_player.speed);
    }

    private void SetJump()
    {
        _animator.SetTrigger("Jump");
    }
}
