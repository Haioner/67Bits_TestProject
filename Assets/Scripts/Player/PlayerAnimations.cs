using UnityEngine;
using System;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float smoothMoveAnimation = 0.1f;

    private float _speedSmoothVelocity;
    private float _currentSpeed;
    private float _targetSpeed;

    private void OnEnable()
    {
        PlayerMovement.onSpeedChange += UpdateSpeed;
        PlayerAttack.OnPlayerAttack += AttackTrigger;
    }

    private void OnDisable()
    {
        PlayerMovement.onSpeedChange -= UpdateSpeed;
        PlayerAttack.OnPlayerAttack -= AttackTrigger;
    }

    private void UpdateSpeed(float speed) { _targetSpeed = speed; }

    private void Update()
    {
        CalculateSpeedAnimation();
    }

    private void CalculateSpeedAnimation()
    {
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, _targetSpeed, ref _speedSmoothVelocity, smoothMoveAnimation);
        _anim.SetFloat("Speed", _currentSpeed);
    }

    private void AttackTrigger(object sender, EventArgs e)
    {
        _anim.SetTrigger("Attack");
    }
}
