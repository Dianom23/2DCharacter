using UnityEngine;

public class BoxCastExm : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float castDistance = 0.1f;
    private Vector2 _upperEdge;
    private Vector2 _center;

    void Update()
    {
        // Получаем размеры коробки автоматически
        Vector2 boxSize = GetComponent<Collider2D>().bounds.size;

        // Определение позиции центра коробки (пример: центр объекта)
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - boxSize.y * 0.5f);
        _center = new Vector2(transform.position.x, transform.position.y - boxSize.y * 0.5f);

        // Вызов BoxCast
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.up, castDistance);

        // Если есть столкновение, получите информацию о точке столкновения
        if (hit.collider != null)
        {
            _upperEdge = hit.point + new Vector2(0f, boxSize.y);
            Debug.Log("Верхний край объекта находится в точке: " + _upperEdge);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_center, _upperEdge);
    }
}