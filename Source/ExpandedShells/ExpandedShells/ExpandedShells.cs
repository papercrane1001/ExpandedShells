using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;


namespace ExpandedShells
{
    public class Projectile_SoapBomb : Projectile_Explosive
    {
        protected override void Explode()
        {
            IntVec3 target = this.Position;
            Map map = this.Map;

            float range = 16.9F;

            for(int i = target.x - (int)range; i < target.x + (int)range; ++i)
            {
                for(int j = target.z - (int)range; j < target.z + (int) range; ++j)
                {
                    if(Math.Pow(Math.Pow(target.x - i, 2) + Math.Pow(target.z - j, 2), 0.5) < range)
                    {
                        IntVec3 ivt = new IntVec3(i, target.y, j);
                        List<Thing> thingList = ivt.GetThingList(map);
                        for(int k = thingList.Count-1; k >= 0; --k)
                        {
                            Filth filth = thingList[k] as Filth;
                            if(filth != null)
                            {
                                filth.Destroy();
                                SpawnFleck(new LocalTargetInfo(ivt), FleckDefOf.Smoke, map);
                            }
                        }
                    }
                }
            }
            base.Explode();
        }

        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            Explode();
            return;
        }

        protected void SpawnFleck(LocalTargetInfo target, FleckDef def, Map map)
        {
            if (target.HasThing)
            {
                FleckMaker.AttachedOverlay(target.Thing, def, Vector3.zero, 1f);
            }
            else
            {
                FleckMaker.Static(target.Cell, map, def, 1f);
            }
        }
    }

    //public class Projectile_Boomrat : Projectile_Explosive
    //{
    //    protected override void Explode()
    //    {
    //        IntVec3 target = this.Position;
    //        Map map = this.Map;

    //        int count = 4;

    //        //float range = 16.9F;

    //        //RimWorld.CompSpawnerPawn
    //        //PawnKindDefOf.
    //        PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDefOf.Boomalope);
    //        //Pawn p1 = PawnGenerator.GeneratePawn(request);
    //        //List<Pawn> pList = new List<Pawn>();
    //        for(int i = 0; i < count; ++i)
    //        {
    //            Pawn pawn = PawnGenerator.GeneratePawn(request);
    //            //PawnUtility.TrySpawnHatchedOrBornPawn(pawn)
    //        }

                
    //        base.Explode();
    //    }

    //    protected override void Impact(Thing hitThing)
    //    {
    //        base.Impact(hitThing);
    //        Explode();
    //        return;
    //    }

    //    protected void SpawnFleck(LocalTargetInfo target, FleckDef def, Map map)
    //    {
    //        if (target.HasThing)
    //        {
    //            FleckMaker.AttachedOverlay(target.Thing, def, Vector3.zero, 1f);
    //        }
    //        else
    //        {
    //            FleckMaker.Static(target.Cell, map, def, 1f);
    //        }
    //    }
    //}
}
