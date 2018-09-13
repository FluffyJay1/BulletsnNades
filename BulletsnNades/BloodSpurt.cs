using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Drawing;

namespace BulletsnNades
{
    class BloodSpurt : GameObject
    {
        Random rand;

        public BloodSpurt(int particleNum, PointF pos)
            : base("BloodSpurt", 0, 0, null)
        {
            ObjectManager.AddGameObject(this);
            rand = new Random();

            for (int particleCount = 0; particleCount < particleNum; particleCount++)
            {
                float mag = (float)rand.NextDouble() * (2 + (0.1f * particleNum));
                //float mag = 20.0f;
                float temp = (float)rand.NextDouble() * 3;
                double s = (rand.NextDouble() * 0.75 * (particleNum + 15)) + 35;
                Particle p = new Particle(pos, temp, s);
                Particle q = new Particle(pos, temp + 0.05f, s - 10);
                /*
                //PointF point = PointInCircle(180, 200);
                //p.Position = point;
                //p.Position.X = (rand.Next((int)Game.WindowWidth)) - Game.WindowWidth / 2;
                //p.Position.Y = (rand.Next((int)Game.WindowHeight)) - Game.WindowHeight / 2;
                 */


                p.xvel = ((float)rand.NextDouble() - 0.5f);
                p.yvel = ((float)rand.NextDouble() - 0.5f);
                float xsquared = p.xvel * p.xvel;
                float ysquared = p.yvel * p.yvel;
                float length = (float)Math.Sqrt(xsquared + ysquared);
                if (length != 0)
                {
                    p.xvel /= length;
                    p.yvel /= length;
                }
                float randNum = ((float)rand.NextDouble() * mag);
                p.xvel *= randNum;
                p.yvel *= randNum;
                p.yvel += 3;
                q.xvel = p.xvel * 0.9f;
                q.yvel = p.yvel * 0.9f;
            }
            this.IsDead = true;
        }
    }
}
