using UnityEngine;

public class BoxCastExm : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float castDistance = 0.1f;
    private Vector2 _upperEdge;
    private Vector2 _center;

    void Update()
    {
        // �������� ������� ������� �������������
        Vector2 boxSize = GetComponent<Collider2D>().bounds.size;

        // ����������� ������� ������ ������� (������: ����� �������)
        Vector2 boxCenter = new Vector2(transform.position.x, transform.position.y - boxSize.y * 0.5f);
        _center = new Vector2(transform.position.x, transform.position.y - boxSize.y * 0.5f);

        // ����� BoxCast
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.up, castDistance);

        // ���� ���� ������������, �������� ���������� � ����� ������������
        if (hit.collider != null)
        {
            _upperEdge = hit.point + new Vector2(0f, boxSize.y);
            Debug.Log("������� ���� ������� ��������� � �����: " + _upperEdge);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_center, _upperEdge);
    }
}