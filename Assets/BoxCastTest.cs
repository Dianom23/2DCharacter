using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoxCastTest : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10)] private float _width = 1;
    [SerializeField, Range(0.1f, 10)] private float _height = 1;
    [SerializeField, Range(-10, 10)] private float _directionX = 0;
    [SerializeField, Range(-10, 10)] private float _directionY = 0;
    private RaycastHit2D[] _hits;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _hits = Physics2D.BoxCastAll(transform.position, new Vector2(_width, _height), 0, new Vector2(_directionX, _directionY));
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height, 1));

        if (_hits.Length > 0)
        {
            Gizmos.color = Color.green;
            foreach (var hit in _hits)
                Gizmos.DrawSphere(hit.point, 0.05f);

            foreach (var hit in _hits)
                Gizmos.DrawSphere(hit.normal, 0.05f);
            foreach (var hit in _hits)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(hit.collider.bounds.max, 0.05f);

                Gizmos.color = Color.gray;
                Gizmos.DrawSphere(hit.collider.bounds.min, 0.05f);

            }

        }
    }
}
