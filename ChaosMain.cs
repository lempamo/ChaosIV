using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GTA;

namespace ChaosIV {
    public class ChaosMain : Script {
		static Random R = new Random();

		Timer EffectTimer;
		List<Action> Loops = new List<Action>();
		List<Effect> Effects = new List<Effect>();
		List<Effect> RecentEffects = new List<Effect>(3);
		List<Vector3> Safehouses = new List<Vector3>() {
			new Vector3(900, -500, 0),  // broker
			new Vector3(595, 1400, 0),  // bohan
			new Vector3(115, 845, 0),   // algonquin - middle park east
			new Vector3(-420, 1490, 0), // algonquin - northwood
			new Vector3(-963, 897, 0),  // alderney
		};
		Dictionary<string, float> Speeds = new Dictionary<string, float>() { //nyoom
			{"ADMIRAL", 140},
			{"AIRTUG", 140},
			{"AMBULAN", 140},
			{"ANNHIL", 160},
			{"BANSHEE", 160},
			{"BENSON", 115},
			{"BIFF", 110},
			{"BLISTA", 150},
			{"BOBBER", 125},
			{"BOBCAT", 125},
			{"BOXVLE", 110},
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
			{"DUKES", 135},
			{"EMPEROR", 130},
			{"EMPEROR2", 130},
			{"ESPERNTO", 120},
			{"FACTION", 140},
			{"FAGGIO", 80},
			{"FBI", 150},
			{"FELTZER", 145},
			{"FEROCI", 140},
			{"FIRETRUK", 140},
			{"FLATBED", 115},
			{"FORTUNE", 143},
			{"FORK", 50},
			{"FUTO", 140},
			{"FXT", 130},
			{"HABANRO", 130},
			{"HAKUMAI", 135},
			{"HELLFURY", 125},
			{"HUNT", 145},
			{"INFERNUS", 160},
			{"INGOT", 130},
			{"INTRUD", 135},
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
			{"NSTOCK", 120},
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
			{"RUSTBOAT", 40},
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
			{"STRATUM", 135},
			{"STRETCH", 140},
			{"SUBWAY", 80},
			{"SULTAN", 145},
			{"SULTANRS", 150},
			{"SUPERGT", 155},
			{"TAXI", 135},
			{"TAXI2", 135},
			{"TOURMAV", 160},
			{"TROPIC", 75},
			{"TRUSH", 100},
			{"TURISMO", 160},
			{"URANUS", 130},
			{"VIGERO", 135},
			{"VIGERO2", 135},
			{"VINCENT", 130},
			{"VIRGO", 125},
			{"VOODOO", 120},
			{"WASHINGT", 135},
			{"WILLARD", 130},
			{"YANKEE", 105},
			{"ZOMB", 125}
		}; 

		Font smol = new Font("Arial", 0.02f, FontScaling.ScreenUnits);

		bool isBlind = false;

		public ChaosMain() {
			Interval = 33;
			Tick += new EventHandler(ChaosLoop);
			PerFrameDrawing += new GraphicsEventHandler(ChaosDraw);

			Effects.Add(new Effect("Invert Current Velocity", EffectMiscInvertVelocity));
			//Effects.Add(new Effect("Zero Gravity", EffectMiscNoGravity, new Timer(28000), null, EffectMiscNormalGravity)); //this one doesn't affect vehicles :/
			Effects.Add(new Effect("Nothing", EffectMiscNothing));

			Effects.Add(new Effect("Everyone Is A Ghost", EffectPedsInvisibleLoop, new Timer(88000), EffectPedsInvisibleLoop, EffectPedsInvisibleStop));
			Effects.Add(new Effect("Ignite All Nearby Peds", EffectPedsIgniteNearby));
			Effects.Add(new Effect("Obliterate All Nearby Peds", EffectPedsObliterateNearby));
			Effects.Add(new Effect("Remove Weapons From Everyone", EffectPedsRemoveWeapons));

			Effects.Add(new Effect("Give Armor", EffectPlayerArmor));
			Effects.Add(new Effect("Bankrupt", EffectPlayerBankrupt));
			Effects.Add(new Effect("Blind", EffectPlayerBlindStart, new Timer(28000), null, EffectPlayerBlindStop));
			Effects.Add(new Effect("It's Time For A Break", EffectPlayerBreakTimeStart, new Timer(28000), null, EffectPlayerBreakTimeStop));
			Effects.Add(new Effect("Exit Current Vehicle", EffectPlayerExitCurrentVehicle));
			Effects.Add(new Effect("Give Grenades", EffectPlayerGiveGrenades));
			Effects.Add(new Effect("Give Rocket Launcher", EffectPlayerGiveRocket));
			Effects.Add(new Effect("Heal Player", EffectPlayerHeal));
			Effects.Add(new Effect("Ignite Player", EffectPlayerIgnite));
			Effects.Add(new Effect("Launch Player Up", EffectPlayerLaunchUp));
			Effects.Add(new Effect("Ragdoll", EffectPlayerRagdoll));
			Effects.Add(new Effect("Randomize Player Outfit", EffectPlayerRandomClothes));
			Effects.Add(new Effect("Remove All Weapons", EffectPlayerRemoveWeapons));
			Effects.Add(new Effect("Set Player Into Random Unoccupied Seat", EffectPlayerSetRandomSeat));
			Effects.Add(new Effect("Set Player Into Closest Vehicle", EffectPlayerSetVehicleClosest));
			Effects.Add(new Effect("Set Player Into Random Vehicle", EffectPlayerSetVehicleRandom));
			Effects.Add(new Effect("Suicide", EffectPlayerSuicide));
			Effects.Add(new Effect("Teleport To Alderney Prison", EffectPlayerTeleportAlderneyPrison));
			Effects.Add(new Effect("Teleport To GetALife Building", EffectPlayerTeleportGetALife));
			Effects.Add(new Effect("Teleport To The Heart Of Liberty City", EffectPlayerTeleportHeart));
			Effects.Add(new Effect("Teleport To Nearest Safehouse", EffectPlayerTeleportNearestSafehouse));
			Effects.Add(new Effect("Clear Wanted Level", EffectPlayerWantedClear));
			Effects.Add(new Effect("Five Wanted Stars", EffectPlayerWantedFiveStars));

			Effects.Add(new Effect("Advance One Day", EffectTimeAdvanceOneDay));
			Effects.Add(new Effect("x0.2 Gamespeed", EffectTimeGameSpeedFifth, new Timer(28000), null, EffectTimeGameSpeedNormal, new[] { "x0.5 Gamespeed" }));
			Effects.Add(new Effect("x0.5 Gamespeed", EffectTimeGameSpeedHalf, new Timer(28000), null, EffectTimeGameSpeedNormal, new[] { "x0.2 Gamespeed" }));
			Effects.Add(new Effect("Set Time To Evening", EffectTimeSetEvening));
			Effects.Add(new Effect("Set Time To Midnight", EffectTimeSetMidnight));
			Effects.Add(new Effect("Set Time To Morning", EffectTimeSetMorning));
			Effects.Add(new Effect("Set Time To Noon", EffectTimeSetNoon));
			Effects.Add(new Effect("Timelapse", EffectTimeLapse, new Timer(88000), EffectTimeLapse, EffectTimeLapse));

			//Effects.Add(new Effect("Break All Doors of Current Vehicle", EffectVehicleBreakDoorsPlayer)); // this one seems to be crashing
			Effects.Add(new Effect("Full Acceleration", EffectVehicleFullAccel, new Timer(28000), EffectVehicleFullAccel, EffectVehicleFullAccel));
			Effects.Add(new Effect("Invisible Vehicles", EffectVehicleInvisibleLoop, new Timer(88000), EffectVehicleInvisibleLoop, EffectVehicleInvisibleStop));
			Effects.Add(new Effect("Kill Engine Of Current Vehicle", EffectVehicleKillEnginePlayer));
			Effects.Add(new Effect("Launch All Vehicles Up", EffectVehicleLaunchAllUp));
			Effects.Add(new Effect("Mass Breakdown", EffectVehicleMassBreakdown));
			Effects.Add(new Effect("Pop Tires Of Current Vehicle", EffectVehiclePopTiresPlayer));
			Effects.Add(new Effect("Repair Current Vehicle", EffectVehicleRepairPlayer));
			Effects.Add(new Effect("Spawn Bus", EffectVehicleSpawnBus));
			Effects.Add(new Effect("Spawn Infernus", EffectVehicleSpawnInfernus));
			Effects.Add(new Effect("Spawn Police Cruiser", EffectVehicleSpawnPolice));
			Effects.Add(new Effect("Spawn Random Vehicle", EffectVehicleSpawnRandom));
			Effects.Add(new Effect("Black Traffic", EffectVehicleTrafficBlack, new Timer(88000), EffectVehicleTrafficBlack, EffectVehicleTrafficBlack, new[] { "Blue Traffic", "Red Traffic" }));
			Effects.Add(new Effect("Blue Traffic", EffectVehicleTrafficBlue, new Timer(88000), EffectVehicleTrafficBlue, EffectVehicleTrafficBlue, new[] { "Black Traffic", "Red Traffic" }));
			Effects.Add(new Effect("Red Traffic", EffectVehicleTrafficBlue, new Timer(88000), EffectVehicleTrafficBlue, EffectVehicleTrafficBlue, new[] { "Black Traffic", "Blue Traffic" }));

			Effects.Add(new Effect("Sunny Weather", EffectWeatherSunny));
			Effects.Add(new Effect("Stormy Weather", EffectWeatherThunder));

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
							RecentEffects.Find(x => x.Name == c).Stop();
							RecentEffects.Find(x => x.Name == c).Timer.Stop();
							Loops.Remove(RecentEffects.Find(x => x.Name == c).Loop);
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
		public void EffectMiscInvertVelocity() {
			if (Player.Character.isInVehicle()) {
				Player.Character.CurrentVehicle.Velocity = new Vector3(Player.Character.CurrentVehicle.Velocity.X * -1, Player.Character.CurrentVehicle.Velocity.Y * -1, Player.Character.CurrentVehicle.Velocity.Z * -1);
			} else {
				Player.Character.Velocity = new Vector3(Player.Character.Velocity.X * -1, Player.Character.Velocity.Y * -1, Player.Character.Velocity.Z * -1);
			}
		}

		public void EffectMiscNoGravity() {
			// floating free...
			// one, two, three...
			// eternity....
			World.GravityEnabled = false;
		}

		public void EffectMiscNormalGravity() {
			World.GravityEnabled = true;
		}

		public void EffectMiscNothing() {
			Game.Console.Print("ok maybe not quite \"nothing\" but eh who cares");
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

		public void EffectPedsIgniteNearby() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.isOnFire = true;
				}
			}
		}

		public void EffectPedsObliterateNearby() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					World.AddExplosion(p.Position);
				}
			}
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character) & !p.isDead) {
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
		public void EffectPlayerArmor() {
			Player.Character.Armor = 100;
		}

		public void EffectPlayerBankrupt() {
			Player.Money = 0;
		}

		public void EffectPlayerBlindStart() {
			isBlind = true;
		}

		public void EffectPlayerBlindStop() {
			isBlind = false;
		}

		public void EffectPlayerBreakTimeStart() {
			Player.CanControlCharacter = false;
		}

		public void EffectPlayerBreakTimeStop() {
			Player.CanControlCharacter = true;
		}

		public void EffectPlayerExitCurrentVehicle() {
			if (Player.Character.isInVehicle()) Player.Character.LeaveVehicle();
		}

		public void EffectPlayerGiveGrenades() {
			Pickup.CreateWeaponPickup(Player.Character.Position, Weapon.Thrown_Grenade, 25).CollectableByCar = true;
		}

		public void EffectPlayerGiveRocket() {
			Pickup.CreateWeaponPickup(Player.Character.Position, Weapon.Heavy_RocketLauncher, 8).CollectableByCar = true;
		}

		public void EffectPlayerHeal() {
			Player.Character.Health = 100;
		}

		public void EffectPlayerIgnite() {
			Player.Character.isOnFire = true;
		}

		public void EffectPlayerLaunchUp() {
			Player.Character.ApplyForce(new Vector3(0f, 0f, 50f));
		}

		public void EffectPlayerRagdoll() {
			Player.Character.isRagdoll = true;
		}

		public void EffectPlayerRandomClothes() {
			Player.Character.RandomizeOutfit();
		}

		public void EffectPlayerRemoveWeapons() {
			Player.Character.Weapons.RemoveAll();
		}

		public void EffectPlayerSetRandomSeat() {
			if (Player.Character.isInVehicle()) {
				Player.Character.WarpIntoVehicle(Player.Character.CurrentVehicle, Player.Character.CurrentVehicle.GetFreeSeat());
			}
		}

		public void EffectPlayerSetVehicleClosest() {
			Vehicle cv = World.GetClosestVehicle(Player.Character.Position, 50f);
			if (cv.Exists()) Player.Character.WarpIntoVehicle(cv, VehicleSeat.Driver);
		}

		public void EffectPlayerSetVehicleRandom() {
			Player.Character.WarpIntoVehicle(World.GetAllVehicles()[R.Next(World.GetAllVehicles().Length)], VehicleSeat.Driver);
		}

		public void EffectPlayerSuicide() {
			Player.Character.Die();
		}

		public void EffectPlayerTeleportAlderneyPrison() {
			Player.TeleportTo(-1000, -400);
		}

		public void EffectPlayerTeleportGetALife() {
			Player.TeleportTo(16, 65);
		}

		public void EffectPlayerTeleportHeart() {
			Player.TeleportTo(new Vector3(-608, -755, 66));
		}

		public void EffectPlayerTeleportNearestSafehouse() {
			float d = 9999;
			Vector3 s = Player.Character.Position;

			foreach (Vector3 v in Safehouses) {
				if (Player.Character.Position.DistanceTo(v) < d) {
					d = Player.Character.Position.DistanceTo(v);
					s = v;
				}
			}

			Player.TeleportTo(s);
		}

		public void EffectPlayerWantedClear() {
			Player.WantedLevel = 0;
		}

		public void EffectPlayerWantedFiveStars() {
			Player.WantedLevel = 5;
		}
		#endregion

		#region Time Effects
		public void EffectTimeAdvanceOneDay() {
			World.OneDayForward();
		}

		public void EffectTimeGameSpeedFifth() {
			Game.TimeScale = 0.2f;
		}

		public void EffectTimeGameSpeedHalf() {
			Game.TimeScale = 0.5f;
		}

		public void EffectTimeGameSpeedNormal() {
			Game.TimeScale = 1f;
		}

		public void EffectTimeLapse() {
			World.UnlockDayTime();
			World.CurrentDayTime = World.CurrentDayTime.Add(new TimeSpan(0, 30, 0));
		}

		public void EffectTimeSetEvening() {
			World.CurrentDayTime = new TimeSpan(World.CurrentDayTime.Days, 18, 0, 0);
		}

		public void EffectTimeSetMidnight() {
			World.CurrentDayTime = new TimeSpan(World.CurrentDayTime.Days, 0, 0, 0);
		}

		public void EffectTimeSetMorning() {
			World.CurrentDayTime = new TimeSpan(World.CurrentDayTime.Days, 6, 0, 0);
		}

		public void EffectTimeSetNoon() {
			World.CurrentDayTime = new TimeSpan(World.CurrentDayTime.Days, 12, 0, 0);
		}
		#endregion

		#region Vehicle Effects
		public void EffectVehicleBreakDoorsPlayer() {
			if (Player.Character.isInVehicle()) {
				//Player.Character.CurrentVehicle.Door(VehicleDoor.Hood).Break();
				Player.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).Open();
				Player.Character.CurrentVehicle.Door(VehicleDoor.LeftFront).Break();
				//Player.Character.CurrentVehicle.Door(VehicleDoor.LeftRear).Break();
				//Player.Character.CurrentVehicle.Door(VehicleDoor.RightFront).Break();
				//Player.Character.CurrentVehicle.Door(VehicleDoor.RightRear).Break();
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

		public void EffectVehicleInvisibleLoop() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Visible = false;
			}
		}
		public void EffectVehicleInvisibleStop() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Visible = true;
			}
		}

		public void EffectVehicleKillEnginePlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.EngineHealth = 0;
		}

		public void EffectVehicleLaunchAllUp() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.ApplyForce(new Vector3(0f, 0f, 50f));
			}
		}

		public void EffectVehicleMassBreakdown() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.EngineHealth = 0;
			}
		}

		public void EffectVehiclePopTiresPlayer() {
			if (Player.Character.isInVehicle()) {
				if (!Player.Character.CurrentVehicle.Model.isBoat | !Player.Character.CurrentVehicle.Model.isHelicopter) {
					Player.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontLeft);
					Player.Character.CurrentVehicle.BurstTire(VehicleWheel.RearLeft);
					if (!Player.Character.CurrentVehicle.Model.isBike) {
						Player.Character.CurrentVehicle.BurstTire(VehicleWheel.FrontRight);
						Player.Character.CurrentVehicle.BurstTire(VehicleWheel.RearRight);
					}
				}
			}
		}

		public void EffectVehicleRepairPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Repair();
		}

		public void EffectVehicleSpawnBus() {
			World.CreateVehicle(new Model("BUS"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnInfernus() {
			World.CreateVehicle(new Model("INFERNUS"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnPolice() {
			World.CreateVehicle(new Model("POLICE"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnRandom() {
			World.CreateVehicle(new Model(Speeds.Keys.ToArray()[R.Next(Speeds.Count)]), Player.Character.Position.Around(2f));
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

		public void EffectVehicleTrafficRed() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.VeryRed;
			}
		}
		#endregion

		#region Weather Effects
		public void EffectWeatherSunny() {
			World.Weather = Weather.ExtraSunny;
		}

		public void EffectWeatherThunder() {
			World.Weather = Weather.ThunderStorm;
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
