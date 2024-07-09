using UnityEngine;
using System;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float smoothMoveAnimation = 0.1f;
    private float currentSpeed;
    private float targetSpeed;
    private float speedSmoothVelocity;

    private void OnEnable()
    {
        MovementController.onSpeedChange += UpdateSpeed;
        AttackController.OnAttack += AttackTrigger;
    }

    private void OnDisable()
    {
        MovementController.onSpeedChange -= UpdateSpeed;
        AttackController.OnAttack -= AttackTrigger;
    }

    private void UpdateSpeed(float speed) { targetSpeed = speed; }

    private void Update()
    {
        CalculateSpeedAnimation();
    }

    private void CalculateSpeedAnimation()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, smoothMoveAnimation);
        _anim.SetFloat("Speed", currentSpeed);
    }

    private void AttackTrigger(object sender, EventArgs e)
    {
        _anim.SetTrigger("Attack");
    }
}
