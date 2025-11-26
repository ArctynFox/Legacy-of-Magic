using UnityEngine;

//弾スポナー

public class BulletSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    //ロードから最初の発射までの秒数
    [Tooltip("Number of seconds until first round is shot.")]
    public float secondsBeforeFirstSpawn = 0;//time in seconds before the first round of bullets is shot, and then bullets are shot in interval determined by spawnsPerSecond
    //一秒あたりのスポーン率
    [Tooltip("Bullet round fire rate per second.")]
    public float spawnsPerSecond = 1;//how many bullets to spawn per second
    //スポナーから弾のスポーン距離
    [Tooltip("Radius distance from the spawner at which to spawn bullets.")]
    public float bulletSpawnRadius = 0.25f;//radius from center that bullets should be spawned at
    //発射方向
    [Tooltip("The angle direction clockwise from right in which to fire bullets (based on central most radial line).")]
    public float arcCenter = 90f;//center of total angle between first and last line of bullets
    //プレイヤーの位置に真っ直ぐ狙うかどうか
    [Tooltip("Determines whether the bullets will be fired straight at the player's current position.")]
    public bool shootAtPlayer = false;//whether the center of the arc should be aimed directly at the player

    [Header("Firing Pattern Settings")]
    //発射する弾の放射状ライン数
    [Tooltip("Number of radial lines of bullets to fire.")]
    public int arcCount = 1;//amount of lines of bullets
    //最反時計回しと最時計回しの間の角度
    [Tooltip("Degree angle between most counterclockwise radial line and most clockwise radial line.")]
    public int arcDegrees = 0;//angle distance between first and last line of bullets
    //放射状ラインの弾数
    [Tooltip("The number of bullets to shoot on a single radial line.")]
    public int lineCount = 1;//number of bullets shot out at one angle on the arc
    //放射状ライン内の弾の速度差
    [Tooltip("Speed difference between bullets on the same radial line.")]
    public float deltaSpeed = 1;//the difference in speed between the bullets in a line

    [Header("Bullet Settings")]
    //弾種類のプレハブ
    [Tooltip("Prefab bullet type to spawn.")]
    public GameObject bulletType;//the type of bullet to be fired. will always be a prefab
    //発射する弾の移動速度
    [Tooltip("Movement speed of the bullets fired.")]
    public float moveSpeed = 1;//movespeed of the bullets
    //発射する弾は起動するかどうか
    [Tooltip("Determines whether the fired bullets will orbit the spawner.")]
    public bool isOrbit = false;//bool to determine whether the bullet is supposed to orbit the enemy while being fired out
    //発射する弾の軌道角度
    [Tooltip("Fired bullets' angle of orbit around the spawner.")]
    public float orbitAngle = 0f;//the angle of offset for orbital movement, is added to the angle found from vector from enemy to bullet
    //発射から軌道停止の秒数
    [Tooltip("Number of seconds until orbit stops.")]
    public float orbitStopTime = 99f;//stop time for orbital movement, the angle will slowly even out such that it continues in a straight direction once this time is reached

    [Header("Firing Sound Settings")]
    //発射の際のAudioClip
    [Tooltip("Name of the sound to play when firing a round.")]
    public AudioClip bulletSoundClip;
    //ベース発射オーディオプレハブ
    [Tooltip("The base SFX prefab for firing sounds.")]
    public GameObject bulletSoundPrefab;
    //発射オーディオ音量
    [Tooltip("Volume to play the audio file at.")]
    public float bulletSoundVolume = 0.15f;

    //計算された放射状ライン角度配列
    float[] directions;//the directions for all the bullets in the arc
    //計算されたスポーン位置配列
    Vector3[] spawnLocations;//the location where all bullets in the arc should be spawned

    //発射当たりのフレーム率
    int framesPerSpawn;//this and the next value are handled by code, and ultimately determined by the spawnsPerSecond variable
    //最近発射からのフレーム数
    int framesSinceLastSpawn = 0;

    //次の四つの変数は、spawnsPerSecond、arcCount、arcDegrees、またはarcCenterDirectionが
    //前フレームから変更されたかどうかを確認するために使用。変更があった場合、
    //方向とスポーン位置の配列が再計算されます。これにより、毎フレーム再計算する必要がなくなり、
    //パフォーマンスを最適化するために必要な場合のみ再計算が行われる。
    //前回のspawnsPerSecond
    private float dSPS; //equal to last spawnsPerSecond value     | These values are used to check if spawns per second, arc count, arc degrees, or arc center direction have changed since the last frame
    //前フレームのarcCount                                          | if so, directions and spawnLocations arrays are re-evaluated. I did it like this so it wouldn't re-evaluate them every frame, 
    private int dCo;    //equal to last arcCount value            | and instead only does it when necessary to optimize performance.
    //前フレームのarcDegrees                                        |
    private int dD;     //equal to last arcDegrees value          |
    //前フレームのarcCenterDirection                                |
    private float dCe;  //equal to last arcCenterDirection value  |

    //放射状ラインが偶数の場合、この変数を利用して発射弧をオフセットする
    private float centerPLC;

    //プレイヤーのGameObjectの参照
    GameObject player;
    
    void Start()
    {
        //最初の発射までのフレーム数を計算
        framesSinceLastSpawn = (int)-Mathf.Floor(secondsBeforeFirstSpawn * (int)(1 / Time.fixedDeltaTime));//controls how long it will be until the first round of bullets is instantiated, used later
        
        //shootAtPlayerがtrueだったらプレイヤーを狙う
        player = PlayerController.singleton.gameObject;
        if (shootAtPlayer)
        {
            AimAtPlayer();
        }

        SetPreviousFrameSpawnDetails();

        CalculateSpawnDetails();
    }

    void Update() //recalculates the values in directions and spawnLocations arrays if the spawns per second, arc count, degree count, or center count change. This is so these values can be updated during runtime by other scripts, allowing for ease of bullet spawn control.
    {
        if (shootAtPlayer)
        {
            AimAtPlayer();
        }
        if(SpawnDetailsHaveChanged())
        {
            CalculateSpawnDetails();

            dSPS = spawnsPerSecond;
            dCo = arcCount;
            dD = arcDegrees;
            dCe = arcCenter;
        }
    }

    void FixedUpdate()//spawns the amount of bullets necessary to fulfil the parameters provided by the public variables.
    {
        if (framesSinceLastSpawn >= framesPerSpawn)//if it has been a specified amount of frames since the previous fire barrage
        {
            FireBulletRound();
        }
        else framesSinceLastSpawn++;//if bullets were not spawned on this frame, increments
    }

    //前フレームのスポーンに関する設定を記録
    void SetPreviousFrameSpawnDetails()
    {
        dSPS = spawnsPerSecond;
        dCo = arcCount;
        dD = arcDegrees;
        dCe = arcCenter;
    }

    //前フレームのスポーン設定が今のと違うかどうか
    bool SpawnDetailsHaveChanged()
    {
        return dSPS != spawnsPerSecond || dCo != arcCount || dD != arcDegrees || dCe != arcCenter;
    }

    //全てのスポーンに関する変数を計算
    void CalculateSpawnDetails()
    {
        //中心放射状ラインの角度を計算
        int center = (int)Mathf.Floor((arcCount - 1f) / 2);
        //角度のオフセットを計算
        centerPLC = arcCenter;
        if (arcCount % 2 == 0)
        {
            centerPLC -= arcDegrees / arcCount / 2;
        }

        //全放射状ラインの角度を計算
        CalculateDirections(center);

        //放射状ラインごとのスポーン位置を計算
        CalculateSpawnLocations();

        //スポーン間のフレーム数を計算
        framesPerSpawn = (int)(Mathf.Floor((int)(1 / Time.fixedDeltaTime) / spawnsPerSecond));
    }

    //放射状ラインの方向角度を計算
    void CalculateDirections(int center)
    {
        directions = new float[arcCount];

        for(int i = 0; i < directions.Length; i++)
        {
            directions[i] = centerPLC + (arcDegrees / arcCount * ((float)i - center - (((arcCount % 2) - 1) / 2)));
        }
    }

    //放射状ラインごとのスポーン位置を計算
    void CalculateSpawnLocations()
    {
        spawnLocations = new Vector3[arcCount];

        for(int i = 0; i < spawnLocations.Length; i++)
        {
            spawnLocations[i].x = Mathf.Cos(directions[i] / 180 * Mathf.PI);
            spawnLocations[i].y = Mathf.Sin(directions[i] / 180 * Mathf.PI);
            spawnLocations[i] = spawnLocations[i] * bulletSpawnRadius;
        }
    }

    //設定を使用して弾を発射
    void FireBulletRound()
    {
        int spawnLocationIndex = 0;
        //放射状ラインごとに
        foreach (Vector3 _ in spawnLocations)//bullets get spawned according to the vector3 values calculated and put into spawnLocations
        {
            //そのラインの弾数によって
            for (int linePositionIndex = 0; linePositionIndex < lineCount; linePositionIndex++)//instantiates the bullets in the line
            {
                //弾をスポーン
                SpawnBullet(spawnLocationIndex, linePositionIndex);
            }
            spawnLocationIndex++;
        }
        //発射音を再生
        PlayShootSound();
        //発射クールダウンをリセット
        framesSinceLastSpawn = 0;//resets the value so that it can count up to the next spawn frame again
    }

    //一つの弾をスポーン
    void SpawnBullet(int spawnPositionIndex, int linePositionIndex)
    {
        //BulletPoolが存在していたら、スポーンする弾をそれの子オブジェクトにする
        GameObject bulletPoolGameObject = GameObject.Find("BulletPool");
        Transform bulletPoolTransform = null;
        if(bulletPoolGameObject != null)
        {
            bulletPoolTransform = bulletPoolGameObject.transform;
        }

        //弾をスポーン
        GameObject bullet = Instantiate(bulletType, spawnLocations[spawnPositionIndex] + transform.position, Quaternion.Euler(0, 0, directions[spawnPositionIndex]), bulletPoolTransform);//instantiates the current bullet
        
        //スポーンした弾の移動方向と速度を設定
        if (bullet.TryGetComponent<MoveDanmaku>(out var mD))
        {
            mD.moveSpeed = moveSpeed + (deltaSpeed * linePositionIndex);//sets the moveSpeed and direction for the bullet, necessary because these values need to keep existing even after the enemy the bullet was fired from is gone
            mD.direction = spawnLocations[spawnPositionIndex].normalized;
        }
        
        //スポーンした弾の軌道設定を設定
        if (bullet.TryGetComponent<AngleOrbit>(out var aO))
        {
            aO.firedFrom = gameObject;//in the angleOrbit component of the bullet, passes through the specified values. They are only used if isOrbit is true, though.
            aO.offsetAngle = orbitAngle;
            aO.isOrbit = isOrbit;
            aO.stopTime = orbitStopTime;
        }
    }

    //プレイヤーを狙う
    void AimAtPlayer()
    {
        //スポナーからプレイヤーまでの方向ベクトルを計算
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;//gets the vector from the spawner to the player and normalizes it
        //発射方向の角度を計算して設定
        arcCenter = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;//gets the angle of the normalized vector found above, and makes it the center of the arc of bullets
    }

    //指定発射音を再生
    void PlayShootSound()
    {
        if (bulletSoundClip != null)
        {
            //Sfxが存在していたら、スポーンする発射音をそれの子オブジェクトにする
            GameObject sfx = GameObject.Find("Sfx");
            if (sfx != null)
            {
                DestroyWhenAudioClipFinishes sound = Instantiate(bulletSoundPrefab, sfx.transform).GetComponent<DestroyWhenAudioClipFinishes>();
                sound.audioClip = bulletSoundClip;
                sound.SetVolume(bulletSoundVolume);
                sound.Play();
            }
        }
    }
}
