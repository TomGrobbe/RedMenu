# RedMenu
RedMenu is a server side menu for **[RedM](https://redm.gg/)**. (Soon™)


## About
This will be built ~~similar to~~ with **[MenuAPI](https://github.com/TomGrobbe/MenuAPI/)** (now available for both **[FiveM](https://fivem.net/)** _and_ **RedM**) ~~only using the new native menu constructor functions in Red Dead Redemption 2~~.


## The goal
This menu will hopefully be similar to **[vMenu for FiveM](https://github.com/TomGrobbe/vMenu/)**, but better (both internal code and visual improvements), with more configuration and permission options, made specifically for RedM.


## Forum topic
_Coming as soon as this resource is somewhat ready for release._


## License
Feel free to use code from this repo in your own resources, however you must provide appropriate credit when taking code.


## Installation
1. Update your server to the latest artifacts. Yes it's required, don't ask me why, just f'ing do it already, thanks.
2. Download the latest [RedMenu Release](https://github.com/TomGrobbe/RedMenu/releases/latest) zip file - note not the source files, download the actual release files - and extract the contents of the zip file into your server resources folder (preferrably in the `[local]` folder). Make sure that you call the resource `RedMenu` (case sensitive), otherwise the resource **will not work**.
3. You should now have everything inside: `\resources\[local]\RedMenu\`. If there is no `fxmanifest.lua` in that folder, then you've done it incorrectly.
A picture for those who can't read: ![](https://vespura.com/hi/i/2019-12-16_17-07_5b40b_2805.png)
4. Add `exec resources/[local]/RedMenu/config.cfg` to your `server.cfg`.
5. Add `start RedMenu` to your `server.cfg` (**make sure it is below the line from step 4, it won't work if you start the resource before executing the config file**).
6. Check for any errors in the server console when you boot the server. You can ignore warnings like these when you start up RedMenu: `server thread hitch warning: timer interval of ... milliseconds`.


>**No errors/warnings?** Great! Now go celebrate that you've successfully read and followed some super basic instructions correctly.

>**Did you get any errors/warnings?** Well, R.I.P your ego. You've failed to follow the instructions correctly, go back to step 1 and try again.


## Contributions
Feel free to contribute to this resource. There are just a few things you need to keep in mind:
-	All code you put into a pull request must be your own, don’t steal code.
-	I will review all pull requests, if they do not meet a specific standard then changes will be required before the pull request can be merged. I’ll request these changes in the review, so you know exactly what to change.
-	If I feel like (some of) the features you are trying to implement do not fit this resource, I will not accept the PR.
-	All pull requests must not break backwards compatibility. Most likely people will install this resource once, and a large portion of the users will never update it after that. So, if a user joins a server where a newer version is installed, they must still be able to join another server with an older version, without losing access to their data or things being broken.
- Use common sense.


## Support
For now, there will be no support, because the resource isn't even remotely close to a working test build yet. Once the resource is ready for the first official release, there will be some form of limited support.
This will most likely be in the form of a documentation website for general installation, configuration and troubleshooting instructions.

More 'advanced' troubleshooting will be given in certain cases only. I simply do not have the time to explain to everyone personally how to properly troubleshoot things themselves, or to do it for them. There is a massive community with plenty of people willing to help, so be nice and accept help from anyone that is willing to help you out.


## CI
AppVeyor will be used to automatically build and release new versions of this resource to GitHub.
You can find RedMenu on AppVeyor [here](https://ci.appveyor.com/project/TomGrobbe/redmenu).

Every latest "development" build will be available on AppVeyor. Development builds will also be pushed to my [Discord server](https://vespura.com/discord) automatically.
