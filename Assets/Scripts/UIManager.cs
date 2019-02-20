using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public CanvasRenderer UICanvasRenderer;
    public Image healthBar;
    public Image shieldBar;
    public Image bossBar;
    public Text bossText;
    public Text scoreText;
    public Text gameOverText;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getHealth() > 0)
        {
            healthBar.transform.localScale = new Vector3(gameManager.getHealth() * 0.2f, 0.25f, 1f);
            shieldBar.transform.localScale = new Vector3(gameManager.getShield() * 0.33f, 0.25f, 1f);
            scoreText.text = "Asteroids Destroyed: " + gameManager.getScore();
        }
        else
        {
            healthBar.transform.localScale = new Vector3(0f, 0.25f, 1f);
            shieldBar.transform.localScale = new Vector3(0f, 0.25f, 1f);
            scoreText.text = "Asteroids Destroyed: " + gameManager.getScore();
            gameOverText.text = "Game Over\nYou destroyed " + gameManager.getScore() + " asteroids!";
            gameOverText.gameObject.SetActive(true);
        }

        if (gameManager.getScore() >= gameManager.maxScore)
        {
            if (gameManager.getBossHealth() > 0)
            {
                bossBar.gameObject.SetActive(true);
                bossText.gameObject.SetActive(true);
                bossBar.transform.localScale = new Vector3(((float)gameManager.getBossHealth() / (float)gameManager.bossMaxHealth), 0.25f, 1f);
            }
            else
            {
                bossBar.gameObject.SetActive(false);
                bossText.gameObject.SetActive(false);
                gameOverText.text = "Congratulations!\nYou destroyed the alien ship\nand " + gameManager.getScore() + " asteroids!";
                gameOverText.gameObject.SetActive(true);
            }
        }
    }
}
