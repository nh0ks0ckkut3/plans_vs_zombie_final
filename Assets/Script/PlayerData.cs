using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine.Analytics;

public class PlayerData : MonoBehaviour

{

  //public TMP_InputField edtEmail;
  //public GameObject panelOTP, paneSendEmail;
  public static PlayerData instance;
  private void Awake()
  {
    instance = this;
  }
  public void chuaLogin()
  {
    PlayerPrefs.DeleteAll();
  }
  public void setAll(string _id, string email, string username, string ingame, int age, string gender, int level, DetailCard[] listDetailCard)
  {
    PlayerPrefs.SetString("_id", _id);
    PlayerPrefs.SetString("email", email);
    PlayerPrefs.SetString("username", username);
    PlayerPrefs.SetString("ingame", ingame);
    PlayerPrefs.SetInt("age", age);
    PlayerPrefs.SetString("gender", gender);
    PlayerPrefs.SetInt("level", level);
    string json = JsonConvert.SerializeObject(listDetailCard);
    PlayerPrefs.SetString("listDetailCard", json);
    PlayerPrefs.Save();
  }
  public void setLevel()
  {
    int currentLevel = PlayerPrefs.GetInt("level", 1);
    int upLevel = currentLevel + 1;
    PlayerPrefs.SetInt("level", upLevel);
    PlayerPrefs.Save();
    // goi api updatelevel
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
        //tài khoản không đúng

        Debug.Log(resetForgetRepone.status);
      }
      else
      {
        Debug.Log(resetForgetRepone.status);
      }
    }
    request.Dispose();
  }









  public int loadLevel()
  {
    return PlayerPrefs.GetInt("level", 1); // Giá trị mặc định là 1 nếu không có giá trị được lưu trữ trước đó
  }
  public string load_id()
  {
    return PlayerPrefs.GetString("_id", "");
  }
  public string loadEmail()
  {
    return PlayerPrefs.GetString("email", "");
  }
  public string loadUsername()
  {
    return PlayerPrefs.GetString("username", "");
  }
  public string loadIngame()
  {
    return PlayerPrefs.GetString("ingame", "");
  }
  public int loadAge()
  {
    return PlayerPrefs.GetInt("age", 0);
  }
  public string loadGender()
  {
    return PlayerPrefs.GetString("gender", "");
  }
  public void buyNewCard(string cardVip_id)
  {
    var user_id = PlayerPrefs.GetString("_id");
    string detail = "dai gia mua hang";
    Debug.Log(user_id + detail + cardVip_id);
    AddDetailRQ addDetailCardRequest = new AddDetailRQ(detail, cardVip_id, user_id);
    gameObject.AddComponent<PlayerData>();
    API_buyNewCard(addDetailCardRequest);
    StartCoroutine(API_buyNewCard(addDetailCardRequest));
  }

  IEnumerator API_buyNewCard(AddDetailRQ addDetailCardRequest)
  {
    string jsonStringRequest = JsonConvert.SerializeObject(addDetailCardRequest);
    var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/detaiBillCardVips", "POST");
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
      DetailCard[] resetForgetRepone = JsonConvert.DeserializeObject<DetailCard[]>(jsonString);
      if (resetForgetRepone != null)
      {
        //tài khoản không đúng

        PlayerPrefs.SetString("listDetailCard", jsonString);
        PlayerPrefs.Save();
        Debug.Log(resetForgetRepone);
      }
      else
      {

        //SceneManager.LoadScene("");
        //panelOTP.SetActive(true);
        //paneSendEmail.SetActive(false);
      }
    }
    request.Dispose();
    PlayerPrefs.SetInt("isShowShop", 1);
    PlayerPrefs.Save();
    SceneManager.LoadScene("Welcome");
  }






  public DetailCard[] loadListDetailCardVip()
  {
    string json = PlayerPrefs.GetString("listDetailCard", "[]");
    if (!string.IsNullOrEmpty(json))
    {
      return JsonConvert.DeserializeObject<DetailCard[]>(json);
    }
    else
    {
      return new DetailCard[0]; // Trả về mảng rỗng nếu không có dữ liệu
    }
  }

}

