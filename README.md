# "CashCat" The Ransomware Simulator
A fun little ransomware simulator for Windows that will rename .TXT files to .LOCKY to simulate ransomware behavior for testing various monitoring tools.

THIS IS **NOT** REAL RANSOMWARE! IT LITERALLY DOES NOT MATCH ANY REAL DEFINITION OF RANSOMWARE! ALL IT DOES IS RENAME files with the extension of .TXT to .LOCKY to test file activity monitoring tools.

![](.\img\cashcat.jpg)

## Usage
1. Place CashCat.exe in a directory with some .txt files in the same directory.
2. Run Cashcast.exe.
3. All .TXT files located in the same directory as Cashcat.exe will be renamed to .Locky - a common ransomware extension
4. Enter the code 123456789 to rename all .LOCKY files to .TXT (this effectily UNLOCKS your files / resets your demo)


![](.\img\CryptoLocker_Simulator.png)


## Requirements
+ Tested on Windows 7, 10, Server 2012+
+ Requires at .NET 4.6.1