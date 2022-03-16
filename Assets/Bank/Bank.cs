using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBal = 150;
    
    int currBal;
    public int CurrBal {get { return currBal;} }

    private void Awake() {
        currBal = startBal;
    }

    public void Deposit(int amount) {
        currBal += Mathf.Abs(amount);
    }

    public void Withdraw(int amount) {
        currBal -= Mathf.Abs(amount);

        if (currBal < 0) {
            //Lose game

        }
    }

    private void ReloadScene() {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.buildIndex);
    }
}
