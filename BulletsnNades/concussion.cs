using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class concussion : GameObject
    {
        bool playerAffected;
        public concussion()
            : base("concussion", 200, 200, "explosion.png", 9, 1, 9, 0.03f)
        {
            ObjectManager.AddGameObject(this);
            this.zOrder = 2;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(200);
            playerAffected = false;
        }
        public override void Update()
        {
            base.Update();
            if (this.AnimationData.CurrentFrame == 8)
            {
                this.IsDead = true;
            }
            if (playerAffected == true)
            {
                HelperObject ho = (HelperObject)ObjectManager.GetObjectByName("HelperObject");
                ho.xVel = 0;
                ho.yVel = 0;
            }
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if (with.Name == "PlayerBody" && playerAffected == false)
            {
                float xdiff = with.Position.X - Position.X;
                float ydiff = with.Position.Y - Position.Y;

                float xsquared = xdiff * xdiff;
                float ysquared = ydiff * ydiff;

                float length = (float)Math.Sqrt(xsquared + ysquared);
                xdiff /= length;
                ydiff /= length;
                HelperObject ho = (HelperObject)ObjectManager.GetObjectByName("HelperObject");
                ho.xVel = xdiff * (1000 / (length + 200));
                ho.yVel = ydiff * (1000 / (length + 200));
                playerAffected = true;
            }
        }
    }
}
