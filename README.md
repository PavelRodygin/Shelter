# Shelter 🏠

**TeachMeSkills** course graduation project - 3D underground bunker survival simulator developed with Unity.

## 📋 Project Description

**Shelter** is an immersive survival game where the player finds themselves in an underground bunker and must manage various systems to maintain life support. The project demonstrates modern game development approaches using modular architecture and design patterns.

## 🎮 Key Features

- **Interaction System** - Interactive bunker objects (doors, generators, ventilation systems)
- **Player Control** - First-person perspective with support for both mobile and desktop platforms
- **Item System** - Flashlight, smartphone, and other useful tools
- **Audio System** - Background music and sound effects
- **Settings System** - Audio settings, camera sensitivity configuration
- **Progress Saving** - Data persistence system

## 🛠 Technology Stack

- **Engine**: Unity 2022.3+ LTS
- **Language**: C#
- **Architecture**: Modular architecture with MVP pattern
- **DI Container**: Zenject/Extenject
- **Async**: UniTask
- **Animation**: DOTween
- **Input**: Unity Input System + SimpleInput (for mobile)
- **Rendering**: Universal Render Pipeline (URP)

## 🏗 Project Architecture

The project follows **modular architecture** where each module is isolated and independent:

### Module Structure

```
📦 Modules/
├── 🎮 GameScreen/          # Main gameplay module
│   ├── Scripts/
│   │   ├── GameScreenController.cs
│   │   ├── LevelManager.cs
│   │   ├── GameMessageManager.cs
│   │   └── StoryManager.cs
│   └── Views/
└── 🎯 MainMenu/            # Main menu module
    ├── Scripts/
    │   ├── MainMenuController.cs
    │   ├── MainMenuUIView.cs
    │   └── SettingsPopup.cs
    └── Views/
```

### Core Components

- **ModuleController** - Runs the main ModuleStateMachine
- **MVP Pattern** - Model-View-Presenter for each module
- **State Management** - Optional use of Stateless library
- **Services** - Common services (Input, Audio, Data Persistence)

### Game Systems

```
📦 Core/
├── 🎮 Gameplay/
│   ├── AbstractClasses/     # Base classes for game objects
│   │   ├── Interactable.cs  # Interactive objects
│   │   ├── OpenClosable.cs  # Openable objects
│   │   ├── Breakable.cs     # Breakable objects
│   │   └── Item.cs          # Items
│   ├── PlayerScripts/       # Player systems
│   │   ├── Player.cs
│   │   ├── PlayerInteractionController.cs
│   │   └── PlayerItemManager.cs
│   └── Interactable/        # Specific interactive objects
│       ├── FVM.cs           # Ventilation system
│       └── Generator.cs     # Generator
├── 🔧 Systems/              # Core systems
│   ├── AudioSystem.cs
│   └── DataPersistenceSystem/
└── 🎨 UI/                   # UI components
    ├── ButtonSounds.cs
    ├── ButtonZoom.cs
    └── Popup.cs
```

## 🚀 Getting Started

### System Requirements

- **Unity**: 2022.3+ LTS
- **Platform**: Windows, Android, iOS
- **.NET**: Framework 4.x

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Shelter.git
   ```

2. Open the project in Unity Hub

3. Make sure all required packages are installed:
   - Universal RP
   - Cinemachine  
   - Input System
   - TextMeshPro

4. Open the scene `Assets/Scenes/Bootstrap.unity`

5. Press Play to start

### Platforms

- **PC**: Full support with keyboard and mouse
- **Mobile**: Adaptive controls via SimpleInput
- **Editor**: Development mode with debug features

## 🎯 Gameplay

### Controls

**PC:**
- **WASD** - Movement
- **Mouse** - Camera look
- **E** - Interact with objects
- **ESC** - Pause/Settings

**Mobile:**
- **Virtual Joystick** - Movement
- **Touch** - Camera look
- **UI Buttons** - Interaction

### Game Objects

- **🚪 Blast Door** - Bunker protective door
- **🚪 Blast Hatch** - Emergency hatch
- **🌪️ FVM** - Air filtration system
- **⚡ Generator** - Power generator
- **🔦 FlashLight** - Lighting tool
- **📱 Smartphone** - Communication device

## 📁 Project Structure

```
📦 Shelter/
├── 📂 Assets/
│   ├── 🎬 Animations/         # Object animations
│   ├── 💻 CodeBase/           # Main project code
│   ├── 🎨 Materials/          # Materials and textures
│   ├── 🗿 Models/            # 3D models
│   ├── 🎮 Prefabs/           # Game prefabs
│   ├── 📦 Resources/         # Game resources
│   ├── 🎯 Scenes/            # Game scenes
│   ├── 📝 Scripts/           # Game scripts
│   └── ⭐ StarterAssets/     # Unity starter assets
├── 📂 Packages/              # Unity packages
├── 📂 ProjectSettings/       # Project settings
└── 📄 README.md
```

## 🔧 Configuration

### Audio Settings
- **Music Volume**: Adjustable in settings
- **Sound Effects**: Separate volume for effects  
- **Background Music**: Auto-play in main menu

### Graphics Settings
- **Render Pipeline**: Universal RP
- **Target Frame Rate**: 30 FPS (mobile optimization)
- **Resolution**: Adaptive to device

### Input Settings
- **Camera Sensitivity**: Configurable mouse sensitivity
- **Mobile Controls**: SimpleInput for touch devices
- **Cross-platform**: Unity Input System

## 👥 Development Team

- **Developer**: [Your Name]
- **Course**: TeachMeSkills
- **Year**: 2024
- **Project Type**: Graduation Project

## 📝 License

This project was created for educational purposes as a graduation work for the TeachMeSkills course.

## 🎓 Educational Goals

The project demonstrates proficiency in the following technologies and concepts:

- ✅ **Modular Architecture** in Unity projects
- ✅ **MVP Pattern** for logic and presentation separation  
- ✅ **Dependency Injection** using Zenject
- ✅ **Asynchronous Programming** with UniTask
- ✅ **Cross-platform Development** (PC/Mobile)
- ✅ **Data Persistence Systems**
- ✅ **Unity Input System** for modern input handling
- ✅ **URP** for optimized graphics
- ✅ **Unity Animation Systems**

---

*Developed with ❤️ for TeachMeSkills course*
