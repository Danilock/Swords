using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeImage : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    [SerializeField] CanvasGroup canvasToFade;
    public enum Target { image, canvasGroup, both }
    public Target target = Target.image;
    public enum FadeMode { Show, Hide }
    public FadeMode fadeMode; 
    enum FadeSpeed { Low = 1, Normal = 2, Fast = 3}
    [SerializeField] FadeSpeed fadeSpeed = FadeSpeed.Normal;
    [SerializeField] UnityEvent onFadeShow, onFadeHide;
    float fadeOperation;
    bool fade;

    Color currentImageColor;

    private void Start()
    {
        if(imageToFade == null && (target == Target.image || target == Target.both))
        {
            Debug.LogError("Select an Image To FADE");
        }
        currentImageColor = imageToFade.color;
    }

    private void Update()
    {
        if (fade)
        {
            fadeOperation = Mathf.Abs((int)fadeSpeed * Time.unscaledDeltaTime) * (fadeMode == FadeMode.Show ? 1 : -1);

            if (target == Target.image)
            {
                currentImageColor.a += fadeOperation;
                imageToFade.color = currentImageColor;
            }
            else if(target == Target.canvasGroup)
            {
                canvasToFade.alpha += fadeOperation;
            }
            else if(target == Target.both)
            {

                currentImageColor.a += fadeOperation;
                imageToFade.color = currentImageColor;
                canvasToFade.alpha += fadeOperation;
            }



            if (imageToFade.color.a > 1f || canvasToFade.alpha > 1f)
            {
                fade = false;
                onFadeShow.Invoke();
            }
            else if (imageToFade.color.a < 0f || canvasToFade.alpha < 0f)
            {
                fade = false;
                onFadeHide.Invoke();
            }
        }
    }

    public void Fade(FadeMode fadeModeOption)
    {
        fadeMode = fadeModeOption;
        fade = true; 
    }

    public void Fade() => fade = true;
}
