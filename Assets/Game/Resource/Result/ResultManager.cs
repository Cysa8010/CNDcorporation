using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResultData(int coin, int minu, int sec, int msec)
	{
        this.coin.SetCoin(coin);
        this.clearTime.SetTime(minu, sec, msec);
	}

    [SerializeField] private S_CoinScore coin = null;
    [SerializeField] private S_TimeScore clearTime = null;
}
