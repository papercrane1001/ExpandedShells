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
    //[DefOf]
    //public static class DamageDefOf
    //{
    //    public static DamageDef SoapBomb;
    //}
    //public class ExpandedShells
    //{

    //}

    public class Projectile_SoapBomb : Projectile_Explosive
    {

        protected override void Explode()
        {
            
            //base.Explode();
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
                        for(int k = 0; k < thingList.Count; ++k)
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


    //public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    //{
    //    base.Apply(target, dest);
    //    Map map = parent.pawn.Map;
    //    foreach (IntVec3 item in Utilities.AffectedCells(target, map, parent))
    //    {
    //        List<Thing> thingList = item.GetThingList(map);
    //        for (int i = 0; i < thingList.Count; i++)
    //        {
    //            Filth filth = thingList[i] as Filth;
    //            if (filth != null)
    //            {
    //                filth.Destroy();
    //                SoundDefOf.Psycast_Skip_Exit.PlayOneShot(new TargetInfo(item, map));
    //                Utilities.SpawnFleck(new LocalTargetInfo(item), FleckDefOf.PsycastSkipInnerExit, map);
    //                Utilities.SpawnFleck(new LocalTargetInfo(item), FleckDefOf.PsycastSkipOuterRingExit, map);
    //                Utilities.SpawnEffecter(new LocalTargetInfo(item), EffecterDefOf.Skip_Exit, map, 60, parent);
    //                //Mote mote = MoteMaker.MakeStaticMote(item.ToVector3Shifted(), map, ThingDefOf.Mote_WaterskipSplashParticles);
    //            }
    //        }
    //    }
    //}
}
