using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;
    
    GameManager _gM;

    [SerializeField]
    public TMPro.TextMeshProUGUI _score, _life, _gameOver;
    
    [SerializeField]
    public Button _dashButton, _retryButton, _playButton, _standard, _tank, _speed;

    [SerializeField]
    public Image _meter;
    [SerializeField]
    public GameObject _dashGuage;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _gM = GameManager.Instance;
        
        //UpdateScore();
        //UpdateLife();
        //UpdateDash();

        _dashButton.onClick.AddListener(Dash);
        _retryButton.onClick.AddListener(Retry);
        _playButton.onClick.AddListener(Play);
    }

    void Update()
    {
        if (_gM._player.GetComponent<Player>().GetDash() < _gM._player.GetComponent<Player>().GetDashCap())
        _dashButton.gameObject.SetActive(false);
        else
        _dashButton.gameObject.SetActive(true);

    }

    public void UpdateScore()
    {
        _score.text = "" + _gM._score;
    }
    public void UpdateLife()
    {
        _life.text = "Life: " + _gM._player.GetComponent<Player>().GetLife();
    }
    public void UpdateDash()
    {
        _meter.fillAmount = (float)_gM._player.GetComponent<Player>().GetDash() / (float)_gM._player.GetComponent<Player>().GetDashCap();
    }

    void Dash()
    {
        _gM._player.GetComponent<Player>().SetDashState(true);
        _gM._player.GetComponent<Player>().SetDash(0);
        UpdateDash();
        StartCoroutine(DashTimer());
    }

    IEnumerator DashTimer()
    {
        while(true) 
        {
                yield return new WaitForSeconds(15f);      
                _gM._player.GetComponent<Player>().SetDashState(false);
        }
        
    }
    void Retry()
    {
        _retryButton.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);
        _playButton.gameObject.SetActive(true);

        _life.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _dashGuage.gameObject.SetActive(false);
        _dashButton.gameObject.SetActive(false);

        _gM._player.GetComponent<Player>().SetLife(1);
    }
    void Play()
    {
        _playButton.gameObject.SetActive(false);
        _dashButton.gameObject.SetActive(false);
        _gM._gState = true;

        _gM._score = 0;
        UpdateScore();
        _gM._player.GetComponent<Player>().SetDash(0);
        UpdateDash();
        _gM._player.GetComponent<Player>().SetLife(3);
        UpdateLife();
        _gM._player.GetComponent<Player>().SetLifeCap(3);

        _score.gameObject.SetActive(true);
        _life.gameObject.SetActive(true);
        _dashGuage.gameObject.SetActive(true);
    }
}
