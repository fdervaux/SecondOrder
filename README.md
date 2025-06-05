# Second Order Dynamic for Unity

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

<br>

## Overview

<p align="center">
  <img src="https://github.com/user-attachments/assets/5bfca54b-68a1-4ae3-a218-981fb67c8eb2" width="250">
  <img src="https://github.com/user-attachments/assets/b58a782e-7062-4548-9565-011e616eaa1c" width="250">
  <img src="https://github.com/user-attachments/assets/4ff3fc8b-9b19-4290-94fe-8a494052973d" width="250">
</p>

Second Order Dynamics for Unity is a lightweight and flexible package that enables smooth and responsive motion through second order dynamic simulation. Inspired by the behavior of real-world physical systems, this technique allows you to create natural, spring-like motion for objects in your game—ideal for camera smoothing, UI animations, character follow behaviors, and more.

By simulating inertia, damping, and acceleration, second order dynamics help bridge the gap between rigid, direct input and smooth, reactive motion. This package provides an easy-to-integrate solution with minimal setup, while still offering full control over responsiveness, damping, and oscillation parameters.

This package was inspired by the work of **t3ssel8r** and the concept of second order dynamics as explained in his video. You can watch the original reference video here:

[Second Order Dynamics Explained by t3ssel8r](https://www.youtube.com/watch?v=KPoeNZZ6H4s&t=1s)

A big thanks to t3ssel8r for providing a clear and detailed explanation of the theory behind this technique.

<br>

## Installation

You can install this package via the Unity Package Manager using the Git URL.

### Option 1: Using Git URL (recommended)

1. Open your Unity project.
2. Go to `Window` → `Package Manager`.
3. Click the `+` button in the top-left corner, then choose **"Add package from Git URL..."**
4. Paste the following URL:
``` 
https://github.com/fdervaux/SecondOrder.git#v1.0.2
```
5. Click **Add**. Unity will fetch and install the package.

### Option 2: Modify `manifest.json` manually

1. Open the `Packages/manifest.json` file in your Unity project with a text editor.  
2. Add the following line to the `"dependencies"` section:

```json
"com.fdervaux.secondorder": "https://github.com/fdervaux/SecondOrder.git#v1.0.2"
```

<br>

## Usage

This package provides a simple and flexible way to apply second order dynamics to any data type (e.g., `Vector3`, `float`, etc.). A typical use case is to smoothly follow a target with natural, spring-like motion.

<br>

### Basic Example: Follow a Target

Here's a minimal example showing how to follow a target using the `SecondOrder<Vector3>` component:

```csharp
using UnityEngine;

namespace Plugins.SecondOrder.Runtime
{
    /// <summary>
    /// FollowTargetExample is a simple example of how to use the FollowTarget component.
    /// </summary>
    public class FollowTargetExample : MonoBehaviour
    {
        /// <summary>
        /// Target is the transform that you want to follow.
        /// </summary>
        [SerializeField]
        private Transform _target;
        
        /// <summary>
        /// SecondOrder is a component that allows you to follow a target with a second order filter.
        /// </summary>
        [SerializeField]
        private SecondOrder<Vector3> _secondOrder;

        private void Start()
        {
            // Initialize the second order filter with the current position of the transform. (optional)
            _secondOrder.Init(transform.position);
        }

        private void Update()
        {
            // Update the second order filter with the current position of the target.
            transform.position = SecondOrderDynamics.Update(
                _target.position, _secondOrder, Time.deltaTime);
        }
    }
}
```

<br>

### How It Works

- `SecondOrder<T>` is a generic struct that holds state for the dynamic system.
- `SecondOrderDynamics.Update()` computes the updated value each frame based on the input target, internal state, and `deltaTime`.
- You can control the behavior using parameters such as **damping**, **frequency**, and **response** through the `SecondOrder<T>` initialization.

<p align="center">
  <img src="https://github.com/user-attachments/assets/882948d8-2934-49c2-a3c1-f3f4bd213060" width="500" />
</p>

This makes it perfect for:

- Camera smoothing  
- Target following  
- UI transitions  
- Any situation where you want physically-inspired motion without writing custom smoothing logic

<br>

### Supported Types

The `SecondOrder<T>` system is designed to work with multiple Unity data types out of the box. The following types are currently supported:

- `float` — for scalar values like speed, zoom, or UI transitions.
- `Vector2` — for 2D positions, screen-space UI elements, etc.
- `Vector3` — for 3D positions, character movement, or camera smoothing.
- `Quaternion` — for smooth and realistic rotation interpolation.

Each supported type has overloads for:

- **Basic update** with automatic velocity estimation:
  ```csharp
  var newValue = SecondOrderDynamics.Update(targetValue, secondOrder, deltaTime);
  ```

<br>

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). See the [LICENSE](LICENSE) file for more details.
