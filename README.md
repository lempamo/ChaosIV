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

1. Install [`ASI loader`][4] (dinput8.dll) and [`.NET ScriptHook`][5] (for Steam game version [v1.7.1.7b][6] worked for me).
2. For `Complete Edition`, install the Compatibility Patch: https://www.lcpdfr.com/downloads/gta4mods/g17media/26726-compatibility-patch-for-gta-iv-complete-edition/
3. Install [ChaosIVTwitchPollProxy][1].
4. Unzip the [ChaosIV-v.x.x.zip][2] file into your GTA IV installation folder.
5. Adjust settings in `ChaosIV.ini`.
6. Install [FrankerFaceZ][3] Chrome extension.
7. Install FFZ `poll-shim` plugin:
![How to add poll-shim plugin](/images/ffz-add-poll-shim.png)

# Start with Twitch Polls

Please note that you must be a Partner/Affiliate to use Twitch polls. 

1. Open Twitch dashboard.
2. Fill out FFZ poll-shim settings: 
![Poll-shim settings](/images/ffz-poll-shim-settings.png)
3. Make sure poll-shim is active:
![Poll-shim settings](/images/ffz-poll-shim-active.png)
4. Start and load the game.
5. Open ingame console (`~`). It shows loaded scripts:
![Console screenshot](/images/gtaiv-scripthook-console.png)
6. Wait for FFZ connection.
First cycle after connection and authentication is cooldown round.
7. Play. Use `reloadscripts` console command to fix disconnects loop.

• If you have problems like you _can't open FFZ in dashboard (no ffz icon)_ or _`poll-shim` is not active on dashboard_ then try using the following URL: 
`https://www.twitch.tv/popout/YOURUSERNAME/chat?ffz-settings=add_ons.poll_shim` (replace YOURUSERNAME with your own username). 

# OBS Poll Widget

Also you can add the [Twitch Poll overlay][7] which allow you to display the currently active Poll on your channel onto your stream.
Here CSS make widget visually similar with ChaosIV (in source settings `width`: `360px`):
```css

:root, #root {
    --color-background-body: transparent;
    --border-radius-large: 0em;
    --color-background-overlay: transparent;
    --progress-background-color: transparent;
    --progress-winner-color: rgb(0 160 0 / 0.6);
    --progress-votes-color: rgb(255 255 0 / 0.5);
}

#root p.tw-c-text-overlay-alt {
    display: none !important;
}

#root p.tw-c-text-overlay.tw-font-size-4.tw-line-height-heading.tw-strong.tw-word-break-word {
    display: none !important;
}

#root .tw-pd-b-05.tw-pd-t-05 {
    padding-bottom: 0.1rem !important;
    padding-top: 0.1rem !important;
}

#root .choice-progress.container.tw-border-radius-large.tw-overflow-hidden {
    height: 3.2rem;
    text-shadow: 1px 1px 1px black, 
        -1px 1px 1px black, 
        1px -1px 1px black, 
        -1px -1px 1px black;
}

#root .tw-pd-2 {
    padding: 0rem !important;
}

#root .tw-pd-1 {
    padding: 0rem !important;
}

#root p.tw-font-size-5.tw-line-height-heading.tw-semibold.tw-title.tw-title--inherit {
    font-weight: 600 !important;
    font-size: 15px !important;
}

#root .choice-progress__fill.choice-progress__fill--default.container__progress.tw-block {
    background: var(--progress-votes-color);
}

#root .tw-progress-bar {
    display: none !important;
}

/* votes values */
#root .tw-mg-r-1 {
    /*margin-right: 0rem!important;*/
    white-space: nowrap;
    margin-left: 0.5rem!important;
    display: none; /* remove this line to show user votes */
}

#root .tw-mg-l-1 {
    margin-left: 0.5rem!important;
}

#root .tw-root--theme-dark .choice-progress,
#root .choice-progress__fill.container__progress--background.tw-block
{
    background: var(--progress-background-color) !important;
}

#root .choice-progress__fill.choice-progress__fill--winner.container__progress.tw-block {
    background: var(--progress-winner-color) !important;
}
```
![OBS Poll Widget](./images/obs64_2020-12-08_14-01-45.jpg)

## How to Contribute
Make sure you have your `ASI loader` and `.NET ScriptHook` installed, and then clone the repo into the `for Developers` folder within the `scripts` folder.

When you build the mod (or it crashes), you don't need to restart the game. Open the console (`~` button) and type `reloadscripts`. It reloads and start scripts from the disk.
Type `abortscripts` to stop scripts.


[1]: https://github.com/shtrih/ChaosIVTwitchPollProxy/releases
[2]: https://github.com/shtrih/ChaosIV/releases
[3]: https://chrome.google.com/webstore/detail/frankerfacez/fadndhdgpmmaapbmfcknlfgcflmmmieb
[4]: https://github.com/ThirteenAG/Ultimate-ASI-Loader/releases/tag/v4.52
[5]: https://gtaforums.com/topic/946154-release-gtaiv-net-scripthook-v1718-support-for-gta-iv-1080-and-eflc-1130-by-arinc9-zolika1351/
[6]: http://hazardx.com/files/gta4_net_scripthook-83
[7]: https://help.twitch.tv/s/article/how-to-use-polls?language=en_US&sf222407025=1#overlay
