# VR-EscapeTomb

A virtual reality escape room game built in Unity featuring immersive hand tracking, physics-based interactions, and puzzle-solving gameplay set in an ancient tomb environment.

## 🎮 Game Overview

VR-EscapeTomb is an immersive VR experience where players must navigate through an ancient tomb, solve intricate puzzles, collect mystical papyrus pieces, and escape before time runs out. The game emphasizes realistic hand interactions and physics-based gameplay without relying on pre-built interaction toolkits.

## ✨ Key Features

### 🖐️ Advanced Hand Tracking & Physics
- **Custom Hand Controller System**: Full hand tracking with realistic grip detection
- **Physics-Based Interactions**: Objects respond naturally to hand movements
- **Dual Hand Support**: Independent left and right hand controllers with collision detection
- **Grab & Release Mechanics**: Intuitive object manipulation using grip strength

### 🧩 Complex Puzzle Systems
- **Multi-Stage Quest System**: 6 interconnected quests with progressive difficulty
- **Papyrus Collection**: Gather ancient scrolls to unlock new areas
- **Object Placement Puzzles**: Strategic positioning of mystical artifacts
- **Environmental Interactions**: Doors, teleportation points, and hidden areas

### 🎯 Game Mechanics
- **Quest Management**: Dynamic quest tracking and completion system
- **Object Respawn System**: Lost items automatically return after timeout
- **Progressive Unlocking**: Areas and abilities unlock as quests are completed
- **Audio Integration**: Immersive sound design with volume controls

### 🏛️ Immersive Environment
- **Ancient Tomb Setting**: Atmospheric Egyptian-inspired architecture
- **Multiple Chambers**: Interconnected rooms with unique challenges
- **Hidden Passages**: Secret areas revealed through puzzle completion
- **Teleportation System**: Smooth movement between areas

## 🛠️ Technical Implementation

### Core Systems

**Hand Tracking (`HandController.cs`)**
- XR input device integration
- Grip strength detection (0.6f grab threshold, 0.8f release threshold)
- Physics-based joint connections for object manipulation
- Collision detection with LayerMask filtering

**Physics Management (`HandTrackingPhysics.cs`)**
- Rigidbody-based hand movement
- Continuous collision detection
- Angular velocity calculations for realistic rotation
- Fixed joint systems for stable object holding

**Game State Management (`GameManager.cs`)**
- Singleton pattern for global state access
- Quest progression tracking
- Dynamic object activation/deactivation
- Player position management and respawn system

**Quest System (`QuestManager.cs`)**
- Array-based quest tracking with string identifiers
- Integration with puzzle completion states
- Progressive unlocking of game areas
- Support for complex multi-part quests

### Key Classes

```csharp
// Core interaction system
public class HandController : MonoBehaviour
public class HandTrackingPhysics : MonoBehaviour

// Game management
public class GameManager : MonoBehaviour
public class QuestManager : MonoBehaviour

// Item system
public class Papirus : ScriptableObject
public class PapirusPickUp : MonoBehaviour

// Utility systems
public class BackToSpawnPosition : MonoBehaviour
public class TriggerPapirus : MonoBehaviour
```

## 🎯 Gameplay Flow

1. **Starting Area**: Players begin in the entrance chamber
2. **Quest 1-3**: Collect mystical objects (snake, bowl, feather) to open first door
3. **Second Chamber**: Solve environmental puzzles to access teleportation
4. **Papyrus Hunt**: Locate and collect three ancient papyrus pieces
5. **Final Chamber**: Complete remaining puzzles to unlock the exit
6. **Escape**: Navigate through the final passage to complete the game

## 🔧 Setup & Installation

### Prerequisites
- Unity 2020.3 LTS or later
- VR headset (Oculus, SteamVR compatible)
- XR Management package
- XR Interaction Toolkit (for basic XR setup only)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/VR-EscapeTomb.git
   ```
2. Open the project in Unity
3. Configure XR settings for your target headset
4. Build and deploy to your VR device

### Controls
- **Grip**: Squeeze grip button to grab objects
- **Release**: Release grip button when grip strength < 80%
- **Movement**: Use teleportation points throughout the tomb
- **Interaction**: Physical contact with hands for all interactions



## 🎮 Design Philosophy

**No Toolkit Dependency**: All interaction mechanics are built from scratch using Unity's core physics system, ensuring full control over the player experience.

**Physics-First Approach**: Every interaction relies on realistic physics simulation, creating an intuitive and immersive VR experience.

**Progressive Complexity**: Puzzles increase in difficulty, introducing new mechanics gradually to maintain player engagement.

**Accessibility**: Volume controls and testing modes ensure the game is accessible to different player preferences.

## 🐛 Known Issues & Future Improvements

### Current Limitations
- Hand collision sometimes requires precise positioning
- Object respawn timer could be more dynamic
- Limited to single-player experience

### Planned Features
- Multiplayer cooperation mode
- Additional puzzle types
- Enhanced haptic feedback
- Mobile VR support

## 🤝 Contributing

This project demonstrates advanced VR development techniques including:
- Custom hand tracking implementation
- Physics-based interaction systems
- Complex state management
- Modular puzzle design

Feel free to explore the codebase to learn about VR development patterns and physics-based gameplay mechanics.

## 📜 License

This project is part of a university coursework and is intended for educational purposes.

## 🏆 Achievements

- **Immersive Interaction**: Successfully implemented toolkit-free hand tracking
- **Physics Mastery**: Realistic object manipulation without pre-built systems
- **Complex State Management**: Robust quest and progression system
- **Educational Value**: Comprehensive example of VR game development

---

*Experience the mystery of the ancient tomb in virtual reality. Will you solve the puzzles and escape, or become another lost soul in the depths of VR-EscapeTomb?*

### Collaborators:
- [Gabriela Pyjas](https://github.com/gabpyj) (Programming (Locomotion, Puzzles))
- [Leena Jarvenpaa](https://github.com/leenajvp) (Programming, UI design and programming, Enviroinment Design, Art (3D models, textures and concepts))
- [Samuel Krzyszpien](https://github.com/akatsukiAti) (Programming (Interactions and physics, Game Manager))
- [Vanessa Pointer](https://github.com/Nessie-J) (Programming (Climbing, Puzzles), Sound Design and implementation, VFX)
