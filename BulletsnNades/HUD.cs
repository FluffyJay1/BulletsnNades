using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class HUD : GameObject
    {
        GameObject healthbar;
        GameObject healthbackground;
        GameObject cursor;
        float iconRowY;
        float bullet1IconPosX;
        float bullet2IconPosX;
        float bullet3IconPosX;
        float bullet4IconPosX;
        float grenade1IconPosX;
        float grenade2IconPosX;
        float grenade3IconPosX;
        float grenade4IconPosX;
        GameObject bullet1Icon;
        GameObject bullet2Icon;
        GameObject bullet3Icon;
        GameObject bullet4Icon;
        GameObject grenade1Icon;
        GameObject grenade2Icon;
        GameObject grenade3Icon;
        GameObject grenade4Icon;

        GameObject bullet1Cooldown;
        GameObject bullet2Cooldown;
        GameObject bullet3Cooldown;
        GameObject bullet4Cooldown;

        GameObject grenade1Cooldown;
        GameObject grenade2Cooldown;
        GameObject grenade3Cooldown;
        GameObject grenade4Cooldown;

        GameObject selection;
        public HUD()
            : base("HUD", 0, 0, null)
        {
            ObjectManager.AddGameObject(this);
            healthbar = new GameObject("healthbar", 300, 20, "sightblock.png");
            healthbar.ZOrder = 10;
            ObjectManager.AddGameObject(healthbar);
            cursor = new GameObject("Crosshairs", 40, 40, "Crosshairs.png");
            cursor.ZOrder = 11;
            ObjectManager.AddGameObject(cursor);
            InputManager.ShowCursor = false;
            healthbackground = new GameObject("healthbackground", healthbar.Width, healthbar.Height, "sightblock.png");
            healthbackground.SetModulationColor(0.3f, 0.1f, 0.1f, 1);
            healthbackground.ZOrder = 9;
            ObjectManager.AddGameObject(healthbackground);

            iconRowY = -200;
            bullet1IconPosX = -300;
            bullet2IconPosX = -250;
            bullet3IconPosX = -200;
            bullet4IconPosX = -150;

            grenade1IconPosX = 150;
            grenade2IconPosX = 200;
            grenade3IconPosX = 250;
            grenade4IconPosX = 300;

            bullet1Icon = new GameObject("bullet1Icon", 50, 50, "bullet1Icon.png");
            bullet2Icon = new GameObject("bullet2Icon", 50, 50, "bullet2Icon.png");
            bullet3Icon = new GameObject("bullet3Icon", 50, 50, "bullet3Icon.png");
            bullet4Icon = new GameObject("bullet4Icon", 50, 50, "bullet4Icon.png");

            grenade1Icon = new GameObject("grenade1Icon", 50, 50, "grenade1Icon.png");
            grenade2Icon = new GameObject("grenade2Icon", 50, 50, "grenade2Icon.png");
            grenade3Icon = new GameObject("grenade3Icon", 50, 50, "grenade3Icon.png");
            grenade4Icon = new GameObject("grenade4Icon", 50, 50, "grenade4Icon.png");

            bullet1Cooldown = new GameObject("bullet1Cooldown", 50, 50, "sightblock.png");
            bullet2Cooldown = new GameObject("bullet2Cooldown", 50, 50, "sightblock.png");
            bullet3Cooldown = new GameObject("bullet3Cooldown", 50, 50, "sightblock.png");
            bullet4Cooldown = new GameObject("bullet4Cooldown", 50, 50, "sightblock.png");

            grenade1Cooldown = new GameObject("grenade1Cooldown", 50, 50, "sightblock.png");
            grenade2Cooldown = new GameObject("grenade2Cooldown", 50, 50, "sightblock.png");
            grenade3Cooldown = new GameObject("grenade3Cooldown", 50, 50, "sightblock.png");
            grenade4Cooldown = new GameObject("grenade4Cooldown", 50, 50, "sightblock.png");

            selection = new GameObject("selection", 50, 50, "selection.png");

            bullet1Icon.ZOrder = 10;
            bullet2Icon.ZOrder = 10;
            bullet3Icon.ZOrder = 10;
            bullet4Icon.ZOrder = 10;

            grenade1Icon.ZOrder = 10;
            grenade2Icon.ZOrder = 10;
            grenade3Icon.ZOrder = 10;
            grenade4Icon.ZOrder = 10;

            bullet1Cooldown.ZOrder = 10;
            bullet2Cooldown.ZOrder = 10;
            bullet3Cooldown.ZOrder = 10;
            bullet4Cooldown.ZOrder = 10;

            grenade1Cooldown.ZOrder = 10;
            grenade2Cooldown.ZOrder = 10;
            grenade3Cooldown.ZOrder = 10;
            grenade4Cooldown.ZOrder = 10;

            bullet1Cooldown.Opacity = 0.6f;
            bullet2Cooldown.Opacity = 0.6f;
            bullet3Cooldown.Opacity = 0.6f;
            bullet4Cooldown.Opacity = 0.6f;

            grenade1Cooldown.Opacity = 0.6f;
            grenade2Cooldown.Opacity = 0.6f;
            grenade3Cooldown.Opacity = 0.6f;
            grenade4Cooldown.Opacity = 0.6f;

            selection.ZOrder = 10;

            ObjectManager.AddGameObject(bullet1Icon);
            ObjectManager.AddGameObject(bullet2Icon);
            ObjectManager.AddGameObject(bullet3Icon);
            ObjectManager.AddGameObject(bullet4Icon);

            ObjectManager.AddGameObject(grenade1Icon);
            ObjectManager.AddGameObject(grenade2Icon);
            ObjectManager.AddGameObject(grenade3Icon);
            ObjectManager.AddGameObject(grenade4Icon);

            ObjectManager.AddGameObject(bullet1Cooldown);
            ObjectManager.AddGameObject(bullet2Cooldown);
            ObjectManager.AddGameObject(bullet3Cooldown);
            ObjectManager.AddGameObject(bullet4Cooldown);

            ObjectManager.AddGameObject(grenade1Cooldown);
            ObjectManager.AddGameObject(grenade2Cooldown);
            ObjectManager.AddGameObject(grenade3Cooldown);
            ObjectManager.AddGameObject(grenade4Cooldown);

            ObjectManager.AddGameObject(selection);
        }
        public override void Update()
        {
            base.Update();
            SetPosition();
            HealthUpdate();
            IconUpdate();
        }
        void SetPosition()
        {
            Position = Camera.Position;
            healthbar.Position.X = Position.X - Game.WindowWidth / 2 + healthbar.Width / 2;
            healthbar.Position.Y = Position.Y - Game.WindowHeight / 2 + healthbar.Height / 2;
            healthbackground.Position.X = Position.X - Game.WindowWidth / 2 + healthbar.Width / 2;
            healthbackground.Position.Y = Position.Y - Game.WindowHeight / 2 + healthbar.Height / 2;
            cursor.Position.X = InputManager.MousePosition.X - Game.WindowWidth / 2 + Camera.Position.X;
            cursor.Position.Y = (InputManager.MousePosition.Y * -1) + Game.WindowHeight / 2 + Camera.Position.Y;

            bullet1Icon.Position.X = bullet1IconPosX + Camera.Position.X;
            bullet1Icon.Position.Y = iconRowY + Camera.Position.Y;
            bullet2Icon.Position.X = bullet2IconPosX + Camera.Position.X;
            bullet2Icon.Position.Y = iconRowY + Camera.Position.Y;
            bullet3Icon.Position.X = bullet3IconPosX + Camera.Position.X;
            bullet3Icon.Position.Y = iconRowY + Camera.Position.Y;
            bullet4Icon.Position.X = bullet4IconPosX + Camera.Position.X;
            bullet4Icon.Position.Y = iconRowY + Camera.Position.Y;

            grenade1Icon.Position.X = grenade1IconPosX + Camera.Position.X;
            grenade1Icon.Position.Y = iconRowY + Camera.Position.Y;
            grenade2Icon.Position.X = grenade2IconPosX + Camera.Position.X;
            grenade2Icon.Position.Y = iconRowY + Camera.Position.Y;
            grenade3Icon.Position.X = grenade3IconPosX + Camera.Position.X;
            grenade3Icon.Position.Y = iconRowY + Camera.Position.Y;
            grenade4Icon.Position.X = grenade4IconPosX + Camera.Position.X;
            grenade4Icon.Position.Y = iconRowY + Camera.Position.Y;

            bullet1Cooldown.Position = bullet1Icon.Position;
            bullet2Cooldown.Position = bullet2Icon.Position;
            bullet3Cooldown.Position = bullet3Icon.Position;
            bullet4Cooldown.Position = bullet4Icon.Position;

            grenade1Cooldown.Position = grenade1Icon.Position;
            grenade2Cooldown.Position = grenade2Icon.Position;
            grenade3Cooldown.Position = grenade3Icon.Position;
            grenade4Cooldown.Position = grenade4Icon.Position;

            if (PersistentData.weaponType == 1)
            {
                selection.Position = bullet1Icon.Position;
            }
            if (PersistentData.weaponType == 2)
            {
                selection.Position = bullet2Icon.Position;
            }
            if (PersistentData.weaponType == 3)
            {
                selection.Position = bullet3Icon.Position;
            }
            if (PersistentData.weaponType == 4)
            {
                selection.Position = bullet4Icon.Position;
            }
        }
        void IconUpdate()
        {
            HelperObject ho = (HelperObject)ObjectManager.GetObjectByName("HelperObject");
            if (PersistentData.bullet1 == true)
            {
                float percent = ho.bullet1CoolDown / PersistentData.weapon1DefaultCooldown;
                bullet1Cooldown.Scale.Y = percent;
                bullet1Cooldown.Position.Y -= bullet1Cooldown.Height * (1 - percent) / 2;
                if (ho.bullet1CoolDown > 0)
                {
                    bullet1Icon.Opacity = 0.4f;
                }
                else
                {
                    bullet1Icon.Opacity = 1;
                }
            }
            else
            {
                bullet1Icon.Opacity = 0.2f;
            }
            if (PersistentData.bullet2 == true)
            {
                float percent = ho.bullet2CoolDown / PersistentData.weapon2DefaultCooldown;
                bullet2Cooldown.Scale.Y = percent;
                bullet2Cooldown.Position.Y -= bullet2Cooldown.Height * (1 - percent) / 2;
                if (ho.bullet2CoolDown > 0)
                {
                    bullet2Icon.Opacity = 0.4f;
                }
                else
                {
                    bullet2Icon.Opacity = 1;
                }
            }
            else
            {
                bullet2Icon.Opacity = 0.2f;
            }
            if (PersistentData.bullet3 == true)
            {
                float percent = ho.bullet3CoolDown / PersistentData.weapon3DefaultCooldown;
                bullet3Cooldown.Scale.Y = percent;
                bullet3Cooldown.Position.Y -= bullet3Cooldown.Height * (1 - percent) / 2;
                if (ho.bullet3CoolDown > 0)
                {
                    bullet3Icon.Opacity = 0.4f;
                }
                else
                {
                    bullet3Icon.Opacity = 1;
                }
            }
            else
            {
                bullet3Icon.Opacity = 0.2f;
            }
            if (PersistentData.bullet4 == true)
            {
                float percent = ho.bullet4CoolDown / PersistentData.weapon4DefaultCooldown;
                bullet4Cooldown.Scale.Y = percent;
                bullet4Cooldown.Position.Y -= bullet4Cooldown.Height * (1 - percent) / 2;
                if (ho.bullet4CoolDown > 0)
                {
                    bullet4Icon.Opacity = 0.4f;
                }
                else
                {
                    bullet4Icon.Opacity = 1;
                }
            }
            else
            {
                bullet4Icon.Opacity = 0.2f;
            }
            if (PersistentData.grenade1 == true)
            {
                float percent = ho.grenade1CoolDown / PersistentData.grenade1DefaultCooldown;
                grenade1Cooldown.Scale.Y = percent;
                grenade1Cooldown.Position.Y -= grenade1Cooldown.Height * (1 - percent) / 2;
                if (ho.grenade1CoolDown > 0)
                {
                    grenade1Icon.Opacity = 0.4f;
                }
                else
                {
                    grenade1Icon.Opacity = 1;
                }
            }
            else
            {
                grenade1Icon.Opacity = 0.2f;
            }
            if (PersistentData.grenade2 == true)
            {
                float percent = ho.grenade2CoolDown / PersistentData.grenade2DefaultCooldown;
                grenade2Cooldown.Scale.Y = percent;
                grenade2Cooldown.Position.Y -= grenade2Cooldown.Height * (1 - percent) / 2;
                if (ho.grenade2CoolDown > 0)
                {
                    grenade2Icon.Opacity = 0.4f;
                }
                else
                {
                    grenade2Icon.Opacity = 1;
                }
            }
            else
            {
                grenade2Icon.Opacity = 0.2f;
            }
            if (PersistentData.grenade3 == true)
            {
                float percent = ho.grenade3CoolDown / PersistentData.grenade3DefaultCooldown;
                grenade3Cooldown.Scale.Y = percent;
                grenade3Cooldown.Position.Y -= grenade3Cooldown.Height * (1 - percent) / 2;
                if (ho.grenade3CoolDown > 0)
                {
                    grenade3Icon.Opacity = 0.4f;
                }
                else
                {
                    grenade3Icon.Opacity = 1;
                }
            }
            else
            {
                grenade3Icon.Opacity = 0.2f;
            }
            if (PersistentData.grenade4 == true)
            {
                float percent = ho.grenade4CoolDown / PersistentData.grenade4DefaultCooldown;
                grenade4Cooldown.Scale.Y = percent;
                grenade4Cooldown.Position.Y -= grenade4Cooldown.Height * (1 - percent) / 2;
                if (ho.grenade4CoolDown > 0)
                {
                    grenade4Icon.Opacity = 0.4f;
                }
                else
                {
                    grenade4Icon.Opacity = 1;
                }
            }
            else
            {
                grenade4Icon.Opacity = 0.2f;
            }
        }
        void HealthUpdate()
        {
            float percent = PersistentData.Health / 100;
            healthbar.SetModulationColor(1 - percent, percent, 0, 1);
            healthbar.Scale.X = percent;
            healthbar.Position.X -= healthbar.Width * (1 - percent) / 2;
        }
    }
}
