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
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _LoseUI;
    private int _score = 0;
    private int _enemiesEscaped = 0;
    private int _enemiesDestroyed = 0;
    

    public void AddScore(int score)
    {
        _score += score;
        UIManager.Instance.SetScore(_score.ToString());
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetEnemiesRemaining()
    {
        return _enemiesEscaped + _enemiesEscaped;
    }

    public void EnemyDestoryed()
    {
        _enemiesDestroyed += 1;
        if (SpawningManager.Instance.MaximumNumberOfEnemies() == _enemiesDestroyed)
        {
            //Game Over
            GameOver();
        }
    }

    public void EnemyEscaped()
    {
        _enemiesEscaped += 1;
        if (SpawningManager.Instance.MaximumNumberOfEnemies() / 2 >= _enemiesEscaped)
        {
            //Game Over
            GameOver();
        }
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
            //Game Over
            GameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        TimeRemaining();
    }

    public void GameOver()
    {
        //GameOver here
        if (_enemiesEscaped < SpawningManager.Instance.MaximumNumberOfEnemies() / 2)
        {
            //Win
        }
        else
        {
            //Lose
        }
    }
}
