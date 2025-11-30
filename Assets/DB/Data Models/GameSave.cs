using UnityEngine;
using System;
using SQLite;

[Table("game_save")]
public class GameSave 
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int UserID { get; set; }


    public int XPosition { get; set; }

    public int YPosition { get; set; }

    public int ZPosition { get; set; }

}
