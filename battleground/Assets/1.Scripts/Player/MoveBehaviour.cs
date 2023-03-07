using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 이동과 점프 동작을 담당하는 컴포넌트
/// 충돌처리에 대한 기능도 포함
/// 기본 동작으로써 작동.
/// </summary>
public class MoveBehaviour : GenericBehaviour
{
    public float walkSpeed = 0.15f;
    public float runSpeed = 1.0f;
    public float spintSpeed = 2.0f;
    public float speedDampTime = 0.1f;

    public float jumpHeight = 1.5f;
    public float jumpInertialForce = 10f; //점프 관성
    public float speed, speedSeeker;

    private int jumpBool;
    private int groundedBool;
    private bool isColliding;
    private CapsuleCollider capsuleCollider;
    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;
        capsuleCollider = GetComponent<CapsuleCollider>();
        jumpBool = Animator.StringToHash(FC.AnimatorKey.Jump);
        groundedBool = Animator.StringToHash(FC.AnimatorKey.Grounded);
        behaviorController.GetAnimator.SetBool(groundedBool, true);

        //
        behaviorController.SubScribeBehavior(this);
        behaviorController.RegisterDefaultBehaviour(this.behaviorCode);
        speedSeeker = runSpeed;
    }
    Vector3 Rotating(float horizontal, float vertical)
    {
        Vector3 forward = behaviorController.playerCamera.TransformDirection(Vector3.forward);

        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

    }

}
