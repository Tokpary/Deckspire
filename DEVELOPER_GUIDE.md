# Developer Guide

This guide provides practical information for developers working on the GameJam Stop Killing Games project.

## Prerequisites

- **Unity 2022.3.31f1** (or compatible LTS version)
- **Visual Studio** or **Visual Studio Code** with C# support
- **Git** for version control

## Project Setup

1. Clone the repository
2. Open Unity Hub and add the project
3. Ensure Unity version 2022.3.31f1 is installed
4. Open the project in Unity
5. Load the `MainMenu` scene to start

## Development Workflow

### Adding New Cards

1. **Create Card Data**:
   ```csharp
   // Create a new CardSO asset
   [CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card Data")]
   ```

2. **Define Abilities** (if needed):
   ```csharp
   // Inherit from CardAbilitySO
   public class MyAbility : CardAbilitySO
   {
       public override void ExecuteAbility(ACard card, GameBoard gameBoard)
       {
           // Implement ability logic
       }
   }
   ```

3. **Test Integration**:
   - Add card to appropriate deck ScriptableObject
   - Test in play mode

### Adding New Enemy Types

1. **Create Entity Data**:
   ```csharp
   [CreateAssetMenu(fileName = "NewEnemy", menuName = "Entities/Enemy")]
   ```

2. **Define Actions** (if needed):
   ```csharp
   public class MyEnemyAction : EnemyActionSO
   {
       public override void ExecuteAction(Enemy enemy, GameBoard gameBoard)
       {
           // Implement enemy behavior
       }
   }
   ```

3. **Configure in Scene**:
   - Assign EntitySO to Enemy component
   - Test combat interactions

### Adding New Game States

1. **Implement State Class**:
   ```csharp
   public class MyGameState : AGameState
   {
       public MyGameState(IGameState gameState) : base(gameState) { }
       
       public override void Enter(IGameState gameManager) { }
       public override void Exit(IGameState gameManager) { }
       public override void Update() { }
   }
   ```

2. **Register State Transitions**:
   - Update GameFlowManager logic
   - Test state transitions

## Code Style Guidelines

### Naming Conventions
- **Classes**: PascalCase (`GameManager`)
- **Methods**: PascalCase (`StartGame()`)
- **Fields**: camelCase with underscore prefix (`_currentState`)
- **Properties**: PascalCase (`CurrentHealth`)
- **Interfaces**: PascalCase with 'I' prefix (`IGameState`)
- **Abstract Classes**: PascalCase with 'A' prefix (`ACard`)

### File Organization
- One class per file
- Files named after their primary class
- Organize by system/feature in appropriate folders
- Use appropriate namespaces

### Comments
- Document public APIs
- Explain complex algorithms
- Use XML documentation for public methods
- Keep comments up-to-date

## Testing

### Play Testing
1. Start with MainMenu scene
2. Test core game loop
3. Verify state transitions
4. Check card interactions
5. Test enemy behaviors

### Code Testing
- Focus on state machine logic
- Test ScriptableObject configurations
- Verify manager interactions
- Test edge cases

## Debugging

### Common Issues

**State Machine Problems**:
- Check GameFlowManager state transitions
- Verify state Enter/Exit calls
- Debug with logging in state methods

**Card System Issues**:
- Verify CardSO references
- Check SnapZone interactions
- Debug ability execution

**Enemy Behavior Problems**:
- Check EntitySO configuration
- Verify action execution
- Debug turn management

### Debugging Tools
- Unity Console for error messages
- Unity Profiler for performance
- Visual Studio debugger for step-through
- Unity Inspector for runtime values

## Build Process

### Development Builds
1. File → Build Settings
2. Select target platform
3. Add scenes to build
4. Build and Run for testing

### Release Builds
1. Set Build Settings to Release
2. Configure Player Settings
3. Build for target platforms
4. Test thoroughly before release

## Performance Considerations

### Memory Management
- Use object pooling for frequently instantiated objects
- Dispose of resources properly
- Monitor memory usage with Profiler

### Rendering
- Optimize UI canvas hierarchies
- Use appropriate texture formats
- Monitor draw calls and batching

### Audio
- Use appropriate audio compression
- Manage audio memory usage
- Implement proper audio pooling

## Common Patterns

### Singleton Usage
```csharp
public class MyManager : Singleton<MyManager>
{
    protected override void Awake()
    {
        base.Awake();
        // Initialize manager
    }
}
```

### ScriptableObject Creation
```csharp
[CreateAssetMenu(fileName = "MyData", menuName = "Custom/My Data")]
public class MyDataSO : ScriptableObject
{
    // Data fields
}
```

### State Implementation
```csharp
public class MyState : AGameState
{
    public MyState(IGameState gameState) : base(gameState) { }
    
    public override void Enter(IGameState gameManager)
    {
        // State entry logic
    }
    
    public override void Exit(IGameState gameManager)
    {
        // State cleanup
    }
    
    public override void Update()
    {
        // Frame update logic
    }
}
```

## Resources

### Documentation
- [Unity Documentation](https://docs.unity3d.com/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [DOTween Documentation](http://dotween.demigiant.com/documentation.php)
- [Fungus Documentation](https://github.com/snozbot/fungus/wiki)

### External Assets Used
- **DOTween**: Animation and tweening
- **Fungus**: Visual scripting and dialogue
- **TextMesh Pro**: Advanced text rendering
- **Universal Render Pipeline**: Modern rendering

## Contributing

1. Follow the established architecture patterns
2. Maintain consistent code style
3. Test thoroughly before submitting
4. Update documentation as needed
5. Use meaningful commit messages

## Troubleshooting

### Common Unity Issues
- **Missing references**: Check Inspector for missing components
- **Build errors**: Verify all scenes are added to build settings
- **Performance issues**: Use Unity Profiler to identify bottlenecks

### Project-Specific Issues
- **State transitions not working**: Check GameFlowManager logic
- **Cards not responding**: Verify ICard interface implementation
- **Audio not playing**: Check AudioManager configuration

---

*For detailed architecture information, see [ARCHITECTURE.md](ARCHITECTURE.md) and [ARCHITECTURE_DIAGRAMS.md](ARCHITECTURE_DIAGRAMS.md)*