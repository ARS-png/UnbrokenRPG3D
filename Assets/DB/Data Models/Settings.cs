using UnityEngine;
using System;
using SQLite;

[Table("settings")]
public class Settings
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int UserID { get; set; }



    public int QualityLevel { get; set; } = 2;

    public float GameVolume { get; set; } = 1;

    public int Difficulty { get; set; } = 1;

}
