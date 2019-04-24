public class LevelConfigClass
{
    public Level[] Levels { get; set; }
}

public class Level
{
    public int spawnIntervalMin { get; set; }
    public int spawnIntervalMax { get; set; }
    public int minLevelPassScore { get; set; }
    public int timer { get; set; }
}

