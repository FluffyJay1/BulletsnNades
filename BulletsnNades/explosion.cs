using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class explosion : GameObject
    {
        public explosion()
            : base("explosion", 200, 200, "explosion.png", 9, 1, 9, 0.03f)
        {
            ObjectManager.AddGameObject(this);
            this.zOrder = 2;
        }
        public override void Update()
        {
            base.Update();
            if (this.AnimationData.CurrentFrame == 8)
            {
                this.IsDead = true;
            }
        }
    }
}
