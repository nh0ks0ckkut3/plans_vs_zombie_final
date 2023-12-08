using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ForgetUserName : MonoBehaviour
{
  // Start is called before the first frame update
  public TMP_InputField edtEmail;
  public TMP_Text txtError;
  public GameObject panelLogin, paneSendEmailUserName, panelLoading;
  public void senEmail()
  {
    var email = edtEmail.text;

    ResetPassRequest resetPassRequest = new ResetPassRequest(email);
    ResetPass(resetPassRequest);
    StartCoroutine(ResetPass(resetPassRequest));
    panelLoading.SetActive(true);
  }

  IEnumerator ResetPass(ResetPassRequest resetPassRequest)
  {
    string jsonStringRequest = JsonConvert.SerializeObject(resetPassRequest);

    var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/forgotUsername", "POST");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
    request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server trả về
    request.SetRequestHeader("Content-Type", "application/json");
    // khi gửi dữ liệu lên server, hàm này sẽ tạm dừng trong khi gửi dữ liệu thành công hoặc có lỗi
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
      txtError.text = "có lỗi xảy ra thử lại sau";
      Debug.Log(txtError.text);
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
        txtError.text = "Gửi email không thành công";
        Debug.Log(resetForgetRepone.status);
      }
      else
      {
        txtError.text = "Gửi email thành công";
        //SceneManager.LoadScene("");
        panelLoading.SetActive(false);
        panelLogin.SetActive(true);
        paneSendEmailUserName.SetActive(false);
        Debug.Log(resetForgetRepone.status);
      }
    }
    request.Dispose();
  }
}
