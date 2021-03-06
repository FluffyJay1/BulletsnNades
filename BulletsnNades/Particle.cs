﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Drawing;

namespace BulletsnNades
{
    class Particle : GameObject
    {
        //Variables
        public float xvel;
        public float yvel;
        float timer;

        //Functions
        public Particle(PointF pos, float timing, double size)
            : base("Particle", (uint)size, (uint)size, "particle.png",3,1,3,0)
        {
            ObjectManager.AddGameObject(this);
            this.Position = pos;
            this.zOrder = 1;
            timer = timing;
        }
        public override void Update()
        {
            base.Update();
            Position.X += xvel;
            Position.Y += yvel;
            yvel -= 0.16f;
            if (Opacity <= 0.0f)
            {
                IsDead = true;
            }
            if (timer <= 0)
            {
                yvel = 0;
                xvel = 0;
                this.zOrder = 0;
            }
            else
            {
                ConstructLevel();
            }
            if (Math.Abs(xvel) >= 0.1f || Math.Abs(yvel) >= 0.1f)
            {
                this.Scale.X -= 0.005f;
                this.Scale.Y -= 0.005f;
            }
            if (timer <= -8)
            {

                Opacity -= 0.05f;
            }
            timer -= FrameRateController.FrameTime;
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
            //left
            if (this.Position.Y >= bottomRightY && this.Position.Y <= topLeftY && this.Position.X >= topLeftX && this.Position.X <= topLeftX + 5)
            {
                AnimationData.GoToFrame(1);
                yvel = 0;
                xvel = 0;
                this.zOrder = 0;
            }
            //right
            if (this.Position.Y >= bottomRightY && this.Position.Y <= topLeftY && this.Position.X <= bottomRightX && this.Position.X >= bottomRightX - 5)
            {
                AnimationData.GoToFrame(1);
                yvel = 0;
                xvel = 0;
                this.zOrder = 0;
            }
            //top
            if (this.Position.X <= bottomRightX && this.Position.X >= topLeftX && this.Position.Y <= topLeftY + 10 && this.Position.Y >= topLeftY - 5)
            {
                AnimationData.GoToFrame(2);
                yvel = 0;
                xvel = 0;
                this.zOrder = 0;
            }
            //bottom
            if (this.Position.Y >= bottomRightY && this.Position.Y <= bottomRightY + 5 && this.Position.X <= bottomRightX && this.Position.X >= topLeftX)
            {
                AnimationData.GoToFrame(2);
                yvel = 0;
                xvel = 0;
                this.zOrder = 0;
            }
        }
    }
}
