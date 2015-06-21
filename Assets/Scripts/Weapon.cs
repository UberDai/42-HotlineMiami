using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Sprite   attachedSprite;
    public Sprite   bulletSprite;
    public float    bulletSpeed;
    public int              ammo;
    public float    range;
    public float    reloadingTime;
    public uint             shrapnelCount;
    public uint             shrapnelAngle;
    public Object   bulletPrefab;
    [HideInInspector]
    public Human    holder;
    protected float _lastShot;

    void            Update()
    {
        _lastShot += Time.deltaTime;
    }

    public void     Fire()
    {
        Vector2         position;
        Quaternion      rotation;
        GameObject      go;
        Bullet          bullet;
        float           deltaAngle;

        if (ammo == 0)
        {
            AudioSource dryFire = GetComponents<AudioSource>()[1];
            dryFire.PlayDelayed(0f);
            return ;
        }
        else if (_lastShot < reloadingTime)
            return ;

        position = holder.transform.position;
        rotation = holder.transform.rotation;
        rotation *= Quaternion.Euler(0, 0, -90);

        deltaAngle = shrapnelAngle / shrapnelCount;
        rotation *= Quaternion.Euler(0, 0, deltaAngle * shrapnelCount / 2 * -1);

        for (uint i = 0; i < shrapnelCount; i++)
        {
            go = (GameObject) Instantiate(bulletPrefab, position, rotation * Quaternion.Euler(0, 0, deltaAngle * i));
            bullet = go.GetComponent<Bullet>();
            bullet.direction = holder.aimingDirection * -1;
            bullet.speed = bulletSpeed;
            bullet.range = range;
            bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        }

        AudioSource shotSound = GetComponents<AudioSource>()[0];
        shotSound.PlayDelayed(0f);

        _lastShot = 0;

        if (ammo > 0)
                ammo--;
    }
}