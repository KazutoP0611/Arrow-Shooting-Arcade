# 3D Archery Challenge : Arrow Shooting Arcade (High-Quality 3D Shooting Game)

## 🎥 Gameplay Video
[Watch Gameplay Video](https://youtu.be/R1gA_aSVZLw)

---

## 🏹 3D Archery Challenge : Arrow Shooting Arcade
A 3D archery game, focused on advanced animation state management, dynamic camera control, and flexible scoring systems designed for level scalability.
This project emphasizes smooth character transitions, responsive aiming mechanics, and designer-friendly target configuration.

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/a1aa4170-777f-4783-ae2f-8c98abc37e93" />

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

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/d4abb6a0-c5f6-49bd-89dd-d42663d86661" />

---

## 🎞 Advanced Animation State Machine

The character animation system is built using a structured Unity Animator State Machine with layered locomotion and combat transitions.

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/5b05cca8-b09a-45f8-829c-979b97ad85c1" />

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

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/23bca826-54bf-424d-8130-5f7f64f8f272" />

Player state determines active virtual camera and transition blending, ensuring smooth perspective changes.

---

## 🎯 Target & Scoring System

### Modular Target Prefab
- A single reusable prefab supports multiple target types.
- Target behavior is configured via parameters:
  - Movement speed
  - Movement direction
  - Score base value
  - Target type (Green / Red)
 
<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/9714fc14-43d4-4690-8c67-fcea213a3186" />
<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/84d9cb02-ceb7-4023-a0f1-66bdaa6f7c97" />

This system allows level designers to configure stages without modifying code.

---

### Target Logic

- 🟢 Green Target → Awards points
  <br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/fe07744a-19da-4b80-9f16-0115460b9de3" />

- 🔴 Red Target → Risk / Reward mechanic
  <br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/58eb01eb-f309-4629-a142-9aca0f978162" />

**Red target scoring rules:**
- Hitting outside the bullseye → Lose points
- Hitting the bullseye → Gain 10x the base score
  <br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/6c2d7bdd-a73a-4f73-b5a4-bb48d457e4e3" />

This introduces strategic decision-making rather than simple avoidance.

---

## 🧮 Point Counter Manager
- Automatically detects and registers targets placed in the scene
- Identifies target types
- Manages scoring logic centrally
- Keeps gameplay scalable and maintainable

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/bd213392-e218-4de8-8f74-2482057f34fb" />
<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/6900ee40-d42e-4918-8df9-8db70489b4e3" />

---

## 🖥 UI & State Control
- UI Manager handles menu transitions
- Opening UI disables player input and movement
- Ensures clean gameplay-state separation

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/d91803f9-11d8-427e-9f87-f324514eb582" />

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
