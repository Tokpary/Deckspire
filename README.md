# 🃏 Deckspire

**Deckspire** es un juego de cartas por turnos desarrollado en Unity, donde los jugadores combaten contra enemigos utilizando un sistema de mazos estratégicos con diversas habilidades y mecánicas de juego.

![Unity](https://img.shields.io/badge/Unity-2022.3.31f1-black?logo=unity)
![License](https://img.shields.io/badge/License-MIT-blue.svg)

## 📖 Descripción

Deckspire es un juego de combate táctico basado en cartas donde deberás enfrentarte a enemigos utilizando estrategia y gestión de recursos. Cada carta tiene habilidades únicas, costos de energía y tiempos de vida que debes administrar sabiamente para lograr la victoria.

## ✨ Características Principales

- **Sistema de Combate por Turnos**: Batalla estratégica entre el jugador y enemigos
- **Gestión de Mazo**: Colecciona y administra cartas con diferentes habilidades
- **Sistema de Energía**: Gestiona recursos de energía para jugar cartas
- **Habilidades Variadas**:
  - Cartas de daño
  - Cartas de curación y soporte
  - Habilidades especiales (Sobrecarga, Anticipación, Prolongación, Planificación)
  - Reglas de juego especiales (Nigromancia, Luz de Luna, Fuego Amigo)
- **Sistema de Tutorial**: Aprende las mecánicas del juego paso a paso
- **Gestión de Diálogos**: Sistema narrativo integrado con Fungus
- **Múltiples Escenas**: Menú principal, créditos, y niveles de juego
- **Sistema de Audio**: Gestión completa de música y efectos de sonido
- **Efectos Visuales**: Renderizado volumétrico con URP (Universal Render Pipeline)

## 🎮 Controles

- **Clic Izquierdo**: Seleccionar/Jugar carta
- **Arrastrar**: Mover cartas al tablero de juego
- **Hover**: Ver detalles de las cartas

## 🛠️ Requisitos del Sistema

### Para Desarrollo
- **Unity**: 2022.3.31f1 o superior
- **Sistema Operativo**: Windows, macOS o Linux
- **Memoria RAM**: 8 GB mínimo (16 GB recomendado)
- **Espacio en Disco**: 5 GB libres

### Paquetes de Unity Utilizados
- Universal Render Pipeline (URP) 14.0.11
- TextMesh Pro 3.0.6
- Cinemachine 2.10.4
- ProBuilder 5.2.4
- Visual Scripting 1.9.4
- Timeline 1.7.6
- Fungus (Sistema de diálogos)
- Unity URP Volumetric Light

## 📦 Instalación

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/Tokpary/Deckspire.git
   cd Deckspire
   ```

2. **Abrir en Unity**:
   - Abre Unity Hub
   - Haz clic en "Abrir" (Open)
   - Selecciona la carpeta del proyecto clonado
   - Espera a que Unity importe todos los assets

3. **Configurar el proyecto**:
   - Asegúrate de que Unity 2022.3.31f1 esté instalado
   - Permite que Unity importe todos los paquetes y dependencias

4. **Ejecutar el juego**:
   - Abre la escena `Assets/Scenes/MainMenu.unity`
   - Presiona el botón Play en el editor de Unity

## 📁 Estructura del Proyecto

```
Deckspire/
├── Assets/
│   ├── Art/                    # Sprites, materiales y assets visuales
│   ├── Audio/                  # Música y efectos de sonido
│   ├── Code/
│   │   └── Scripts/           # Scripts principales del juego
│   │       ├── Components/    # Componentes del juego
│   │       │   ├── Audio/     # Gestión de audio
│   │       │   ├── Camera/    # Sistema de cámara
│   │       │   ├── Card/      # Sistema de cartas
│   │       │   ├── Entity/    # Jugador y enemigos
│   │       │   ├── GameBoard/ # Tablero de juego
│   │       │   ├── GameManagment/ # Gestión del juego
│   │       │   │   ├── GameStates/ # Estados del juego
│   │       │   │   ├── GameManager.cs
│   │       │   │   ├── TurnManager.cs
│   │       │   │   ├── UIManager.cs
│   │       │   │   └── DialogueManager.cs
│   │       │   ├── Handdeck/  # Gestión de mano y mazo
│   │       │   └── TutorialManager/ # Sistema de tutorial
│   │       ├── Credits/       # Pantalla de créditos
│   │       ├── MainMenu/      # Menú principal
│   │       └── DesignPatterns/ # Patrones de diseño (Singleton, etc.)
│   ├── Fungus/                # Sistema de diálogos y narrativa
│   ├── Level/                 # Configuración de niveles
│   │   ├── Prefabs/          # Prefabs de niveles
│   │   └── ScriptableObjects/ # Datos de cartas y enemigos
│   ├── Resources/             # Recursos cargables dinámicamente
│   ├── Scenes/                # Escenas del juego
│   │   ├── MainMenu.unity
│   │   ├── TheFool.unity
│   │   ├── Credits.unity
│   │   └── PostCredits.unity
│   └── Settings/              # Configuraciones del proyecto
├── Packages/                  # Paquetes de Unity
├── ProjectSettings/           # Configuración del proyecto Unity
└── README.md                  # Este archivo
```

## 🏗️ Arquitectura del Código

### Patrones de Diseño Implementados
- **Singleton**: Usado para managers centrales (GameManager, AudioManager)
- **State Pattern**: Gestión de estados del juego (GameFlowManager, TurnManager)
- **ScriptableObjects**: Almacenamiento de datos de cartas y configuraciones

### Componentes Principales

#### GameManager
Controla el flujo general del juego, incluyendo:
- Gestión de turnos
- Control de jugadores y enemigos
- Coordinación de UI y diálogos
- Sistema de tutorial

#### Card System
Sistema modular de cartas con:
- **ACard**: Clase abstracta base para todas las cartas
- **CardSO**: ScriptableObject para datos de cartas
- **CardAbility**: Sistema de habilidades modulares

#### Turn Management
- **TurnManager**: Controla el flujo de turnos
- **GameFlowManager**: Gestiona los estados del juego

## 🎨 Características Técnicas

- **Render Pipeline**: Universal Render Pipeline (URP)
- **UI**: Canvas-based con TextMesh Pro
- **Audio**: Sistema de gestión de audio centralizado
- **Animaciones**: DOTween para animaciones fluidas
- **Diálogos**: Integración con Fungus para sistema narrativo
- **Efectos**: Volumetric lighting y post-processing

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Haz fork del proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👥 Créditos

Desarrollado por [Tokpary](https://github.com/Tokpary)

### Tecnologías y Herramientas Utilizadas
- [Unity](https://unity.com/) - Motor de juego
- [Fungus](https://github.com/snozbot/fungus) - Sistema de diálogos
- [DOTween](http://dotween.demigiant.com/) - Sistema de animaciones
- [TextMesh Pro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) - Sistema de texto
- [Cinemachine](https://unity.com/unity/features/editor/art-and-design/cinemachine) - Sistema de cámaras
- [ProBuilder](https://unity.com/features/probuilder) - Herramienta de modelado
