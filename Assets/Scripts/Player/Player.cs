using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    [Header("Movement Settings")]
    public float walkingSpeed;
    public float runningSpeed;
    public float friction;
    public float jumpingForce;
    public float turnDuration;

    [Header("Scaling Settings")]
    public Vector3 scaleOnJump;
    public Vector3 scaleOnFall;

    [Header("Ease Settings")]
    public Ease scalingEase;
    
    [Header("Scaling Durations")]
    public float jumpScaleTime;
    public float fallScaleTime;
    public float redoScaleTime;

    private float _currentSpeed;
    
    private Vector3 scaleOnRedo;
    private Vector3 scaleOnJumpLeft;
    private Vector3 scaleOnFallLeft;
    private Vector3 scaleOnRedoLeft;

    private Sequence _jumpSequence;
    private Sequence _jumpLeftSequence;

    private Tween
        _tweenJump,
        _tweenFall,
        _tweenRedo;
    private Tween
        _tweenJumpLeft,
        _tweenFallLeft,
        _tweenRedoLeft;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        HandleMovement();
        HandleJumps();

        if (_rigidBody.velocity.y <= 0)
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void Init()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        
        _currentSpeed = walkingSpeed;

        scaleOnRedo = Vector3.one;
        scaleOnJumpLeft = new Vector3(-scaleOnJump.x, scaleOnJump.y, scaleOnJump.z);
        scaleOnFallLeft = new Vector3(-scaleOnFall.x, scaleOnFall.y, scaleOnFall.z);
        scaleOnRedoLeft = new Vector3(-1, 1, 1);
    }

    private void HandleMovement()
    {
        if (_rigidBody.velocity.x == 0 && _rigidBody.velocity.y != 0)
        {
            //_currentSpeed = 0;
        }
        else
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                _animator.SetBool("Run", false);
                _currentSpeed = walkingSpeed;
            }
            else
            {
                _animator.SetBool("Run", true);
                _currentSpeed = runningSpeed;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.localScale.x < 0)
            {
                if (_rigidBody.velocity.y != 0)
                {
                    DOTween.Kill(_jumpLeftSequence);
                    TweenJump();
                }

                gameObject.transform.DOScaleX((+1), turnDuration);
            }

            _animator.SetBool("Walk", true);
            _rigidBody.velocity = new Vector2((+1) * _currentSpeed, _rigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                if(_rigidBody.velocity.y != 0)
                {
                    DOTween.Kill(_jumpSequence);
                    TweenJumpLeft();
                }

                gameObject.transform.DOScaleX((-1), turnDuration);
            }

            _animator.SetBool("Walk", true);
            _rigidBody.velocity = new Vector2((-1) * _currentSpeed, _rigidBody.velocity.y);
        }
        else
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", false);
        }

        if (_rigidBody.velocity.x != 0 && _rigidBody.velocity.y == 0)
        {
            _rigidBody.velocity -= friction * _rigidBody.velocity.normalized;
        }
    }

    private void HandleJumps()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpingForce);

            DOTween.Kill(_jumpSequence);
            DOTween.Kill(_jumpLeftSequence);
            _animator.SetBool("Jump", true);

            if (gameObject.transform.localScale.x > 0)
                TweenJump();
            else
                TweenJumpLeft();
        }
    }

    private void TweenJump()
    {
        _jumpSequence = DOTween.Sequence();

        _jumpSequence
            .Append(_tweenJump = gameObject.transform
            .DOScale(scaleOnJump, jumpScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpSequence
            .Append(_tweenFall = gameObject.transform
            .DOScale(scaleOnFall, fallScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpSequence
            .Append(_tweenRedo = gameObject.transform
            .DOScale(scaleOnRedo, redoScaleTime))
            .SetEase(scalingEase);

        _jumpSequence.SetDelay(redoScaleTime);
    }

    private void TweenJumpLeft()
    {
        _jumpLeftSequence = DOTween.Sequence();

        _jumpLeftSequence
            .Append(_tweenJump = gameObject.transform
            .DOScale(scaleOnJumpLeft, jumpScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpLeftSequence
            .Append(_tweenFall = gameObject.transform
            .DOScale(scaleOnFallLeft, fallScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpLeftSequence
            .Append(_tweenRedo = gameObject.transform
            .DOScale(scaleOnRedoLeft, redoScaleTime))
            .SetEase(scalingEase);

        _jumpLeftSequence.SetDelay(redoScaleTime);
    }
}
