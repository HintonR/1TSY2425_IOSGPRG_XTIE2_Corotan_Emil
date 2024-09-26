using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SwipeManager _swipeManager;
    Animator _anims;
    void Start()
    {
        _swipeManager = SwipeManager.Instance;
        _anims = GetComponent<Animator>();
    }

    void Update()
    {
        if (_swipeManager._direction == Direction.Tap) {
        _anims.SetBool("isTapping", true);
        StartCoroutine(ResetTapping());
        }
    }

    private IEnumerator ResetTapping()
    {
        yield return new WaitForSeconds(0.5f);      
        _anims.SetBool("isTapping", false);
    }
}
