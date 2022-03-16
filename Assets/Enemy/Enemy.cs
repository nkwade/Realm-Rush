using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void AddReward() {
        if (bank == null) {return;}
        bank.Deposit(reward);
    }

    public void RemoveReward() {
        if (bank == null) {return;}
        bank.Withdraw(reward);
    }
}
