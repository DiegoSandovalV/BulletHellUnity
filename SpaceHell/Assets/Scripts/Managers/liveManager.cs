using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class LiveManager : MonoBehaviour
{

    public Image healthBar;

    public Image gameOver;

    public TextMeshProUGUI gameOverText;

    public Image win;

    public TextMeshProUGUI winText;

    public int lives = 50;

    private float width = 280;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.enabled = false;
        gameOverText.enabled = false;
        win.enabled = false;
        winText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        lives -= 1;
        width -= 5.6f;
        healthBar.rectTransform.sizeDelta = new Vector2(width, 30);

        if (lives <= 0)
        {
            isDead = true;
            showGameOver();
        }

    }

    public void showGameOver()
    {
        Debug.Log("Game Over");
        gameOver.enabled = true;
        gameOverText.enabled = true;
    }

    public void showWin()
    {
        Debug.Log("You Win");
        win.enabled = true;
        winText.enabled = true;
    }
}
