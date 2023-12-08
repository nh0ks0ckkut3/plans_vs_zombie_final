using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCard
{
  // Start is called before the first frame update
  //"_id": "65706c26923d16b8cbcccbed",
  //  "name": "cardvip1",
  //  "detail": "detail",
  //  "price": 50000,
  //  "image": "abc",
  //  "cardVip_id": "65706b8e892309f3caa2ec74",
  //  "user_id": "656f0470254da3c12f24b69d",
  //  "created_at": "2023-12-06T12:42:14.372Z",
  //  "__v": 0
  public DetailCard(string _id, string name, string detail, int __v, int price, string image, string cardVip_id, string user_id, string created_at)
  {
    this._id = _id;
    this.name = name;
    this.detail = detail;
    this.__v = __v;
    this.price = price;
    this.image = image;
    this.cardVip_id = cardVip_id;
    this.user_id = user_id;
    this.created_at = created_at;

  }

  public string _id { get; set; }
  public string name { get; set; }
  public string detail { get; set; }
  public int price { get; set; }
  public string image { get; set; }
  public string cardVip_id { get; set; }
  public int __v { get; set; }
  public string user_id { get; set; }
  public string created_at { get; set; }

}
