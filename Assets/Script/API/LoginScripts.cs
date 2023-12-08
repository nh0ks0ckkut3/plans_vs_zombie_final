using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LoginScripts : MonoBehaviour
{
    public GameObject panel, panelLoading;
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError, txtShowName;
    private UserData userData;
    private PlayerData playerData;

    private void Start()
    {
        userData = new UserData();
        playerData = new PlayerData();
    }
    public void kiemTraDangNhap()
    {
        var user = edtUser.text;
        var pass = edtPass.text;

        LoginRequest loginRequest = new LoginRequest(user, pass);
        CheckLogin(loginRequest);
        StartCoroutine(CheckLogin(loginRequest)); // cấp quyền cho CheckLogin
        panelLoading.SetActive(true);
    }

    IEnumerator CheckLogin(LoginRequest loginRequest)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(loginRequest);

        var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
        request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server trả về
        request.SetRequestHeader("Content-Type", "application/json");
        // khi gửi dữ liệu lên server, hàm này sẽ tạm dừng trong khi gửi dữ liệu thành công hoặc có lỗi
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            // nếu result của yêu cầu không phải là success =>>>> lỗi
            Debug.Log(request.error);
            txtError.text = "sai tên đăng nhập hoặc mật khẩu";
            Invoke("hiddenError", 3f);
      panelLoading.SetActive(false);
          }
        else
        {
            // trả về dữ liệu phản hồi của server
            var jsonString = request.downloadHandler.text.ToString();
      //chuyển đổi kiểu Json thành một đối tượng C#
            LoginResponse loginReponse = JsonConvert.DeserializeObject<LoginResponse>(jsonString);
      //Debug.Log(loginReponse.ToString());
      //Debug.Log(loginReponse.listDetailCard[0].name);
      if (loginReponse.error == "Xảy ra lỗi khi đăng nhập")
      {
        //tài khoản không đúng
        txtError.text = loginReponse.error;
        Invoke("hiddenError", 3f);
        Debug.Log(loginReponse.error);
        panelLoading.SetActive(false);
      }
      else
      {
        playerData.setAll(loginReponse._id, loginReponse.email, loginReponse.username, loginReponse.ingame, loginReponse.age, loginReponse.gender, loginReponse.level, loginReponse.listDetailCard);
        panel.SetActive(false);
        panelLoading.SetActive(false);
        txtError.text = loginReponse.ingame;
        txtShowName.SetText(loginReponse.ingame);
        userData.username = loginReponse.username;
        Invoke("hiddenError", 3f);
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
