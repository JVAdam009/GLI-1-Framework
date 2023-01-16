using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Enemy"))
    {
      other.gameObject.SetActive(false);
      UIManager.Instance.EnemyDestroyed();
    }
  }
}
