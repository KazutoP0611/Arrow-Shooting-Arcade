# 3D Archery Challenge : Arrow Shooting Arcade (High-Quality 3D Shooting Game)

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
  - Aiming mode (focused precision view)

Player state determines active virtual camera and transition blending, ensuring smooth perspective changes.

---

## 🎯 Target & Scoring System

### Modular Target Prefab
- A single reusable prefab supports multiple target types.
- Target behavior is configured via parameters:
  - Movement speed
  - Movement direction
  - Score value
  - Target type (Green / Red)

This system allows level designers to configure stages without modifying code.

---

### Target Logic

- 🟢 Green Target → Awards points
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/22f1dd20-e0cd-4c6a-8e10-1a70ce54c630" />

- 🔴 Red Target → Risk / Reward mechanic
  <img width="854" height="480" alt="image" src="https://github.com/user-attachments/assets/2017e75c-0d25-4f61-b59d-9eb7596d1594" />

Red target scoring rules:
- Hitting outside the bullseye → Lose points  
- Hitting the bullseye → Gain 10x the base score  

This introduces strategic decision-making rather than simple avoidance.

---

## 🧮 Point Counter Manager
- Automatically detects and registers targets placed in the scene
- Identifies target types
- Manages scoring logic centrally
- Keeps gameplay scalable and maintainable

---

## 🖥 UI & State Control
- UI Manager handles menu transitions
- Opening UI disables player input and movement
- Ensures clean gameplay-state separation

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
