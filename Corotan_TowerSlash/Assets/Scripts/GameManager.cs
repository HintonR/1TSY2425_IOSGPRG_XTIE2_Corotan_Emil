using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    UIManager _uiM;
    [SerializeField]
    public Sprite _redArrow, _greenArrow, _yellowArrow, _redArrowN, _greenArrowN, _yellowArrowN;
    [SerializeField]
    public GameObject _arrow, _player;
    public List<GameObject> _enemies = new List<GameObject>();
    public int _score;
    public bool _gState = false;


    void Awake()
    {
        Instance = this;
        _score = 0;
    }
    

    void Start()
    {
        _uiM = UIManager.Instance;
    }

    void Update()
    {
        GameOver();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void GameOver()
    {
        if (_player.GetComponent<Player>().GetLife() <= 0 && _gState) {
            _gState = false;
            if(_enemies.Count != 0) foreach (GameObject enemy in _enemies) 
            {
                if (enemy) 
                {
                    GameObject arrow = enemy.GetComponent<Enemy>().getArrow();
                    Destroy(arrow);
                }
                Destroy(enemy);
            }
            _enemies.Clear();

            _uiM._gameOver.gameObject.SetActive(true);
            _uiM._retryButton.gameObject.SetActive(true);
        }
    }
}
