using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Payment_VietQR : MonoBehaviour
{
  public GameObject pannelLoading;
  public GameObject btnCheck;
  private PlayerData playerData;
  [System.Serializable]
  
  public class PaymentData
  {
    
    public string code;
    public string desc;
    public Data data;

    [System.Serializable]
    public class Data
    {
      public string qrCode;
      public string qrDataURL;
    }
  }
  private string apiKey = "06bcd588-16f0-41ca-ad80-9dc54e4cadb1";
  private string apiUrl = "https://api.vietqr.io/v2/generate";
  private string returnUrl = "https://plantsvszombies-54771713370f.herokuapp.com/categories/return-url/";
  public RawImage qrCodeImage;
  private string imageURL = "https://media.tenor.com/JBgYqrobdxsAAAAi/loading.gif";
  private float amount;
  private bool done_call_load_qr = false;

    public void Start()
    {
    playerData = PlayerData.instance;
    qrCodeImage = transform.parent.gameObject.transform.Find("parent").gameObject.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
    //yield return StartCoroutine(LoadImageStart());
    amount = PlayerPrefs.GetInt("priceCardChoose", 0);
    Debug.Log(amount);
    StartCoroutine(GenerateQRCode());
  }



  IEnumerator GenerateQRCode()
    {
    Debug.Log(returnUrl);
        string requestData = "{\"amount\":"+amount+",\"order_id\":\"ORDER123\",\"description\":\"Payment for order\"," +
                             "\"currency\":\"VND\",\"accountNo\":\"0160163366868\",\"accountName\":\"NGUYEN BA SON\"," +
                             "\"acqId\":\"970422\",\"template\":\"print\",\"returnUrl\":\""+returnUrl+"\"}";

        UnityWebRequest request = UnityWebRequest.PostWwwForm(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            
            string responseData = request.downloadHandler.text;
            PaymentData paymentData = JsonUtility.FromJson<PaymentData>(responseData);
            Debug.Log("QR Code generated successfully!"+ paymentData.data.qrDataURL);
            imageURL = paymentData.data.qrDataURL;

            LoadImage();
        }
        else
        {
            Debug.LogError("Failed to generate QR Code: " + request.error);
        }
    }

  void LoadImage()
  {
    Debug.Log("loadImageQR");
    string base64Data = imageURL.Substring(imageURL.IndexOf(',') + 1);

    byte[] imageData = System.Convert.FromBase64String(base64Data);
    Texture2D texture = new Texture2D(1, 1);
    texture.LoadImage(imageData);

    qrCodeImage.texture = texture;
    done_call_load_qr = true;
    qrCodeImage.transform.parent.transform.gameObject.SetActive(true);
    //if (done_call_load_qr) StartCoroutine(CheckPaymentStatus());

    pannelLoading.SetActive(false);
    btnCheck.SetActive(true);
  }
  IEnumerator LoadImageStart()
  {
    Debug.Log("loadImageStart");
    using (WWW www = new WWW(imageURL))
    {
      yield return www;

      if (string.IsNullOrEmpty(www.error))
      {
        Texture2D texture = www.texture;
        qrCodeImage.texture = texture;
      }
      else
      {
        Debug.LogError("Failed to load image: " + www.error);
      }
    }
  }
  IEnumerator CheckPaymentStatus()
  {
    Debug.Log("CheckPaymentStatus");
    UnityWebRequest request = UnityWebRequest.Get(returnUrl);
    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
      Debug.Log("Payment status checked successfully!");
      // Xử lý thông tin từ response nếu cần
    }
    else
    {
      Debug.LogError("Failed to check payment status: " + request.error);
    }
  }
  public void unLockCard()
  {
    playerData.buyNewCard(PlayerPrefs.GetString("idCardChoose", ""));
  }
}
