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

		bool isBlind = false;
		
		public ChaosMain() {
			Interval = 50;
			Tick += new EventHandler(ChaosLoop);
			PerFrameDrawing += new GraphicsEventHandler(ChaosDraw);

			//Effects.Add(new Effect("Nothing", EffectMiscNothing));
			//Effects.Add(new Effect("Obliterate All Nearby Peds", EffectPedsObliterateNearby));
			//Effects.Add(new Effect("Remove Weapons From Everyone", EffectPedsRemoveWeapons));
			Effects.Add(new Effect("Blind", EffectPlayerBlindStart, new Timer(28000), null, EffectPlayerBlindStop));
			//Effects.Add(new Effect("Exit Current Vehicle", EffectPlayerExitCurrentVehicle));
			//Effects.Add(new Effect("Remove All Weapons", EffectPlayerRemoveWeapons));
			//Effects.Add(new Effect("Break All Doors of Current Vehicle", EffectVehicleBreakDoorsPlayer));
			//Effects.Add(new Effect("Launch All Vehicles Up", EffectVehicleLaunchAllUp));
			//Effects.Add(new Effect("Repair Current Vehicle", EffectVehicleRepairPlayer));

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
			e.Graphics.DrawText("ChaosIV", new RectangleF(0f, 0f, 1f, 0.02f), TextAlignment.Center, Color.FromArgb(20, 20, 20), new Font("Arial", 0.02f, FontScaling.ScreenUnits));
			e.Graphics.DrawRectangle(new RectangleF(0f, 0f, (float)((double)EffectTimer.ElapsedTime / 30000), 0.02f), Color.Yellow);

			// Draw Recent Effects
			if (RecentEffects.Count >= 1) {
				e.Graphics.DrawRectangle(0.5f, 0.05f, (float)(((double)(RecentEffects[0].Timer.Interval - RecentEffects[0].Timer.ElapsedTime) / RecentEffects[0].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(40, 40, 40));
				e.Graphics.DrawText(RecentEffects[0].Name, new RectangleF(0f, 0.04f, 1f, 0.02f), TextAlignment.Center, new Font("Arial", 0.02f, FontScaling.ScreenUnits));
			}

			if (RecentEffects.Count >= 2) {
				e.Graphics.DrawRectangle(0.5f, 0.05f, (float)(((double)(RecentEffects[1].Timer.Interval - RecentEffects[1].Timer.ElapsedTime) / RecentEffects[1].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(40, 40, 40));
				e.Graphics.DrawText(RecentEffects[1].Name, new RectangleF(0f, 0.07f, 1f, 0.02f), TextAlignment.Center, new Font("Arial", 0.02f, FontScaling.ScreenUnits));
			}

			if (RecentEffects.Count == 3) {
				e.Graphics.DrawRectangle(0.5f, 0.05f, (float)(((double)(RecentEffects[2].Timer.Interval - RecentEffects[2].Timer.ElapsedTime) / RecentEffects[2].Timer.Interval) * 0.4), 0.02f, Color.FromArgb(40, 40, 40));
				e.Graphics.DrawText(RecentEffects[2].Name, new RectangleF(0f, 0.1f, 1f, 0.02f), TextAlignment.Center, new Font("Arial", 0.02f, FontScaling.ScreenUnits));
			}
		}

		public void ChaosLoop(object s, EventArgs e) {
			if (Loops.Count > 0) {
				foreach (Action a in Loops) a();
			}
		}

		public void DeployEffect(object s, EventArgs e) {
			Effect next = Effects[R.Next(Effects.Count)];
			next.Start();
			if (next.Timer != null) {
				next.Timer.Tick += new EventHandler(next.Stop);
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
						if (!f.Timer.isRunning) {
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

			EffectTimer.Start();
		}

		// !!! EFFECT METHODS BEGIN BELOW !!! //

		#region Misc Effects
		public void EffectMiscNothing() {
			Console.Out.WriteLine("ok maybe not quite \"nothing\" but eh who cares");
		}
		#endregion

		#region Ped Effects
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

		public void EffectPlayerBlindStop(object s, EventArgs e) {
			isBlind = false;
		}

		public void EffectPlayerExitCurrentVehicle() {
			if (Player.Character.isInVehicle()) Player.Character.LeaveVehicle();
		}

		public void EffectPlayerRemoveWeapons() {
			Player.Character.Weapons.RemoveAll();
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
				Player.Character.CurrentVehicle.Door(VehicleDoor.Trunk).Break();
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
			World.CreateVehicle(new Model("POLICE"), Player.Character.Position.Around(10f));
		}
		#endregion
	}

	public struct Effect {
		public Effect(string name, Action start) {
			Name = name;
			Start = start;
			Timer = null;
			Loop = null;
			Stop = null;
		}

		public Effect(string name, Action start, Timer timer, Action loop, Action<object, EventArgs> stop) {
			Name = name;
			Timer = timer;
			Start = start;
			Loop = loop;
			Stop = stop;
		}

		public string Name { get; }
		public Timer Timer { get; }
		public Action Start { get; }
		public Action Loop { get; }
		public Action<object, EventArgs> Stop { get; }
	}
}
