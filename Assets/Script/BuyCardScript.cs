using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class BuyCardScript : MonoBehaviour
{
  public GameObject panel, panelLoading;
  public TMP_InputField edtUser, edtPass;
  public TMP_Text txtError, txtShowName;
  private PlayerData playerData;

  private void Start()
  {
  }
  public void buyNewCard(string cardVip_id)
  {
    var user_id = PlayerPrefs.GetString("_id", "");
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
      }
      else
      {

        //SceneManager.LoadScene("");
        //panelOTP.SetActive(true);
        //paneSendEmail.SetActive(false);
      }
    }
    request.Dispose();
  }

}
