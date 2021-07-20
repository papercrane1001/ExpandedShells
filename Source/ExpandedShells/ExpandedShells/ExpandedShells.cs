using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;


namespace ExpandedShells
{
    [DefOf]
    public static class DamageDefOf
    {
        public static DamageDef SoapBomb;
    }
    public class ExpandedShells
    {

    }

    public class ExplosionEffect_Clean : CompProperties_Explosive
    {
        //RimWorld.CompExplosive
        //RimWorld.DamageDefOf

        public ExplosionEffect_Clean() : base()
        {
            explosiveDamageType = DamageDefOf.SoapBomb;
        }

        //public void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        //{
            
        //}

        //public override 
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
