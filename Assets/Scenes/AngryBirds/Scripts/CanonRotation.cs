using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Camera mainCam;
    public float _maxRotation;
    public float _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        initialScaleX = PotencyBar.transform.localScale.x;
        _maxRotation = -offset;
        _minRotation = offset;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        var mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.localPosition;
        var angle = (Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI + offset);
        //var angle = (Mathf.Atan2(dist.y, dist.x) * 180f / Mathf.PI + offset);
        
        if (angle > _maxRotation)
        {
            angle = _maxRotation;
        }
        else if (angle < _minRotation)
        {
            angle = _minRotation;
        }
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += 4f/360f;
            if (ProjectileSpeed > MaxSpeed)
            {
                ProjectileSpeed = MaxSpeed;
            }
            else if (ProjectileSpeed < MinSpeed)
            {
                ProjectileSpeed = MinSpeed;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {

            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity); 
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * ProjectileSpeed;
            Debug.Log(projectile.GetComponent<Rigidbody2D>().velocity);
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
