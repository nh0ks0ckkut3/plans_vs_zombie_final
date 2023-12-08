using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ResetPassWordOTP : MonoBehaviour
{
  public GameObject panelLogin, paneOTP;
  public TMP_InputField edtOTP, edtPassNew, edtRetypeNewPass;
  public TMP_Text txtError;
  public GameObject pannelLoading;

  public void CheckOTP_DoiMatKhau()
  {
    var otp = edtOTP.text;
    var passNew = edtPassNew.text;
    var newPassRepeat = edtRetypeNewPass.text;
    if (otp.Length == 0 || passNew.Length == 0 || newPassRepeat.Length == 0) txtError.text = "không bỏ trống";
    else if (!newPassRepeat.Equals(passNew)) txtError.text = "xác nhận mật khẩu không đúng";
    else
    {
      pannelLoading.SetActive(true);
      ResetPassRequest resetPassRequest = new ResetPassRequest(otp, passNew);
      ResetPass(resetPassRequest);
      StartCoroutine(ResetPass(resetPassRequest));
    }
    // cấp quyền cho CheckLogin
  }

  IEnumerator ResetPass(ResetPassRequest resetPassRequest)
  {
    string jsonStringRequest = JsonConvert.SerializeObject(resetPassRequest);

    var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/checkOTP_PassWord", "POST");
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
      ResetForgetRepone resetForgetReponse = JsonConvert.DeserializeObject<ResetForgetRepone>(jsonString);
      if (resetForgetReponse.status != true)
      {
        //tài khoản không đúng
        txtError.text = "OTP không trùng khớp";
        Debug.Log(resetForgetReponse.status);
      }
      else
      {
        pannelLoading.SetActive(false);
        txtError.text = "Đổi mật khẩu thành công";
        //SceneManager.LoadScene("Test");
        panelLogin.SetActive(true);
        paneOTP.SetActive(false);
        Debug.Log(resetForgetReponse.status);
      }
    }
    request.Dispose();
  }
}
