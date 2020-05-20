using System;
using System.Collections.Generic;
using System.Drawing;
using GTA;

namespace ChaosIV {
    public class ChaosMain : Script {
		static Random R = new Random();

		Timer EffectTimer;
		List<Action> Loops = new List<Action>();
		List<Effect> Effects = new List<Effect>();
		List<Effect> RecentEffects = new List<Effect>(3);
		Dictionary<string, float> Speeds = new Dictionary<string, float>() { //nyoom
			{"ADMIRAL", 140},
			{"AIRTUG", 140},
			{"AMBULAN", 140},
			{"ANDROM", 160},
			{"ANNHIL", 160},
			{"BANSHEE", 160},
			{"BENSON", 115},
			{"BIFF", 110},
			{"BLISTA", 150},
			{"BOBBER", 125},
			{"BOBCAT", 125},
			{"BOXVILLE", 110},
			{"BUCCANEER", 140},
			{"BURRITO", 130},
			{"BURRITO2", 130},
			{"BUS", 135},
			{"CABBY", 135},
			{"CABLECAR", 80},
			{"CAVCADE", 135},
			{"CHAVOS", 145},
			{"COGNONTI", 150},
			{"COMET", 160},
			{"CONTENDE", 135},
			{"COQUETTE", 160},
			{"DF8", 150},
			{"DILETT", 130},
			{"DINGHY", 60},
			{"DODO", 160},
			{"DUKES", 135},
			{"EMPEROR", 130},
			{"EMPEROR2", 130},
			{"ESPERNTO", 120},
			{"FACTION", 140},
			{"FAGGIO", 80},
			{"FBI", 150},
			{"FELTZER", 145},
			{"FEROCI", 140},
			{"FIGHTER", 160},
			{"FIRETRUK", 140},
			{"FLATBED", 115},
			{"FORTUNE", 143},
			{"FORKLIFT", 50},
			{"FUTO", 140},
			{"FXT", 130},
			{"HABANRO", 130},
			{"HAKUMAI", 135},
			{"HELLFURY", 125},
			{"HUNT", 145},
			{"INFERNUS", 160},
			{"INGOT", 130},
			{"INTRUDER", 135},
			{"JETMAX", 75},
			{"LANSTALK", 135},
			{"LOKUS", 135},
			{"MANANA", 130},
			{"MARBELLA", 130},
			{"MARQUI", 30},
			{"MAVERICK", 160},
			{"MERIT", 140},
			{"MINVAN", 130},
			{"MOONB", 130},
			{"MRTASTY", 110},
			{"MULE", 100},
			{"NOOSE", 150},
			{"NRG900", 150},
			{"ORACLE", 143},
			{"PACKER", 105},
			{"PATRIOT", 130},
			{"PCJ", 140},
			{"PEREN", 130},
			{"PEYOTE", 135},
			{"PHANTOM", 135},
			{"PINNACLE", 140},
			{"PMP600", 140},
			{"POLICE", 150},
			{"POLICE2", 150},
			{"POLMAV", 160},
			{"POLPAT", 140},
			{"PONY", 115},
			{"PREDATOR", 70},
			{"PREMIER", 138},
			{"PRES", 145},
			{"PRIMO", 140},
			{"RANCHER", 130},
			{"REBLA", 134},
			{"REEFER", 40},
			{"RIPLEY", 70},
			{"ROMAN", 140},
			{"ROMERO", 120},
			{"RUINER", 150},
			{"SABRE", 140},
			{"SABRE2", 140},
			{"SABREGT", 145},
			{"SANCHEZ", 130},
			{"SCHAFTER", 141},
			{"SENTINEL", 150},
			{"SOLAIR", 130},
			{"SPEEDO", 125},
			{"SQUALO", 70},
			{"STALION", 140},
			{"STEED", 100},
			{"STOCKADE", 120},
			{"STRATUM", 135},
			{"STRETCH", 140},
			{"SUBWAY", 80},
			{"SULTAN", 145},
			{"SULTANRS", 150},
			{"SUPERGT", 155},
			{"TAXI", 135},
			{"TAXI2", 135},
			{"TOURMAV", 160},
			{"TRASH", 100},
			{"TROPIC", 75},
			{"TUG", 40},
			{"TURISMO", 160},
			{"URANUS", 130},
			{"VIGERO", 135},
			{"VIGERO2", 135},
			{"VINCENT", 130},
			{"VIRGO", 125},
			{"VOODOO", 120},
			{"WASHINGTON", 135},
			{"WILLARD", 130},
			{"YANKEE", 105},
			{"ZOMBIE", 125}
		}; 

		Font smol = new Font("Arial", 0.02f, FontScaling.ScreenUnits);

		bool isBlind = false;

		public ChaosMain() {
			Interval = 50;
			Tick += new EventHandler(ChaosLoop);
			PerFrameDrawing += new GraphicsEventHandler(ChaosDraw);

			Effects.Add(new Effect("Nothing", EffectMiscNothing));

			Effects.Add(new Effect("Everyone Is A Ghost", EffectPedsInvisibleLoop, new Timer(88000), EffectPedsInvisibleLoop, EffectPedsInvisibleStop));
			Effects.Add(new Effect("Obliterate All Nearby Peds", EffectPedsObliterateNearby));
			Effects.Add(new Effect("Remove Weapons From Everyone", EffectPedsRemoveWeapons));

			Effects.Add(new Effect("Blind", EffectPlayerBlindStart, new Timer(28000), null, EffectPlayerBlindStop));
			Effects.Add(new Effect("Exit Current Vehicle", EffectPlayerExitCurrentVehicle));
			Effects.Add(new Effect("Remove All Weapons", EffectPlayerRemoveWeapons));
			Effects.Add(new Effect("Teleport To GetALife Building", EffectPlayerTeleportGetALife));
			Effects.Add(new Effect("Clear Wanted Level", EffectPlayerWantedClear));
			Effects.Add(new Effect("Five Wanted Stars", EffectPlayerWantedFiveStars));

			//Effects.Add(new Effect("Break All Doors of Current Vehicle", EffectVehicleBreakDoorsPlayer)); // this one seems to be crashing
			Effects.Add(new Effect("Full Acceleration", EffectVehicleFullAccel, new Timer(28000), EffectVehicleFullAccel, EffectVehicleFullAccel));
			Effects.Add(new Effect("Launch All Vehicles Up", EffectVehicleLaunchAllUp));
			Effects.Add(new Effect("Repair Current Vehicle", EffectVehicleRepairPlayer));
			Effects.Add(new Effect("Spawn Police Cruiser", EffectVehicleSpawnPolice));
			Effects.Add(new Effect("Black Traffic", EffectVehicleTrafficBlack, new Timer(88000), EffectVehicleTrafficBlack, EffectVehicleTrafficBlack, new[] { "Blue Traffic" }));
			Effects.Add(new Effect("Blue Traffic", EffectVehicleTrafficBlue, new Timer(88000), EffectVehicleTrafficBlue, EffectVehicleTrafficBlue, new[] { "Black Traffic" }));

			EffectTimer = new Timer();
			EffectTimer.Tick += new EventHandler(DeployEffect);
			EffectTimer.Interval = 30000;
			EffectTimer.Start();
		}

		public void ChaosDraw(object s, GraphicsEventArgs e) {
			e.Graphics.Scaling = FontScaling.ScreenUnits;

			// Blind
			if (isBlind) e.Graphics.DrawRectangle(new RectangleF(0f, 0f, 1f, 1f), Color.Black);

			// Draw Timer Bar
			e.Graphics.DrawRectangle(new RectangleF(0f, 0f, 1f, 0.02f), Color.FromArgb(10, 10, 10));
			e.Graphics.DrawText("ChaosIV", new RectangleF(0f, 0f, 1f, 0.02f), TextAlignment.Center, Color.FromArgb(40, 40, 40), smol);
			e.Graphics.DrawRectangle(new RectangleF(0f, 0f, (float)((double)EffectTimer.ElapsedTime / 30000), 0.02f), Color.Yellow);

			// Draw Recent Effects
			if (RecentEffects.Count >= 1) {
				if (RecentEffects[0].Timer != null) e.Graphics.DrawRectangle(0.5f, 0.05f, (float)(((double)(RecentEffects[0].Timer.Interval - RecentEffects[0].Timer.ElapsedTime) / RecentEffects[0].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(128, 80, 80, 80));
				e.Graphics.DrawText(RecentEffects[0].Name, new RectangleF(0f, 0.04f, 1f, 0.02f), TextAlignment.Center, smol);
			}

			if (RecentEffects.Count >= 2) {
				if (RecentEffects[1].Timer != null) e.Graphics.DrawRectangle(0.5f, 0.08f, (float)(((double)(RecentEffects[1].Timer.Interval - RecentEffects[1].Timer.ElapsedTime) / RecentEffects[1].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(128, 80, 80, 80));
				e.Graphics.DrawText(RecentEffects[1].Name, new RectangleF(0f, 0.07f, 1f, 0.02f), TextAlignment.Center, smol);
			}

			if (RecentEffects.Count == 3) {
				if (RecentEffects[2].Timer != null) e.Graphics.DrawRectangle(0.5f, 0.11f, (float)(((double)(RecentEffects[2].Timer.Interval - RecentEffects[2].Timer.ElapsedTime) / RecentEffects[2].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(128, 80, 80, 80));
				e.Graphics.DrawText(RecentEffects[2].Name, new RectangleF(0f, 0.1f, 1f, 0.02f), TextAlignment.Center, smol);
			}
		}

		public void ChaosLoop(object s, EventArgs e) {
			for (int i = RecentEffects.Count - 1; i >= 0; i--) {
				if (RecentEffects[i].Timer != null) {
					if (RecentEffects[i].Timer.ElapsedTime > RecentEffects[i].Timer.Interval) {
						RecentEffects[i].Stop();
						RecentEffects[i].Timer.Stop();
						Loops.Remove(RecentEffects[i].Loop);
						RecentEffects.RemoveAt(i);
					}
				}
			}

			if (Loops.Count > 0) {
				foreach (Action a in Loops) a();
			}
		}

		public void DeployEffect(object s, EventArgs e) {
			Effect next = Effects[R.Next(Effects.Count)];

			if (next.Timer != null && RecentEffects.Contains(next)) {
				RecentEffects.Find(x => x.Name == next.Name).Start();
			} else {
				if (next.Conflicts != null) {
					foreach(string c in next.Conflicts) {
						if (RecentEffects.Find(x => x.Name == c).Name != null) {
							RecentEffects.Remove(RecentEffects.Find(x => x.Name == c));
						}
					}
				}

				next.Start();
				if (next.Timer != null) {
					next.Timer.Start();
				}
				if (next.Loop != null) {
					Loops.Add(next.Loop);
				}

				if (RecentEffects.Count != 3) {
					RecentEffects.Add(next);
				} else {
					foreach (Effect f in RecentEffects) {
						if (f.Timer != null) {
							if (f.Timer.ElapsedTime > f.Timer.Interval) {
								RecentEffects.Remove(f);
								break;
							}
						} else {
							RecentEffects.Remove(f);
							break;
						}
					}
					RecentEffects.Add(next);
				}
			}

			EffectTimer.Start();
		}

		// !!! EFFECT METHODS BEGIN BELOW !!! //

		#region Misc Effects
		public void EffectMiscNothing() {
			Console.WriteLine("ok maybe not quite \"nothing\" but eh who cares");
		}
		#endregion

		#region Ped Effects

		public void EffectPedsInvisibleLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) {
					p.Visible = false;
				}
			}
		}

		public void EffectPedsInvisibleStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) {
					p.Visible = true;
				}
			}
		}

		public void EffectPedsObliterateNearby() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					World.AddExplosion(p.Position);
				}
			}
		}

		public void EffectPedsRemoveWeapons() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Weapons.RemoveAll();
			}
		}
		#endregion

		#region Player Effects
		public void EffectPlayerBlindStart() {
			isBlind = true;
		}

		public void EffectPlayerBlindStop() {
			isBlind = false;
		}

		public void EffectPlayerExitCurrentVehicle() {
			if (Player.Character.isInVehicle()) Player.Character.LeaveVehicle();
		}

		public void EffectPlayerTeleportGetALife() {
			Player.TeleportTo(10, 65);
		}

		public void EffectPlayerRemoveWeapons() {
			Player.Character.Weapons.RemoveAll();
		}

		public void EffectPlayerWantedClear() {
			Player.WantedLevel = 0;
		}

		public void EffectPlayerWantedFiveStars() {
			Player.WantedLevel = 5;
		}
		#endregion

		#region Vehicle Effects
		public void EffectVehicleBreakDoorsPlayer() {
			if (Player.Character.isInVehicle()) {
				Player.Character.CurrentVehicle.Door(VehicleDoor.Hood).Break();
				Player.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).Break();
				Player.Character.CurrentVehicle.Door(VehicleDoor.LeftRear).Break();
				Player.Character.CurrentVehicle.Door(VehicleDoor.RightFront).Break();
				Player.Character.CurrentVehicle.Door(VehicleDoor.RightRear).Break();
			}
		}

		public void EffectVehicleFullAccel() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists() & v.isOnAllWheels) {
					Game.Console.Print(v.Name);
					v.Speed = Speeds[v.Name]; 
				}
			}
		}

		public void EffectVehicleLaunchAllUp() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.ApplyForce(new Vector3(0f, 0f, 50f));
			}
		}

		public void EffectVehicleRepairPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Repair();
		}

		public void EffectVehicleSpawnPolice() {
			World.CreateVehicle(new Model("POLICE"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleTrafficBlack() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.Black;
			}
		}

		public void EffectVehicleTrafficBlue() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.BrightBluePoly3;
			}
		}
		#endregion
	}

	public struct Effect {
		public Effect(string name, Action start, string[] conflicts = null) {
			Name = name;
			Start = start;
			Timer = null;
			Loop = null;
			Stop = null;
			Conflicts = conflicts;
		}

		public Effect(string name, Action start, Timer timer, Action loop, Action stop, string[] conflicts = null) {
			Name = name;
			Timer = timer;
			Start = start;
			Loop = loop;
			Stop = stop;
			Conflicts = conflicts;
		}

		public string Name { get; }
		public Timer Timer { get; }
		public Action Start { get; }
		public Action Loop { get; }
		public Action Stop { get; }
		public string[] Conflicts { get; }
	}
}
