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

## Work done.

### Outline requirements.

- The player's space fighter can decelerate or accelerate in 2D, while there is no player input, the spacecraft preserves its current speed.
- Each level is an open world, the player can choose to get farther away from the objective.
- Each level has an objective. Survival should get progressively harder as the player approaches the objective.
- The camera uses the _perspective_ view, to give some depth to the game. But all game interactions happen on `z=0` making it a 2D game.
- Background is auto-generated.

## Attributions.

## Assets.

The player's space fighter was crafted by [Devekros](https://assetstore.unity.com/publishers/34228) and it is available at [this link](https://assetstore.unity.com/packages/3d/vehicles/space/space-shuttle-of-the-future-111392) in the Unity Asset Store.
