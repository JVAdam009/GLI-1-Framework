using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
  [SerializeField] private AudioClip _clip;
  private void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Enemy"))
    {
      other.gameObject.SetActive(false);
      UIManager.Instance.EnemyDestroyed();
      GameManager.Instance.EnemyEscaped();
      AudioSource.PlayClipAtPoint(_clip,transform.position);
    }
  }
}
