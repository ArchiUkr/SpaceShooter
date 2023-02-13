using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text score;

    [SerializeField] Image livesImage;

    [SerializeField]
    private Sprite[] livesSprite;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text restartText;

    GameManager gameManager;

    private void Start()
    {        
        score.text = "Score: " + 0;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerScore)
    {
        score.text = "Score :" + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprite[currentLives];

        if(currentLives == 0)
        {
            GameOverSequance();
        }
    }

    void GameOverSequance()
    {
        gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

  
}
