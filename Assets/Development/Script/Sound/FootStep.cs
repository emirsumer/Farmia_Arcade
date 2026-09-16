using UnityEngine;

public class FootStep : MonoBehaviour
{
    public void StepRightFootSound()
    {
        AudioManager.Instance.PlayRightStepSfx();
    }

    public void StepLeftFootSound()
    {
        AudioManager.Instance.PlayLeftStepSfx();
    }
}
