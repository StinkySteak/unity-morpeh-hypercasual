
# unity-morpeh-hypercasual

A Simple project for Morpeh ECS Framework Learning.

### Project Specification
Unity Version: 2021.3.21.f1

Platform: Windows & Android

### Project Preview

![ProjectPreview](Docs/project_preview.gif)

### Known Issues
1. Inconsistent sensitivity between editor & build
2. High horizontal input may push player off the map

### Manuals
#### Systems
1. `PlayerMovementSystem`
Move Player Forward & Horizontally based on Input

2. `FloorMovingSystem`
Move the Floor based on Player Forward Speed

3. `GoalPostSpawnerSystem`
Spawn Goals & Relocate them on triggered

4. `PlayerCollideSystem`
Track & Check for trigger between goals & player

5. `PlayerInputApplierSystem`
Apply Input to player movement

6. `PlayerInputCollectorSystem`
Poll & Store Input from Unity
