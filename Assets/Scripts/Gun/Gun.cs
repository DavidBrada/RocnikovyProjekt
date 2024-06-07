using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Reference")]
    public Camera playerCam;
    public ParticleSystem muzzleFlash;
    public GameObject weaponHolder;
    public GameObject bulletHole;
    public RaycastHit rayHit;
    public LayerMask playerMask;
    
    GunRecoil recoilScript;
    PlayerMove moveScript;
    ObjectPooler objectPooler;
    PauseMenu pauseMenu;
    NextLevel levelEndScript;
    
    [Header("Gun settings")]
    public int damage;
    public int ammoCapacity, bulletsPerTap;
    public bool isAutomatic;
    public int knockback;
    
    public float timeBetweenShooting, xSpread, ySpread, zSpread, range, timeBetweenShots;
    [HideInInspector] public int bulletsLeft;
    int bulletsShot;
    public int eliminations;
    public int targetsHit;

    bool shooting, readyToShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        recoilScript = GetComponent<GunRecoil>();
        moveScript = GameObject.Find("Player").GetComponent<PlayerMove>();
        objectPooler = ObjectPooler.instance;
        pauseMenu = FindObjectOfType<PauseMenu>();
        levelEndScript = FindObjectOfType<NextLevel>();
        
        bulletsLeft = ammoCapacity;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (isAutomatic)
        {
            shooting = Input.GetButton("Fire1");
        }
        else
        {
            shooting = Input.GetButtonDown("Fire1");
        }

        if (shooting && readyToShoot && bulletsLeft > 0 && !pauseMenu.paused && !levelEndScript.levelEnded)
        {
            if (bulletsShot == 0)
            {
                muzzleFlash.Play();
                recoilScript.StartGunRecoil();
            }
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;
        
        //Rozptyl
        float x = Random.Range(-xSpread, xSpread);
        float y = Random.Range(-ySpread, ySpread);
        float z = Random.Range(-zSpread, zSpread);

        Vector3 direction = playerCam.transform.forward + new Vector3(x, y, z);
        
        if (Physics.Raycast(playerCam.transform.position, direction, out rayHit, range, ~playerMask))
        {
            Enemy enemy = rayHit.transform.GetComponent<Enemy>();
            Target target = rayHit.transform.GetComponent<Target>();
            Rigidbody projectile = rayHit.transform.GetComponent<Rigidbody>();
            
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log(rayHit.transform.name);
            }
            else if (target != null)
            {
                target.TakeDamage(damage);
            }
            
            if(rayHit.transform.GetComponent<Rigidbody>() != null)
            {
                Destroy(projectile);
            }
            else
            {
                objectPooler.SpawnFromPool("BulletHole", rayHit.point - transform.forward / 1000, Quaternion.LookRotation(rayHit.normal));
            }
        }
        // Přidat: jestli je equipnutá pistole, najde to jiný zvuk, pokud brokovnice, najde to tenhle
        FindObjectOfType<AudioManager>().Play("ShotgunShot");
        
        moveScript.playerRb.AddForce(-playerCam.transform.forward * knockback, ForceMode.Impulse);
        
        bulletsLeft--;
        bulletsShot--;
        
        Invoke(nameof(ResetShot), timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
    }
}
