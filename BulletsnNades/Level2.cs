using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Drawing;
using System.Windows.Forms;

namespace BulletsnNades
{
    class Level2 : State
    {
        //Variables
        public float playerPosX;
        public float playerPosY;
        float playerVelX;
        float playerVelY;
        GameObject body;
        GameObject head;
        GameObject gun;
        GameObject deadbody;
        GameObject edgeprotectors;
        GameObject g3;
        bool[,] sb = new bool[PersistentData.sightGridWidth, PersistentData.sightGridHeight];
        bool[,] gb = new bool[PersistentData.sightGridWidth, PersistentData.sightGridHeight];
        bool playerIsOnGround;
        bool canGoRight;
        bool canGoLeft;
        bool isWalking;
        float movementSpeed;
        int playerBodyFrameNum;
        float playerBodyFrameTime;
        int deadBodyFrameNum;
        float deadBodyFrameTime;
        int deadBodyFrameOffset;
        float deathTimer;

        float weapon1Cooldown;
        float weapon2Cooldown;
        float weapon3Cooldown;
        float weapon4Cooldown;
        float grenade1Cooldown;
        float grenade2Cooldown;
        float grenade3Cooldown;
        float grenade4Cooldown;

        //Functions
        public override void Create()
        {
            //WHEN MAKING A NEW LEVEL, MAKE SURE TO UPDATE THE BRUSHING IN:
            //BULLETS 1-4
            //GRENADES 1-4
            //ENEMY
            //FRAGMENT
            //PARTICLE
            //ALSO MAKE THE PLAYER RESPAWN BACK IN THE SAME LEVEL
            //DA FONT FOR DA BACKGROUND IS MACROPSIA BRK
            base.Create();
            playerVelX = 0;
            playerVelY = 0;
            GameObject background = new GameObject("Level2Background", 1600, 600, "Level2Background.png");// <----------------------CHANGE THIS AS WELL
            background.Position.X += background.Width / 2;
            background.Position.Y -= background.Height / 2;
            ObjectManager.AddGameObject(background);
            background.ZOrder = -1;

            canGoRight = true;
            canGoLeft = true;
            isWalking = false;

            weapon1Cooldown = 0;
            weapon2Cooldown = 0;
            weapon3Cooldown = 0;
            weapon4Cooldown = 0;

            PersistentData.Health = 100;

            /////////////////////////////////////////////////////////////////////////////
            PersistentData.Level = 2;
            playerPosX = 75;
            playerPosY = -575;
            new EnemyClass(520, -375, 50, 2);
            new EnemyClass(100, -275, 50, 1);
            new EnemyClass(1000, -575, 50, 1);
            new EnemyClass(1075, -125, 50, 2);
            new EnemyClass(1405, -425, 50, 1);

            new MedKit(425, -125);
            new MedKit(1050, -575);

            new PickUp(700, -575, "bullet2.png");
            new Finish(1475, -575);

            SoundManager.AddBackgroundMusic("BGMusic", "InTheWind.wav", true);
            SoundManager.PlayBackgroundMusic("BGMusic");
            /////////////////////////////////////////////////////////////////////////////
            GameObject start = new GameObject("Start", 50, 86, "doorstart.png");
            start.Position.X = playerPosX;
            start.Position.Y = playerPosY + 16 + start.Height / 2;
            ObjectManager.AddGameObject(start);
            new HUD();
            body = new GameObject("PlayerBody", 40, 80, "playerbody.png", 16, 2, 8, 0.1f);
            head = new GameObject("PlayerHead", 40, 70, "playerface.png", 2, 2, 1, 1.0f);
            gun = new GameObject("PlayerGun", 80, 40, "playergun.png", 2, 2, 1, 1.0f);
            deadbody = new GameObject("DeadBody", 100, 120, "playerdeath.png", 24, 2, 12, 0.1f);
            deadBodyFrameTime = 0.1f;
            deadBodyFrameNum = 0;
            deathTimer = 3;
            body.ZOrder = 1;
            head.ZOrder = 1;
            gun.ZOrder = 1;
            body.CollisionData.CollisionEnabled = true;
            body.CollisionData.SetCollisionData(body.Width - 30, body.Height + 5);
            edgeprotectors = new GameObject("EdgeProtectors", 800, 600, "edgeprotectors.png");
            edgeprotectors.ZOrder = 4;
            ObjectManager.AddGameObject(body);
            ObjectManager.AddGameObject(head);
            ObjectManager.AddGameObject(gun);
            ObjectManager.AddGameObject(deadbody);
            new HelperObject();
            deadbody.Opacity = 0;
            ObjectManager.AddGameObject(edgeprotectors);

            playerBodyFrameNum = 0;
            playerBodyFrameTime = 0.1f;

            for (int i = 0; i < PersistentData.sightGridHeight; i++)
            {
                for (int p = 0; p < PersistentData.sightGridWidth; p++)
                {
                    sb[p, i] = true;
                    gb[p, i] = true;
                }
            }

            movementSpeed = PersistentData.movementSpeed;
        }
        public override void Update()
        {
            base.Update();
            g3 = ObjectManager.GetObjectByName("grenade3");
            for (int i = 0; i < PersistentData.sightGridHeight; i++)
            {
                for (int p = 0; p < PersistentData.sightGridWidth; p++)
                {
                    sb[p, i] = true;
                    gb[p, i] = true;
                }
            }
            playerIsOnGround = false;
            canGoLeft = true;
            canGoRight = true;
            ConstructLevel();
            SelectBullet();
            PlayerMovement();
            Camera.Position = head.Position;
            SightBlocks();
            PlayerDamage();
            PersistentData.PrevHealth = PersistentData.Health;
            if (PersistentData.Health <= 0)
            {
                PlayerDeath();
            }
        }
        void SelectBullet()
        {
            if (InputManager.IsTriggered(Keys.D1) && PersistentData.bullet1 == true)
            {
                PersistentData.weaponType = 1;
            }
            if (InputManager.IsTriggered(Keys.D2) && PersistentData.bullet2 == true)
            {
                PersistentData.weaponType = 2;
            }
            if (InputManager.IsTriggered(Keys.D3) && PersistentData.bullet3 == true)
            {
                PersistentData.weaponType = 3;
            }
            if (InputManager.IsTriggered(Keys.D4) && PersistentData.bullet4 == true)
            {
                PersistentData.weaponType = 4;
            }
        }
        void PlayerDeath()
        {
            if (body.IsDead == false)
            {
                new BloodSpurt(50, body.Position);
            }
            head.IsDead = true;
            body.IsDead = true;
            gun.IsDead = true;
            deadbody.Opacity = 100;
            deadbody.ZOrder = 1;
            deadbody.Position.X = playerPosX;
            deadbody.Position.Y = playerPosY + 46;
            if (deadBodyFrameOffset == 12)
            {
                deadbody.Rotation = 180;
                deadbody.AnimationData.GoToFrame((uint)(deadBodyFrameNum + deadBodyFrameOffset));
            }
            if (deadBodyFrameTime <= 0 && deadBodyFrameNum != 11 && playerIsOnGround == true)
            {
                deadBodyFrameTime = 0.1f;
                deadBodyFrameNum++;
                deadbody.AnimationData.GoToFrame((uint)(deadBodyFrameNum + deadBodyFrameOffset));
            }
            deathTimer -= FrameRateController.FrameTime;
            deadBodyFrameTime -= FrameRateController.FrameTime;
            if (deathTimer <= 0)
            {
                if (PersistentData.Level == 1)
                {
                    GameStateManager.GoToState(new Level1());
                }
                if (PersistentData.Level == 2)
                {
                    GameStateManager.GoToState(new Level2());
                }
            }
        }
        void PlayerDamage()
        {
            if (PersistentData.PrevHealth > PersistentData.Health && head.IsDead == false)
            {
                float diff = PersistentData.PrevHealth - PersistentData.Health;
                diff /= 3;
                new BloodSpurt((int)diff, body.Position);
            }
        }
        void PlayerMovement()
        {
            HelperObject ho = (HelperObject)ObjectManager.GetObjectByName("HelperObject");
            if (ho.xVel != 0)
            {
                playerVelX += ho.xVel;
                playerIsOnGround = false;
            }
            if (ho.yVel != 0)
            {
                playerVelY += ho.yVel;
            }
            if (playerIsOnGround == true)
            {
                playerVelY = 0;
                playerVelX *= 0.9f;
                if (InputManager.IsPressed(Keys.A) && canGoLeft == true && head.IsDead == false)
                {
                    playerVelX -= movementSpeed;
                }
                if (InputManager.IsPressed(Keys.D) && canGoRight == true && head.IsDead == false)
                {
                    playerVelX += movementSpeed;
                }
                if (InputManager.IsTriggered(Keys.W) && head.IsDead == false)
                {
                    playerVelY = PersistentData.jumpPower;
                }
                if (Math.Abs(playerVelX) > 0.5f)
                {
                    isWalking = true;
                }
                else
                {
                    isWalking = false;
                }
            }
            else
            {
                playerVelY -= 0.16f;
                if (InputManager.IsPressed(Keys.A) && canGoLeft == true && head.IsDead == false)
                {
                    playerVelX -= 0.02f;
                    playerPosX -= 0.7f;
                }
                if (InputManager.IsPressed(Keys.D) && canGoRight == true && head.IsDead == false)
                {
                    playerVelX += 0.02f;
                    playerPosX += 0.7f;
                }
            }
            if (playerIsOnGround == true)
            {
                if (isWalking == true)
                {
                    playerBodyFrameTime -= FrameRateController.FrameTime;
                    if (playerBodyFrameTime <= 0)
                    {
                        playerBodyFrameNum++;
                        playerBodyFrameTime = 0.1f;
                    }
                    if (playerBodyFrameNum >= 8)
                    {
                        playerBodyFrameNum = 0;
                    }
                }
                else
                {
                    playerBodyFrameNum = 0;
                }
            }
            else
            {
                playerBodyFrameNum = 2;
            }
            body.Position.X = playerPosX;
            body.Position.Y = playerPosY + 46;
            head.Position.X = playerPosX;
            head.Position.Y = playerPosY + 76;
            gun.Position.X = playerPosX;
            gun.Position.Y = playerPosY + 66;
            playerPosX += playerVelX;
            playerPosY += playerVelY;

            //float mousePosX = Cursor.Position.X - 965 + Camera.Position.X ;
            //float mousePosY = (Cursor.Position.Y * -1) + 500 + Camera.Position.Y;

            float mousePosX = Cursor.Position.X - 960 + gun.Position.X;
            float mousePosY = (Cursor.Position.Y * -1) + 540 + gun.Position.Y;

            float xdiff = mousePosX - gun.Position.X;
            float ydiff = mousePosY - gun.Position.Y;

            float xsquared = xdiff * xdiff;
            float ysquared = ydiff * ydiff;

            float length = (float)Math.Sqrt(xsquared + ysquared);
            xdiff /= length;
            ydiff /= length;

            //firing
            if (weapon1Cooldown > 0)
            {
                weapon1Cooldown -= FrameRateController.FrameTime;
                ho.bullet1CoolDown = weapon1Cooldown;
            }
            if (weapon2Cooldown > 0)
            {
                weapon2Cooldown -= FrameRateController.FrameTime;
                ho.bullet2CoolDown = weapon2Cooldown;
            }
            if (weapon3Cooldown > 0)
            {
                weapon3Cooldown -= FrameRateController.FrameTime;
                ho.bullet3CoolDown = weapon3Cooldown;
            }
            if (weapon4Cooldown > 0)
            {
                weapon4Cooldown -= FrameRateController.FrameTime;
                ho.bullet4CoolDown = weapon4Cooldown;
            }
            if (grenade1Cooldown > 0)
            {
                grenade1Cooldown -= FrameRateController.FrameTime;
                ho.grenade1CoolDown = grenade1Cooldown;
            }
            if (grenade2Cooldown > 0)
            {
                grenade2Cooldown -= FrameRateController.FrameTime;
                ho.grenade2CoolDown = grenade2Cooldown;
            }
            if (grenade3Cooldown > 0)
            {
                grenade3Cooldown -= FrameRateController.FrameTime;
                ho.grenade3CoolDown = grenade3Cooldown;
            }
            if (grenade4Cooldown > 0)
            {
                grenade4Cooldown -= FrameRateController.FrameTime;
                ho.grenade4CoolDown = grenade4Cooldown;
            }
            if (InputManager.IsTriggered(Keys.LButton) && head.IsDead == false)
            {
                if (PersistentData.weaponType == 1 && weapon1Cooldown <= 0)
                {
                    GameObject b = new bullet1(xdiff * 10, ydiff * 10);
                    b.Position.X = gun.Position.X + xdiff * 30;
                    b.Position.Y = gun.Position.Y + ydiff * 30;
                    weapon1Cooldown = PersistentData.weapon1DefaultCooldown;
                }
                if (PersistentData.weaponType == 2 && weapon2Cooldown <= 0)
                {
                    GameObject b = new bullet2(xdiff * 10, ydiff * 10);
                    b.Position.X = gun.Position.X + xdiff * 30;
                    b.Position.Y = gun.Position.Y + ydiff * 30;
                    weapon2Cooldown = PersistentData.weapon2DefaultCooldown;
                }
                if (PersistentData.weaponType == 3 && weapon3Cooldown <= 0)
                {
                    GameObject b = new bullet3(xdiff * 10, ydiff * 10);
                    b.Position.X = gun.Position.X + xdiff * 30;
                    b.Position.Y = gun.Position.Y + ydiff * 30;
                    weapon3Cooldown = PersistentData.weapon3DefaultCooldown;
                }
                if (PersistentData.weaponType == 4 && weapon4Cooldown <= 0)
                {
                    GameObject b = new bullet4(xdiff * 10, ydiff * 10);
                    b.Position.X = gun.Position.X + xdiff * 30;
                    b.Position.Y = gun.Position.Y + ydiff * 30;
                    weapon4Cooldown = PersistentData.weapon4DefaultCooldown;
                }
            }
            if (head.IsDead == false)
            {
                if (InputManager.IsTriggered(Keys.Z) && grenade1Cooldown <= 0 && PersistentData.grenade1 == true)
                {
                    GameObject g = new grenade1(xdiff * 5, ydiff * 5);
                    g.Position.X = gun.Position.X + xdiff * 5;
                    g.Position.Y = gun.Position.Y + ydiff * 5;
                    grenade1Cooldown = PersistentData.grenade1DefaultCooldown;
                }
                if (InputManager.IsTriggered(Keys.X) && grenade2Cooldown <= 0 && PersistentData.grenade2 == true)
                {
                    GameObject g = new grenade2(xdiff * 5, ydiff * 5);
                    g.Position.X = gun.Position.X + xdiff * 5;
                    g.Position.Y = gun.Position.Y + ydiff * 5;
                    grenade2Cooldown = PersistentData.grenade2DefaultCooldown;
                }
                if (InputManager.IsTriggered(Keys.C) && grenade3Cooldown <= 0 && PersistentData.grenade3 == true)
                {
                    GameObject g = new grenade3(xdiff * 7, ydiff * 7);
                    g.Position.X = gun.Position.X + xdiff * 5;
                    g.Position.Y = gun.Position.Y + ydiff * 5;
                    grenade3Cooldown = PersistentData.grenade3DefaultCooldown;
                }
                if (InputManager.IsTriggered(Keys.V) && grenade4Cooldown <= 0 && PersistentData.grenade4 == true)
                {
                    GameObject g = new grenade4(xdiff * 7, ydiff * 7);
                    g.Position.X = gun.Position.X + xdiff * 5;
                    g.Position.Y = gun.Position.Y + ydiff * 5;
                    grenade4Cooldown = PersistentData.grenade4DefaultCooldown;
                }
            }

            float radians = (float)(Math.Atan2(ydiff, xdiff));
            float degrees = (float)(radians * 180 / Math.PI);
            gun.Rotation = degrees;
            int FrameOffset = 0;
            if (degrees < 90 && degrees > -90 && head.IsDead == false)
            {
                gun.AnimationData.GoToFrame(0);
                head.AnimationData.GoToFrame(0);
                body.Rotation = 0;
                FrameOffset = 0;
                deadBodyFrameOffset = 0;
            }
            else if (head.IsDead == false)
            {
                gun.AnimationData.GoToFrame(1);
                head.AnimationData.GoToFrame(1);
                body.Rotation = 180;
                FrameOffset = 8;
                deadBodyFrameOffset = 12;
            }
            if (degrees > -60 || degrees < -120)
            {
                head.Rotation = degrees;
            }
            else if (degrees > -90)
            {
                head.Rotation = -60;
            }
            else
            {
                head.Rotation = -120;
            }
            body.AnimationData.GoToFrame((uint)(playerBodyFrameNum + FrameOffset));

        }
        void ConstructLevel()
        {
            //outer walls
            Brush(0, -575, 1600, -600);
            Brush(0, -25, 25, -575);
            Brush(1575, -25, 1600, -575);
            Brush(0, 0, 1600, -25);

            Brush(300, -550, 400, -575);
            Brush(400, -525, 475, -575);
            Brush(475, -375, 550, -575);
            Brush(25, -275, 425, -325);
            Brush(150, -125, 600, -175);
            Brush(550, -175, 600, -575);
            Brush(650, -25, 700, -475);
            Brush(900, -425, 950, -575);
            Brush(750, -425, 850, -475);
            Brush(800, -325, 850, -425);
            Brush(800, -275, 1100, -325);

            Brush(1100, -175, 1150, -575);
            Brush(1050, -125, 1250, -175);
            Brush(1300, -125, 1425, -175);
            Brush(1475, -125, 1575, -175);

            Brush(1150, -275, 1175, -325);
            Brush(1225, -275, 1400, -325);
            Brush(1500, -275, 1575, -325);

            Brush(1275, -325, 1325, -425);
            Brush(1275, -425, 1575, -475);
        }
        void SightBlocks()
        {
            for (int i = 0; i < PersistentData.sightGridHeight; i++)
            {
                for (int p = 0; p < PersistentData.sightGridWidth; p++)
                {
                    bool temp = sb[p, i];
                    bool temp2 = gb[p, i];
                    if (g3 == null)
                    {
                        if (temp == false)
                        {
                            GameObject sightblock;
                            sightblock = new SightBlock();
                            sightblock.Position.X = (p * Game.WindowWidth / PersistentData.sightGridWidth) - ((Game.WindowWidth / 2) - ((Game.WindowWidth / PersistentData.sightGridWidth) / 2)) + Camera.Position.X;
                            sightblock.Position.Y = (i * Game.WindowHeight / PersistentData.sightGridHeight) - ((Game.WindowHeight / 2) - ((Game.WindowHeight / PersistentData.sightGridHeight) / 2)) + Camera.Position.Y;
                            sightblock.ZOrder = 5;
                            ObjectManager.AddGameObject(sightblock);
                        }
                    }
                    else
                    {
                        if (temp == false && temp2 == false)
                        {
                            GameObject sightblock;
                            sightblock = new SightBlock();
                            sightblock.Position.X = (p * Game.WindowWidth / PersistentData.sightGridWidth) - ((Game.WindowWidth / 2) - ((Game.WindowWidth / PersistentData.sightGridWidth) / 2)) + Camera.Position.X;
                            sightblock.Position.Y = (i * Game.WindowHeight / PersistentData.sightGridHeight) - ((Game.WindowHeight / 2) - ((Game.WindowHeight / PersistentData.sightGridHeight) / 2)) + Camera.Position.Y;
                            sightblock.ZOrder = 5;
                            ObjectManager.AddGameObject(sightblock);
                        }
                    }
                }
            }
            edgeprotectors.Position = Camera.Position;
        }
        void Brush(float topLeftX, float topLeftY, float bottomRightX, float bottomRightY)
        {
            //Keep in mind that the Y from the .png has to be inverted
            //also keep in mind that no brush should be smaller than 15x15 or else all hell breaks loose
            for (int i = 0; i < PersistentData.sightGridHeight; i++)
            {
                for (int p = 0; p < PersistentData.sightGridWidth; p++)
                {
                    bool temp = sb[p, i];
                    if (temp == true && head.IsDead == false)
                    {
                        sb[p, i] = CheckLOS(topLeftX, topLeftY, bottomRightX, bottomRightY, playerPosX, head.Position.Y, (p * Game.WindowWidth / PersistentData.sightGridWidth) - ((Game.WindowWidth / 2) - ((Game.WindowWidth / PersistentData.sightGridWidth) / 2)) + Camera.Position.X, (i * Game.WindowHeight / PersistentData.sightGridHeight) - ((Game.WindowHeight / 2) - ((Game.WindowHeight / PersistentData.sightGridHeight) / 2)) + Camera.Position.Y);
                    }
                }
            }

            for (int i = 0; i < PersistentData.sightGridHeight; i++)
            {
                for (int p = 0; p < PersistentData.sightGridWidth; p++)
                {
                    bool temp = gb[p, i];
                    if (temp == true && g3 != null)
                    {
                        gb[p, i] = CheckLOS(topLeftX, topLeftY, bottomRightX, bottomRightY, g3.Position.X, g3.Position.Y, (p * Game.WindowWidth / PersistentData.sightGridWidth) - ((Game.WindowWidth / 2) - ((Game.WindowWidth / PersistentData.sightGridWidth) / 2)) + Camera.Position.X, (i * Game.WindowHeight / PersistentData.sightGridHeight) - ((Game.WindowHeight / 2) - ((Game.WindowHeight / PersistentData.sightGridHeight) / 2)) + Camera.Position.Y);
                    }
                }
            }
            //left
            if (playerPosY + 80 >= bottomRightY && playerPosY <= topLeftY && playerPosX + 20 >= topLeftX && playerPosX + 20 <= topLeftX + 15)
            {
                if (playerVelX > 0)
                {
                    playerVelX = 0;
                }
                else
                {
                    playerVelX *= 0.5f;
                }
                canGoRight = false;
                playerPosX = topLeftX - 20;
            }
            //right
            if (playerPosY + 80 >= bottomRightY && playerPosY <= topLeftY && playerPosX - 20 <= bottomRightX && playerPosX - 20 >= bottomRightX - 15)
            {
                if (playerVelX < 0)
                {
                    playerVelX = 0;
                }
                else
                {
                    playerVelX *= 0.5f;
                }
                canGoLeft = false;
                playerPosX = bottomRightX + 20;
            }
            //top
            if (playerPosX - 19 <= bottomRightX && playerPosX + 19 >= topLeftX && playerPosY <= topLeftY && playerPosY >= topLeftY - 15)
            {
                playerIsOnGround = true;
                playerPosY = topLeftY;
            }
            //bottom
            if (playerPosY + 80 >= bottomRightY && playerPosY + 80 <= bottomRightY + 15 && playerPosX - 19 <= bottomRightX && playerPosX + 19 >= topLeftX)
            {
                playerVelY = 0;
                playerPosY = bottomRightY - 80;
            }
        }
        bool CheckLOS(float topLeftX, float topLeftY, float bottomRightX, float bottomRightY, float pos1X, float pos1Y, float pos2X, float pos2Y)
        {
            if ((pos1X > topLeftX && pos1X < bottomRightX && pos1Y < topLeftY && pos1Y > bottomRightY) || (pos2X > topLeftX && pos2X < bottomRightX && pos2Y < topLeftY && pos2Y > bottomRightX))
            {
                return false;
            }
            else if (pos2Y - pos1Y != 0)
            {
                //top edge
                float run = ((topLeftY - pos1Y) * (pos2X - pos1X) / (pos2Y - pos1Y));
                if (pos1X + run > topLeftX && pos1X + run < bottomRightX && ((pos1X + run < pos2X && pos1X + run > pos1X) || (pos1X + run > pos2X && pos1X + run < pos1X)))
                {
                    return false;
                }
                //bottom edge
                run = ((bottomRightY - pos1Y) * (pos2X - pos1X) / (pos2Y - pos1Y));
                if (pos1X + run > topLeftX && pos1X + run < bottomRightX && ((pos1X + run < pos2X && pos1X + run > pos1X) || (pos1X + run > pos2X && pos1X + run < pos1X)))
                {
                    return false;
                }
                if (pos2X - pos2Y != 0)
                {
                    //left edge
                    float rise = ((topLeftX - pos1X) * (pos2Y - pos1Y) / (pos2X - pos1X));
                    if (pos1Y + rise < topLeftY && pos1Y + rise > bottomRightY && ((pos1Y + rise < pos2Y && pos1Y + rise > pos1Y) || (pos1Y + rise > pos2Y && pos1Y + rise < pos1Y)))
                    {
                        return false;
                    }
                    //right edge
                    rise = ((bottomRightX - pos1X) * (pos2Y - pos1Y) / (pos2X - pos1X));
                    if (pos1Y + rise < topLeftY && pos1Y + rise > bottomRightY && ((pos1Y + rise < pos2Y && pos1Y + rise > pos1Y) || (pos1Y + rise > pos2Y && pos1Y + rise < pos1Y)))
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
            else if (pos2Y - pos1Y == 0)
            {
                //left edge
                float rise = ((topLeftX - pos1X) * (pos2Y - pos1Y) / (pos2X - pos1X));
                if (pos1Y + rise < topLeftY && pos1Y + rise > bottomRightY && ((pos1Y + rise < pos2Y && pos1Y + rise > pos1Y) || (pos1Y + rise > pos2Y && pos1Y + rise < pos1Y)))
                {
                    return false;
                }
                //right edge
                rise = ((bottomRightX - pos1X) * (pos2Y - pos1Y) / (pos2X - pos1X));
                if (pos1Y + rise < topLeftY && pos1Y + rise > bottomRightY && ((pos1Y + rise < pos2Y && pos1Y + rise > pos1Y) || (pos1Y + rise > pos2Y && pos1Y + rise < pos1Y)))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
