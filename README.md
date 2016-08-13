# Clap
`Clap` stands for `Command` `Line` `Audio` `Player`

It's written in C# using libsdl (found [here](https://www.libsdl.org/index.php)) for LINUX/UNIX based systems.

## Building
1. Install SDL (libsdl)

  **Ubuntu**
  ```
  sudo apt-get install libsdl-mixer1.2-dev libsdl1.2-dev
  ```
  
  **Arch & Antergos** (Untested)
  ```
  sudo pacman -S libsdl-mixer1.2-dev libsdl1.2-dev
  ```
2. Install mono (If not already done)
  
  **Ubuntu**
  ```
  sudo apt-get install mono-complete
  ```
  
  **Arch & Antergos**
  ```
  sudo pacman -S mono
  ```
3. Map libsdl in mono
  
  Add inside of the file `/etc/mono/config`
  ```
  <dllmap dll="SDL2_mixer" target="libSDL_mixer">
  ```
4. Build the project
  
  Move inside of the repository and type `make`
  
  You can also run `make run` to execute Clap after it's built, or by using the run command found [here](https://github.com/McSwaggens/run).
