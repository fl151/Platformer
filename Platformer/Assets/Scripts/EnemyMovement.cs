using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Vector2 _movingDirection;
    private float _rayDistance = 0.7f;

    private void Awake()
    {
        _movingDirection = Vector2.right;

        _animator.SetBool("IsMoving", true);
    }

    private void Update()
    {
        CanMoveThisWay();

        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void CanMoveThisWay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + _movingDirection, _rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
        {
            ChangMovingDirection();
        }
    }

    private void ChangMovingDirection()
    {
        float rightRotation = 0;
        float leftRotation = 180;

        if(_movingDirection == Vector2.left)
        {
            _movingDirection = Vector2.right;
            transform.rotation = new Quaternion(0, rightRotation, 0, 0);
        }
        else if (_movingDirection == Vector2.right)
        {
            _movingDirection = Vector2.left;
            transform.rotation = new Quaternion(0, leftRotation, 0, 0);
        }
    }
}
