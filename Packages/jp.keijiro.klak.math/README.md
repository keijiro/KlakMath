# KlakMath

`KlakMath` is an extension library for the [Unity Mathematics] package. It adds
tweening, noise, hashing, and rotation helpers for Unity projects.

[Unity Mathematics]:
  https://docs.unity3d.com/Packages/com.unity.mathematics@latest

## Features

### Interpolation

![gif](https://user-images.githubusercontent.com/343936/192008103-75173fe6-606b-4fb2-8d5c-b0f6cd971a4b.gif)

- `CdsTween`: critically damped spring interpolation
- `ExpTween`: exponential interpolation

### Noise

![gif](https://user-images.githubusercontent.com/343936/192008124-0ccfc896-89a5-4424-9bc5-130788d7b72f.gif)
![gif](https://user-images.githubusercontent.com/343936/192008130-cfa0426d-0624-4b04-877e-ac0048e5089a.gif)
![gif](https://user-images.githubusercontent.com/343936/192008133-aa1cc370-b800-41f7-aa71-a0d8b80775b9.gif)

- `Noise`: 1D gradient noise

### Pseudo-random number generator

![Screenshot](https://user-images.githubusercontent.com/343936/192008215-69c48c8d-4cf2-4cb1-9688-b9fed94aa41c.png)

- `XXHash`: fast hash function usable as a PRNG

### Rotation

- `FromTo`: rotation between two vectors

## How to Install

You can install the KlakMath package (`jp.keijiro.klak.math`) via the "Keijiro"
scoped registry using the Unity Package Manager. To add the registry to your
project, follow [these instructions].

[these instructions]:
  https://gist.github.com/keijiro/f8c7e8ff29bfe63d86b888901b82644c
