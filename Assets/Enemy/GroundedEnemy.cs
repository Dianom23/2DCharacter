using UnityEngine;

public class GroundedEnemy : Enemy, IDamagable
{
    private enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private float _speed;
    [SerializeField] private float _moveDirectionX;
    [SerializeField] private LayerMask _groundLayer;
    private Player _player;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private float _jumpTimer;
    private float _jumpDelay = 1f;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void Update()
    {
        _jumpTimer += Time.deltaTime;

        SetSpriteDirection(CalculateXDirection());
        Move();
        Jump(CheckObstacles(CalculateXDirection()));
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }

    protected override void Attack()
    {
        
    }

    protected override void Move()
    {
        Vector2 directionMove = Vector2.left;

        if (CalculateXDirection() == Direction.Left)
            directionMove = Vector2.left;
        if (CalculateXDirection() == Direction.Right)
            directionMove = Vector2.right;

        transform.Translate(directionMove * Time.deltaTime);
    }


    private Direction CalculateXDirection()
    {
        _moveDirectionX = Mathf.Clamp(_player.transform.position.x - transform.position.x, -1, 1);
        _moveDirectionX = Mathf.FloorToInt(_moveDirectionX);
        if( _moveDirectionX < 0 )
            return Direction.Left;
        else return Direction.Right;
    }

    private void SetSpriteDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.Left:
                _sr.flipX = false;
                break;
            case Direction.Right:
                _sr.flipX = true;
                break;
        }
    }

    private bool CheckObstacles(Direction direction)
    {
        Vector2 directionMove = Vector2.left;

        if (CalculateXDirection() == Direction.Left)
            directionMove = Vector2.left;
        if (CalculateXDirection() == Direction.Right)
            directionMove = Vector2.right;

        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), directionMove);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 0.5f, _groundLayer);
        if(hits.Length == 0 )
            return false;
        else
            return true;
    }

    private void Jump(bool isJump)
    {
        if (_jumpTimer >= _jumpDelay && isJump)
        {
            _jumpTimer = 0;
            _rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
    }
}
