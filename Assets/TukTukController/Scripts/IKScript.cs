using UnityEngine;
public class IKScript : MonoBehaviour
{
    public Transform leftPoint, rightPoint, Lfoot, RFoot;
    public Animator animator;
    public bool ikActive = false;

    private void Start()
    {
    }

    void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightPoint.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightPoint.rotation);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftPoint.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftPoint.rotation);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, Lfoot.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Lfoot.rotation);

                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, RFoot.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, RFoot.rotation);

            }
        }
    }
}
