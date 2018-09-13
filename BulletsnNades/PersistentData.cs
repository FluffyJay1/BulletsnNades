using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    public static class PersistentData
    {
        public static int Level;
        public static float Health;
        public static float PrevHealth;
        public static float jumpPower;
        public static float movementSpeed;
        public static bool bullet1;
        public static bool bullet2;
        public static bool bullet3;
        public static bool bullet4;
        public static float bleedDamage;
        public static int bounceNum;
        public static int fragmentCount;
        public static float weapon1DefaultCooldown;
        public static float weapon2DefaultCooldown;
        public static float weapon3DefaultCooldown;
        public static float weapon4DefaultCooldown;
        public static bool grenade1;
        public static bool grenade2;
        public static bool grenade3;
        public static bool grenade4;
        public static float grenade1DefaultCooldown;
        public static float grenade2DefaultCooldown;
        public static float grenade3DefaultCooldown;
        public static float grenade4DefaultCooldown;
        public static float nullFieldLength;
        public static float nullFieldHeal;
        public static float medKitHeal;
        public static int weaponType;
        public static int sightGridWidth;
        public static int sightGridHeight;
        public static void ResetData()
        {
            //remember to change these back
            jumpPower = 5.8f;
            movementSpeed = 0.3f;
            bullet1 = true;
            bullet2 = false;
            bullet3 = false;
            bullet4 = false;
            grenade1 = true;
            grenade2 = false;
            grenade3 = false;
            grenade4 = false;
            bleedDamage = 3;
            bounceNum = 3;
            fragmentCount = 20; //originally 20
            nullFieldLength = 10;
            nullFieldHeal = 3.5f;
            medKitHeal = 35;
            weapon1DefaultCooldown = 0.3f;
            weapon2DefaultCooldown = 8.5f;
            weapon3DefaultCooldown = 1.5f;
            weapon4DefaultCooldown = 12.5f; //originally 12.5
            grenade1DefaultCooldown = 5.5f;
            grenade2DefaultCooldown = 1.5f;
            grenade3DefaultCooldown = 20.5f;
            grenade4DefaultCooldown = 30.5f;
            weaponType = 1;
            sightGridWidth = 40; //32
            sightGridHeight = 30; //24
        }
        public static void Impulse101()
        {
            bullet1 = true;
            bullet2 = true;
            bullet3 = true;
            bullet4 = true;
            grenade1 = true;
            grenade2 = true;
            grenade3 = true;
            grenade4 = true;
        }
    }
}
