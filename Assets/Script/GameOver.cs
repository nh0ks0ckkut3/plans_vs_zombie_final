using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
  public GameObject pnGameOver;
  public AudioSource gameOverSound;
  public TMP_Text btnHome, btnReset;
  private bool pause = false;
  // Start is called before the first frame update
  void Start()
  {
    Time.timeScale = 1;
  }
  private void Update()
  {
    if(pause)
    {
      pause = false;
      Invoke("showBtn", 2f);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("zombie"))
    {
      // Ví dụ: dừng time scale để tạm dừng trò chơi
      pause = true;
      pnGameOver.SetActive(true);
      gameOverSound.Play();

      Time.timeScale = 0;

    }
  }

  public void Back()
    {
    Time.timeScale = 1;
    PlayerPrefs.SetInt("isshowSelectLV", 1);
    PlayerPrefs.Save();
    
    SceneManager.LoadScene("Welcome");
  }
  public void ResetGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  public void showBtn()
  {
    btnHome.transform.parent.transform.gameObject.SetActive(true);
    btnReset.transform.parent.transform.gameObject.SetActive(true);
  }
}
