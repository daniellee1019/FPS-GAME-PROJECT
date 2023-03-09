using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using FC;
/// <summary>
/// 다른 Behaviour 보다 상위에 있다. -> 조준이 먼저.
/// 마우스 오른쪽 버튼으로 조준 및 휠버튼으로 좌우 카메라 변경
/// 벽의 모서리에서 조준할 때 상체를 살짝 기울여주는 기능.
/// </summary>
public class AimBehaviour : GenericBehaviour
{
    public Texture2D crossHair; // 십자선 이미지.
    public float aimTurnSmoothing = 0.15f; // 카메라를 향하도록 조준할 때 회전속도.
    public Vector3 aimPivotOffSet = new Vector3(0.5f, 1.2f, 0.0f);
    public Vector3 aimCamOffSet = new Vector3(0.0f, 0.4f, -0.7f);

    private int aimBool; // 애니메이터 파라미터. 조준.
    private bool aim; // 조준중?
    private int conerBool; // 애니메이터 관련. 코너. -> 조준
    private bool peekConer; // 플레이어가 코너 모서리에 있는지 여부.
    private Vector3 initialRootRotation; // 루트 본으로부터 로컬까지 회전값
    private Vector3 initialHipRotation;
    private Vector3 initialSpineRotation;

    private void Start()
    {
        //setup
        aimBool = Animator.StringToHash(AnimatorKey.Aim);
        conerBool = Animator.StringToHash(AnimatorKey.Corner);

        //value
        Transform hips = behaviorController.GetAnimator.GetBoneTransform(HumanBodyBones.Hips);
        initialRootRotation = (hips.parent == transform) ? Vector3.zero : hips.parent.localEulerAngles;
        initialHipRotation = hips.localEulerAngles;
        initialSpineRotation = behaviorController.GetAnimator.
            GetBoneTransform(HumanBodyBones.Spine).localEulerAngles;

    }
}
