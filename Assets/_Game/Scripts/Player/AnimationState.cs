using UnityEngine;

public class AnimationState : MonoBehaviour
{
    [SerializeField] private PlayerStateController stateController;

    public void OnAnimationFinishPlaying()
    {
        stateController.isAnimationFinishPlaying = true;
    }
}
