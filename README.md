# 3lk3 Unity StateMachine

A simple implementation of a state machine to be used in Unity game engine.
This is a slightly modified version of the state machine used in the [UIToolkit Sample QuizU](https://assetstore.unity.com/packages/essentials/tutorial-projects/quizu-a-ui-toolkit-sample-268492). 
In [this article](https://discussions.unity.com/t/quizu-state-pattern-for-game-flow/309255) the developers explain how it works in general.

Have a look at the full documentation here: <br>
[https://3lk3.github.io/Unity.StateMachine](https://3lk3.github.io/Unity.StateMachine)

## Installation

### via Package Manager Window

- Open the Unity package manager via **Window** -> **Package Manager**.<br>
- Click the plus icon on the top left and select **Add package from git URL** 
- Use `https://github.com/3LK3/Unity.StateMachine.git` as URL.

### or via manifest.json

Add the repository as a dependency manually to your `<YourProject>/Packages/manifest.json`.

```json
{
  "dependencies": {
    "...",
    "party.elke.unity.statemachine": "https://github.com/3LK3/Unity.StateMachine.git#0.0.1"
  }
}
```

You can choose a specific version [from this list](https://github.com/3LK3/Unity.StateMachine/releases).
