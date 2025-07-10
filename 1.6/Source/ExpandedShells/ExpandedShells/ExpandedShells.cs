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
//using UnityEngine.CoreModule;

//using HarmonyLib;

//using System.Reflection;


namespace ExpandedShells
{
    public class Projectile_SoapBomb : Projectile_Explosive
    {
        protected override void Explode()
        {
            IntVec3 target = this.Position;
            Map map = base.Map;

            float range = 16.9F;

            //if(target == null) { Log.Message("Target"); }
            for(int i = target.x - (int)range; i < target.x + (int)range; ++i)
            {
                for(int j = target.z - (int)range; j < target.z + (int) range; ++j)
                {
                    if(Math.Pow(Math.Pow(target.x - i, 2) + Math.Pow(target.z - j, 2), 0.5) < range)
                    {
                        //if(i > 0)
                        IntVec3 ivt = new IntVec3(i, target.y, j);
                        if (map == null) 
                        {
                            continue;
                            //Log.Message("null" + i.ToString() + j.ToString()); 
                        }
                        //else { Log.Message("Not null"); }
                        //if (ivt == null) { Log.Message("ivt, " + i.ToString() + " " + j.ToString()); }

                        if (map.AllCells.Contains<IntVec3>(ivt))
                        {
                            List<Thing> thingList = ivt.GetThingList(map);
                            if (thingList != null)
                            {
                                for (int k = thingList.Count - 1; k >= 0; --k)
                                {
                                    Filth filth = thingList[k] as Filth;
                                    if (filth != null)
                                    {
                                        filth.Destroy();
                                        SpawnFleck(new LocalTargetInfo(ivt), FleckDefOf.Smoke, map);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            //base.Explode();
            if(!this.DestroyedOrNull()) base.Explode();
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);
            Explode();
            return;
        }

        protected void SpawnFleck(LocalTargetInfo target, FleckDef def, Map map)
        {
            if (target.HasThing)
            {
                
                RimWorld.FleckMaker.AttachedOverlay(target.Thing, def, Vector3.zero, 1f);
            }
            else
            {
                FleckMaker.Static(target.Cell, map, def, 1f);
            }
        }
    }

    public class Projectile_Boomrat : Projectile_Explosive
    {
        protected override void Explode()
        {
            IntVec3 target = this.Position;
            Map map = this.Map;

            int count = 3;
            
            PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDefOf.Megascarab);
            
            for (int i = 0; i < count; ++i)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                pawn.health.AddHediff(HediffDefOf.Scaria, null, null, null);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this); 
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter);
            }

            if (!this.DestroyedOrNull()) base.Explode();
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);
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
}
