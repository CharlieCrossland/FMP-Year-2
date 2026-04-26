using TMPro;
using UnityEngine;

public class ChaosEffectText : MonoBehaviour
{
    public TMP_Text text;

    public void Update()
    {
        text.SetText(ChooseEffect.Instance.chaosEffectName.ToString());
    }
}
