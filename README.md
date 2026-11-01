# Coin Catcher

A simple 2D Unity game: move the basket and catch falling coins.

Editor version: **Unity 6000.6.7f1**. Clone this repository as the Unity project root (`Assets`, `Packages`, and `ProjectSettings` must stay at the top level).

## Import in Unity Hub

1. Clone the repo:
   ```bash
   git clone https://github.com/harsha1709acc/ISDonlineUnity.git
   ```
2. In **Unity Hub**, install editor **6000.6.7f1** if it is not already installed (Installs → Install Editor).
3. In Unity Hub go to **Projects → Add → Add project from disk** and select the cloned `ISDonlineUnity` folder.
4. Open the project with **6000.6.7f1**. Hub will restore packages and generate `Library` on first open.

## Play

1. Open `Assets/Scenes/SampleScene.unity`.
2. Press Play.
3. Move with **A/D** or **arrow keys** (gamepad stick also works).
4. Catch gold coins for **+10** score. Miss a coin and you lose a life. You have **3 lives**.
5. After Game Over, press **Space** or click to play again.

The scene is built in code when Play starts, so you do not need to wire objects in the Inspector.

## VS Code

File → Open Folder → the same cloned folder. Install **C#** and **Visual Studio Tools for Unity** if VS Code suggests them.
