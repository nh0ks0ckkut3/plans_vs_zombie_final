using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendOTPScripts : MonoBehaviour
{
    public GameObject panelOTP, panelChangePass, panelLoading;
    public TMP_InputField edtUser;
    public TMP_Text txtError;

    public void layOTP()
    {
        var email = edtUser.text;
        SendOTPRequest sendOTPRequest = new SendOTPRequest(email);
        SendOTP(sendOTPRequest);
        StartCoroutine(SendOTP(sendOTPRequest)); // cấp quyền cho CheckLogin
        panelLoading.SetActive(true);
  }

    IEnumerator SendOTP(SendOTPRequest sendOTPRequest)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(sendOTPRequest);

        var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/forgot-password", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server trả về
        request.SetRequestHeader("Content-Type", "application/json");
        // khi gửi dữ liệu lên server, hàm này sẽ tạm dừng trong khi gửi dữ liệu thành công hoặc có lỗi
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
          txtError.text = "Có lỗi xảy ra! Thử lại sau!";
      Invoke("hiddenError", 3f);
      panelLoading.SetActive(false);
    }
        else
        {
            // trả về dữ liệu phản hồi của server
            var jsonString = request.downloadHandler.text.ToString();
            // chuyển đổi kiểu Json thành một đối tượng C#
            SendOTPReponse sendOTPReponse = JsonConvert.DeserializeObject<SendOTPReponse>(jsonString);
            if (sendOTPReponse.message.Substring(0,23) != "Gửi email thành công đến")
            {
                //tài khoản không đúng
                txtError.text = sendOTPReponse.message;
        panelLoading.SetActive(false);
        Invoke("hiddenError", 3f);
            }
            else
            {
                txtError.text = sendOTPReponse.message;
        Invoke("hiddenError", 3f);
        panelLoading.SetActive(false);
        panelOTP.SetActive(false);
                panelChangePass.SetActive(true);
                //SceneManager.LoadScene("Test");
            }
        }
        request.Dispose();
    }
  public void hiddenError()
  {
    txtError.text = "";
  }
}
