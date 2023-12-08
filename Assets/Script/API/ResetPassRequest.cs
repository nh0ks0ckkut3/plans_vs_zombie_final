using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPassRequest
{
    public ResetPassRequest(string email, string currentPass,  string newPass)
    {
        this.currentPass = currentPass;
        this.newPass = newPass;
        this.email = email;
    }
  public ResetPassRequest(int leveup)
  {
    this.level = leveup;
  }

  public ResetPassRequest(string email) {
    this.email = email;
  }

  public ResetPassRequest(string otp, string passNew)
  {
    this.otp = otp;
    this.passNew = passNew;
  }

    public string currentPass { get; set; }
    public string newPass { get; set; }
    public string email { get; set; }
    public string otp { get; set; }
    public string passNew {  get; set; }
  public int level { get; set;}
}
