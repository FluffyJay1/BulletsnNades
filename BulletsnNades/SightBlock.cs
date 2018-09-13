using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class SightBlock : GameObject
    {
        bool die;
        public SightBlock()
            : base("SightBlock", (uint)(Game.WindowWidth / PersistentData.sightGridWidth), (uint)(Game.WindowHeight / PersistentData.sightGridHeight), "sightblock.png")
        {
            ObjectManager.AddGameObject(this);
            die = false;
            Opacity = 1.0f;
        }
        public override void Update()
        {
            base.Update();
            if (die == true)
            {
                this.IsDead = true;
            }
            die = true;
        }
    }
}
