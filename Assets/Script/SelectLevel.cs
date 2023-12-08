using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public int targetSceneName;
  public TMP_Text txtError;
  public void SwitchScene()
    {
    if (PlayerPrefs.GetInt("level", 0) == 0)
    {
      txtError.text = "vui lòng đăng nhập";
      Invoke("hiddenError", 3f);
    }else if (PlayerPrefs.GetInt("level", 0) < targetSceneName)
    {
      txtError.text = "bạn chưa mở khóa màn này";
      Invoke("hiddenError", 3f);
    }
    else
    {
      loadScene(targetSceneName);
    }   
    }
  public void playGameMaxLv()
  {
    if (PlayerPrefs.GetInt("level", 0) == 0)
    {
      txtError.text = "vui lòng đăng nhập";
      Invoke("hiddenError", 3f);
    }
    else
    {
      loadScene(PlayerPrefs.GetInt("level", 0));
    }
  }
  public void hiddenError()
  {
    txtError.text = "";
  }
  public void loadScene(int target)
  {
    switch (target)
    {
      case 3:
        SceneManager.LoadScene("level_day_roof");
        break;
      case 2:
        SceneManager.LoadScene("level_night_roof");
        break;
      case 4:
        SceneManager.LoadScene("level_ice");
        break;
      case 1:
        SceneManager.LoadScene("level_night_pool2");
        break;
      default:

        break;
    }
  }
  public void loadPaymentScene()
  {
    SceneManager.LoadScene("Payment");
  }
}
