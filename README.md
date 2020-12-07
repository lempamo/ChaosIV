# ChaosIV

*ChaosIV* is a Chaos mod for Grand Theft Auto IV inspired by similar mods for GTA V and GTA:SA.
The mod has been tested to work with 1.0.4.0 and 1.0.7.0, though it may likely also work with 1.0.8.0 and the Complete Edition. 

A list of implemented effects can be found here: https://docs.google.com/spreadsheets/d/16mKSpQb9KH387viPWK97ejFQ6xchB1lvvpeXNYW66rI/edit?usp=sharing

# Known Issues

* Infinite loop of connects/disconnects. It happen mostly than you have opened multiple tabs with dashboard and/or popout chat. 
So to avoid this open only one instance of dashboard/chat.
* Sometimes FFZ return error `Unable to create poll` (dunno why). So script waiting some time before next try creating poll. 
For that purpose used additional cooldown round. So it's not recommended to use low `ffzPollCooldown` values.

# Installation

1. Install `ASI loader` and `.NET ScriptHook`.
2. For `Complete Edition`, install the Compatibility Patch: https://www.lcpdfr.com/downloads/gta4mods/g17media/26726-compatibility-patch-for-gta-iv-complete-edition/
3. Install [ChaosIVTwitchPollProxy][1].
4. Unzip the [ChaosIV-v.x.x.zip][2] file into your GTA IV installation folder.
5. Adjust settings in `ChaosIV.ini`.
6. Install [FrankerFaceZ][3] chrome extension.
7. Install FFZ `poll-shim` plugin:
![How to add poll-shim plugin](/images/ffz-add-poll-shim.png)

# Start with Twitch Polls

Please note that you must be a Partner/Affiliate to use Twitch polls. 

1. Start and load the game.
2. Open ingame console. It shows loaded scripts:
![Console screenshot](/images/gtaiv-scripthook-console.png)
3. Open Twitch dashboard.
4. Fill out FFZ poll-shim settings: 
![Poll-shim settings](/images/ffz-poll-shim-settings.png)
5. Return to the game and wait for FFZ connection.
First cycle after connection and authentication is cooldown round.
6. Play

## How to Contribute
Make sure you have your `ASI loader` and `.NET ScriptHook` installed, and then clone the repo into the `for Developers` folder within the `scripts` folder.

When you build the mod (or it crashes), you don't need to restart the game. Open the console (`~` button) and type `reloadscripts`. It reloads and start scripts from the disk.
Type `abortscripts` to stop scripts.


[1]: https://github.com/shtrih/ChaosIVTwitchPollProxy/releases
[2]: https://github.com/shtrih/ChaosIV/releases
[3]: https://chrome.google.com/webstore/detail/frankerfacez/fadndhdgpmmaapbmfcknlfgcflmmmieb