using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator = default;
    private bool _canAttack = true;
    private float _health = 100f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    public void TakeDamage(float damage)
    {
        _health -= (int)damage;
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
