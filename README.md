# WARNING!!!

This project was a thing I did while I was learning C#. I cannot recommend enough **NOT** to use this software as it's plagued with bad coding and probably bad comments and more. All dependencies are also extremely outdated and this program will probably not work with current versions of iCue.

The library that decodes the JSON data that CS:GO sends to the server also has a vulnerability in it that can cause a DOS attack (DOS=Denial-Of-Service, not DDOS! [Cloudflare](https://www.cloudflare.com/learning/ddos/glossary/denial-of-service/) has a good writeup on DOS attacks incase you would like to learn more) against you easily.

More information about the vulnerability exists here: https://github.com/advisories/GHSA-5crp-9r3c-p9vr

You also need to run this program as administrator, which is a **HORRIBLE** idea for a problem that I could have easily solved in code by not using port 80 to respond to web requests!

I will archive this repositor after this README.md file has been modified to add this warning as a way for people to see how to do similar things, but I very explicity would like you to **NOT DOWNLOAD OR RUN THIS SOFTWARE**. It's atrocious both in security and being up-to-date!

# CSGO Keyboard Effects

This is a software to display ammunition and health on the keyboard from the popular game "Counter-Strike : Global Offensive".

This software is currently marked as "BETA" because of only half-supporting corsair RGB keyboards, as I still have a lot to learn when it comes to the wrapper that DarthAffe made([link to wrapper](https://github.com/DarthAffe/CUE.NET)), and how to make plugins.

***

#How do I set it up?

This is actually one of the easier things:

1. Open steam
2. Select CS:GO, and rightclick on the game
3. Select `properties`
4. On the tabs, select `Local Files`
5. Press `Browse local files`
6. Go into the folder marked `csgo`, then `cfg`
7. Get the file called `gamestate_integration_csgokeyboardpcnorden.cfg` from the zip file, and place it in the `cfg` folder.

And you're done!

***

#How do I run it?

You need to have the .exe file in the same place as the `x64` and `x86` folders.

The program will create a default template xml file if it doesn't exists and fill it with values.

With the xml generated, you can edit what buttons light up for diffrent weapon slots.

***

#How could I help with this project?

Well, the very best thing you could do is support me with some money, as I currently go in school, and I can't afford keyboards to develop on.

Othervise it could be great if you reported the errors you get when running the program.

***

#Subreddit

Today I created a brand new subreddit, so users can post ideas and bugs there, if they do not want to register to github.

[/r/csgoKeyboardEffects](https://www.reddit.com/r/csgoKeyboardEffects/)
