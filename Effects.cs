using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using GTA.Native;

namespace ChaosIV {
	public partial class ChaosMain {
		public void AddEffects() {
			Effects.Add(new Effect("Earthquake", EffectMiscEarthquake, new Timer(28000), EffectMiscEarthquake, EffectMiscEarthquake));
			Effects.Add(new Effect("Invert Current Velocity", EffectMiscInvertVelocity));
			Effects.Add(new Effect("No HUD", EffectMiscNoHUD, new Timer(88000), null, EffectMiscShowHUD));
			Effects.Add(new Effect("Nothing", EffectMiscNothing));
			Effects.Add(new Effect("One Bullet Mags", EffectMiscOneBulletMags, new Timer(88000), EffectMiscOneBulletMags, null));
			Effects.Add(new Effect("Spawn Jet", EffectMiscSpawnJet));
			Effects.Add(new Effect("SPEEN", EffectMiscSPEEN, new Timer(28000), EffectMiscSPEEN, EffectMiscSPEEN));

			Effects.Add(new Effect("Aimbot Peds", EffectPedsAimbot, new Timer(88000), EffectPedsAimbot, null));
			Effects.Add(new Effect("Everyone Exits Their Vehicles", EffectPedsAllExitVehs));
			Effects.Add(new Effect("All Nearby Peds Are Wanted", EffectPedsAllNearbyWanted));
			Effects.Add(new Effect("Peds Don't See Very Well", EffectPedsBlindLoop, new Timer(88000), EffectPedsBlindLoop, EffectPedsBlindStop));
			Effects.Add(new Effect("In The Hood", EffectPedsDance, new Timer(88000), EffectPedsDance, EffectPedsWander));
			Effects.Add(new Effect("Explosive Peds", EffectPedsExplosive, new Timer(88000), EffectPedsExplosive, EffectPedsExplosive));
			Effects.Add(new Effect("All Nearby Peds Flee", EffectPedsFlee));
			Effects.Add(new Effect("You Are Famous", EffectPedsFollow));
			Effects.Add(new Effect("Give Everyone A Rocket Launcher", EffectPedsGiveAllRocket));
			Effects.Add(new Effect("Everyone Is Invincible", EffectPedsInvincibleLoop, new Timer(88000), EffectPedsInvincibleLoop, EffectPedsInvincibleStop));
			Effects.Add(new Effect("Everyone Is A Ghost", EffectPedsInvisibleLoop, new Timer(88000), EffectPedsInvisibleLoop, EffectPedsInvisibleStop));
			Effects.Add(new Effect("Ignite All Nearby Peds", EffectPedsIgniteNearby));
			Effects.Add(new Effect("Launch All Nearby Peds Up", EffectPedsLaunch));
			Effects.Add(new Effect("No Headshots", EffectPedsNoHeadshotsLoop, new Timer(88000), EffectPedsNoHeadshotsLoop, EffectPedsNoHeadshotsStop));
			Effects.Add(new Effect("No Ragdoll", EffectPedsNoRagdollLoop, new Timer(88000), EffectPedsNoRagdollLoop, EffectPedsNoRagdollStop));
			Effects.Add(new Effect("Obliterate All Nearby Peds", EffectPedsObliterateNearby));
			Effects.Add(new Effect("One Hit KO", EffectPedsOHKO, new Timer(28000), EffectPedsOHKO, EffectPedsOHKOStop));
			Effects.Add(new Effect("Remove Weapons From Everyone", EffectPedsRemoveWeapons));
			Effects.Add(new Effect("Revive Dead Peds", EffectPedsReviveDead));
			Effects.Add(new Effect("Scooter Brothers", EffectPedsScooterBros));
			//Effects.Add(new Effect("Set All Peds Into Random Vehicles", EffectPedsShuffle)); fucking ghost cars man
			Effects.Add(new Effect("Spawn An Angry Doctor Who Wants To Knife You", EffectPedsSpawnAngryDoctor));

			Effects.Add(new Effect("Give Armor", EffectPlayerArmor));
			Effects.Add(new Effect("Bankrupt", EffectPlayerBankrupt));
			Effects.Add(new Effect("Blind", EffectPlayerBlindStart, new Timer(28000), null, EffectPlayerBlindStop));
			Effects.Add(new Effect("It's Time For A Break", EffectPlayerBreakTime, new Timer(28000), EffectPlayerBreakTime, EffectPlayerBreakTimeStop));
			Effects.Add(new Effect("Exit Current Vehicle", EffectPlayerExitCurrentVehicle));
			Effects.Add(new Effect("Give All Weapons", EffectPlayerGiveAll));
			Effects.Add(new Effect("Give Grenades", EffectPlayerGiveGrenades));
			Effects.Add(new Effect("Give Molotov Cocktails", EffectPlayerGiveMolotovs));
			Effects.Add(new Effect("Give Rocket Launcher", EffectPlayerGiveRocket));
			Effects.Add(new Effect("Heal Player", EffectPlayerHeal));
			Effects.Add(new Effect("Ignite Player", EffectPlayerIgnite));
			Effects.Add(new Effect("Invincibility", EffectPlayerInvincible, new Timer(88000), EffectPlayerInvincible, EffectPlayerInvincibleStop));
			Effects.Add(new Effect("Launch Player Up", EffectPlayerLaunchUp));
			Effects.Add(new Effect("Millionaire", EffectPlayerMillionaire));
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
			Effects.Add(new Effect("Teleport To Swingset", EffectPlayerTeleportSwingset));
			Effects.Add(new Effect("Teleport To Waypoint", EffectPlayerTeleportWaypoint));
			Effects.Add(new Effect("+2 Wanted Stars", EffectPlayerWantedAddTwo));
			Effects.Add(new Effect("Clear Wanted Level", EffectPlayerWantedClear));
			Effects.Add(new Effect("Five Wanted Stars", EffectPlayerWantedFiveStars));
			Effects.Add(new Effect("Never Wanted", EffectPlayerWantedClear, new Timer(88000), EffectPlayerWantedClear, EffectPlayerWantedClear));

			Effects.Add(new Effect("Advance One Day", EffectTimeAdvanceOneDay));
			Effects.Add(new Effect("x0.2 Gamespeed", EffectTimeGameSpeedFifth, new Timer(7000), null, EffectTimeGameSpeedNormal, new[] { "x0.5 Gamespeed", "Lag" }));
			Effects.Add(new Effect("x0.5 Gamespeed", EffectTimeGameSpeedHalf, new Timer(14000), null, EffectTimeGameSpeedNormal, new[] { "x0.2 Gamespeed", "Lag" }));
			Effects.Add(new Effect("Lag", EffectTimeLagStart, new Timer(28000), EffectTimeLagLoop, EffectTimeGameSpeedNormal, new[] { "x0.2 Gamespeed", "x0.5 Gamespeed" }));
			Effects.Add(new Effect("Set Time To Evening", EffectTimeSetEvening));
			Effects.Add(new Effect("Set Time To Midnight", EffectTimeSetMidnight));
			Effects.Add(new Effect("Set Time To Morning", EffectTimeSetMorning));
			Effects.Add(new Effect("Set Time To Noon", EffectTimeSetNoon));
			Effects.Add(new Effect("Timelapse", EffectTimeLapse, new Timer(88000), EffectTimeLapse, EffectTimeLapse));

			Effects.Add(new Effect("Break All Doors of Current Vehicle", EffectVehicleBreakDoorsPlayer));
			Effects.Add(new Effect("Clean Current Vehicle", EffectVehicleCleanPlayer));
			Effects.Add(new Effect("Explode Nearby Vehicles", EffectVehicleExplodeNearby));
			Effects.Add(new Effect("Explode Current Vehicle", EffectVehicleExplodePlayer));
			Effects.Add(new Effect("Flip Current Vehicle", EffectVehicleFlipPlayer));
			Effects.Add(new Effect("Full Acceleration", EffectVehicleFullAccel, new Timer(28000), EffectVehicleFullAccel, EffectVehicleFullAccel));
			Effects.Add(new Effect("Invisible Vehicles", EffectVehicleInvisibleLoop, new Timer(88000), EffectVehicleInvisibleLoop, EffectVehicleInvisibleStop, new[] { "Black Traffic", "Blue Traffic", "Red Traffic", "White Traffic", "Green Traffic" }));
			Effects.Add(new Effect("Jumpy Vehicles", EffectVehicleJumpy, new Timer(28000), EffectVehicleJumpy, EffectVehicleJumpy));
			Effects.Add(new Effect("Kill Engine Of Current Vehicle", EffectVehicleKillEnginePlayer));
			Effects.Add(new Effect("Launch All Vehicles Up", EffectVehicleLaunchAllUp));
			Effects.Add(new Effect("Lock All Vehicles", EffectVehicleLockAll));
			Effects.Add(new Effect("Lock Vehicle Player Is In", EffectVehicleLockPlayer));
			Effects.Add(new Effect("Mass Breakdown", EffectVehicleMassBreakdown));
			Effects.Add(new Effect("No Traffic", EffectVehicleTrafficNone, new Timer(88000), EffectVehicleTrafficNone, null));
			Effects.Add(new Effect("Pop Tires Of Current Vehicle", EffectVehiclePopTiresPlayer));
			Effects.Add(new Effect("Remove Current Vehicle", EffectVehicleRemovePlayer));
			Effects.Add(new Effect("Repair Current Vehicle", EffectVehicleRepairPlayer));
			Effects.Add(new Effect("Spammy Vehicle Doors", EffectVehicleSpamDoors, new Timer(88000), EffectVehicleSpamDoors, null));
			Effects.Add(new Effect("Spawn Bus", EffectVehicleSpawnBus));
			Effects.Add(new Effect("Spawn Faggio", EffectVehicleSpawnFaggio));
			Effects.Add(new Effect("Spawn Infernus", EffectVehicleSpawnInfernus));
			Effects.Add(new Effect("Spawn Maverick", EffectVehicleSpawnMaverick));
			Effects.Add(new Effect("Spawn Police Cruiser", EffectVehicleSpawnPolice));
			Effects.Add(new Effect("Spawn Random Vehicle", EffectVehicleSpawnRandom));
			Effects.Add(new Effect("Need A Cab?", EffectVehicleSpawnTaxis));
			Effects.Add(new Effect("Spawn Tug", EffectVehicleSpawnTug));
			Effects.Add(new Effect("Black Traffic", EffectVehicleTrafficBlack, new Timer(88000), EffectVehicleTrafficBlack, EffectVehicleTrafficBlack, new[] { "Invisible Vehicles", "Blue Traffic", "Red Traffic", "White Traffic", "Green Traffic" }));
			Effects.Add(new Effect("Blue Traffic", EffectVehicleTrafficBlue, new Timer(88000), EffectVehicleTrafficBlue, EffectVehicleTrafficBlue, new[] { "Invisible Vehicles", "Black Traffic", "Red Traffic", "White Traffic", "Green Traffic" }));
			Effects.Add(new Effect("Green Traffic", EffectVehicleTrafficGreen, new Timer(88000), EffectVehicleTrafficGreen, EffectVehicleTrafficGreen, new[] { "Invisible Vehicles", "Black Traffic", "Blue Traffic", "Red Traffic", "White Traffic" }));
			Effects.Add(new Effect("Red Traffic", EffectVehicleTrafficRed, new Timer(88000), EffectVehicleTrafficRed, EffectVehicleTrafficRed, new[] { "Invisible Vehicles", "Black Traffic", "Blue Traffic", "White Traffic", "Green Traffic" }));
			Effects.Add(new Effect("White Traffic", EffectVehicleTrafficWhite, new Timer(88000), EffectVehicleTrafficWhite, EffectVehicleTrafficWhite, new[] { "Invisible Vehicles", "Black Traffic", "Blue Traffic", "Red Traffic", "Green Traffic" }));

			Effects.Add(new Effect("Cloudy Weather", EffectWeatherCloudy));
			Effects.Add(new Effect("Foggy Weather", EffectWeatherFoggy));
			Effects.Add(new Effect("Sunny Weather", EffectWeatherSunny));
			Effects.Add(new Effect("Stormy Weather", EffectWeatherThunder));
		}

		// !!! EFFECT METHODS BEGIN BELOW !!! //

		#region Misc Effects
		public void EffectMiscEarthquake() {
			Vector3 q = new Vector3(0, 0, R.Next(-9, 7));
			foreach (Vehicle v in World.GetAllVehicles()) {
				try {
					if (v.Exists()) v.ApplyForce(q);
				} catch (NonExistingObjectException) {
					continue;
				}
			}
			foreach (Ped p in World.GetAllPeds()) {
				try {
					if (p.Exists()) p.ApplyForce(q);
				} catch (NonExistingObjectException) {
					continue;
				}
			}
			foreach (GTA.Object o in World.GetAllObjects()) {
				try {
					if (o.Exists()) o.ApplyForce(q);
				} catch (NonExistingObjectException) {
					continue;
				}
			}
		}

		public void EffectMiscInvertVelocity() {
			if (Player.Character.isInVehicle()) {
				Player.Character.CurrentVehicle.Velocity = new Vector3(Player.Character.CurrentVehicle.Velocity.X * -1, Player.Character.CurrentVehicle.Velocity.Y * -1, Player.Character.CurrentVehicle.Velocity.Z * -1);
			} else {
				Player.Character.Velocity = new Vector3(Player.Character.Velocity.X * -1, Player.Character.Velocity.Y * -1, Player.Character.Velocity.Z * -1);
			}
		}

		public void EffectMiscNoHUD() {
			isHUDless = true;
			Function.Call("DISPLAY_HUD", false);
			Function.Call("DISPLAY_RADAR", false);
		}

		public void EffectMiscNothing() {
			Game.Console.Print("ok maybe not quite \"nothing\" but eh who cares");
		}

		public void EffectMiscOneBulletMags() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & p.Weapons.Current.Slot != WeaponSlot.Melee) {
					if (p.Weapons.Current.AmmoInClip > 1) {
						int a = p.Weapons.Current.AmmoInClip - 1;
						p.Weapons.Current.AmmoInClip = 1;
						p.Weapons.Current.Ammo += a;
					}
				}
			}
		}

		public void EffectMiscShowHUD() {
			isHUDless = false;
			Function.Call("DISPLAY_HUD", true);
			Function.Call("DISPLAY_RADAR", true);
		}

		public void EffectMiscSpawnJet() {
			World.CreateObject("ec_jet", Player.Character.Position.Around(2f));
		}

		public void EffectMiscSPEEN() {
			foreach (GTA.Object o in World.GetAllObjects()) {
				try {
					if (o.Exists()) o.Heading += 5f;
				} catch (NonExistingObjectException) {
					continue;
				}
			}
			foreach (Vehicle v in World.GetAllVehicles()) {
				try {
					if (v.Exists()) {
						v.Heading += 5f;
						if (v.Heading >= 355f) v.Heading = 0f;
						v.ApplyForce(new Vector3(0f, 0f, 0.1f));
					}
				} catch (NonExistingObjectException) {
					continue;
				}
			}
			foreach (Ped p in World.GetAllPeds()) {
				try {
					if (p.Exists() & (p != Player.Character)) {
						p.Heading += 5f;
					}
				} catch (NonExistingObjectException) {
					continue;
				}
			}
		}
		#endregion

		#region Ped Effects
		public void EffectPedsAimbot() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & p != Player.Character) {
					p.Accuracy = 100;
				}
			}
		}

		public void EffectPedsAllExitVehs() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.EveryoneLeaveVehicle();
			}
		}

		public void EffectPedsAllNearbyWanted() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character) & (p.PedType != PedType.Cop)) {
					p.WantedByPolice = true;
				}
			}
		}

		public void EffectPedsBlindLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.SenseRange = 0f;
				}
			}
		}

		public void EffectPedsBlindStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.SenseRange = 50f;
				}
			}
		}

		public void EffectPedsDance() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					if (!p.Animation.isPlaying(hood, "loop_a")) p.Task.PlayAnimation(hood, "loop_a", 1f);
					p.Task.AlwaysKeepTask = true;
				}
			}
		}

		public void EffectPedsExplosive() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					if (p.isRagdoll & !p.isDead) World.AddExplosion(p.Position);
				}
			}
		}

		public void EffectPedsFlee() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.Task.FleeFromChar(Player.Character);
					p.Task.AlwaysKeepTask = true;
				}
			}
		}

		public void EffectPedsFollow() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.LeaveVehicle();
					p.Task.GoTo(Player.Character);
					p.Task.AlwaysKeepTask = true;
				}
			}
		}

		public void EffectPedsGiveAllRocket() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					Function.Call("GIVE_WEAPON_TO_CHAR", p, 18, 9999);
					p.Weapons.Select(Weapon.Heavy_RocketLauncher);
				}
			}
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 18, 9999);
		}

		public void EffectPedsInvincibleLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Invincible = true;
			}
		}

		public void EffectPedsInvincibleStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Invincible = false;
			}
		}

		public void EffectPedsInvisibleLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Visible = false;
			}
		}

		public void EffectPedsInvisibleStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Visible = true;
			}
		}

		public void EffectPedsIgniteNearby() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.isOnFire = true;
				}
			}
		}

		public void EffectPedsLaunch() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					p.ApplyForce(new Vector3(0f, 0f, 250f));
				}
			}
		}

		public void EffectPedsNoHeadshotsLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) Function.Call("SET_CHAR_SUFFERS_CRITICAL_HITS", p, false);
			}
		}

		public void EffectPedsNoHeadshotsStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) Function.Call("SET_CHAR_SUFFERS_CRITICAL_HITS", p, true);
			}
		}

		public void EffectPedsNoRagdollLoop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.PreventRagdoll = true;
			}
		}

		public void EffectPedsNoRagdollStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.PreventRagdoll = false;
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

		public void EffectPedsOHKO() {
			foreach (Ped p in World.GetAllPeds()) {
				try {
					if (p.Exists() && !p.isDead && p.Health > 5 && p != Player) {
						p.Health = 5;
						p.Armor = 0;
					}
					if (p == Player && p.Health > 5) {
						p.Health = 1;
						p.Armor = 0;
					}
				} catch (NonExistingObjectException) {
					continue;
				}
			}
		}

		public void EffectPedsOHKOStop() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & !p.isDead) {
					p.Health = 100;
				}
			}
		}

		public void EffectPedsRemoveWeapons() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists()) p.Weapons.RemoveAll();
			}
		}

		public void EffectPedsReviveDead() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & p.isDead) {
					World.CreatePed(p.Model, p.Position, p.RelationshipGroup).NoLongerNeeded();
					p.Delete();
				}
			}
		}

		public void EffectPedsScooterBros() {
			Dictionary<Ped, Vehicle> scoots = new Dictionary<Ped, Vehicle>();

			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() && p != Player && !p.isDead) {
					if (p.isSittingInVehicle()) {
						if (p.CurrentVehicle.Name == "FAGGIO") continue;
					}

					Vector3 tp = p.Position;

					if (p.isInVehicle()) p.LeaveVehicle();
					p.Position = new Vector3(p.Position.X, p.Position.Y, p.Position.Z + 5f);

					scoots.Add(p, World.CreateVehicle(new Model("FAGGIO"), tp));
				}
			}

			Wait(200);

			foreach (KeyValuePair<Ped, Vehicle> pv in scoots) {
				try {
					pv.Key.WarpIntoVehicle(pv.Value, VehicleSeat.Driver);
					Function.Call("TASK_CAR_MISSION_PED_TARGET", pv.Key, pv.Value, Player.Character, 2, 80f, 2, 0, 10);
				} catch (NonExistingObjectException) {
					continue;
				}
			}
		}

		public void EffectPedsShuffle() {
			foreach (Ped p in World.GetAllPeds()) if (p.Exists()) p.Position = new Vector3(p.Position.X, p.Position.Y, p.Position.Z + 3f);

			foreach (Ped p in World.GetAllPeds()) {
				if (!p.Exists()) continue;

				List<Vehicle> freeVs = new List<Vehicle>();

				foreach (Vehicle v in World.GetAllVehicles()) if (v.Exists() && v.GetFreeSeat() != VehicleSeat.None && !Function.Call<bool>("IS_CAR_WAITING_FOR_WORLD_COLLISION", v)) freeVs.Add(v);

				Vehicle sV = freeVs[R.Next(freeVs.Count)];

				for (int s = -1; s < sV.PassengerSeats; s++) {
					if (sV.isSeatFree((VehicleSeat)s)) {
						Game.Console.Print("warping ped" + p.Model.Hash + " into vehicleseat" + s);
						p.WarpIntoVehicle(sV, (VehicleSeat)s);
						break;
					}
				}
			}
		}

		public void EffectPedsSpawnAngryDoctor() {
			var doc = World.CreatePed(new Model("m_m_dodgydoc"), Player.Character.Position.Around(5f), RelationshipGroup.Criminal);
			doc.Task.FightAgainst(Player.Character);
			doc.Task.AlwaysKeepTask = true;
			Function.Call("GIVE_WEAPON_TO_CHAR", doc, 3, 9999);
			doc.Weapons.Select(Weapon.Melee_Knife);
		}

		public void EffectPedsWander() {
			foreach (Ped p in World.GetAllPeds()) {
				if (p.Exists() & (p != Player.Character)) {
					if (p.isInVehicle()) p.Task.CruiseWithVehicle(p.CurrentVehicle, 30f, true);
					else p.Task.WanderAround();
					p.Task.AlwaysKeepTask = false;
				}
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

		public void EffectPlayerBreakTime() {
			Player.CanControlCharacter = false;
		}

		public void EffectPlayerBreakTimeStop() {
			Player.CanControlCharacter = true;
		}

		public void EffectPlayerExitCurrentVehicle() {
			if (Player.Character.isInVehicle()) Player.Character.LeaveVehicle();
		}

		public void EffectPlayerGiveAll() {
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 3, 9999); // knife
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 7, 9999); // pistol
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, R.Next(12, 13), 9999); // smg
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, R.Next(10, 11), 9999); // shotgun
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, R.Next(14, 15), 9999); // rifle
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, R.Next(16, 17), 9999); // sniper
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 18, 9999); // rocket launcher
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, R.Next(4, 5), 9999); // grenades or molotov
		}

		public void EffectPlayerGiveGrenades() {
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 4, 9999);
		}

		public void EffectPlayerGiveMolotovs() {
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 5, 9999);
		}

		public void EffectPlayerGiveRocket() {
			Function.Call("GIVE_WEAPON_TO_CHAR", Player.Character, 18, 9999);
		}

		public void EffectPlayerHeal() {
			Player.Character.Health = 100;
		}

		public void EffectPlayerIgnite() {
			if (Player.Character.isInVehicle()) {
				Player.Character.CurrentVehicle.EngineHealth = -10;
				Player.Character.CurrentVehicle.EngineRunning = true;
				Player.Character.CurrentVehicle.isOnFire = true;
				Player.Character.CurrentVehicle.EngineRunning = true;
			} else Player.Character.isOnFire = true;
		}

		public void EffectPlayerInvincible() {
			Player.Character.Invincible = true;
		}

		public void EffectPlayerInvincibleStop() {
			Player.Character.Invincible = false;
		}

		public void EffectPlayerLaunchUp() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.ApplyForce(new Vector3(0f, 0f, 50f));
			else Player.Character.ApplyForce(new Vector3(0f, 0f, 250f));
		}

		public void EffectPlayerMillionaire() {
			Player.Money += 1000000;
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
			if (cv != null && cv.Exists()) Player.Character.WarpIntoVehicle(cv, VehicleSeat.Driver);
		}

		public void EffectPlayerSetVehicleRandom() {
			Vehicle rv = World.GetAllVehicles()[R.Next(World.GetAllVehicles().Length)];
			if (rv != null && rv.Exists()) Player.Character.WarpIntoVehicle(rv, VehicleSeat.Driver);
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

		public void EffectPlayerTeleportSwingset() {
			Player.TeleportTo(new Vector3(1354, -256, 22));
		}

		public void EffectPlayerTeleportWaypoint() {
			if (Game.GetWaypoint() != null) Player.TeleportTo(Game.GetWaypoint().Position);
			else {
				foreach (Blip b in Blip.GetAllBlipsOfType(BlipType.Contact).Concat(Blip.GetAllBlipsOfType(BlipType.Coordinate)).Concat(Blip.GetAllBlipsOfType(BlipType.Ped))) {
					if (b.Icon == BlipIcon.Misc_Destination || b.Icon == BlipIcon.Misc_Destination1 || b.Icon == BlipIcon.Misc_Destination2) {
						Player.TeleportTo(b.Position);
						break;
					}
				}
			}
		}

		public void EffectPlayerWantedAddTwo() {
			Player.WantedLevel += 2;
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

		public void EffectTimeLagStart() {
			lagTicks = 0;
		}

		public void EffectTimeLagLoop() {
			lagTicks++;

			if (lagTicks < 50) Game.TimeScale = 1f;
			else Game.TimeScale = 0.05f;

			if (lagTicks >= 60) lagTicks = 0;
		}

		public void EffectTimeLapse() {
			World.UnlockDayTime();
			World.CurrentDayTime = World.CurrentDayTime.Add(new TimeSpan(0, 10, 0));
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
				for (int d = 0; d < 6; d++) Function.Call("BREAK_CAR_DOOR", Player.Character.CurrentVehicle, d, false);
			}
		}

		public void EffectVehicleCleanPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Wash();
		}

		public void EffectVehicleExplodeNearby() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists() & v != Player.Character.CurrentVehicle) v.Explode();
			}
		}

		public void EffectVehicleExplodePlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Explode();
		}

		public void EffectVehicleFlipPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Rotation = new Vector3(Player.Character.CurrentVehicle.Rotation.X, 180, Player.Character.CurrentVehicle.Rotation.Z);
		}

		public void EffectVehicleFullAccel() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				try {
					if (v.Exists() && v.isOnAllWheels) {
						Game.Console.Print(v.Name);
						v.Speed = Speeds[v.Name];
					}
				} catch (NonExistingObjectException) {
					continue;
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

		public void EffectVehicleJumpy() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				try {
					if (v.Exists() & v.isOnAllWheels) v.ApplyForce(new Vector3(0f, 0f, 1f));
				} catch (NonExistingObjectException) {
					continue;
				}
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

		public void EffectVehicleLockAll() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.DoorLock = DoorLock.ImpossibleToOpen;
			}
		}

		public void EffectVehicleLockPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.DoorLock = DoorLock.ImpossibleToOpen;
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

		public void EffectVehicleRemovePlayer() {
			if (Player.Character.isInVehicle()) {
				List<Ped> vPeds = new List<Ped>();
				Vehicle v = Player.Character.CurrentVehicle;

				for (int s = -1; s < v.PassengerSeats; s++) {
					if (v.isSeatFree((VehicleSeat)s)) continue;

					Ped p = v.GetPedOnSeat((VehicleSeat)s);
					Function.Call("CLEAR_CHAR_TASKS_IMMEDIATELY", p);
					Function.Call("SWITCH_PED_TO_RAGDOLL", p, 5000, 5000, 0, true, true, false);

					vPeds.Add(p);
				}

				Vector3 vVel = v.Velocity;
				v.isRequiredForMission = false;
				v.Delete();

				foreach (Ped p in vPeds) p.Velocity = vVel;
			}
		}

		public void EffectVehicleRepairPlayer() {
			if (Player.Character.isInVehicle()) Player.Character.CurrentVehicle.Repair();
		}

		public void EffectVehicleSpamDoors() {
			if (Math.Round(EffectTimer.ElapsedTime / 100d, 0) % 10 == 0) foreach (Vehicle v in World.GetAllVehicles()) if (v.Exists()) for (int i = 0; i < 6; i++) Function.Call("SHUT_CAR_DOOR", v, i);
			if (Math.Round((EffectTimer.ElapsedTime + 500) / 100d, 0) % 10 == 0) foreach (Vehicle v in World.GetAllVehicles()) if (v.Exists()) for (int i = 0; i < 6; i++) Function.Call("OPEN_CAR_DOOR", v, i);
		}

		public void EffectVehicleSpawnBus() {
			World.CreateVehicle(new Model("BUS"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnFaggio() {
			World.CreateVehicle(new Model("FAGGIO"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnInfernus() {
			World.CreateVehicle(new Model("INFERNUS"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnMaverick() {
			World.CreateVehicle(new Model("MAVERICK"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnPolice() {
			World.CreateVehicle(new Model("POLICE"), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnRandom() {
			World.CreateVehicle(new Model(Vehicles[R.Next(Vehicles.Count)]), Player.Character.Position.Around(2f));
		}

		public void EffectVehicleSpawnTaxis() { // this one's for the speedrunners out there :^)
			for (int i = 0; i < 10; i++) {
				World.CreateVehicle(new Model("TAXI"), Player.Character.Position.Around((float)R.NextDouble() * 8f)).CreatePedOnSeat(VehicleSeat.Driver, new Model("m_m_taxidriver"));
			}
		}

		public void EffectVehicleSpawnTug() {
			World.CreateVehicle(new Model("TUGA"), Player.Character.Position.Around(2f));
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

		public void EffectVehicleTrafficGreen() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.SecuricorDarkGreen;
			}
		}

		public void EffectVehicleTrafficNone() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				try {
					if (v.Exists() & !v.isRequiredForMission & v != Player.Character.CurrentVehicle) v.Delete();
				} catch (NonExistingObjectException) {
					continue;
				}
			}
		}

		public void EffectVehicleTrafficRed() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.VeryRed;
			}
		}

		public void EffectVehicleTrafficWhite() {
			foreach (Vehicle v in World.GetAllVehicles()) {
				if (v.Exists()) v.Color = ColorIndex.FrostWhite;
			}
		}
		#endregion

		#region Weather Effects
		public void EffectWeatherCloudy() {
			Function.Call("FORCE_WEATHER_NOW", 3);
		}

		public void EffectWeatherFoggy() {
			Function.Call("FORCE_WEATHER_NOW", 6);
		}

		public void EffectWeatherSunny() {
			Function.Call("FORCE_WEATHER_NOW", 0);
		}

		public void EffectWeatherThunder() {
			Function.Call("FORCE_WEATHER_NOW", 7);
		}
		#endregion
	}
}
