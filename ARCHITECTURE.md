# GameJam Stop Killing Games - Project Architecture

## Overview
This is a Unity 2022.3.31f1 project implementing a turn-based card game with strategic combat mechanics. The architecture follows Unity's component-based design pattern combined with established software design patterns for maintainable and extensible code.

## Core Architectural Patterns

### 1. Singleton Pattern
Used for critical game managers to ensure single instances:
- **GameManager**: Central game coordinator and state holder
- **AudioManager**: Handles all audio playback and management
- **CameraManager**: Manages camera behavior and transitions

**Implementation**: `Assets/Code/Scripts/DesignPatterns/Singleton.cs`

### 2. State Pattern
Comprehensive state management system for game flow control:

**Core Interfaces**:
- `IState`: Basic state interface with Enter, Exit, Update methods
- `IGameState`: Extended interface for game-specific states
- `AGameState`: Abstract base class for concrete game states

**Game States**:
- `DrawState`: Player draws cards at turn start
- `DeployCardState`: Player can deploy cards to the board
- `EnemyActionState`: Enemy performs their actions
- `AfterEnemyState`: Post-enemy action processing
- `DialogueState`: Handles narrative dialogue sequences
- `MenuState`: Menu and UI interaction state

**State Manager**: `GameFlowManager` orchestrates state transitions

### 3. ScriptableObject Pattern
Extensive use of ScriptableObjects for data-driven design:

**Card System**:
- `CardSO`: Card data definitions
- `CardAbilitySO`: Card ability definitions
- Specialized abilities in `Assets/Code/Scripts/Components/Card/ScriptableObjects/Abilities/`

**Entity System**:
- `EntitySO`: Base entity data
- `EnemyActionSO`: Enemy behavior definitions
- `EnemyPassiveSO`: Enemy passive abilities
- `DieEventSO`: Death event handlers

**Game Rules**:
- `GameRulesSO`: Board game rule definitions

### 4. Component-Based Architecture
Following Unity's ECS principles with MonoBehaviour components:
- Modular, reusable components
- Clear separation of concerns
- Interface-driven design for flexibility

## System Architecture

### Core Systems

#### Game Management Layer
```
GameManager (Singleton)
├── GameFlowManager (State Management)
├── TurnManager (Turn Logic)
├── UIManager (UI Coordination)
├── DialogueManager (Narrative)
└── TutorialManager (Tutorial Flow)
```

#### Entity System
```
Entity (Abstract Base)
├── Player
└── Enemy
    ├── EnemyActions (ScriptableObjects)
    ├── EnemyPassives (ScriptableObjects)
    └── DeathEvents (ScriptableObjects)
```

#### Card System
```
ACard (Abstract Base)
└── BaseCard
    ├── CardSO (Data)
    ├── CardAbilitySO (Abilities)
    └── Snap Zone Integration
```

#### Game Board System
```
GameBoard
├── SnappableAreas
│   ├── TappableSnapArea
│   ├── RuleSnapArea
│   └── DiscardSnapArea
├── GameRulesData
└── ASnapZone (Abstract)
```

### Folder Structure

```
Assets/
├── Code/Scripts/                    # Main game logic
│   ├── DesignPatterns/             # Reusable design patterns
│   ├── Components/                 # Game components
│   │   ├── GameManagment/         # Core managers and states
│   │   ├── Entity/                # Player/Enemy system
│   │   ├── Card/                  # Card system
│   │   ├── GameBoard/             # Board and snap zones
│   │   ├── Audio/                 # Audio management
│   │   ├── Camera/                # Camera control
│   │   ├── Interfaces/            # System interfaces
│   │   └── Interactables/         # Interactive objects
│   ├── MainMenu/                  # Menu screens
│   └── Credits/                   # Credits functionality
├── Scripts/Patterns/              # Additional pattern implementations
├── Art/                           # Visual assets
├── Audio/                         # Audio assets
├── Scenes/                        # Unity scenes
└── Resources/                     # Runtime-loaded assets
```

## Key Design Principles

### 1. Separation of Concerns
- Each manager handles a specific domain
- Clear interfaces between systems
- Minimal coupling between components

### 2. Data-Driven Design
- ScriptableObjects for configuration
- Runtime behavior modification
- Easy content creation for designers

### 3. Event-Driven Architecture
- State transitions trigger events
- Loose coupling through events
- Reactive UI updates

### 4. Interface Segregation
- Multiple small, focused interfaces
- `ICard`, `ISnapZone`, `IInteractableObject`
- Easy testing and mocking

## System Interactions

### Game Flow
1. **Initialization**: GameManager orchestrates system startup
2. **State Management**: GameFlowManager handles state transitions
3. **Turn Processing**: TurnManager coordinates player/enemy turns
4. **Card Interaction**: Cards interact with snap zones and board
5. **Combat Resolution**: Entity system processes damage and effects

### Data Flow
1. **Configuration**: ScriptableObjects define behavior
2. **Runtime State**: Managers maintain current game state
3. **UI Updates**: UIManager reflects state changes
4. **Persistence**: Game state maintained across scenes

## External Dependencies

### Third-Party Assets
- **DOTween**: Animation and tweening system
- **Fungus**: Visual scripting and dialogue system
- **TextMesh Pro**: Advanced text rendering
- **Universal Render Pipeline**: Unity's modern rendering

### Unity Systems
- **Scene Management**: Multi-scene architecture
- **Input System**: Player input handling  
- **Audio System**: 3D audio and mixing
- **UI System**: Canvas-based interface

## Extensibility Points

### Adding New Cards
1. Create new `CardSO` asset
2. Implement `CardAbilitySO` if needed
3. Cards automatically integrate with existing systems

### Adding New Enemy Types
1. Create `EntitySO` configuration
2. Define `EnemyActionSO` behaviors
3. Set up `EnemyPassiveSO` if needed

### Adding New Game States
1. Implement `AGameState` abstract class
2. Define Enter/Exit/Update logic
3. Register with `GameFlowManager`

## Performance Considerations

### Memory Management
- Object pooling for frequently instantiated objects
- ScriptableObject sharing for common data
- Proper cleanup in state transitions

### Rendering Optimization
- Universal Render Pipeline for modern rendering
- Efficient UI canvas organization
- Asset streaming for large scenes

## Testing Strategy

### Unit Testing
- Interface-based design enables easy mocking
- State pattern allows isolated state testing
- ScriptableObject data can be tested independently

### Integration Testing
- State transition validation
- Manager interaction testing
- Card-board interaction verification

## Future Architecture Improvements

### Potential Enhancements
1. **Command Pattern**: For undo/redo functionality
2. **Observer Pattern**: More robust event system
3. **Factory Pattern**: Dynamic entity/card creation
4. **Repository Pattern**: Save/load game state
5. **Service Locator**: Dependency injection system

### Scalability Considerations
- Modular scene loading for larger games
- Asset bundle system for downloadable content
- Network layer for multiplayer support
- Localization system for multiple languages

---

*This architecture documentation reflects the current state of the project and serves as a guide for understanding and extending the codebase.*