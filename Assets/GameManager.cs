using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _HidingSpots;
    private static GameManager _instance;
    [SerializeField] private Transform _endingPoint;
    [Tooltip("Enter Time in Minutes multiplied 60. Ex: 10*60")][SerializeField] private float _TimeRemaining; 

    private int _score = 0;

    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.SetScore(_score.ToString());
    }

    public int GetScore()
    {
        return _score;
    }

    public static GameManager Instance
    {
        get{
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Vector3 GenerateNextHidingSpot(ref int currentSpot,int SpotIncrement)
    {
        if (currentSpot + SpotIncrement >= _HidingSpots.Count)
        {
            return _endingPoint.position;
        }
        else
        {
            int index = Random.Range(currentSpot, currentSpot + SpotIncrement);
            currentSpot = index;
            return _HidingSpots[index].position;
        }
    }

    void TimeRemaining()
    {
        _TimeRemaining -= Time.deltaTime;
        float mRemaining = (_TimeRemaining / 60);
        float sRemaining = (_TimeRemaining % 60);
        UIManager.Instance.SetTime(Mathf.FloorToInt(mRemaining).ToString(),Mathf.FloorToInt(sRemaining).ToString());

        if (_TimeRemaining <= 0)
        {
            ///GameOver here
        }
    }
    // Update is called once per frame
    void Update()
    {
        TimeRemaining();
    }

    public void GameOver()
    {
        
    }
}
