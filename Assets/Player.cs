using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UnityEvent OnJumpEvent = new UnityEvent();
    public UnityEvent OnDashEvent = new UnityEvent();

    [SerializeField] private float _speed;
    [SerializeField] private float _dashSpeed = 5;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxCountJump;
    [SerializeField] private int _currentCountJump;
    [SerializeField] private bool _isDashing;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private PlayerAnimation _playerAnimation;
    private float _inputMove;
    private float _activeMoveSpeed;
    private bool _isGrounded;
    private bool _isFalling;

    public float speed => _speed;
    public float dashSpeed => _dashSpeed;
    public bool isFalling => _isFalling;
    public float inputMove => _inputMove;
    public SpriteRenderer sr => _sr;

    private void Awake()
    {
        _activeMoveSpeed = _speed;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_inputMove * _activeMoveSpeed, _rb.velocity.y);

        SetSpriteFlip();

        CheckFalling();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = true;
            _currentCountJump = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
            _isGrounded = false;
    }

    public void OnMove(InputValue value)
    {
        _inputMove = value.Get<float>(); 
    }

    public void OnJump()
    {
        if(_isGrounded == false || _currentCountJump >= _maxCountJump || _playerAnimation.isAttack == true) return;

        _currentCountJump++;
        OnJumpEvent?.Invoke();
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void OnDash()
    {
        OnDashEvent?.Invoke();
    }

    public void SetSpeed(float speed)
    {
        _activeMoveSpeed = speed;
    }

    private void SetSpriteFlip()
    {
        if (_rb.velocity.x < 0)
            _sr.flipX = true;
        else if (_rb.velocity.x > 0)
            _sr.flipX = false;
    }

    private void CheckFalling()
    {
        if (_rb.velocity.y < 0)
            _isFalling = true;
        else
            _isFalling = false;
    }
}
