using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GTA;
using GTA.Native;

namespace ChaosIV {
    public partial class ChaosMain : Script {
		static Random R = new Random();

		Timer EffectTimer;
		List<Action> Loops = new List<Action>();
		List<Effect> Effects = new List<Effect>();
		List<Effect> RecentEffects = new List<Effect>(3);
		List<Vector3> Safehouses = new List<Vector3>() {
			new Vector3(900, -500, 0),  // broker
			new Vector3(592, 1400, 0),  // bohan
			new Vector3(115, 845, 0),   // algonquin - middle park east
			new Vector3(-420, 1490, 0), // algonquin - northwood
			new Vector3(-963, 897, 0),  // alderney
		};

		// the fact that i have to make this instead of using a maxspeed function is very stupid. oh well
		Dictionary<string, float> Speeds = new Dictionary<string, float>() { //nyoom
			{"ADMIRAL", 140}, {"AIRTUG", 140}, {"AKUMA", 150}, {"ALBANY", 140},
			{"AMBULAN", 140}, {"ANGEL", 125}, {"ANNHIL", 160}, {"APC", 80},
			{"AVAN", 100}, {"BANSHEE", 160}, {"BATI", 150}, {"BATI2", 150},
			{"BENSON", 115}, {"BIFF", 110}, {"BLADE", 83}, {"BLISTA", 150},
			{"BOBBER", 125}, {"BOBCAT", 125}, {"BOXVLE", 110}, {"BUCCNEER", 140}, 
			{"BUFFALO", 155}, {"BULLET", 163}, {"BURRITO", 130}, {"BURRITO2", 130},
			{"BUS", 135}, {"BUZZARD", 160}, {"CABBY", 135}, {"CABLECAR", 80},
			{"CADDY", 40}, {"CAVCADE", 135}, {"CHAV", 145}, {"COGNONTI", 150},
			{"COMET", 160}, {"CONTENDE", 135}, {"COQUETTE", 160}, {"DAEMON", 125},
			{"DF8", 150}, {"DIABO", 125}, {"DILANTE", 130}, {"DINGHY", 60},
			{"DOUBLE", 150}, {"DOUBLE2", 160}, {"DUKES", 135}, {"EMPEROR", 130},
			{"EMPEROR2", 130}, {"ESPERNTO", 120}, {"F620", 155}, {"FACTION", 140}, 
			{"FAGGIO", 80}, {"FBI", 150}, {"FELTZER", 145}, {"FEROCI", 140}, 
			{"FEROCI2", 140}, {"FIRETRUK", 140}, {"FLATBED", 115}, {"FLOATER", 80}, 
			{"FORTUNE", 143}, {"FORK", 50}, {"FUTO", 140}, {"FXT", 130},
			{"GBURRITO", 130}, {"HABANRO", 130}, {"HAKUCH", 150}, {"HAKUCH2", 160},
			{"HAKUMAI", 135}, {"HELLFURY", 125}, {"HEXER", 125}, {"HUNT", 145},
			{"INFERNUS", 160}, {"INGOT", 130}, {"INNOV", 125}, {"INTRUD", 135},
			{"JETMAX", 75}, {"LANSTALK", 135}, {"LIMO2", 140}, {"LOKUS", 135},
			{"LYCAN", 125}, {"MANANA", 130}, {"MARBELLA", 130}, {"MARQUI", 30}, 
			{"MAVERICK", 160}, {"MERIT", 140}, {"MINVAN", 130}, {"MOONB", 130}, 
			{"MRTASTY", 110}, {"MULE", 100}, {"NOOSE", 150}, {"NRG900", 150}, 
			{"NSTOCK", 120}, {"ORACLE", 143}, {"PACKER", 105}, {"PATRIOT", 130},
			{"PBUS", 140}, {"PCJ", 140}, {"PEREN", 130}, {"PEREN2", 130},
			{"PEYOTE", 135}, {"PHANTOM", 135}, {"PINCLE", 140}, {"PMP600", 140},
			{"POLICE", 150}, {"POLICE2", 150}, {"POLICE3", 150}, {"POLICE4", 160},
			{"POLICEB", 130}, {"POLMAV", 160}, {"POLPAT", 140}, {"PONY", 115}, 
			{"PREDTOR", 70}, {"PREMIER", 138}, {"PRES", 145}, {"PRIMO", 140}, 
			{"RANCHER", 130}, {"REBLA", 134}, {"REEFER", 40}, {"REGINA", 130},
			{"REVENANT", 130}, {"RHAPSODY", 130}, {"RIPLEY", 70}, {"ROMERO", 120},
			{"RUINER", 150}, {"RUSTBOAT", 40}, {"SABRE", 140}, {"SABRE2", 140},
			{"SABREGT", 145}, {"SANCHEZ", 130}, {"SCHAFTER", 141}, {"SCHAFTE2", 141},
			{"SENTINEL", 150}, {"SERRANO", 145}, {"SKYLIFT", 160}, {"SLAMVAN", 125},
			{"SMUGGLER", 85}, {"SOLAIR", 130}, {"SPEEDO", 125}, {"SQUALO", 70},
			{"STALION", 140}, {"STEED", 100}, {"STRATUM", 135}, {"STRETCH", 140},
			{"SUBWAY", 80}, {"SULTAN", 145}, {"SULTANRS", 150}, {"SUPER", 155},
			{"SUPERD", 160}, {"SUPERD2", 160}, {"SWIFT", 160}, {"TAMPA", 145},
			{"TAXI", 135}, {"TAXI2", 135}, {"TOURMAV", 160}, {"TOWTRUCK", 125},
			{"TROPIC", 75}, {"TRUSH", 100}, {"TURISMO", 160}, {"URANUS", 130},
			{"VADER", 154}, {"VIGERO", 135}, {"VIGERO2", 135}, {"VINCENT", 130},
			{"VIRGO", 125}, {"VOODOO", 120}, {"WASHINGT", 135}, {"WAYFAR", 125},
			{"WILARD", 130}, {"WOLFS", 125}, {"YANKEE", 105}, {"YANKEE2", 130}, {"ZOMB", 125}
		};
		List<string> Vehicles = new List<string>() { 
			"ADMIRAL", "AIRTUG", "AMBULANCE", "ANNIHILATOR", "BANSHEE", "BENSON",
			"BIFF", "BLISTA", "BOBBER", "BOBCAT", "BOXVILLE", "BUCCANEER",
			"BURRITO", "BURRITO2", "BUS", "CABBY", "CAVALCADE", "CHAVOS",
			"COGNOSCENTI", "COMET", "COQUETTE", "DF8", "DILETTANTE", "DINGHY",
			"DUKES", "E109", "EMPEROR", "EMPEROR2", "ESPERANT", "FACTION",
			"FAGGIO", "FBI", "FELTZER", "FEROCI", "FEROCI2", "FIRETRUK",
			"FLATBED", "FORTUNE", "FORKLIFT", "FUTO", "FXT", "HABANERO",
			"HAKUMAI", "HELLFURY", "HUNTLEY", "INFERNUS", "INGOT", "INTRUDER",
			"JETMAX", "LANDSTALKER", "LOKUS", "MANANA", "MARBELLA", "MARQUIS",
			"MAVERICK", "MERIT", "MINIVAN", "MOONBEAM", "MRTASTY", "MULE",
			"NOOSE", "NRG900", "ORACLE", "PACKER", "PATRIOT", "PCJ",
			"PERENNIAL", "PERENNIAL2", "PEYOTE", "PHANTOM", "PINNACLE", 
			"PMP600", "POLICE", "POLICE2", "POLMAV", "POLPATRIOT", "PONY",
			"PREDATOR", "PREMIER", "PRES", "PRIMO", "RANCHER", "REBLA", 
			"REEFER", "RIPLEY", "ROM", "ROMERO", "RUINER", "SABRE",
			"SABRE2", "SABREGT", "SANCHEZ", "SCHAFTER", "SENTINEL", "SOLAIR",
			"SPEEDO", "SQUALO", "STALION", "STEED", "STOCKADE", "STRATUM",
			"STRETCH", "SULTAN", "SULTANRS", "SUPERGT", "TAXI", "TAXI2", 
			"TOURMAV", "TRASH", "TROPIC", "TUGA", "TURISMO", "URANUS",
			"VIGERO", "VIGERO2", "VINCENT", "VIRGO", "VOODOO", "WASHINGTON",
			"WILLARD", "YANKEE", "ZOMBIEB"
		};

		Font smol = new Font("Arial", 0.02f, FontScaling.ScreenUnits);
		AnimationSet hood = new AnimationSet("amb@dance_maleidl_a");
		Color barcolor = Color.Yellow;

		bool isBlind = false;
		bool isDVD = false;
		bool isHUDless = false;
		int lagTicks = 0;

		public ChaosMain() {
			Interval = 16;
			Tick += new EventHandler(ChaosLoop);
			PerFrameDrawing += new GraphicsEventHandler(ChaosDraw);

			if (Game.CurrentEpisode != GameEpisode.GTAIV) {
				Vehicles.AddRange(new string[] {"BATI2", "DOUBLE", "HAKUCHOU", "HEXER", "SLAMVAN", "TAMPA"});

				if (Game.CurrentEpisode == GameEpisode.TLAD) {
					barcolor = Color.Red;

					Vehicles.AddRange(new string[] {"ANGEL", "BATI", "DAEMON", "DIABOLUS", "DOUBLE2", "GBURRITO",
						"HAKUCHOU2", "INNOVATION", "LYCAN", "PBUS", "REGINA", "REVENANT", "RHAPSODY", "TOWTRUCK",
						"WAYFARER", "WOLFSBANE", "YANKEE2"
					});
				} else {
					barcolor = Color.Magenta;

					Vehicles.AddRange(new string[] {"AKUMA", "APC", "AVAN", "BLADE", "BUFFALO", "BULLET",
						"BUZZARD", "CADDY", "F620", "FLOATER", "LIMO2", "POLICE3", "POLICE4", "POLICEB",
						"SCHAFTER2", "SERRANO", "SKYLIFT", "SMUGGLER", "SUPERD", "SUPERD2", "SWIFT", "VADER"
					});
				}
			}

			AddEffects();

			// Settings Time
			var dE = Settings.GetValueString("disabledEffects").Split(',');

			if (dE[0] != "") 
				foreach (string e in dE) {
					Effects.Remove(Effects.Find(x => x.Name == e));
					Game.Console.Print("Disabled effect \"" + e + "\".");
				}


			EffectTimer = new Timer();
			EffectTimer.Tick += new EventHandler(DeployEffect);
			EffectTimer.Interval = 30000;
			EffectTimer.Start();
		}

		public void ChaosDraw(object s, GraphicsEventArgs e) {
			e.Graphics.Scaling = FontScaling.ScreenUnits;

			// Blind
			if (isBlind) e.Graphics.DrawRectangle(new RectangleF(0f, 0f, 1f, 1f), Color.Black);

			// DVD
			if (isDVD) {

			}

			// No HUD
			if (isHUDless) Function.Call("HIDE_HUD_AND_RADAR_THIS_FRAME");

			// Draw Timer Bar
			e.Graphics.DrawRectangle(new RectangleF(0f, 0f, 1f, 0.02f), Color.FromArgb(10, 10, 10));
			e.Graphics.DrawText("ChaosIV", new RectangleF(0f, 0f, 1f, 0.02f), TextAlignment.Center, Color.FromArgb(50, 50, 50), smol);
			e.Graphics.DrawRectangle(new RectangleF(0f, 0f, (float)((double)EffectTimer.ElapsedTime / 30000), 0.02f), barcolor);

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
			Function.Call("SET_MAX_WANTED_LEVEL", 6);

			for (int i = RecentEffects.Count - 1; i >= 0; i--) {
				if (RecentEffects[i].Timer != null) {
					if (RecentEffects[i].Timer.ElapsedTime > RecentEffects[i].Timer.Interval) {
						RecentEffects[i].Stop?.Invoke();
						RecentEffects[i].Timer.Stop();
						Loops.Remove(RecentEffects[i].Loop);
						RecentEffects.RemoveAt(i);
					}
				}
			}

			if (Loops.Count > 0) {
				foreach (Action a in Loops) {
					try { 
						a(); 
					} catch (Exception ex) {
						Game.Console.Print(ex.Message + ex.StackTrace);
					}
			 	}
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
							RecentEffects.Find(x => x.Name == c).Stop?.Invoke();
							RecentEffects.Find(x => x.Name == c).Timer.Stop();
							Loops.Remove(RecentEffects.Find(x => x.Name == c).Loop);
							RecentEffects.Remove(RecentEffects.Find(x => x.Name == c));
						}
					}
				}

				next.Start();
				if (next.Timer != null) next.Timer.Start();
				if (next.Loop != null) Loops.Add(next.Loop);

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
