using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{

    public static FMODEvents instance;

    public enum Player
    {
        WALK
    }

    public enum Guns
    {
        GLOCK_SHOT = Player.WALK + 1,
        GLOCK_RELOAD,
        LASER_SHOT,
        LASER_RELOAD
    }

    public enum General
    {
        ITEM_IDLE = Guns.LASER_RELOAD + 1,
        ENEMY_CLOSE
    }

    public enum Music
    {

    }

    public enum GlobalParameters
    {
        Grass
    }

    public enum LocalParameters{
        Distance
    }

    public static string[] events =
    {
        //      PLAYER
        "event:/PlayerWalk",
        //      GUNS
        "",
        "",
        "event:/LaserWeaponShot",
        "",
        //      GENERAL
        "event:/ItemIdle",
        "event:/EnemyClose"
    };

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // print(events[(int)General.LEAVES]);
    }

}
