using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgamePanel : UIPanel {

    public GameObject winShowing, looseShowing;
    public Text unlockMsg, coinsEarned, coins;
    public Button nextLevelBtn;
    public GameObject defaultWinBtn, defaultLooseBtn;
    bool won;
    public void SetLayout(bool won)
    {
        this.won = won;
        winShowing.SetActive(won);
        looseShowing.SetActive(!won);

        defaultSelectedObject = won ? defaultWinBtn : defaultLooseBtn;
    }

    public override void Show()
    {
        base.Show();
        int moneyEarned = GameManager.Instance.money;
        coins.text = PlayerProfile.Instance.Money + " coins";
        if (moneyEarned > 0)
        {
            PlayerProfile.Instance.Money += moneyEarned;
            coinsEarned.text = moneyEarned + " coins earned";
            

        }
        else
        {
            coinsEarned.text = "No coins earned";
        }
        if (won)
        {
            if (PlayerProfile.CurrentLevel < GameManager.Instance.LevelCount - 1)
            {
                unlockMsg.text = "Level " + PlayerProfile.CurrentLevel + 1 + " unlocked !";
                nextLevelBtn.interactable = true;
            }
        }
        

    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        PlayerProfile.CurrentLevel++;
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {

    }


}
