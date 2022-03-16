using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBal = 150;
    [SerializeField] TextMeshProUGUI displayBalance;

    int currBal;
    public int CurrBal {get { return currBal;} }

    private void Awake() {
        currBal = startBal;
        UpdateDisplay();
    }

    public void Deposit(int amount) {
        currBal += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount) {
        currBal -= Mathf.Abs(amount);

        if (currBal < 0) {
            //Lose game

        }
        UpdateDisplay();
    }

    private void ReloadScene() {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex);
    }

    void UpdateDisplay() {
        displayBalance.text = "Gold: " + currBal;
    }
}
