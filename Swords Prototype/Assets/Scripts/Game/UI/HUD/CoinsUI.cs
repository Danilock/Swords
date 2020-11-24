using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private CoinManager coinManager;

    private void Start()
    {
        if(coinsText == null)
            coinsText = GetComponentInChildren<TextMeshProUGUI>();
        coinManager = FindObjectOfType<CoinManager>();
    }

    private void OnEnable()
    {
        CoinManager.onCoinAdded += UpdateCoinsText;
    }

    private void OnDisable()
    {
        CoinManager.onCoinAdded -= UpdateCoinsText;
    }

    void UpdateCoinsText() => coinsText.text = coinManager.CoinAmount.ToString();
}
