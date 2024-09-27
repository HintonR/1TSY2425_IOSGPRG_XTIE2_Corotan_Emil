using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Standard,
    Tank,
    Speed
} 
public class Player : MonoBehaviour
{
    GameManager _gM;
    SwipeManager _sW;
    UIManager _uiM;
    Animator _anims;
    int _life, _lifeCap, _dash, _dashCap;
    Type _type;
    bool _isHurt;
    bool _isDash;
    bool _isTapped = false;

    public void SetLife(int value) { _life = value; }
    public int GetLife() { return _life; }
    public void SetLifeCap(int value) { _lifeCap = value; }
    public void SetDash(int value) { _dash = value; }
    public int GetDash() { return _dash; }
    public void SetDashCap(int value) { _dashCap = value; }
    public int GetDashCap() { return _dashCap; }
    public bool GetDashState() { return _isDash; }
    public void SetDashState(bool value) { _isDash = value;}
    public void UseDash()
    {

    }

    void Start()
    {
        _gM = GameManager.Instance;
        _sW = SwipeManager.Instance;
        _uiM = UIManager.Instance;
        _anims = GetComponent<Animator>();
        _lifeCap = 3;
        _life = _lifeCap;
        _dash = 0;
        _dashCap = 100;
        _isHurt = false;
        _isDash = false;
        _uiM.GetComponent<UIManager>().UpdateLife();
    }

    void Update()
    {
        if (_sW._direction == Direction.Tap && _gM._gState && _gM._enemies.Count == 0 && !_isTapped) 
        {
        _anims.SetBool("isTapping", true);
        StartCoroutine(ResetTapping());
        _isTapped = true;
        }

    }

    private IEnumerator ResetTapping()
    {
        _gM._score += 10;
        _uiM.GetComponent<UIManager>().UpdateScore();
        yield return new WaitForSeconds(0.5f);        
        _anims.SetBool("isTapping", false);
        _isTapped = false;
    }

    public void GetHurt()
    {
        if (!_isDash)
        {
        _life--;
        _uiM.GetComponent<UIManager>().UpdateLife();
        _anims.SetBool("isHurt", true);
        StartCoroutine(ResetHurt());
        }
        else 
        { 
            _gM._score += 25;
            _uiM.GetComponent<UIManager>().UpdateScore();
        }
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.3f);
        _anims.SetBool("isHurt", false);
    }

    public void AddDash()
    {
        if (_dash < _dashCap) _dash += 5;
    }

    public void PowerUp()
    {
        int r = Random.Range(0, 100);
        //Debug.Log(r);
        if (r < 3)
        {
            if (_life < _lifeCap)
            { 
            _life++;
            _uiM.GetComponent<UIManager>().UpdateLife();
            //Debug.Log("Bonus!");
            }
        }
    }
}
