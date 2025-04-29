using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : Bullet
{
    public ObjectPool<PlayerBullets> pool;
    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private List<Material> _bulletMaterials;

    public override void MakeDamage()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.forward * 0.3f, transform.forward, out hit, _viewRadius, _enemiesMask))
        {
            Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();

            if (enemy != null) enemy.TakeDamage(20f);

            pool.ReturnObject(this);
        }
        else StartCoroutine(DestroyBullet(_destroyCooldown));
    }

    private IEnumerator DestroyBullet(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        pool.ReturnObject(this);
    }
}
