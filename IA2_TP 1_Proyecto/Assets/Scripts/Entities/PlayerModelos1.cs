//using System.Collections;
//using TMPro;
//using UnityEngine;

//public class Player : MonoBehaviour
//{
//    private PlayerModel _playerModel;
//    private IController _playerController;

//    private BulletType _bulletType;

//    [SerializeField] private UnityEngine.UI.Image _healthBar, _commonBulletsBG, _iceBulletsBG, _fireBulletsBG;
//    [SerializeField] private TextMeshProUGUI _commonBulletsText, _iceBulletsText, _fireBulletsText;

//    [SerializeField] private float _maxHealth = default, _speed = default, _rotationSpeed = default;

//    private bool _canTakeDamage = true;

//    private Factory<PlayerBullets> _factory;
//    private ObjectPool<PlayerBullets> _pool;

//    [SerializeField] private GameManager _gameManager;

//    [SerializeField] private int _bulletStock = default;

//    [SerializeField] PlayerBullets bulletPrefab;

//    void Start()
//    {
//        ResetStats();

//        _factory = new BulletFactory(bulletPrefab);
//        _pool = new ObjectPool<PlayerBullets>(_factory.GetObject, PlayerBullets.TurnOn, PlayerBullets.TurnOff, _bulletStock);

//        _playerModel = new PlayerModel(_maxHealth, _speed, _rotationSpeed, transform, _pool, this);
//        _bulletType = new BulletType();

//        PlayerView playerView = new PlayerView(_healthBar);
//        BulletsView bulletsView = new BulletsView(_commonBulletsText, _iceBulletsText, _fireBulletsText, _commonBulletsBG, _iceBulletsBG, _fireBulletsBG);

//        bulletsView.OnStart();

//        _playerModel.onGetDamage += playerView.UpdateHUD;
//        _bulletType.onBulletChange += bulletsView.UpdateWeaponType;

//        _playerController = new PlayerController(_playerModel, _bulletType);

//    }

//    private void Update() => _playerController.OnUpdate();

//    private void ResetStats()
//    {
//        _maxHealth = 200f;
//        _speed = 8f;
//        _rotationSpeed = 720f;
//        _bulletStock = 15;
//    }

//    public void ShootBullet(BulletType bulletType)
//    {
//        if (_playerModel.canShoot)
//        {
//            var b = _pool.GetObject();

//            if (!b) return;

//            b.pool = _pool;
//            b.transform.position = transform.position;
//            b.transform.forward = transform.forward;
//            b.SetBulletType(bulletType);

//            ShootCooldown(b.shootCooldown);
//        }
//    }

//    public void TakeDamage(float damage)
//    {
//        if (_canTakeDamage)
//        {
//            _playerModel.TakeDamage(damage);
//            StartCoroutine(ResetTakeDamage(0.5f));
//        }
//    }

//    public void KilledEnemy() =>_gameManager.AddKilledEnemy();

//    public void Die() => _gameManager.PlayerDied();

//    public void ShootCooldown(float cooldown) => StartCoroutine(Cooldown(cooldown));

//    private IEnumerator Cooldown(float cooldown)
//    {
//        _playerModel.canShoot = false;

//        yield return new WaitForSecondsRealtime(cooldown);

//        _playerModel.canShoot = true;
//    }

//    private IEnumerator ResetTakeDamage(float cooldown)
//    {
//        _canTakeDamage = false;
//        yield return new WaitForSecondsRealtime(cooldown);
//        _canTakeDamage = true;
//    }
//}

//public enum SelectedBullets
//{
//    Common,
//    Ice,
//    Fire
//}
