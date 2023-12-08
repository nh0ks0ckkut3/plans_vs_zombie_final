using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDetailRQ
{
  //detail, cardVip_id, user_id
  public string detail {  get; set; }
  public string cardVip_id {  get; set; }
  public string user_id {  get; set; }

  public AddDetailRQ(string detail, string cardVip_id, string user_id)
  {
    this.detail = detail;
    this.cardVip_id = cardVip_id;
    this.user_id = user_id;
  }

}
