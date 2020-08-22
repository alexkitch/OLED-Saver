# OLED-Saver
A largely pointless program to play a full-screen video on a display at regular intervals. I created this because I was concerned that holding virtual webcam parties on my living room OLED TV was going to end up burning elements of Skype into it.

The more I've read about this, the less I'm actually concerned, but it's a nice little extra tool to have.

# Build Instructions

You'll need a suitable video in the project root named `video.webm` (feel free to change this in the code because it's not configurable right now).

Then you should be good to go :thumbsup:

There's also an Inno setup script provided for compiling the whole thing into a Windows setup package. Just install Inno, compile a Release version of the tool, and the script should spit out a setup package in the same directory as the output exe (`/bin/Release`)

# Usage

When the program launches it'll be an empty black form which can be moved around and positioned onto whichever monitor you want to protect. When you're happy that it's on the correct monitor, press your `Alt` key and the tool will disappear invisibly to full-screen - you should be able to work as normal.

What are unit tests?
