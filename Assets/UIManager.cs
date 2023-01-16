using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _AmmoText;
    [SerializeField] private TextMeshProUGUI _EnemyText;
    [SerializeField] private TextMeshProUGUI _TimeText;
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SetScore(string score)
    {
        _ScoreText.text = score;
    }

    public void SetTime(string minutes, string seconds)
    {
        _TimeText.text = minutes +":" + seconds;
    }

    public void EnemyDestroyed()
    {
        int enemies;
        int.TryParse(_EnemyText.text,out enemies);
        _EnemyText.text = (enemies - 1).ToString();
    }

    public void UseAmmo()
    {
        int ammo;
        int.TryParse(_AmmoText.text,out ammo);
        _AmmoText.text = (ammo - 1).ToString();
    }

    public int GetAmmo()
    {
        int ammo;
        int.TryParse(_AmmoText.text, out ammo);
        return ammo;
    }

}
