using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPassWordOTPRespone : MonoBehaviour
{
    // Start is called before the first frame update
    public ResetPassWordOTPRespone(string otp, string passNew)
  {
    this.otp = otp;
    this.passNew = passNew;
  }

  public string otp { get; set; }
  public string passNew { get; set; }
}
