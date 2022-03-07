using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject startGameUI;
    [SerializeField] private GameObject score;

    private void Start() 
    {
        startGameUI.SetActive(true);
    }
    public void GameEnded()
    {
        endGameUI.SetActive(true);
        player.SetActive(false);
        stairs.SetActive(false);
        enemies.SetActive(false);
        score.SetActive(false);
        endGameUI.transform.Find("Result").GetComponent<TMP_Text>().text = "Your score was " + player.GetComponent<PlayerController>().score;
    }
    
    public void StartGame()
    {
        startGameUI.SetActive(false);
        endGameUI.SetActive(false);
        player.SetActive(true);
        stairs.SetActive(true);
        stairs.transform.position = new Vector3(-5,-5,0);
        for (int i = 0 ; i < stairs.transform.childCount; i++)
        {
            print(i);
            stairs.transform.GetChild(i).position = new Vector3(i-5,i-5,0);
        }
        enemies.SetActive(true);
        score.SetActive(true);
        player.GetComponent<PlayerController>().score = 0;
        score.GetComponent<TMP_Text>().text = "Score: 0";

        for (int i = enemies.transform.childCount - 1; i>=0; i--)
        {
            Destroy(enemies.transform.GetChild(i).gameObject);
        }
    }
}
