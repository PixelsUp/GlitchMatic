# PixelsUp

# GDD

## PixelsUp - GlitchMatic  
**September 25th, 2024**

### Designed by PixelsUp:  
Iván Jiménez Blas, Sergio López Llorente, Sonia Montero Nogales, Álvaro Redondo Molina, Alejandro Serrejón Beltrán, Marcos Vivar Muiño

---

## Index

1. [Introduction](#introduction)  
    1.1 [Brief Concept Description](#brief-concept-description)  
    1.2 [Brief Story and Characters Description](#brief-story-and-characters-description)  
    1.3 [Purpose, Target Audience, and Platforms](#purpose-target-audience-and-platforms)  

2. [Business and Monetization Model](#business-and-monetization-model)  
    2.1 [Business Model](#business-model)  
    2.2 [Ad-Supported Model](#ad-supported-model)  
    2.3 [Empathy Map](#empathy-map)  
    2.4 [Canvas](#canvas)  
    2.5 [Toolbox](#toolbox)  
    2.6 [Monetization Model](#monetization-model)  
        2.6.1 [Ad-Supported Gameplay](#ad-supported-gameplay)  
        2.6.2 [In-Game Currency and Cosmetic Purchases](#in-game-currency-and-cosmetic-purchases)  
    2.7 [Product and Pricing Tables](#product-and-pricing-tables)  

3. [Planning and Costs](#planning-and-costs)  
    3.1 [Human Team](#human-team)  
    3.2 [Estimated Development Time](#estimated-development-time)  
    3.3 [Associated Costs](#associated-costs)  

4. [Gameplay Mechanics and Game Elements](#gameplay-mechanics-and-game-elements)  
    4.1 [Detailed Concept](#detailed-concept)  
    4.2 [Detailed Mechanics](#detailed-mechanics)  
    4.3 [Controls](#controls)  
    4.4 [Levels and Missions](#levels-and-missions)  
    4.5 [Objects, Weapons, and Power-ups](#objects-weapons-and-power-ups)  
    4.6 [Software Architecture](#software-architecture)  

5. [Backstory](#backstory)  
    5.1 [Detailed Story](#detailed-story)  
    5.2 [Characters](#characters)  
    5.3 [Environments and Places](#environments-and-places)  

6. [Art](#art)  
    6.1 [General Aesthetic](#general-aesthetic)  
    6.2 [Pixel Art](#pixel-art)  
    6.3 [Visual Style](#visual-style)  
    6.4 [Concept Art](#concept-art)  
    6.5 [2D Pixel Art](#2d-pixel-art)  

7. [Animation](#animation)  
    7.1 [Animation Style](#animation-style)  
    7.2 [Character Animations](#character-animations)  
    7.3 [Enemy Animations](#enemy-animations)  
    7.4 [Rigging & Skinning](#rigging-skinning)  
    7.5 [Scenario Animations](#scenario-animations)  

8. [Sound](#sound)  
    8.1 [Ambient Sound and Music](#ambient-sound-and-music)  
    8.2 [Sound Effects](#sound-effects)  
    8.3 [Sound List](#sound-list)  

9. [Interface](#interface)  
    9.1 [Basic Menu Designs](#basic-menu-designs)  
    9.2 [Flow Diagram](#flow-diagram)  

10. [Development Roadmap](#development-roadmap)  
    10.1 [Milestone 1: Prototype](#milestone-1-prototype)  
    10.2 [Milestone 2](#milestone-2)  
    10.3 [Milestone 3: Testing & Polishing](#milestone-3-testing-polishing)


---

## 1. Introduction

In **GlitchMatic**, players experience a 2D pixel-art, roguelike game where they control a character trapped in a tower of glitches. The tower is riddled with broken environments, malfunctioning enemies, and an unstable reality. The character believes the solution to fix the world’s glitches lies at the top of the tower, but their own personal glitch causes them to respawn at the entrance every time they die, locking them in a loop of infinite attempts to climb to the top.

### Brief Concept Description

The game combines elements of speedrunning and roguelike gameplay. The player races against time through glitchy environments, defeating enemies and solving puzzles to progress. Death resets them to the beginning, with each respawn offering a slightly altered experience. The game emphasizes replayability, fast reflexes, and strategic thinking.

### Brief Story and Characters Description

The main character is a **Glitch Hunter** who enters the tower with a mission to fix the glitches that have corrupted the world. However, they are unaware of their own glitch: every time they die, they are reset to the tower's entrance. The game’s loop revolves around this endless pursuit, with no one ever truly reaching the top.

- **Main Character**: The Glitch Hunter – a stoic, determined individual whose goal is to stop the glitches, unaware of their own curse.
- **Tower Entities**: Various glitch-based enemies that inhabit the tower, each with unique behaviors affected by the world’s instability.

### Purpose, Target Audience, and Platforms

- **Purpose**: Provide an immersive roguelike experience with a unique glitch-based narrative, challenging both speedrunners and casual players alike.
- **Target Audience**: Fans of roguelikes, speedrunners, and those who enjoy narrative-driven, pixel-art games like *The Binding of Isaac* and *Celeste*. It focuses on both competitive players and casual players who prefer the game’s theme and progression at their own pace.
- **Platforms**: PC, with potential expansion to consoles.

---

## 2. Business and Monetization Model

### Business Model

**Ad-Supported Model**

*GlitchMatic* follows an ad-supported business model. The game is free to download and play, generating revenue primarily through in-game ads displayed at key moments (e.g., between levels or after player respawns). Players can choose to pay a one-time fee to permanently remove ads, providing an option for a seamless, uninterrupted experience.

Additionally, players can purchase in-game currency with real money to unlock cosmetic items like character skins and weapon skins, which do not affect gameplay balance, ensuring a fair experience for both paying and non-paying users.

- **Empathy map**
- **Canvas**
- **Toolbox**

### Monetization Model

- **Ad-Supported Gameplay**: Players will experience ads during natural breaks in gameplay after deaths. A pay-per-tap advertising feature is integrated, where players can watch an ad to double their in-game currency after death.
- **In-Game Currency and Cosmetic Purchases**: Players can purchase in-game currency with real money or earn it through gameplay. The currency can be spent on cosmetic items such as character skins, weapon skins, and special visual effects.

**No pay-to-win mechanics**: All purchasable items are purely cosmetic and do not provide any in-game advantages.

### Product and Pricing Tables

| **Product**              | **Price Range**           |
|--------------------------|---------------------------|
| Remove ads (One-time purchase) | $XX.XX                  |
| In-Game Currency Packages  | $X.XX - $XX.XX           |
| Character Skins           | X In-Game Currency         |
| Weapon Skins              | X In-Game Currency         |
| Special Visual Effects    | X In-Game Currency         |
| Pay-Per-Tap Ad Reward     | Free by Watching Ads       |

---

## 3. Planning and Costs

- **Human Team**: Six members responsible for game design, programming, art, sound, and project management.
- **Estimated Development Time**: Four months of development time with clear milestones and checkpoints.
- **Associated Costs**:
  - **Software licenses**: Game engine, art software, and sound editing tools.
  - **Marketing and promotion budget**: Social media campaigns, trailers, and early access demos.
  - **Potential porting to consoles**: Costs for porting the game from PC to consoles.

---

## 4. Gameplay Mechanics and Game Elements

### Detailed Concept

- **Core Gameplay Loop**: The player progresses through rooms filled with enemies and glitches, aiming to clear each level as fast as possible. The game restarts upon death, with slight variations in the tower layout and AI behavior.

### Detailed Mechanics

- **Movement**: Standard movement mechanics (run, attack).
- **Combat**: Fast-paced, with a variety of weapons (glitch guns, melee attacks).
- **Time Mechanic**: Players race to clear as many rooms as possible before dying.
- **Upgrading**: As players progress through levels, they can upgrade their character, making each playthrough easier.

### Levels and Missions

- **Rooms**: Procedurally generated rooms with increasing difficulty.
- **Glitch Hazards**: Obstacles that alter gameplay unpredictably.

### Objects, Weapons, and Power-ups

- **Glitch Weapons**: Reflect the chaotic nature of the world.
- **Power-ups**: Personalize each playthrough and empower the player.

### Software Architecture

- **Modular structure**: AI, room generation, and combat systems are modular, allowing dynamic development and testing.

---

## 5. Backstory

### Detailed Story

The tower stands as the central point of the time-world’s corruption. The Glitch Hunter enters the tower, hoping to fix the glitches and restore the timeline to its original form, but becomes trapped in an endless loop of resetting, cursed to repeat the same fate over and over again. The story indirectly unfolds as the player moves upward.

### Characters

- **The Glitch Hunter**: A lone protagonist determined to fix the glitches.
- **Tower Inhabitants**: Glitchy enemies with AI that evolves based on player behavior. These entities came from various eras and places across time, and they will do everything they can to stop the Hunter from achieving his goal.

### Environments and Places

- **Tower Rooms**: Procedurally generated rooms provide unique experiences to enhance replayability.

---

## 6. Art

### General Aesthetic

- **Pixel Art**: Retro-inspired, with glitch effects and distortions adding to the game’s chaotic atmosphere.
- **Visual Style**: Dark, futuristic with a focus on digital corruption and broken environments.

### Concept Art

- Early sketches of characters, weapons, and enemies will be used as a guide for final assets.

### 2D Pixel Art

---

## 7. Animation

### Animation Style

- **Character Animations**: Smooth, responsive movements including running, jumping, and attacking.
- **Enemy Animations**: Erratic and glitchy movements reflecting the unstable environment.
- **Scenario Animations**: Glitchy environmental elements change unpredictably.

---

## 8. Sound

### Ambient Sound and Music

- A glitch-inspired soundtrack filled with synthetic and distorted sounds to reflect the unstable world.

### Sound Effects

- Effects for enemy deaths, weapon usage, and environmental interactions fitting the overall theme.

### Sound List

- Footsteps, weapon attacks, enemy and character growls, and environmental glitches.

---

## 9. Interface

### Basic Menu Designs

- **Simple UI**: Glitch-themed UI reflecting the game’s concept of digital instability.
- **Flow Diagram**: 

---

## 10. Development Roadmap

### Milestone 1: Prototype

- Finalize the core game concept.
- Develop early Pre-Alpha build with a playable environment. This version will have an initial view of the final game.

### Milestone 2

- Implement core mechanics (movement, combat, AI behavior).
- Begin creating the procedural room generation system.

### Milestone 3: Testing & Polishing

- Playtesting and refining the game for balance.
- Finalizing assets, animations, and sound.

### Release Date

- **Estimated Release Date**: TBD
