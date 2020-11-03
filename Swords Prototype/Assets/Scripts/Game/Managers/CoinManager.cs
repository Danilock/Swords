using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public delegate void OnCoinAddedAction();

    public static event OnCoinAddedAction onCoinAdded;
    private int coinAmount;

    public int CoinAmount
    {
        get => coinAmount;
        private set => coinAmount = value;
    }

    public void AddCoins(int amount)
    {
        coinAmount += amount;
        
        if(onCoinAdded != null)
            onCoinAdded();
    }
}
