# Practicum

Final paper for the 2020/21 2D game programming course. Create a fully playable game on any subject you choose.

## Choosing a theme.

Choosing a theme for the game is probably the single decision that will bear the biggest influence on the resulting game, so I decided to take as much time as needed. During the first few days of the decision process, I followed a few tutorials that offered advice for beginner and intermediate game developers, out of them I got the idea that a few points were key making a good decision:

1. The game had to be about a subject I liked myself. There are a few reasons for this, but the most obvious are that you will enjoy more the development process, and that you will be more familiar with what makes a good game.
2. Be conservative, the development process will, very likely, be harder than you expect. If you choose an objective that is too ambitious, you are likely to either never finish the game or to have so many different aspects to worry about that you will not have the resources to implement every aspect properly.
3. The most successful games are not necessarily the most innovative ones, usually they follow a well established pattern but introduce a new twist to the well known pattern. But still fall inside a category that is familiar to players.

With these guidelines in place, I started looking at options.

### A platformer game using the [Sunny Land](https://assetstore.unity.com/packages/2d/characters/sunny-land-103349) asset pack.

**Why?** This is something that I kept thinking about while developing the Mario-like game for the previous exercises, this asset pack looked fun to work with. I had just developed a pretty complete platformer game, most of that experience would help develop a better game if I had chosen this option.

**Why not?** I felt like, after working in a platformer for almost two months, I could learn more from working with a different style game.

### A Lemmings style game.

**Why?** This was one of my favorite games back on its day, and I feel like it could still be very fun to play even in the original form, even more so with some twists.

**Why not?** This game introduces many aspects that are very different to the style that I developed before. I wasn't completely sure I could produce a good quality game in the amount of time available.

### An break-bricks style game.

**Why?** This seems like a simple concept that should be fairly simple to implement. Once you have a playable game, you can introduce more variations to the _powerups_ and _bricks_ to make the game more interesting.

**Why not?** I feel like, with this style of game, most of the functionality could be implemented in-house, and I felt like experimenting with some packages that added functionality to the game, like particle systems, Cinemachine and other assets from the **Unity Asset Store**.

### A space shooter.

**Why?** Offers the possibility to build a fast-paced game with nice imagery and lots of visual effects. I couldn't really find many points against this theme. It could prove to be too complex, but it seems like it should be possible to provide, at the very least, a few playable levels in the amount of time provided. So this is the theme I decided to develop.

## The development process outline.

In this case, I think the visual part of the game is going to be very important, it makes sense to build the basic functionality first, without worrying too much about the looks, and then try imagery on to see what it looks like when the game is running.

We will start by creating a `Player` GameObject and giving it a controller. From the beginning we are going to give the game a **mobile first** approach, so the game will get mobile controls and any actions available have to be operable by touch.

The objective is to create a space shooter that runs in an open world where the player has to complete one mission per level. As the player approaches the level's objective, it will become harder to survive.

Levels should be progressively harder. The player should be able to learn about the enemies in earlier levels to help survive later levels.

## Work done.

### Outline requirements.

- The player's space fighter can decelerate or accelerate in 2D, while there is no player input, the spacecraft preserves its current speed.
- Each level is an open world, the player can choose to get farther away from the objective.
- Each level has an objective. Survival should get progressively harder as the player approaches the objective.
- The camera uses the _perspective_ view, to give some depth to the game. But all game interactions happen on `z=0` making it a 2D game.
- Background is auto-generated.
- Enemies, except for the level's _boss_ are auto-generated.

### Star background.

The objective is to have a multi-layer star background that gives an impression of depth, we are in space after all!

The game has two star backgrounds that are children of the main camera, since the camera follows the player, the background also does. To make a _parallax_ effect, we can use the material's offset thus:

```c#
void Update()
{
    MeshRenderer mr = GetComponent<MeshRenderer>();
    Material material = mr.material;
    Vector2 offset = material.mainTextureOffset;
    offset.x = transform.position.x / transform.localScale.x * parallax;
    offset.y = transform.position.y / transform.localScale.y * parallax;
    material.mainTextureOffset = offset;
}
```

The `parallax` variable is editable from within the inspector, giving it different values, we can adjust the speed of each _layer_ until it feels adequate.

The star background was inspired by this [video tutorial][1].

### World navigation.

The game aims to be a side-scroller space-shooter with a twist, part of that twist is that the game takes place in an open world, I think that could let us add some interesting updates later on, for example, different missions inside one level.

At the moment, each level only has one mission, _find the boss and destroy it_, since the play takes place in an open world, we need to give the player some help accomplishing the task, some indication of wherein lays the _boss_, we can use an on-screen arrow that points in the bosses' direction to accomplish that. The result will look as follows:

// TODO screen capture with arrow here.

### Enemies.

The types of enemies that we will encounter on the level can be classified in two groups, regular enemies and bosses.

#### Regular enemies.

This enemies are spawned at random intervals by a _spawner_ game object that stays slightly ahead of the player as she moves through the level.

The spawner is slightly bigger than the screen height and spawns elements at random intervals, within a range, and at random points of the _quad_ element that it is made of. The spawning rate range can be adjusted from the editor to make levels progressively harder.

The _basic_ regular enemies are capsule shaped ships that do not fire any shots and just try to collide with the player character. This characters use the [Astar project][5] for pathfinding.

#### Bosses.

TODO fill this up.

### User interface.

#### Start menu.

At the start of the game, is good to offer a simple interface that entices users to start the game as quick as possible. I opted for a simple interface with two options, _play_ and _quit_ and some special effects, like a point light that moves around the scene to give the impression of time moving.

![Main menu](./res/main-menu.png)

#### Pause overlay.

During the development of this game, I learnt that [we can use `Time.timeScale`][8] as an easy way to pause any game activities that are related with game time, which in this game means all of the activities, effectively giving us a way to pause/restart the game using one line of code.

```c#
Time.timeScale = 0;
```

An overlay was used to present the user with the _pause_ options, namely **quit** and **start**.

![Pause Overlay](./res/pause-overlay.png)

The game is paused while the screen is visible. In **mobile** clicking the back button triggers the pause.

## Attributions.

### Assets.

The player's **space fighter** was crafted by [Devekros][3] and it is available at [this link][4] in the Unity Asset Store.  
The built in **UI elements** come from the [Techno Blue GUI Skin asset][9] on the asset store. The custom text was created following this [video tutorial][7].
Some of the asteroids come from the free [Asteroids Pack][11] by [Mark Dion][12] on the Unity Asset Store.

### References.

Scrolling star background [video tutorial][1].  
Cinemachine [video tutorial][2].  
Astar project [website][5].  
UI using _screen camera_ instead of _screen overlay_ [tutorial][6]. Removes clutter while developing.  
Nice gradient for menu text on this [tutorial][7].  
Using `Time.timeScale` to [pause the game][8].  
Touch controls [tutorial][10].

[1]: https://www.youtube.com/watch?v=nGw_UBJQPDY
[2]: https://www.youtube.com/watch?v=2jTY11Am0Ig
[3]: https://assetstore.unity.com/publishers/34228
[4]: https://assetstore.unity.com/packages/3d/vehicles/space/space-shuttle-of-the-future-111392
[5]: https://arongranberg.com/astar/
[6]: https://www.youtube.com/watch?v=VHFJgQraVUs
[7]: https://www.youtube.com/watch?v=zc8ac_qUXQY
[8]: https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
[9]: https://assetstore.unity.com/packages/2d/gui/techno-blue-gui-skin-19115
[10]: https://www.youtube.com/watch?v=bp2PiFC9sSs
[11]: https://assetstore.unity.com/packages/3d/environments/asteroids-pack-84988
[12]: https://assetstore.unity.com/publishers/27658
