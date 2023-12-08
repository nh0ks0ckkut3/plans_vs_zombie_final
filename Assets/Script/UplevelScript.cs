using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class UplevelScript : MonoBehaviour
{
  private game_manager gameManager;
  private void Start()
  {
    gameManager = game_manager.instance;
    Debug.Log(gameManager);
  }
  private void Update()
  {
    Debug.Log(gameManager.countToWin);
    if(gameManager.countToWin == 0)
    {
      setLevel();
    }
  }
  public void setLevel()
  {
    int currentLevel = PlayerPrefs.GetInt("level", 1);
    int upLevel = currentLevel + 1;
    PlayerPrefs.SetInt("level", upLevel);
    PlayerPrefs.Save();
    // goi api updatelevel
    Debug.Log(PlayerPrefs.GetInt("level", 9));
    ResetPassRequest resetPassRequest = new ResetPassRequest(upLevel);
    API_upLevel(resetPassRequest);
    StartCoroutine(API_upLevel(resetPassRequest));
  }



  IEnumerator API_upLevel(ResetPassRequest resetPassRequest)
  {
    string jsonStringRequest = JsonConvert.SerializeObject(resetPassRequest);

    var id = PlayerPrefs.GetString("_id");
    var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/updateProfile/" + id, "PUT");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
    request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server trả về
    request.SetRequestHeader("Content-Type", "application/json");
    // khi gửi dữ liệu lên server, hàm này sẽ tạm dừng trong khi gửi dữ liệu thành công hoặc có lỗi
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
    }
    else
    {
      // trả về dữ liệu phản hồi của server
      var jsonString = request.downloadHandler.text.ToString();
      // chuyển đổi kiểu Json thành một đối tượng C#
      ResetForgetRepone resetForgetRepone = JsonConvert.DeserializeObject<ResetForgetRepone>(jsonString);
      if (resetForgetRepone.status != true)
      {


        Debug.Log(resetForgetRepone.status);
      }
      else
      {
        Debug.Log(resetForgetRepone.status);
      }
    }
    request.Dispose();
  }

}
