using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] private Animator _animator;

    private bool _isGround;
    private bool _isMoving;

    private float _rayDistance = 0.6f;
    private float _rightDirection = 0;
    private float _leftDirection = 180;

    private bool _canJump;

    private void Awake()
    {
        _canJump = true;
        _rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        _isMoving = false;

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) == false)
        {
            Moving(_rightDirection);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false)
        {
            Moving(_leftDirection);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            CheckGround();

            if (_isGround && _canJump)
            {
                _rigidBody.AddForce(Vector2.up * _jumpForse, ForceMode2D.Impulse);

                StartCoroutine(WaitTimeBeforeNextJump());
            }
        }

        _animator.SetBool("IsMoving", _isMoving);
    }

    private void Moving(float direction)
    {
        transform.rotation = new Quaternion(0, direction, 0, 0);

        transform.Translate(_speed * Time.deltaTime, 0, 0);

        _isMoving = true;
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(_rigidBody.position, Vector2.down, _rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
    }

    private IEnumerator WaitTimeBeforeNextJump()
    {
        float timeBeforeNextJump = 0.5f;

        _canJump = false;

        yield return new WaitForSeconds(timeBeforeNextJump);

        _canJump = true;
    }
}
