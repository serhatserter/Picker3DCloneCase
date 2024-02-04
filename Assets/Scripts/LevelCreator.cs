using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public ObjectPooler EntityPooler;
    public ObjectPooler PlatformPooler;
    public ObjectPooler CollectParticlePooler;

    [Space(20)]

    public AllLevelFeatures AllLevelFeatures;

    private float finalPlatformZSize;
    private float platforStartZPos;

    void Start()
    {
        finalPlatformZSize = 10;
        platforStartZPos = 0;
        LevelInit();
    }

    void LevelInit()
    {
        if (!GameManager.LoadLevelsJSON("Levels", ref AllLevelFeatures))
        {
            Debug.LogError("JSON not found! Please create levels in LevelGenerator scene.");
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                CreateLevel(GameManager.Instance.LevelIndex + i);
                
            }
        }


    }

    public void CreateLevel(int levelIndex)
    {
        Debug.Log("Level " + levelIndex + " created.");

        CreateEntites(levelIndex);
        CreatePlatforms(levelIndex);
    }

    void CreateEntites(int levelIndex)
    {
        for (int j = 0; j < AllLevelFeatures.Levels[levelIndex].EntitiesInLevel.Count; j++)
        {
            var newEntity = GetCollectablePrefab(AllLevelFeatures.Levels[levelIndex].EntitiesInLevel[j].TypeEntity);

            var tempPos = AllLevelFeatures.Levels[levelIndex].EntitiesInLevel[j].EntityPosition;
            tempPos.z += platforStartZPos;
            newEntity.transform.position = tempPos;

            newEntity.transform.eulerAngles = AllLevelFeatures.Levels[levelIndex].EntitiesInLevel[j].EntityRotation;

            newEntity.SetActive(true);
        }

    }

    GameObject GetCollectablePrefab(EntityType type)
    {
        switch (type)
        {
            case EntityType.Cube:
                return EntityPooler.GetPooledObject(0);

            case EntityType.Sphere:
                return EntityPooler.GetPooledObject(1);

            case EntityType.Cylinder:
                return EntityPooler.GetPooledObject(2);

            default:
                return EntityPooler.GetPooledObject(0);
        }
    }

    void CreatePlatforms(int levelIndex)
    {
        var tempEntityList = AllLevelFeatures.Levels[levelIndex].EntitiesInLevel;

        float lastEntityPosZ = tempEntityList.Max(t => t.EntityPosition.z);

        int platformCount = ((int)(lastEntityPosZ / finalPlatformZSize)) + 2;

        for (int i = 0; i < platformCount; i++)
        {
            var newPlatform = PlatformPooler.GetPooledObject(0);
            newPlatform.transform.position = new Vector3(0, -0.5f, platforStartZPos + (i * finalPlatformZSize));
            newPlatform.SetActive(true);
        }

        var finalPlatform = PlatformPooler.GetPooledObject(1);
        finalPlatform.transform.position = new Vector3(0, -0.5f, platforStartZPos + (platformCount * finalPlatformZSize));

        platforStartZPos = finalPlatform.transform.position.z + (finalPlatformZSize);

        finalPlatform.SetActive(true);

    }
}
