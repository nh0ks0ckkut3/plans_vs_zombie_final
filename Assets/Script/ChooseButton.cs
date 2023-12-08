using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseButton : MonoBehaviour, IPointerClickHandler
{
  // Start is called before the first frame update


  public string buttonID;
  public int price;
  private void Start()
  {
    DetailCard[] listCard;
    string json = PlayerPrefs.GetString("listDetailCard", "[]");
    Debug.Log(json);
    if (!string.IsNullOrEmpty(json))
    {
      listCard = JsonConvert.DeserializeObject<DetailCard[]>(json);
      
    }
    else
    {
      listCard = new DetailCard[0]; // Trả về mảng rỗng nếu không có dữ liệu
    }
    Debug.Log(listCard.Length);
    for (int i = 0; i < listCard.Length; i++)
    {
      if (listCard[i].cardVip_id.Equals(buttonID)) {
         gameObject.SetActive(false);
      }
    }
  }

  // Được gọi khi button được nhấn
  public void OnPointerClick(PointerEventData eventData)
  {
    PlayerPrefs.SetString("idCardChoose", buttonID);
    PlayerPrefs.SetInt("priceCardChoose", price);



    // Thực hiện các hành động khác dựa trên buttonID
  }
}
