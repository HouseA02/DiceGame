using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFlip : Effect
{
    [SerializeField]
    protected Coin coin;

    public override void Activate(Character source, Character target, float value)
    {
        FlipCoin(coin);
    }

    protected virtual void FlipCoin(Coin coin)
    {
        Coin newCoin = Instantiate(coin);
    }
}
