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

    public CurrentLevelInfoDTO GetCurrentLevelInfoDTO(int currentLevel)
    {
        CurrentLevelInfoDTO levelInfo = new CurrentLevelInfoDTO();
        levelInfo.spawnIntervalMin = levelConfigObject.Levels[currentLevel].spawnIntervalMin;
        levelInfo.spawnIntervalMax= levelConfigObject.Levels[currentLevel].spawnIntervalMax;
        levelInfo.minLevelPassScore= levelConfigObject.Levels[currentLevel].minLevelPassScore;
        levelInfo.timer= levelConfigObject.Levels[currentLevel].timer;
        levelInfo.levelCount = levelConfigObject.Levels.Length;

        return levelInfo;
    }   
}
