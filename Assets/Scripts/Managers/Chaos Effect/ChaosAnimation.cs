using UnityEngine;
using TMPro;
using UnityEngine.Timeline;

public class ChaosAnimation : MonoBehaviour
{
    public static ChaosAnimation Instance;

    [Header("References")]
    public Animator animator;
    public TMP_Text chaosEffectText;

    private void Awake()
    {
        Instance = this;
    }

    // get chaos effect name for Choose Effect script
    // this way means I don't have to create a class wide string in this script
    public void StartAnimation(string chaosEffectName)
    {
        animator.SetTrigger("Start");

        chaosEffectText.SetText(chaosEffectName);
    }
}
