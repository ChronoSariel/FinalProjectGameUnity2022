using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDtext : MonoBehaviour
{
    public TextMeshProUGUI CoinsHud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       CoinsHud.text = "Coins: " + FindFirstObjectByType<ScoreManager>().score.ToString();
    }
}
