using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Tags
    public static string TAG_GROUND = "Ground";
    public static string TAG_MOUNTAIN = "Mountain";
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_PROJECTILE = "Projectile";

    // Controls
    public static KeyCode KEY_BEGIN_WAVE = KeyCode.Space;
    public static KeyCode KEY_HIDE_SHOP = KeyCode.Space;
    public static KeyCode KEY_PLACEMENT = KeyCode.Mouse0;
    public static KeyCode KEY_ROTATE_BUILDING = KeyCode.R; // Later change this to scsroll wheel stuff

    public static KeyCode KEY_CAMERA_ROTATE = KeyCode.Mouse1;
    public static KeyCode KEY_CAMERA_FORWARD = KeyCode.W;
    public static KeyCode KEY_CAMERA_BACKWARD = KeyCode.S;
    public static KeyCode KEY_CAMERA_RIGHT = KeyCode.D;
    public static KeyCode KEY_CAMERA_LEFT = KeyCode.A;

    // Building identifiers
    public static string ID_SINGLE_DAMAGE = "SingleDamage";
    public static string ID_AOE = "AOE";
    public static string ID_FIRERATE = "FireRate";
    public static string ID_SPEEDUP = "SpeedUp";
    public static string ID_DAMAGEUP = "DamageUp";
    public static string ID_RANGEUP = "RangeUp";

    // Projectile identifiers
    public static string ID_PROJECTILE_LEMONSEED = "LemonSeed";

    // Values
    public static float holdForPickUpLength = 0.3f;
    public static float buildingShakeStrength = 0.2f;
}
