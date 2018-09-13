using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class EnemyClass : GameObject
    {
        GameObject body;
        GameObject head;
        GameObject gun;
        GameObject deadbody;
        bool isOnGround;
        bool isWalking;
        float health;
        float prevHealth;
        float velX;
        float velY;
        float bodyFrameTime;
        float bodyFrameNum;
        float timer;
        float bleedTimer;
        int bleedTicks;
        int deadBodyFrameOffset;
        int deadBodyFrameNum;
        float deadBodyFrameTime;
        float deathTimer;
        int weapontype;
        bool LOSWithPlayer;
        bool isAffectedByConcussion;
        float degrees;
        int FrameOffset;
        float playerLastSeenPosX;
        float playerLastSeenPosY;
        bool isAwareOfPlayer;
        bool canGoLeft;
        bool canGoRight;
        public EnemyClass(float posX_, float posY_, float health_, int weapontype_)
            : base("Enemy", 0, 0, null)
        {
            ObjectManager.AddGameObject(this);
            body = new GameObject("EnemyBody", 40, 80, "enemybody.png", 16, 2, 8, 0.1f);
            head = new GameObject("EnemyHead", 40, 70, "enemyface.png", 2, 2, 1, 1.0f);
            gun = new GameObject("EnemyGun", 80, 40, "enemygun.png", 2, 2, 1, 1.0f);
            deadbody = new GameObject("EnemyDeadBody", 100, 120, "enemydeath.png", 24, 2, 12, 0.1f);
            deadBodyFrameOffset = 0;
            deadBodyFrameNum = 0;
            deadBodyFrameTime = 0.1f;
            deadbody.Opacity = 0;
            deathTimer = 5.0f;
            body.ZOrder = 1;
            head.ZOrder = 1;
            gun.ZOrder = 1;
            ObjectManager.AddGameObject(body);
            ObjectManager.AddGameObject(head);
            ObjectManager.AddGameObject(gun);
            ObjectManager.AddGameObject(deadbody);
            velX = 0;
            velY = 0;
            timer = -1.0f;

            bleedTimer = 0;
            bleedTicks = 0;

            isAffectedByConcussion = false;

            degrees = 0;
            FrameOffset = 0;

            bodyFrameTime = 0.1f;
            bodyFrameNum = 0;
            isOnGround = false;
            health = health_;
            prevHealth = health;
            Position.X = posX_;
            Position.Y = posY_;
            weapontype = weapontype_;
            body.CollisionData.CollisionEnabled = true;
            body.CollisionData.SetCollisionData(body.Width - 30, body.Height + 5);
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(body.Width - 30, (body.Height + 10) * 2);

            LOSWithPlayer = false;
            isAwareOfPlayer = false;
        }
        public override void Update()
        {
            base.Update();
            canGoLeft = true;
            canGoRight = true;
            LOSWithPlayer = true;
            isOnGround = false;
            timer -= FrameRateController.FrameTime;
            ConstructLevel();
            EnemyMovement();
            Bleed();
            EnemyDamage();
            if (health <= 0)
            {
                EnemyDie();
            }
            prevHealth = health;
        }
        void Bleed()
        {
            if (bleedTimer <= 0 && bleedTicks > 0)
            {
                bleedTimer = 0.4f;
                bleedTicks -= 1;
                health -= PersistentData.bleedDamage;
            }
            if (bleedTimer > 0)
            {
                bleedTimer -= FrameRateController.FrameTime;
            }
        }
        void EnemyDie()
        {
            if (body.IsDead == false)
            {
                new BloodSpurt(25, body.Position);
                deadbody.Opacity = 1;
            }
            deadbody.Position.X = Position.X;
            deadbody.Position.Y = Position.Y + 46;
            head.IsDead = true;
            body.IsDead = true;
            gun.IsDead = true;
            deadbody.ZOrder = 1;
            if (deadBodyFrameOffset == 12)
            {
                deadbody.Rotation = 180;
            }
            deadbody.AnimationData.GoToFrame((uint)(deadBodyFrameNum + deadBodyFrameOffset));
            if (deadBodyFrameTime <= 0 && deadBodyFrameNum <= 10 && isOnGround == true)
            {
                deadBodyFrameTime = 0.1f;
                deadBodyFrameNum++;
            }
            deathTimer -= FrameRateController.FrameTime;
            deadBodyFrameTime -= FrameRateController.FrameTime;
            if (deathTimer <= 0)
            {
                deadbody.Opacity -= 0.05f;
            }
            if (deadbody.Opacity <= 0)
            {
                this.IsDead = true;
                deadbody.IsDead = true;
            }
        }
        void EnemyDamage()
        {
            if (prevHealth > health && head.IsDead == false)
            {
                float diff = prevHealth - health;
                diff /= 3;
                new BloodSpurt((int)diff, body.Position);
            }
        }
        void EnemyMovement()
        {
            if (isOnGround == true)
            {
                velY = 0;
                velX *= 0.9f;
                if (Math.Abs(velX) > 0.5f)
                {
                    isWalking = true;
                    body.AnimationData.Play();
                }
                else
                {
                    isWalking = false;
                }
            }
            else
            {
                velY -= 0.16f;
            }
            if (isOnGround == true)
            {
                if (isWalking == true)
                {
                    bodyFrameTime -= FrameRateController.FrameTime;
                    if (bodyFrameTime <= 0)
                    {
                        bodyFrameNum++;
                        bodyFrameTime = 0.1f;
                    }
                    if (bodyFrameNum >= 8)
                    {
                        bodyFrameNum = 0;
                    }
                }
                else
                {
                    bodyFrameNum = 0;
                }
            }
            else
            {
                bodyFrameNum = 2;
            }
            body.Position.X = Position.X;
            body.Position.Y = Position.Y + 46;
            head.Position.X = Position.X;
            head.Position.Y = Position.Y + 76;
            gun.Position.X = Position.X;
            gun.Position.Y = Position.Y + 66;
            Position.X += velX;
            Position.Y += velY;

            GameObject playerbody = ObjectManager.GetObjectByName("PlayerBody");
            if (playerbody != null && head.IsDead == false && LOSWithPlayer == true)
            {
                playerLastSeenPosX = playerbody.Position.X;
                playerLastSeenPosY = playerbody.Position.Y;
                isAwareOfPlayer = true;

                float mousePosX = playerbody.Position.X;
                float mousePosY = playerbody.Position.Y;

                float xdiff = mousePosX - gun.Position.X;
                float ydiff = mousePosY - gun.Position.Y + (Math.Abs(xdiff) / 11);

                if (weapontype == 3)
                {
                    ydiff = mousePosY - gun.Position.Y + (Math.Abs(xdiff) / 3);
                }

                float xsquared = xdiff * xdiff;
                float ysquared = ydiff * ydiff;

                float length = (float)Math.Sqrt(xsquared + ysquared);
                xdiff /= length;
                ydiff /= length;

                if (timer <= 0)
                {
                    if (weapontype == 1)
                    {
                        GameObject b = new bullet1(xdiff * 10, ydiff * 10);
                        b.Position.X = gun.Position.X + xdiff * 30;
                        b.Position.Y = gun.Position.Y + ydiff * 30;
                        timer = PersistentData.weapon1DefaultCooldown * 1.8f;
                    }
                    if (weapontype == 2)
                    {
                        GameObject b = new bullet2(xdiff * 10, ydiff * 10);
                        b.Position.X = gun.Position.X + xdiff * 30;
                        b.Position.Y = gun.Position.Y + ydiff * 30;
                        timer = PersistentData.weapon2DefaultCooldown * 0.7f;
                    }
                    if (weapontype == 3)
                    {
                        GameObject b = new bullet3(xdiff * 10, ydiff * 10);
                        b.Position.X = gun.Position.X + xdiff * 30;
                        b.Position.Y = gun.Position.Y + ydiff * 30;
                        timer = PersistentData.weapon3DefaultCooldown * 0.9f;
                    }
                    if (weapontype == 4)
                    {
                        GameObject b = new bullet4(xdiff * 10, ydiff * 10);
                        b.Position.X = gun.Position.X + xdiff * 30;
                        b.Position.Y = gun.Position.Y + ydiff * 30;
                        timer = PersistentData.weapon4DefaultCooldown * 0.7f;
                    }
                }

                float radians = (float)(Math.Atan2(ydiff, xdiff));
                degrees = (float)(radians * 180 / Math.PI);
                gun.Rotation = degrees;
                if (degrees < 90 && degrees > -90)
                {
                    gun.AnimationData.GoToFrame(0);
                    head.AnimationData.GoToFrame(0);
                    body.Rotation = 0;
                    FrameOffset = 0;
                    deadBodyFrameOffset = 0;
                }
                else
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
            }
            else if(playerbody != null && head.IsDead == false && LOSWithPlayer == false && isAwareOfPlayer == true)
            {
                if (isOnGround)
                {
                    if (playerLastSeenPosX > body.Position.X)
                    {
                        velX += PersistentData.movementSpeed;
                    }
                    if (playerLastSeenPosX < body.Position.X)
                    {
                        velX -= PersistentData.movementSpeed;
                    }
                    if (canGoLeft == false || canGoRight == false)
                    {
                        velY = PersistentData.jumpPower * 1.35f;
                        Position.Y += 1;
                        isOnGround = false;
                    }
                }
                else
                {
                    if (playerLastSeenPosX > body.Position.X)
                    {
                        velX += 0.03f;
                    }
                    if (playerLastSeenPosX < body.Position.X)
                    {
                        velX -= 0.03f;
                    }
                }
            }
            body.AnimationData.GoToFrame((uint)(bodyFrameNum + FrameOffset));
        }
        void ConstructLevel()
        {
            if (PersistentData.Level == 1)
            {
                //outer walls
                Brush(0, -550, 800, -600);
                Brush(0, -50, 50, -550);
                Brush(750, -50, 800, -550);
                Brush(0, 0, 800, -50);

                Brush(200, -525, 300, -550);
                Brush(700, -450, 750, -550);
                Brush(50, -200, 400, -250);
                Brush(350, -250, 400, -351);
                Brush(350, -351, 600, -401);
            }
            if (PersistentData.Level == 2)
            {
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
            if (PersistentData.Level == 3)
            {
                Brush(0, -750, 1200, -800);
                Brush(0, -50, 50, -750);
                Brush(1150, -50, 1200, -750);
                Brush(0, 0, 1200, -50);

                Brush(50, -550, 250, -600);
                Brush(425, -725, 475, -750);
                Brush(475, -700, 525, -750);
                Brush(525, -675, 1150, -750);

                Brush(1025, -525, 1150, -575);
                Brush(1025, -525, 1150, -575);
                Brush(725, -525, 975, -575);
                Brush(725, -425, 825, -525);
                Brush(725, -300, 775, -425);
                Brush(725, -250, 825, -300);
                Brush(1100, -350, 1150, -525);
                Brush(450, -200, 1050, -250);
                Brush(450, -250, 500, -350);
                Brush(50, -350, 500, -400);
            }
            if (PersistentData.Level == 4)
            {
                //outer walls
                Brush(0, -1150, 800, -1200);
                Brush(0, -50, 50, -1150);
                Brush(750, -50, 800, -1150);
                Brush(0, 0, 800, -50);

                Brush(50, -200, 275, -350);
                Brush(325, -200, 475, -350);
                Brush(525, -200, 750, -350);

                Brush(50, -450, 100, -500);
                Brush(375, -350, 425, -500);
                Brush(575, -350, 750, -550);
                Brush(50, -500, 425, -550);

                Brush(50, -700, 175, -825);
                Brush(225, -700, 375, -825);
                Brush(425, -700, 575, -825);
                Brush(625, -700, 750, -1025);
                Brush(275, -825, 325, -975);
                Brush(525, -825, 575, -975);
                Brush(50, -975, 575, -1025);
            }
            if (PersistentData.Level == 5)
            {
                Brush(0, -550, 3200, -600);
                Brush(0, -50, 50, -550);
                Brush(3150, -50, 3200, -550);
                Brush(0, 0, 3150, -50);

                Brush(200, -525, 400, -550);
                Brush(400, -475, 525, -550);
                Brush(525, -400, 625, -550);
                Brush(625, -300, 675, -550);

                Brush(950, -500, 1000, -550);
                Brush(1000, -450, 1050, -550);
                Brush(1050, -400, 1100, -550);
                Brush(1150, -400, 1300, -550);
                Brush(1250, -250, 1300, -400);

                Brush(1400, -250, 1550, -325);
                Brush(1650, -250, 1800, -325);
                Brush(1450, -450, 1500, -550);
                Brush(1700, -450, 1750, -550);
                Brush(1875, -400, 2025, -550);
                Brush(1975, -300, 2025, -400);
                Brush(1975, -250, 2175, -300);

                Brush(2250, -50, 2300, -150);
                Brush(2225, -300, 2600, -350);
                Brush(2350, -150, 2400, -300);
                Brush(2550, -50, 2600, -300);

                Brush(2400, -500, 2450, -550);
                Brush(2650, -450, 2700, -550);
            }
            if (PersistentData.Level == 6)
            {
                //outer walls
                Brush(0, -750, 1200, -800);
                Brush(0, -50, 50, -750);
                Brush(1150, -50, 1200, -750);
                Brush(0, 0, 1200, -50);

                Brush(50, -50, 150, -600);
                Brush(150, -550, 200, -600);
                Brush(150, -400, 200, -450);
                Brush(150, -250, 200, -300);
                Brush(450, -50, 500, -600);
                Brush(400, -550, 450, -600);
                Brush(400, -400, 450, -450);
                Brush(400, -250, 450, -300);

                Brush(600, -600, 650, -750);
                Brush(650, -450, 700, -750);
                Brush(700, -300, 750, -750);
                Brush(750, -150, 800, -750);

                Brush(800, -600, 850, -750);
                Brush(900, -600, 950, -750);
                Brush(1000, -600, 1050, -750);
                Brush(1100, -600, 1150, -750);
            }
        }
        void Brush(float topLeftX, float topLeftY, float bottomRightX, float bottomRightY)
        {
            GameObject pbody = ObjectManager.GetObjectByName("PlayerBody");
            if (LOSWithPlayer == true && pbody != null)
            {
                LOSWithPlayer = CheckLOS(topLeftX, topLeftY, bottomRightX, bottomRightY, head.Position.X, head.Position.Y, pbody.Position.X, pbody.Position.Y);
            }
            //left
            if (Position.Y + 80 >= bottomRightY && Position.Y <= topLeftY && Position.X + 20 >= topLeftX && Position.X + 20 <= topLeftX + 10)
            {
                if (velX > 0)
                {
                    velX = 0;
                }
                else
                {
                    velX *= 0.5f;
                }
                canGoLeft = false;
                Position.X = topLeftX - 20;
            }
            //right
            if (Position.Y + 80 >= bottomRightY && Position.Y <= topLeftY && Position.X - 20 <= bottomRightX && Position.X - 20 >= bottomRightX - 10)
            {
                if (velX < 0)
                {
                    velX = 0;
                }
                else
                {
                    velX *= 0.5f;
                }
                canGoRight = false;
                Position.X = bottomRightX + 20;
            }
            //top
            if (Position.X - 19 <= bottomRightX && Position.X + 19 >= topLeftX && Position.Y <= topLeftY && Position.Y >= topLeftY - 10)
            {
                isOnGround = true;
                Position.Y = topLeftY;
            }
            //bottom
            if (Position.Y + 80 >= bottomRightY && Position.Y + 80 <= bottomRightY + 10 && Position.X - 19 <= bottomRightX && Position.X + 19 >= topLeftX)
            {
                velY = 0;
                Position.Y = bottomRightY - 80;
            }
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if(with.Name == "bullet1")
            {
                if (with.Position.Y > Position.Y && body.IsDead == false)
                {
                    health -= bullet1.damage;
                }
            }
            if (with.Name == "bullet2")
            {
                if (with.Position.Y > Position.Y && body.IsDead == false)
                {
                    Random rand = new Random();
                    health -= bullet2.damage;
                    bleedTicks = (int)(rand.NextDouble() * 14) + 8;
                }
            }
            if (with.Name == "bullet3")
            {
                if (with.Position.Y > Position.Y && body.IsDead == false)
                {
                    health -= bullet3.damage;
                }
            }
            if (with.Name == "bullet4")
            {
                if (with.Position.Y > Position.Y && body.IsDead == false)
                {
                    health -= bullet4.damage;
                }
            }
            if (with.Name == "fragment")
            {
                if (with.Position.Y > Position.Y && body.IsDead == false)
                {
                    health -= fragment.damage;
                }
            }
            if (with.Name == "grenade1")
            {
                float xdiff = with.Position.X - body.Position.X;
                float ydiff = with.Position.Y - body.Position.Y;

                float xsquared = xdiff * xdiff;
                float ysquared = ydiff * ydiff;

                float length = (float)Math.Sqrt(xsquared + ysquared);
                if (length <= 110)
                {
                    health -= grenade1.damage;
                }
            }
            if (with.Name == "concussion")
            {
                if (isAffectedByConcussion == false && with.AnimationData.CurrentFrame == 0)
                {
                    float xdiff = body.Position.X - with.Position.X;
                    float ydiff = body.Position.Y - with.Position.Y;

                    float xsquared = xdiff * xdiff;
                    float ysquared = ydiff * ydiff;

                    float length = (float)Math.Sqrt(xsquared + ysquared);
                    if (length <= 220)
                    {
                        xdiff /= length;
                        ydiff /= length;
                        velX += xdiff * (1000 / (length + 200));
                        velY += ydiff * (1000 / (length + 200));
                        Position.Y += ydiff * (1200 / (length + 200));
                        isOnGround = false;
                    }
                }
                isAffectedByConcussion = true;
            }
            else
            {
                isAffectedByConcussion = false;
            }
            if (with.Name == "grenade3")
            {
                float xdiff = with.Position.X - body.Position.X;
                float ydiff = with.Position.Y - body.Position.Y;

                float xsquared = xdiff * xdiff;
                float ysquared = ydiff * ydiff;

                float length = (float)Math.Sqrt(xsquared + ysquared);
                if (length <= 110)
                {
                    health -= grenade3.damage;
                }
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
