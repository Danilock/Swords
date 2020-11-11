using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
namespace Game.UI
{
    public class InteractiveObjectFill : MonoBehaviour
    {
        private Image fillImage;
        private InteractableObject parent;

        private void Awake()
        {
            parent = GetComponentInParent<InteractableObject>();
            fillImage = GetComponentInChildren<Image>();
        }

        void UpdateFill360Amount()
        {
            fillImage.fillAmount = parent.Interacting;
        }

        public void SetFillAmount(float amount)
        {
            fillImage.fillAmount = amount;
        }

        private void OnEnable()
        {
            parent.OnInteracting += UpdateFill360Amount;
            parent.OnStopInteraction += UpdateFill360Amount;
        }

        private void OnDisable()
        {
            parent.OnInteracting -= UpdateFill360Amount;
            parent.OnStopInteraction -= UpdateFill360Amount;
        }
    }
}