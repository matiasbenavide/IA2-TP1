using System.Collections;
using UnityEngine;

public class Enemy : SteeringAgent
{
    [SerializeField] private EnemyBullet _bulletPrefab;
    [SerializeField] private int _bulletStock = default;

    public Player target;
    private Factory<EnemyBullet> _factory;
    private ObjectPool<EnemyBullet> _pool;
    protected float _currentHealth = 100, _damage = 20, _currentSpeed = 10f, _attackDistance = 10f;
    private bool _canAttack = true;

    void Start()
    {
        _currentSpeed = _maxSpeed;
        _factory = new EnemyBulletsFactory(_bulletPrefab);
        _pool = new ObjectPool<EnemyBullet>(_factory.GetObject, EnemyBullet.TurnOn, EnemyBullet.TurnOff, _bulletStock);
    }

    void Update()
    {
        Move(target.transform.position);
        Attack();
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= _attackDistance)
        {
            _distanceAttack = true;
            ShootPlayer();
        }
        else
        {
            _distanceAttack = false;
            _canAttack = true;
        }
    }

    private void ShootPlayer()
    {
        if (_canAttack)
        {
            Debug.Log("Shoot");
            var b = _pool.GetObject();
            Debug.Log(b);
            if (!b) return;

            b.pool = _pool;
            b.transform.position = transform.position;
            b.transform.forward = transform.forward;

            StartCoroutine(ShootCooldown(b.shootCooldown));
        }
    }

    private IEnumerator ShootCooldown(float cooldown)
    {
        _canAttack = false;
        yield return new WaitForSecondsRealtime(cooldown);
        _canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= (int)damage;
        if (_currentHealth <= 0) Destroy(gameObject);
    }
}
