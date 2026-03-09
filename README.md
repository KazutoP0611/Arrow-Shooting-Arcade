# 3D Archery Challenge : Arrow Shooting Arcade (High-Quality 3D Shooting Game)

## 🎥 Gameplay Video
[Watch Gameplay Video](https://youtu.be/R1gA_aSVZLw)

---

#### 🏹 3D Archery Challenge : Arrow Shooting Arcade
A high-quality 3D archery game built in Unity, focused on advanced animation state management, dynamic camera control, and flexible scoring systems designed for level scalability.
This project emphasizes smooth character transitions, responsive aiming mechanics, and designer-friendly target configuration.

<img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/a1aa4170-777f-4783-ae2f-8c98abc37e93" />

---

## ⚙️ Technical Highlights
- Engine: Unity 6 (6000.0.3f1)
- Programming Language: C#
- Complex Unity Animator State Machine with smooth transition blending
- Cinemachine-based dynamic camera system
- State-driven camera behavior (Aiming / Normal movement)
- Modular target prefab system with configurable parameters
- Centralized point counter manager
- UI system

---

## 🎮 Core Gameplay

### 🏹 Archery System
- Third-person bow shooting mechanics
- Animator state machine handles blended transitions between locomotion and action animations

<img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/d4abb6a0-c5f6-49bd-89dd-d42663d86661" />

---

## 🎞 Advanced Animation State Machine

The character animation system is built using a structured Unity Animator State Machine with layered locomotion and combat transitions.

<img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/77dafdbe-2844-4e66-a3ac-f360b4e426ed" />

### Implemented States:
- Idle
- Walk
- Sprint
- DrawArrow
- OverDraw (charged state)
- Shoot
- Walking_Aim

### Animation Flow Design:
- DrawArrow transitions into OverDraw using Exit Time to ensure animation continuity
- OverDraw cannot be entered directly — it is a continuation state from DrawArrow
- Exit Time is used strategically for smooth animation continuity (Draw → OverDraw)
- Multiple parameters control transitions (running, waling, aiming, shoot action triggers)
- Smooth blending to preserve responsiveness during gameplay

This structure ensures:
- Fluid movement-to-combat transitions
- Responsive input handling
- Clear visual feedback for charge-based shooting mechanics

---

## 🎥 Camera Management

- Cinemachine is used for dynamic camera control
- Camera behavior changes based on player state:
  - Normal movement mode
    <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/17214a73-d782-4ccf-ad45-f5d678d2cb7c" />

  - Aiming mode (focused precision view)
    <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/5b1ad26a-db41-4457-b59d-5e0bea66733a" />

Player state determines active virtual camera and transition blending, ensuring smooth perspective changes.

---

## 🎯 Target & Scoring System

### Modular Target Prefab
- A single reusable prefab supports multiple target types.
- Target behavior is configured via parameters:
  Such as
  - Movement speed
  - Movement direction
  - Score base value
  - Target type (Green / Red)
 
<img width="1779" height="1000" alt="image" src="https://github.com/user-attachments/assets/9714fc14-43d4-4690-8c67-fcea213a3186" />

This system allows level designers to configure stages without modifying code.

---

### Target Logic

- 🟢 Green Target → Awards points
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/22f1dd20-e0cd-4c6a-8e10-1a70ce54c630" />

- 🔴 Red Target → Risk / Reward mechanic
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/2017e75c-0d25-4f61-b59d-9eb7596d1594" />
  
**Red target scoring rules:**
- Hitting outside the bullseye → Lose points
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/0c0d519a-7cf7-4d5f-a93f-402778fe03ff" />
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/bc26c38d-bbbd-44c8-b9ed-2d74d92f695d" />
  
- Hitting the bullseye → Gain 10x the base score
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/778c32cc-bcd5-434a-bf2f-934fc1ca8c46" />
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/cb6b4a57-344f-441f-9f96-026fce1033a8" />

This introduces strategic decision-making rather than simple avoidance.

---

## 🧮 Point Counter Manager
- Automatically detects and registers targets placed in the scene
- Identifies target types
- Manages scoring logic centrally
- Keeps gameplay scalable and maintainable

<img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/bd213392-e218-4de8-8f74-2482057f34fb" />

---

## 🖥 UI & State Control
- UI Manager handles menu transitions
- Opening UI disables player input and movement
- Ensures clean gameplay-state separation

<img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/d91803f9-11d8-427e-9f87-f324514eb582" />

---

## 🔄 Scene Management
- Smooth scene transitions
- Controlled gameplay flow between levels

---

## 🧠 Gameplay Focus
- Responsive animation transitions
- State-based camera behavior
- Risk vs. reward scoring mechanics
- Reusable and scalable level design systems

---

This project emphasizes animation system design, modular gameplay architecture, and scalable scoring mechanics within a polished 3D environment.
