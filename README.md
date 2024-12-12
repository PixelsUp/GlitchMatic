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
        2.1.1 [Ad-Supported Model](#ad-supported-model)  
        2.1.2 [Empathy Map](#empathy-map)  
        2.1.3 [Canvas](#canvas)  
        2.1.4 [Toolbox](#toolbox)  
    2.2 [Monetization Model](#monetization-model)  
        2.2.1 [Ad-Supported Gameplay](#ad-supported-gameplay)  
        2.2.2 [In-Game Currency and Cosmetic Purchases](#in-game-currency-and-cosmetic-purchases)  
    2.3 [Product and Pricing Tables](#product-and-pricing-tables)  

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
        7.1.1 [Character Animations](#character-animations)  
        7.1.2 [Enemy Animations](#enemy-animations)  
        7.1.3 [Scenario Animations](#scenario-animations)  
        7.1.4 [Damage Animations](#damage-animations)  
        7.1.5 [Death Animations](#death-animations)  

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
    10.4 [Release Date](#release-date)


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

### Empathy map

<p align= "center"><strong> Empathy Map </strong> </p>
<p align="center">
  <img src="https://github.com/user-attachments/assets/da72bd94-df1d-4156-b061-679eea30155b" width= '800' height='450'/>
</p>
<p align=center>Image 2.1 Empathy Map</p>
  
### Canvas

- This business canvas outlines the core structure of PixelsUp’s gaming business model centered around free-to-play games with in-game purchases and ad-based monetization. Key Partners include ad networks (e.g., Unity Ads, Google AdMob) for revenue through ads, distribution platforms (e.g., Steam, Epic Games) for game hosting, and payment processors (e.g., Stripe, PayPal) to manage transactions. Key Activities revolve around game development, ad integration, and offering cosmetic items. The business provides a Value Proposition emphasizing fair, skill-based gameplay without pay-to-win mechanics, with optional cosmetic upgrades for customization that do not impact performance. Key Resources include developers, designers, and server infrastructure to support ads and in-game transactions.

- The business targets diverse Customer Segments, including free-to-play users who watch ads, premium players who pay to remove ads, cosmetic buyers seeking visual upgrades, and community members like streamers and influencers. It builds Customer Relationships via in-game notifications, social media marketing, and sales promotions, leveraging Channels such as PC distribution platforms and social media. Revenue Streams include ad revenue, in-app purchases (ad removal or cosmetic items), and limited-time bundles. The Cost Structure primarily includes development, marketing, platform fees, and content updates, ensuring ongoing engagement and monetization. The ecosystem balances free access with optional purchases, supported by strategic partnerships and efficient cost management.

- The model allows for a sustainable ecosystem that favors growth and a safe investment.

<p align= "center"><strong> Canvas </strong> </p>
<p align="center">
  <img src="https://github.com/PixelsUp/GlitchMatic/blob/main/Assets/Images/Canvas.jpg" width= '800' height='500'/>
</p>
<p align=center>Image 2.2 Canvas</p>

  
### Toolbox

Represents a business ecosystem. Pixels Up functions as a central platform connecting various stakeholders and facilitating exchanges of goods, services, and money.

- **Suppliers**: Exchanges exposure in the form of ads for their respective monetary value to PixelsUp.

- **Consumers**: End users of the platform, paying for services or products such as in-game ads, cosmetics, or other digital content.

- **Google**: Represents an advertising platform or infrastructure provider. Google allows for an ad hosting service while they receive their monetary compensation.

- **Unity**: Provides a development environment for game creation. Unity is paid for its services.

- **Launch Platform**: Distributes the content produced by PixelsUp to the market or audiences. They receive the product and, depending on their policies, financial compensation. In exchange, they offer exposure for PixelsUp's offerings. GlitchMatic will launch in itch.io as soon as possible.


<p align= "center"><strong> ToolBox </strong> </p>
<p align="center">
  <img src="https://github.com/user-attachments/assets/9a797a2c-1c07-485c-8f45-d506eeab7520" width= '600' height='500'/>
</p>
<p align=center>Image 2.3 ToolBox</p>

### Monetization Model

- **Ad-Supported Gameplay**: Players will experience ads during natural breaks in gameplay after deaths. A pay-per-tap advertising feature is integrated, where players can watch an ad to double their in-game currency after death. Players can also make a one-time payment to permanently remove all ads, providing a smoother, ad-free experience.
- **In-Game Currency and Cosmetic Purchases**: Players can purchase in-game currency with real money or earn it through gameplay. To be archived in game, the player will finish different runs of the game loop to get a different small value that varies with how many rooms he has cleared. The currency can be spent on cosmetic items such as character skins, weapon skins, and special visual effects. They will be displayed and available for purchase in the shop.

**No pay-to-win mechanics**: All purchasable items are purely cosmetic and do not provide any in-game advantages.

### Product and Pricing Tables

| **Product**              | **Price Range**           |
|--------------------------|---------------------------|
| Remove ads (One-time purchase) | $9.99                  |
| In-Game Currency Packages  | $X.XX - $XX.XX           |
| Character Skins           | X In-Game Currency         |
| Weapon Skins              | X In-Game Currency         |
| Special Visual Effects    | X In-Game Currency         |
| Pay-Per-Tap Ad Reward     | Free by Watching Ads       |

---

## 3. Planning and Costs

### Human Team  
Our team consists of six members, each specializing in different areas of the game's development:  
- **Game Design**: Conceptualizing gameplay mechanics and creating the overall game design.  
- **Programming**: Coding the game, creating systems, and implementing gameplay mechanics using Unity. Unity is the chosen game engine due to its robust toolset, cross-platform support, and flexibility in handling 2D games. Unity’s extensive asset store and community support will allow us to accelerate development and find solutions efficiently.  
- **Art**: Creating 2D pixel art assets, including characters, environments, and items, as well as concept art.  
- **Sound**: Producing sound effects and music, ensuring a glitchy, immersive audio experience.  
- **Project Management**: Managing timelines, coordinating tasks between team members, and ensuring we meet development goals.  

### Estimated Development Time  
We estimate a development time of two to three months, with clear milestones and checkpoints along the way. These milestones will help us stay on track and allow for adjustments as necessary. Regular reviews will ensure that we are meeting our goals at each stage.

### Associated Costs
  - **Software licenses**: Game engine (Unity), art software, and sound editing tools.
  - **Marketing and promotion budget**: Social media campaigns, trailers, and early access demos. A LinkTree has been developed with all the corresponding websites.
  - **Potential porting to consoles**:  While the initial development will focus on PC, we are considering porting the game to consoles like PlayStation and Xbox. This will involve additional costs for platform certification, development kits, and ensuring compatibility across different systems. Porting will depend on the game's success and demand from the player base.

---

## 4. Gameplay Mechanics and Game Elements

### Detailed Concept

- **Core Gameplay Loop**: The player progresses through rooms filled with enemies and glitches, aiming to clear each level as fast as possible. The game restarts upon death, with slight variations in the tower layout and AI behavior.

### Detailed Mechanics

- **Movement**: Standard movement mechanics (run, attack), with a glimpse of RPG systems, such as the roll mechanic. The roll mechanic is a key concept of the game. Enemies are designed around it, making it hard to use and abuse, as it has a small amount of invincibility frames that assist in the elimination of certain theme bosses throughout the gameplay runs of the players.
- **Combat**: Fast-paced, with a variety of weapons (glitch guns, melee attacks). The player will select between a melee option and a ranged option, which changes the way the enemies behave drastically. This makes it so the AI feels fresh every run and avoids any clear abusable strategies suited for speedrunning.
- **Time Mechanic**: Players race to clear as many rooms as possible before dying. This mechanic makes the player hesitant to take their time against more defensive enemies and forces him to, when being competitive, organise their combat according to the rooms' exit and entrance.

### Levels and Missions

- **Rooms**: Procedurally generated rooms with increasing difficulty. Enemies are spawned differently every room, enhancing the replayability aspect of the game. They are built to be dynamic and immersive. Enemies respond to the environment accordingly, making every room feel fresh and difficult in its own way.
- **Glitch Hazards**: Obstacles that alter gameplay unpredictably. They are meant to be used as covers for the player or the enemies, as well as stopping its progress through certain stages.

### Controls

- Movement: Players use WASD keys to move their character around the environment.
- Rolling: When the Spacebar is pressed the character rolls. This allows for some invincibility frames, essential for surviving difficult encounters and allowing the player to dodge certain attacks.
- Aiming: The mouse is used to aim the weapon at enemy targets. When the players' cursor is on top of an enemy, the cursor automatically indicates it as a target.
- Attacking: To either shoot or perform melee attacks, the left mouse button is pressed.

### Objects, Weapons, and Power-ups

- **Glitch Weapons**: Reflect the chaotic nature of the world. Distinction between melee and ranged options, including swords, bows and guns. This makes the different room themes feel fresh and new for players to explore and experiment.

### Software Architecture

- **Modular structure**: AI, room generation, and combat systems are modular, allowing dynamic development and testing.

---

## 5. Backstory

### Detailed Story

The tower stands as the central point of the time-world’s corruption. The Glitch Hunter enters the tower, hoping to fix the glitches and restore the timeline to its original form, but becomes trapped in an endless loop of resetting, cursed to repeat the same fate over and over again. The story indirectly unfolds as the player moves upward.

### Characters

- **The Glitch Hunter**: A lone protagonist determined to fix the glitches.
    - His destiny is to try to destroy the glitches happening in the world, which leads him to the tower which holds the origin of the disaster. He will attempt to end it all, unknowingly entering a neverending crusade.

<p align="center">
  <img src="https://github.com/user-attachments/assets/8cb0c156-9607-4a73-9592-3f189eb6d33d" width= '300' height='300'/>
</p>
<p align=center>Image 5.1 Glitch Hunter</p>


- **Tower Inhabitants**: Glitchy enemies with AI that evolves based on player behavior. These entities came from various eras and places across time, and they will do everything they can to stop the Hunter from achieving his goal.

    The rooms are never ending, and they are placed there as the protectors of the root of the glitches that are happening around the world. The glitches' origin stops anyone who tries to destroy it by creating more and harder rooms as the player gets closer to it.

<p align="center">
  <img src="https://github.com/user-attachments/assets/e54cab36-1e4b-4ab7-8023-51c3edd1fc69" width= '300' height='300'/>
</p>
<p align=center>Image 5.2 The Knight</p>


### Environments and Places

- **Tower Rooms**: Procedurally generated rooms with varied challenges that enhance replayability. There will be a selection of themes which spawn randomly every three rooms. These make it so the player is constantly engaging with different enemies with different strengths.
  
    - The rooms are neverending, and they are placed there as the protectors of the root of the glitches that are happening around the world. The glitches' origin stops anyone who tries to destroy it by creating more and harder rooms as the player gets closer to it.

<p align="center">
  <img src="https://github.com/user-attachments/assets/de47e342-a4b6-43a2-b2a2-f04d06d998f2" width= '500' height='400'/>
</p>
<p align=center>Image 5.3 Medieval Room</p>

---

## 6. Art

### General Aesthetic  
  The general aesthetic of the game is heavily influenced by retro and glitch-inspired themes, where digital distortion becomes a core visual element. The game will feature pixel art reminiscent of older 2D titles but enhanced with modern glitch effects, such as pixelation distortion, color shifts, and broken environmental assets, to evoke a world constantly on the verge of collapse.

### Pixel Art  
  The pixel art style is retro-inspired, bringing a nostalgic feel to the game. However, the addition of glitch effects, such as pixel scrambling and color disintegration, creates a chaotic, disorienting atmosphere. These distortions emphasize the core game theme of a fractured, unstable reality. By using pixel art, we can keep development streamlined while leveraging the aesthetic's simplicity to enhance visual effects without overcomplicating the production pipeline.
  
### Visual Style  
  The game’s visual style is dark, futuristic, and focused on the idea of digital corruption. Broken environments, distorted assets, and glitchy characters contribute to an overall feeling of instability. The color palette will reflect this with muted tones, punctuated by bright, glitchy visuals. Shadows, light distortions, and flickering environments will play an important role in conveying a world on the edge of collapse.

### Concept Art  
  Early sketches of characters, weapons, and enemies will guide the development of final assets. These concept pieces will serve as the foundation for designing the pixel art versions of in-game elements. Each character and enemy design will incorporate glitch-inspired traits to visually distinguish them as part of the unstable digital world.

<p align="center">
  <img src="https://github.com/user-attachments/assets/270befe2-4f92-4c32-9d23-4f83ae70b800" width= '500' height='300'/>
</p>
<p align=center>Image 6.1 Character concept art with different variations</p>
	Making  a moodboard has been key in the creation of the visual elements. Allowing to make the game visually cohesive.
 <p align="center">
  <img src="https://github.com/user-attachments/assets/a78260a1-ea46-4201-96ca-2f5e9d3d1a1c" width= '500' height='300'/>
</p>
<p align=center>Image 6.2  Game Art Moodboard</p>


### 2D Pixel Art  
  The decision to use 2D pixel art helps avoid unnecessary complications while perfectly suiting the game's aesthetic. The simplicity of pixel art allows us to focus on glitch effects and detailed environmental storytelling. This style complements the digital instability and chaotic nature of the game's world, enabling smooth gameplay transitions and easier asset creation.

Additionally, we will create social media links and marketing content to showcase the development process. Concept art, early character animations, and teasers will be shared across platforms like Twitter, Instagram, and YouTube. These posts will help build excitement and draw in our target audience.

### Game Backgrounds

The style of the game backgrounds, like the rest, is made with pixelart. There are three different themes, where the said style changes. Additionally, the size and shape of the room changes.
<p align="center">
  <img src="https://github.com/user-attachments/assets/f602edad-0912-45bb-b6ba-ba535ccdcc90" width= '300' height='200'/>
  <img src="https://github.com/user-attachments/assets/ae55a034-6b67-4639-8576-81e5a6d00563" width= '300' height='200'/>
  <img src="https://github.com/user-attachments/assets/8288cdf7-64fa-445a-a16c-59d243ea853b" width= '300' height='200'/>
</p>
<p align=center>Image 6.3 Game Background</p>
---

## 7. Animation

### Animation Style

- **Character Animations**: Smooth, responsive movements including running, rolling, and attacking. They respond to the players' inputs, selected to be as intuitive as possible. They try to be as clear as possible and interact responsively with the weapon animations of the character. These are developed to get a clear feel of the combat.
- **Enemy Animations**: Erratic and glitchy movements reflecting the unstable environment. They act according to the player's actions, as they are adjusted to be used along the main mechanics of the game.
- **Mele enemies**: This type of enemy will attack the player by approaching him with a melee weapon, which will vary depending on the theme of the enemy.
<p align=center>
  <img src="https://github.com/user-attachments/assets/e753876c-8704-4b6f-82ff-9531b8e7c5f9" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/3fff2c4b-bab4-4970-a258-1224fc2bddb5" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/7bb212c2-9a76-456e-84b5-b9cf6eb9a8ec" width= '300' height='300'/>
 </p>
<p align=center>Image 7.1 Mele enemies</p>

- **Ranged enemies** :
This type of enemy will attack the player by keeping distance with the player and attacking him with a ranged weapon, which will vary depending on the theme of the enemy.
<p align=center>
  <img src="https://github.com/user-attachments/assets/9f7918ad-3724-409c-a05a-ed6971502c94" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/0cd40319-5b3a-45ef-9965-8634342e9f43" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/b40d191a-febd-40eb-864f-8ca59bf74c76" width= '300' height='300'/>
</p>
<p align=center>Image 7.2 Ranged enemies</p>

- **Whistler enemies**:
This enemy will not attack the player, but will notify the rest of the enemies of their location.
<p align=center>
  <img src="https://github.com/user-attachments/assets/80c89d73-ea65-432f-9235-b5002256bdcd" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/dbfec219-e481-41ed-9743-e4d7a6a89799" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/e0990f0f-3bbd-460a-85f8-4c17c5fb5413" width= '300' height='300'/>
</p>
<p align=center>Image 7.3 Whistler enemies</p>


- **Scenario Animations**: Glitchy environmental elements change in every room, making the player feel better about the replayability of the game. They make the enemies feel more alive and intractable.
- **Damage Animations**: When the main character or enemies take damage, a glitch effect briefly distorts their bodies, emphasizing the impact. This visual "glitch" animation gives the sensation that the character is destabilized by the attack.

<p align="center">
  <img src="https://github.com/user-attachments/assets/c9931d22-0e6e-463d-bd6e-7534358f95ba" width= '300' height='300'/>
</p>
<p align=center>Image 7.4 Character hurt animation</p>

- **Death Animations**: Upon death, the glitch effect becomes more aggressive, fully corrupting their form. The character or enemy breaks apart into a chaotic sequence of glitchy distortions, signifying their complete destruction within the game world.

<p align="center">
  <img src="https://github.com/user-attachments/assets/b9c74592-8106-4127-9357-9890d7574d4c" width= '300' height='300'/>
</p>
<p align=center>Image 7.5 Character dead animation</p>

- **Drake Animations**: 
The boss of the medieval theme is a drake. Three attack animations have been made, along with damage and death animations.

<p align=center>
  <img src="https://github.com/user-attachments/assets/15da9803-f77b-43d8-bcc0-b56c811c571d" width= '250' height='300'/>
  <img src="https://github.com/user-attachments/assets/0fed56d3-956c-4a9f-ad53-6cc86f838f1f" width= '250' height='300'/>
  <img src="https://github.com/user-attachments/assets/32f684e4-5332-49b1-aad6-b5f4cf2b46c7" width= '250' height='300'/>

<p align=center>Image 7.6 Drake animation</p>

- **Cyber Skull Animations**:
The boss of the cyberpunk theme is a flying robotic skull. Three attack animations have been made, along with damage and death animations.

<p align=center>
  <img src="https://github.com/user-attachments/assets/e87d6a68-4908-4548-a06a-e2dd5a68b556" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/ac9a66b8-373a-43eb-a25e-9c6c3c68ffe5" width= '300' height='300'/>
  <img src="https://github.com/user-attachments/assets/64b06d19-d2b9-48c5-8134-550223fc2c90" width= '300' height='300'/>

<p align=center> Image 7.7 Cyber Skull animation </p>



---

## 8. Sound

### Ambient Sound and Music  
A glitch-inspired soundtrack with synthetic and distorted sounds to emphasize the unstable world. The main menu features a glitch-inspired loop with synthetic and distorted elements. The music flows seamlessly, allowing it to loop perfectly without any noticeable start or end points, creating a hypnotic, continuous atmosphere. This fits the theme of a destabilized world, where the audio subtly distorts reality. The composition relies on digital, glitchy textures, and sparse melodic elements to evoke an unsettling, futuristic mood.

Each area or room in the game also features its own soundtrack, consisting of short 6-10 second loops. These loops share the same drum base for consistency, but differ in basslines and additional elements to distinguish the atmosphere of each room. The short loops are essential to creating smooth transitions between rooms, avoiding abrupt changes in the music and maintaining immersion.

### Sound Effects  
Sound effects are essential for immersing players in the distinct environments of each room while maintaining the glitch theme tied to the protagonist. Although each room has a unique setting, many sound effects will be shared across rooms to ensure consistency. For example, certain enemy death sounds will remain common, reflecting the idea that defeating them glitches them into the protagonist's unstable reality. Similarly, weapon and environmental sounds may be adapted to fit each theme, but core elements—like subtle glitch distortions—will unify the audio experience. This approach allows for creative diversity while maintaining cohesion throughout the game.

### Sound List  
- Footsteps  
- Weapon attacks  
- Enemy deaths  
- Growls and vocalizations  
- Environmental interactions  
- Door sounds  
- Object pickups  

---

## 9. Interface

### Basic Menu Designs

- **Simple UI**: The game's interface will feature a simple, glitch-themed UI that complements the overall concept of digital instability. The main menu will include glitch effects like distorted text, flickering elements, and digital artifacts to immerse players in the chaotic, unstable world from the very beginning. Buttons and transitions will incorporate visual glitch effects, giving the interface a dynamic and unpredictable feel. The UI will remain intuitive and easy to navigate, ensuring that the visual theme does not interfere with functionality. Each section of the menu such as the start game, settings, and exit will have a minimalistic design but maintain thematic consistency with the glitch-based aesthetics.

<p align="center">
  <img src="https://github.com/user-attachments/assets/4e6f320a-80b5-44ed-9aad-3554eb8fe3ce" width= '600' height='300'/>
</p>
<p align=center>Image 9.1 Main menu</p>

For the character selection, credits, and leaderboard interfaces, the same background image format has been maintained for the interface, featuring elements in purple with vibrant shades.

<p align=center>
	<img src="https://github.com/user-attachments/assets/569d7280-f7e2-46fa-ae3a-6829a675e151", width= '600' height='300'/>
<p align=center> Image 9.2 Leaderboard interface </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/055112ff-8c00-47e0-acff-55a43bb7aeaa", width= '600' height='300'/>
<p align=center> Image 9.3 Select skin interface </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/9c5f8d69-995b-4b62-a787-ecdb9b5b79cf", width= '600' 
height='300'/>
<p align=center> Image 9.4 Credits interface </p>

For the options, login, and store interfaces, a futuristic cyberpunk-style background has been designed with a purple and gray color palette of a similar aesthetic.

<p align=center>
	<img src="https://github.com/user-attachments/assets/63f696da-0034-44ff-946d-be65136e2013", width= '600' 
height='300'/>
<p align=center> Image 9.5 Shop interface </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/d11babbf-b013-42b4-9dfe-518d74b4543a", width= '600' 
height='300'/>
<p align=center> Image 9.6 Login interface </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/25ec1c14-9494-4f12-a891-05b3fe5df36e", width= '600' 
height='300'/>
<p align=center> Image 9.7 Options interface </p>

Regarding the elements designed for the interface itself, numerous buttons have been developed, along with a new cursor style when hovering over an enemy, health bars, game settings bars, and a goal indicator.

<p align=center>
	<img src="https://github.com/user-attachments/assets/965b6ff3-a48f-4e18-8dd0-4f6331a7b17f", width= '600' 
height='150'/>
<p align=center> Image 9.8 Play button </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/d408c332-cb5c-4cbf-811a-87ff6c93f364", width= '1200' 
height='150'/>
	<img src="https://github.com/user-attachments/assets/36209aa7-9b67-4cd1-8c3e-e79ef64c6fc0", width= '1200' 
height='150'/>
	<img src="https://github.com/user-attachments/assets/168abbfb-e15c-46dc-bd18-c02932f902bc", width= '1200' 
height='150'/>
<p align=center> Image 9.9 Life and options bar </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/d585ac66-f24a-46f3-bd9d-c46b6fe223e0", width= '600' 
height='300'/>
<p align=center> Image 9.10 Attack cursor </p>

<p align=center>
	<img src="https://github.com/user-attachments/assets/e309abb4-299c-4aaa-b543-91254f0756a8", width= '600' 
height='300'/>
<p align=center> Image 9.11 Goal indicator </p>

- **Flow Diagram**:
<p align="center">
  <img src="https://github.com/user-attachments/assets/be9b9fbf-64f6-4c33-812e-58e778f3c6ba" width= '600' height='300'/>
  <img src="https://github.com/user-attachments/assets/23d97a61-514f-4057-b8bb-d64a74fc0988" width= '600' height='300'/>
</p>
<p align=center>Image 9.12 Flow Diagram</p>
---

## 10. Development Roadmap

### Milestone 1: Prototype  

- The first milestone focuses on finalizing the core game concept and developing an early Pre-Alpha build. This prototype will offer a basic, yet playable, environment where players can experience the core mechanics, aesthetics, and gameplay loops. The prototype will represent an initial view of the final game, including the main character's movements, environmental interactions, and early glitch effects. 

- In parallel with development, we will begin crafting social media content, establishing our online presence, and producing marketing assets to generate interest. Short gameplay teasers, sneak peeks at character designs, and environmental showcases will be posted regularly to gain traction in the community. The social media strategy will build a following that will be crucial for funding campaigns and eventual game release.

### Milestone 2: Beta Build

- Implement and clean core mechanics (movement, combat, AI behavior).
- Begin creating the procedural room generation system.

### Milestone 3: Testing & Polishing

- Playtesting and refining the game for balance.
- Finalizing assets, animations, and sound.

### Release Date

- **Estimated Release Date**: TBD
