using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 현재 동작, 기본 동작, 오버라이딩 동작, 잠긴 동작, 마우스 이동값
/// 땅에 서있는지, GenericBehavior를 상속받은 동작들을 업데이트 시켜줌.
/// </summary>
public class BehaviorController : MonoBehaviour
{
    private List<GenericBehavior> behaviors; // 동작들
    private List<GenericBehavior> genericBehaviors; // 우선시 되는 동작
    private int currentBehavior; // 현재 동작 해시코드
    private int defalutBehavior; // 기본 동작 해시코드
    private int behaviorLocked; // 잠긴 동작 해시코드

    // 캐싱.
    public Transform playerCamera;
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private ThirdPersonOrbitCam camSprint;

    // 기본 속성 값들.
    private float h; // horizontal axis
    private float v; // vertical axis
    public float turnSmoothing = 0.06f; // 카메라를 향하도록 움직일 때 회전속도.
    private bool changedFOV; // 달리기 동작이 카메라 시야각이 변경되었을 때 저장됐는지.
    public float sprintFOV = 100; // 달리기 시야각.
    private Vector3 lastDirection; // 마지막 향했던 방향.
    private bool isSprint;
    private int hFloat; // 애니메이터 관련 가로축 값
    private int vFloat; // 애니메이터 관련 세로축 값
    private int groundedBool; // 애니메이터 지상 판별
    private Vector3 colExtents; // 땅과의 출동체크를 위한 충돌체 영역.

    public float GetH { get => h; }
    public float GetV { get => v; }
    public ThirdPersonOrbitCam GetCamScript { get => camSprint; }
    public Rigidbody GetRigidbody { get => myRigidbody; }
    public Animator GetAnimator { get => myAnimator; }
    public int GetDefaultBehavior { get => defalutBehavior; }


}

public abstract class GenericBehavior : MonoBehaviour
{
    protected int speedFloat;
    protected BehaviorController behaviorController;
    protected int behaviorCode;
    protected bool canSprint;

    private void Awake()
    {
        this.behaviorController = GetComponent<BehaviorController>();
        speedFloat = Animator.StringToHash(FC.AnimatorKey.Speed);
        canSprint = true;

        // 동작 타입을 해시코드로 갖고 있다가, 추후에 구별용으로 사용.
        behaviorCode = this.GetType().GetHashCode();
    }

    public int GetBehaviorCode
    {
        get => behaviorCode;
    }
    public bool AllowSprint
    {
        get => canSprint;
    }
    public virtual void LocalLateUpdate()
    {

    }
    public virtual void LocalFixedUpdate()
    {

    }
    public virtual void OnOverride()
    {

    }

}

