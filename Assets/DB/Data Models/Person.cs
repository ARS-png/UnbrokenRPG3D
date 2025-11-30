using UnityEngine;
using SQLite;
using System;

[Table("users")]
public class Person 
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public override string ToString()
    {
        return $"[Person: Id={Id}, Name={Name}, Password={Password}]";
    }
}
