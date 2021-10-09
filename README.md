# Clicknado

![alt text](https://github.com/DavidRieman/Clicknado/blob/main/src/TrayIcon.ico?raw=true)
A Windows accessibility tool which simulates repetitive mouse clicks.
![alt text](https://github.com/DavidRieman/Clicknado/blob/main/src/TrayIcon.ico?raw=true)

Clicknado is an application which lives as an icon in the notification area of your task bar.
It listens to low-level keyboard input and responds by simulating repetitive mouse clicks for you.
This can be helpful because many games and applications require clicking at a fast rate, for prolonged times, to be effective.
Such constant clicking patterns can be unhealthy for your hands, especially for those who already have physical difficulties with these activities.
My hope is that this simple application will help a few people to participate in games and applications that they would not have been able to without it.

## Status

Currently, Clicknado is a _functional prototype_.
Tray functionality is working (including enabling and disabling input reactions via context menu).
Holding F11 results in spamming left mouse clicks wherever your mouse cursor is... but the app is _not yet configurable_ through the UI.

Unfortunately, Clicknado will not work with all software.
The technique used to simulate mouse clicks is a simple one, asking Windows APIs to post the events.
So, success may depend on how the individual software reads its input (whether receiving Windows events, or directly reading hardware).

**WARNING**: Do not use near any software which might do dangerous things when clicking certain areas (like making purchases when you click, or deleting things).
This easier-clicking input mode can be a blessing and a curse, taking a little while to get used to: It can be all too easy to absent-mindedly sweep the mouse while still holding the input that is also causing mouse clicks.
Carefully test how this software works, and then use with care!

## Other Info

* For bug reports, or to see upcoming planned work: See the Issues tab.
* For suggestions, ideas, and incoming contributors: See the Discussions tab.
