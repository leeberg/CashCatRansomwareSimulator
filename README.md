Cashcat : The "Ransomware" Simulator
==================
A little "ransomware-like" simulator for Windows that will rename .TXT files to .LOCKY to simulate ransomware behavior for demos and testing various file monitoring tools and response systems. 

![](./img/cashcat.png)  
Cashcat from: http://cashcats.biz/

## Download
Visit the releases section on this repository: https://github.com/leeberg/CashCatRansomwareSimulator/releases

## VERY IMPORTANT NOTE
THIS IS **NOT** REAL RANSOMWARE! IT LITERALLY DOES NOT MATCH ANY REAL DEFINITION OF RANSOMWARE! ALL IT DOES IS RENAME files with the extension of .TXT to .LOCKY to test file activity monitoring tools. Nothing gets Encrypted!

## Usage
1. Build/Download CashCat.exe 
2. Place CashCat.exe in a directory with some .txt files in the same directory.
3. Run Cashcast.exe.
4. All .TXT files located in the same directory as Cashcat.exe will be renamed to .Locky - a common ransomware extension. No files are ACTUALLY encrypted - they are simply renamed.
5. Enter the code 123456789 to rename all .LOCKY files to .TXT (this effectively UNLOCKS your files / resets your demo)

## UI
![](./img/CryptoLocker_Simulator.png)

## Demo
![](./img/cashcatdemo132.gif)

## Requirements
+ Tested on Windows 7, 10, Server 2012R2, 2016+ 
+ Requires at least .NET 4.6.1

## Configuration
CashCat has some basic configuration options! Simply place a json file named "CashCat.Json" in the directory alongside the executable to unlock a number of functions:

```
{
    "webHookEnabled":  0,
    "webHookURI":  "http://URIGORESHERE.COM",
    "catMode":  0
}

```
**Options**
* webHookEnabled - instructs CashCat to execute a GET webhook execution on open
* webHookURI - Webhook URI to be called
* catMode - "Secret" **CashCat** mode!


## Todo
+ ~~UNC path support (works on my machine)~~
+ ~~Add Webhook Support for Canary like feature~~
+ ~~Enable Optional Easter Egg Cash Cat Mode~~
+ Make the UI more ransomware-ey
