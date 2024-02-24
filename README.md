# 3lk3 Unity StateMachine

A simple implementation of a state machine to be used in Unity game engine.

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


## Project development

### Documentation

You can build and view this documentation project locally - we recommend that you activate `a local Python virtual environment first <https://packaging.python.org/en/latest/guides/installing-using-pip-and-virtual-environments/#creating-a-virtual-environment>`_:

```bash
    # Install required Python dependencies (Sphinx etc.)
    pip install -r Docs~/requirements.txt

    # Run Jupyter Book
    jupyter-book build Docs~/
```
