using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletType;//the type of bullet to be fired. will always be a prefab

    public float moveSpeed = 1;//movespeed of the bullets
    public float spawnsPerSecond = 1;//how many bullets to spawn per second
    public float bulletSpawnRadius = 0.25f;//radius from center that bullets should be spawned at

    public int arcCount = 1;//amount of lines of bullets
    public int arcDegrees = 0;//angle distance between first and last line of bullets
    public float arcCenter = 90f;//center of total angle between first and last line of bullets

    public bool isOrbit = false;//bool to determine whether the bullet is supposed to orbit the enemy while being fired out
    public float orbitAngle = 0f;//the angle of offset for orbital movement, is added to the angle found from vector from enemy to bullet
    public float orbitStopTime = 99f;//stop time for orbital movement, the angle will slowly even out such that it continues in a straight direction once this time is reached

    public bool shootAtPlayer = false;//whether the center of the arc should be aimed directly at the player

    public int lineCount = 1;//number of bullets shot out at one angle on the arc
    public float deltaSpeed = 1;//the difference in speed between the bullets in a line
    public string bulletSoundName;//sound played for each round of bullet instantiations, if null, will not make sound
    GameObject bulletSoundParent;
    AudioSource[] bulletSounds;


    float[] directions;//the directions for all the bullets in the arc
    Vector3[] spawnLocations;//the location where all bullets in the arc should be spawned

    int framesPerSpawn = 50;        //this and the next value are handled by code, and ultimately determined by the spawnsPerSecond variable
    int framesSinceLastSpawn = 0;
    public float secondsBeforeFirstSpawn = 0;//time in seconds before the first round of bullets is shot, and then bullets are shot in interval determined by spawnsPerSecond

    private float dSPS; //equal to last spawnsPerSecond value| These values are used to check if spawns per second, arc count, arc degrees, or arc center have changed since the last frame
    private int dCo;    //equal to last arcCount value       | if so, directions and spawnLocations arrays are re-evaluated. I did it like this so it wouldn't re-evaluate them every frame,
    private int dD;     //equal to last arcDegrees value     | and instead only does it when necessary to optimize performance.
    private float dCe;  //equal to last arcCenter value      |
    private float centerPLC;

    GameObject player;//totally not the player
    
    void Start()
    {
        if(bulletSoundName != "")//gets the AudioSource by GameObject name if given
        {
            bulletSounds = GameObject.Find(bulletSoundName).GetComponentsInChildren<AudioSource>();
        }
        framesSinceLastSpawn = (int)-Mathf.Floor(secondsBeforeFirstSpawn * 50);//controls how long it will be until the first round of bullets is instantiated, used later
        player = GameObject.Find("Player");//obvious
        if (shootAtPlayer)
        {
            Vector3 tmpDir = (player.transform.position - transform.position).normalized;//gets the vector from player to bullet and normalizes it
            arcCenter = Mathf.Atan2(tmpDir.y, tmpDir.x) * Mathf.Rad2Deg;//gets the angle of the normalized vector found above, and makes it the center of the arc of bullets
        }
        directions = new float[arcCount];//make a float array that holds the directions to fire each bullet in a barrage
        spawnLocations = new Vector3[arcCount];//make a Vector3 array that determines the spawn positions of each bullet using the corresponding float from directions
        dSPS = spawnsPerSecond;
        dCo = arcCount;
        dD = arcDegrees;
        dCe = arcCenter;

        int center = (int)Mathf.Floor(((float)arcCount - 1f) / 2);//gets the bullet number at the center of the arc, floors the number if even
        centerPLC = arcCenter;
        int i = 0;
        if(arcCount % 2 == 0)
        {
            centerPLC -= arcDegrees / arcCount / 2;//offsets the center of the arc if the arc has an even number of bullet lines
        }
        while (i < directions.Length)
        {
            directions[i] = centerPLC + (arcDegrees / arcCount * ((float)i - center - (((arcCount % 2) - 1) / 2)));//sets the bullet directions. Honestly cannot be bothered to reconfirm the logic behind this but it is correct.
            i++;
        }
        i = 0;
        while (i < spawnLocations.Length)
        {
            spawnLocations[i].x = Mathf.Cos(directions[i]/180 * Mathf.PI);//the spawn locations are just bulletSpawnRadius distance away from the center in the direction the bullet will be sent
            spawnLocations[i].y = Mathf.Sin(directions[i]/180 * Mathf.PI);
            spawnLocations[i] = spawnLocations[i] * bulletSpawnRadius;
            i++;
        }
        
        framesPerSpawn = (int)Mathf.Floor(50 / spawnsPerSecond);//determines the number of frames between each round of bullets given the amount of rounds per second
    }

    void Update() //recalculates the values in directions and spawnLocations arrays if the spawns per second, arc count, degree count, or center count change. This is so these values can be updated during runtime by other scripts, allowing for ease of bullet spawn control.
    {
        if (shootAtPlayer)
        {
            Vector3 tmpDir = (player.transform.position - transform.position).normalized;//same as lines 54 and 55
            arcCenter = Mathf.Atan2(tmpDir.y, tmpDir.x) * Mathf.Rad2Deg;
        }
        if(dSPS != spawnsPerSecond || dCo != arcCount || dD != arcDegrees || dCe != arcCenter)//same as lines 57 through 83
        {
            directions = new float[arcCount];
            spawnLocations = new Vector3[arcCount];
            int center = (int)Mathf.Floor(((float)arcCount - 1f) / 2);
            centerPLC = arcCenter;
            int i = 0;
            if (arcCount % 2 == 0)
            {
                centerPLC -= arcDegrees / arcCount / 2;
            }
            while (i < directions.Length)
            {
                directions[i] = centerPLC + (arcDegrees / arcCount * ((float)i - center - (((arcCount % 2) - 1) / 2)));
                i++;
            }
            i = 0;
            while (i < spawnLocations.Length)
            {
                spawnLocations[i].x = Mathf.Cos(directions[i] / 180 * Mathf.PI);
                spawnLocations[i].y = Mathf.Sin(directions[i] / 180 * Mathf.PI);
                spawnLocations[i] = spawnLocations[i] * bulletSpawnRadius;
                i++;
            }

            framesPerSpawn = (int)(Mathf.Floor(50 / spawnsPerSecond));

            dSPS = spawnsPerSecond;
            dCo = arcCount;
            dD = arcDegrees;
            dCe = arcCenter;
        }
    }

    void FixedUpdate()//spawns the amount of bullets necessary to fulfil the parameters provided by the public variables.
    {
        if (framesSinceLastSpawn == framesPerSpawn)//if it has been a specified amount of frames since the previous fire barrage
        {
            int k = 0;
            foreach (Vector3 vec in spawnLocations)//bullets get spawned according to the vector3 values calculated above
            {
                for (int i = 0; i < lineCount; i++)//instantiates the bullets in the line
                {
                    GameObject bullet = Instantiate(bulletType, spawnLocations[k] + transform.position, Quaternion.Euler(0, 0, directions[k]));//instantiates the current bullet
                    MoveDanmaku tmp = bullet.GetComponent<MoveDanmaku>();
                    tmp.moveSpeed = moveSpeed + (deltaSpeed * i);//sets the moveSpeed and direction for the bullet, necessary because these values need to keep existing even after the enemy the bullet was fired from is gone
                    tmp.direction = spawnLocations[k].normalized;
                    AngleOrbit tmpAng = bullet.GetComponent<AngleOrbit>();
                    tmpAng.firedFrom = gameObject;//in the angleOrbit component of the bullet, passes through the specified values. They are only used if isOrbit is true, though.
                    tmpAng.offsetAngle = orbitAngle;
                    tmpAng.isOrbit = isOrbit;
                    tmpAng.stopTime = orbitStopTime;

                }
                k++;
            }
            PlayShootSound();
            framesSinceLastSpawn = 0;//resets the value so that it can count up to the next spawn frame again
        }
        else framesSinceLastSpawn++;//if bullets were not spawned on this frame, increments
    }

    void PlayShootSound()
    {
        if(bulletSounds != null)//makes sure bulletSound type isn't null before trying to play its audio, then play the first AudioSource of that type that isn't already playing (multiple of the same clip exist to prevent cutting off the previous play)
        {
            for(int i = 0; i < bulletSounds.Length; i++)
            {
                if (!bulletSounds[i].isPlaying)
                {
                    bulletSounds[i].Play();
                    return;
                }
            }
        }
    }
}
