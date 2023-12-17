using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected abstract void Move();
    protected abstract void Attack();
}