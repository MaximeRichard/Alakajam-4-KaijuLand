using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableRock : MonoBehaviour {

    public EndTemplateManager mgr;
    public int ptsToBreak = 30;
    public int currencyToEarn = 50; 

    private void Start()
    {
        mgr.RegisterRock(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager playerMgr = collision.transform.GetComponent<PlayerManager>();
            if (playerMgr.isDashing)
            {
                if (playerMgr.CurrentPower >= ptsToBreak)
                {
                    playerMgr.AddPower(-ptsToBreak);
                    GameManager.Instance.EarnCurrency(currencyToEarn);
                    mgr.OnRockBroken(this);

                }
                else
                {
                    GameManager.Instance.OnFail();
                }
            }
            
        }
        
    }
}
