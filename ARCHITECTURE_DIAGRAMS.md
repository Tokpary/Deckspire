# System Architecture Diagram

## High-Level System Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                        UNITY GAME ENGINE                        │
├─────────────────────────────────────────────────────────────────┤
│                      PRESENTATION LAYER                         │
├─────────────────────┬─────────────────────┬─────────────────────┤
│    Scene Management │      UI System      │   Audio System      │
│  ┌─────────────────┐│  ┌─────────────────┐│  ┌─────────────────┐│
│  │   MainMenu      ││  │   UIManager     ││  │  AudioManager   ││
│  │   TheFool       ││  │   Canvas        ││  │   (Singleton)   ││
│  │   Credits       ││  │   Components    ││  │   Sound         ││
│  │   PostCredits   ││  │                 ││  │   Components    ││
│  └─────────────────┘│  └─────────────────┘│  └─────────────────┘│
└─────────────────────┴─────────────────────┴─────────────────────┘
├─────────────────────────────────────────────────────────────────┤
│                      GAME LOGIC LAYER                           │
├─────────────────────────────────────────────────────────────────┤
│                     GAME MANAGER (Singleton)                    │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │              Central Game Coordinator                        │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                │                                 │
│      ┌─────────────────────────┼─────────────────────────┐       │
│      │                         │                         │       │
│  ┌───▼────┐  ┌────────────┐  ┌─▼──────────┐  ┌──────────▼───┐   │
│  │  Turn  │  │ GameFlow   │  │ Dialogue   │  │  Tutorial    │   │
│  │Manager │  │ Manager    │  │  Manager   │  │  Manager     │   │
│  └────────┘  └────────────┘  └────────────┘  └──────────────┘   │
│                     │                                            │
│               ┌─────▼─────┐                                      │
│               │   STATE   │                                      │
│               │  MACHINE  │                                      │
│               └───────────┘                                      │
└─────────────────────────────────────────────────────────────────┘
├─────────────────────────────────────────────────────────────────┤
│                      GAME SYSTEMS LAYER                         │
├─────────────────────┬─────────────────────┬─────────────────────┤
│    ENTITY SYSTEM    │     CARD SYSTEM     │   BOARD SYSTEM      │
│  ┌─────────────────┐│  ┌─────────────────┐│  ┌─────────────────┐│
│  │     Entity      ││  │      ACard      ││  │   GameBoard     ││
│  │   (Abstract)    ││  │   (Abstract)    ││  │                 ││
│  │  ┌───────────┐  ││  │  ┌───────────┐  ││  │  ┌───────────┐  ││
│  │  │  Player   │  ││  │  │ BaseCard  │  ││  │  │ SnapZones │  ││
│  │  └───────────┘  ││  │  └───────────┘  ││  │  └───────────┘  ││
│  │  ┌───────────┐  ││  │  ┌───────────┐  ││  │  ┌───────────┐  ││
│  │  │  Enemy    │  ││  │  │   CardSO  │  ││  │  │GameRules  │  ││
│  │  └───────────┘  ││  │  └───────────┘  ││  │  └───────────┘  ││
│  └─────────────────┘│  └─────────────────┘│  └─────────────────┘│
└─────────────────────┴─────────────────────┴─────────────────────┘
├─────────────────────────────────────────────────────────────────┤
│                        DATA LAYER                               │
├─────────────────────────────────────────────────────────────────┤
│                   SCRIPTABLEOBJECTS                             │
│  ┌──────────────┬──────────────┬──────────────┬──────────────┐  │
│  │   Card Data  │ Entity Data  │ Ability Data │  Game Rules  │  │
│  │ ┌──────────┐ │ ┌──────────┐ │ ┌──────────┐ │ ┌──────────┐ │  │
│  │ │ CardSO   │ │ │EntitySO  │ │ │AbilitySO │ │ │ RulesSO  │ │  │
│  │ └──────────┘ │ └──────────┘ │ └──────────┘ │ └──────────┘ │  │
│  │ ┌──────────┐ │ ┌──────────┐ │ ┌──────────┐ │              │  │
│  │ │ Abilities│ │ │ Actions  │ │ │Death     │ │              │  │
│  │ │    SO    │ │ │    SO    │ │ │Events SO │ │              │  │
│  │ └──────────┘ │ └──────────┘ │ └──────────┘ │              │  │
│  └──────────────┴──────────────┴──────────────┴──────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

## State Machine Flow

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│  DialogueState  │────▶│    DrawState    │────▶│ DeployCardState │
│   (Optional)    │     │  (Turn Start)   │     │ (Player Action) │
└─────────────────┘     └─────────────────┘     └─────────────────┘
                                                          │
                        ┌─────────────────┐              │
                        │ AfterEnemyState │◀─────────────┘
                        │ (Cleanup/Check) │
                        └─────────────────┘
                                 │
                        ┌─────────────────┐
                        │ EnemyActionState│
                        │  (AI Turn)      │
                        └─────────────────┘
```

## Component Interaction Flow

```
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│ Player      │───▶│ Card        │───▶│ SnapZone    │
│ Input       │    │ Selection   │    │ Validation  │
└─────────────┘    └─────────────┘    └─────────────┘
                           │                   │
                           ▼                   ▼
┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│ UI Updates  │◀───│ GameBoard   │◀───│ Card        │
│             │    │ Processing  │    │ Deployment  │
└─────────────┘    └─────────────┘    └─────────────┘
                           │
                           ▼
                  ┌─────────────┐
                  │ Turn        │
                  │ Resolution  │
                  └─────────────┘
```

## Design Pattern Implementation

### Singleton Pattern
```
GameManager ──┐
AudioManager──┼── Singleton<T>
CameraManager─┘   (Base Class)
```

### State Pattern
```
IState ◀── AGameState ◀── DrawState
  ▲           ▲         ├── DeployCardState
  │           │         ├── EnemyActionState
  │           │         ├── DialogueState
  │           │         └── AfterEnemyState
  │           │
  │        Concrete States
  │
GameFlowManager (Context)
```

### Strategy Pattern (Abilities)
```
CardAbilitySO ◀── CleanseAbility
     ▲         ├── DealDamageAbility
     │         ├── RestAbility
     │         └── TransfusionAbility
     │
   ACard uses different abilities
```

---

*This diagram shows the layered architecture and how different systems interact within the Unity game engine framework.*