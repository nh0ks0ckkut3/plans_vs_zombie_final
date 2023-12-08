using System.Collections;
using UnityEngine;

public class bullet_sin : MonoBehaviour
{
  Transform targetPosition;
  public float jumpHeight = 0.5f;
  public float jumpDuration = 1f;

  private void Start()
  {
   
  }
  public void Update()
  {
    if (targetPosition != null)
    {
      StartCoroutine(JumpToPosition(targetPosition.position, jumpHeight, jumpDuration));
    }
    else
    {

    }
  }
  
  IEnumerator JumpToPosition(Vector3 targetPos, float height, float duration)
  {
    Vector3 startPos = transform.position;
    float time = 0f;

    while (time < 1f)
    {
      time += Time.deltaTime / duration;

      float yOffset = Mathf.Sin(Mathf.Clamp01(time) * Mathf.PI) * height;

      transform.position = Vector3.Lerp(startPos, targetPos, time) + new Vector3(0f, yOffset, 0f);

      GameObject real = gameObject.transform.parent.transform.gameObject;
      real.transform.position = new Vector3(transform.position.x, real.transform.position.y, 0f);
      yield return null;
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (targetPosition != null)
    {

    }
    else
    {
      if (collision.CompareTag("zombie"))
      {
        targetPosition = collision.GetComponent<Transform>();
        Debug.Log(targetPosition.position);
      }
    }
    
  }
}
