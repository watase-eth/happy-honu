using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to refer to score Text variable
using UnityEngine.SceneManagement; //to refer to the restart button

public class logicScript : MonoBehaviour
{
  public int playerScore;
  public Text scoreText;
  public GameObject gameOverScreen;
  public AudioSource cowbellSFX;

  [ContextMenu("Increase Score")]
  public void addScore(int scoreToAdd)
  {
    playerScore = playerScore + scoreToAdd;
    scoreText.text = playerScore.ToString();   
    cowbellSFX.Play();
  }

  public void restartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void gameOver()
  {
    gameOverScreen.SetActive(true);
  }

}
