using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator = default;
    private bool _canAttack = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            _animator.SetBool("Attacking", true);
            StartCoroutine(AttackCD());
        }
    }

    private IEnumerator AttackCD()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
}
