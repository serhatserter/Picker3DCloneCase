using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public enum EntityType
{
    Cube,
    Sphere,
    Cylinder
}

[Serializable]
public class Entity
{
    public EntityType TypeEntity;
    public Vector3 EntityPosition;
    public Vector3 EntityRotation;

    public Entity(EntityType entityType, Vector3 entityPosition, Vector3 entityRotation)
    {

        TypeEntity = entityType;
        EntityPosition = entityPosition;
        EntityRotation = entityRotation;
    }
}
[Serializable]
public class Level
{
    [HideInInspector] public string name = "Level";

    public int LevelPassCount;

    public List<Entity> EntitiesInLevel;

    public Level(List<Entity> entityInLevel, int levelPassCount)
    {
        EntitiesInLevel = entityInLevel;
        LevelPassCount = levelPassCount;
    }
}

[ExecuteInEditMode]
[Serializable]
public class AllLevelFeatures
{
    public List<Level> Levels;

    public AllLevelFeatures(List<Level> levels)
    {
        Levels = levels;
        
    }
}

public class LevelGenerator : MonoBehaviour
{

    public AllLevelFeatures AllLevelFeatures;

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < AllLevelFeatures.Levels.Count; i++)
        {
            AllLevelFeatures.Levels[i].name = "Level " + (i + 1);
        }
    }


}

[CustomEditor(typeof(LevelGenerator), true)]
public class LevelEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var levelGenerator = target as LevelGenerator;

        if (GUILayout.Button("SAVE TO JSON"))
        {
            GameManager.SaveLevelsJSON("Levels", levelGenerator.AllLevelFeatures);


            Debug.Log("SAVE JSON COMPLATED");

        }

    }
}





