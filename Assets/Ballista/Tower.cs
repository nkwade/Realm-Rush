using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float delay = 1f;

    private void Start() {
        StartCoroutine(BuildTower());
    }

    public bool CreateTower(Tower tower, Vector3 position) {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) {return false;}

        if (bank.CurrBal >= cost) {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    private IEnumerator BuildTower() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child) {
                grandChild.gameObject.SetActive(false);
            }   
        }

        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            foreach(Transform grandChild in child) {
                grandChild.gameObject.SetActive(true);
            }   
        }
    }
}
