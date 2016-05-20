using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public float timeBetweenBullets = 0.15f;
    public float range;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;
    Camera cameraMain;

	void Awake ()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        cameraMain = Camera.main;
	}
	

	void Update ()
    {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        
        gunLine.SetPosition(0, transform.position);

        shootRay = cameraMain.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        

        if (Physics.Raycast(shootRay.origin,shootRay.direction, out shootHit, range))
        {
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction*range);
        }
        Debug.DrawRay(shootRay.origin, shootRay.direction * range);
        gunLine.enabled = true;
    }
}
