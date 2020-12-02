using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] RawImage imageTargetForParallaxEffect;
    [SerializeField] Vector2 parallaxSpeed;
    Rect actualUVrect;

    // Update is called once per frame
    void Update()
    {
        actualUVrect = imageTargetForParallaxEffect.uvRect;
        actualUVrect.x += parallaxSpeed.x * Time.deltaTime;
        actualUVrect.y += parallaxSpeed.y * Time.deltaTime;

        imageTargetForParallaxEffect.uvRect = actualUVrect;
    }
}
