using Newtonsoft.Json;
using UnityEngine;

public class LevelConfigParser 
{
    private TextAsset levelConfig;
    private LevelConfigClass levelConfigObject;
    

    public LevelConfigParser(TextAsset levelCofigFile)
    {
        levelConfig = levelCofigFile;
    }

    public void ParseLevelConfigJSON()
    {
        levelConfigObject = new LevelConfigClass();
        levelConfigObject = JsonConvert.DeserializeObject<LevelConfigClass>(levelConfig.text);     
        
    }

    public int GetSpawnIntervalMin(int currentLevel)
    {
        return levelConfigObject.Levels[currentLevel].spawnIntervalMin;
    }

    public int GetSpawnIntervalMax(int currentLevel)
    {
        return levelConfigObject.Levels[currentLevel].spawnIntervalMax;
    }

    public int GetMinLevelPassScore(int currentLevel)
    {
        return levelConfigObject.Levels[currentLevel].minLevelPassScore;
    }

    public int GetCurrentTimer(int currentLevel)
    {
        return levelConfigObject.Levels[currentLevel].timer;
    }
}
