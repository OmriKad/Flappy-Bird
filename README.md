# Flappy Bird

A 2D Flappy Bird clone built with **Unity 6** (2D URP). Tap or press `Space` to keep the bird aloft, thread it through the pipe gaps without touching the ground, and rack up the highest score you can.

## Requirements

- **Unity 6** (built with `6000.3.20f1`, 2D URP template)
- Built-in **Input System** package (used by `FlyBehavior`)
- **TextMesh Pro** (used by the score UI)

## Getting Started

1. Clone the repo and open the project folder with Unity Hub / Unity Editor.
2. Open the scene **`Assets/Scenes/SampleScene.unity`**.
3. Press **Play** in the editor.

## How to Play

| Action | Control |
|--------|---------|
| Flap / Jump | **Left mouse click** or **Spacebar** |
| Start / Play again | Click the **Play** button |

**Rules**
- tap to flap — the bird rises with each tap and falls under gravity when you don't
- fly through the **gap between the pipe pairs** to score points
- **Hitting a pipe or the ground ends the game**
- After game over, click **Play** to restart with a fresh scene

## Gameplay Flow

The game opens on a **title screen**: the bird holds still in the air and `Time.timeScale = 0` freezes all physics. Clicking **Play** hides the canvas, resumes time, and the bird begins to fall.

- **Game start** — title screen shown, physics frozen
- **Play clicked** — game resumes, bird falls and responds to flaps
- **Game over** — `GameOver()` shows the canvas and pauses time
- **Play clicked again** — reloads the scene and auto-starts a fresh run

## Scripts

All gameplay scripts live in **`Assets/Scripts/`**.

| Script | What it does |
|--------|--------------|
| `FlyBehavior.cs` | Reads click / Space input and applies an upward velocity; rotates the bird by vertical speed; calls `GameManager.GameOver()` on collision |
| `GameManager.cs` | Central state machine (title / playing / game over), manages the canvas, `Time.timeScale`, and scene restart |
| `MovePipe.cs` | Moves a pipe pair to the left each frame |
| `PipeSpawner.cs` | Spawns the pipe prefab repeatedly with a random vertical offset, and auto-destroys it after 10s |
| `PipeIncreaseScore.cs` | Increments the score when the bird passes through a pipe-gap trigger |
| `Score.cs` | Tracks the current score and high score (persisted via `PlayerPrefs`) and updates the on-screen UI |
| `LoopGround.cs` | Scrolls the ground sprite by growing its width, resetting it to loop the motion |

## Project Structure

```
Assets/
├── Scenes/
│   └── SampleScene.unity        # the main playable scene
├── Scripts/                     # gameplay logic (see table above)
├── Prefabs/
│   └── PipePrefab.prefab        # pipe pair prefab spawned by PipeSpawner
├── Fonts/                       # TextMesh Pro font(s), e.g. FlappyBirdFont
├── Sprites/                     # Flappy Bird sprite sheet + animations
└── Animations/                  # wing-flap animation & animator controller
```

## Notes

- The ground collider is a **`BoxCollider2D`** — it must remain a 2D collider so it interacts with the bird's `Rigidbody2D` (a 3D collider would let the bird fall through).
- Bird collision detection uses a `CapsuleCollider2D`; pipe gaps use long colliders and the score trigger.
- Settings, Library, Temp, and similar Unity-generated folders are excluded via `.gitignore`.