# Engine2D

A C# **ray tracing renderer** built from scratch on .NET 8 — despite the name, this is not a 2D game engine but a software (CPU) ray tracer that renders 3D scenes to PNG images. It was built as a school project (course `IMN236`, 3D rendering).

> Repo: [`cdoresca/Engine2D`](https://github.com/cdoresca/Engine2D)

## What it does

The engine builds a 3D scene (`World.cs`) made of geometric shapes and lights, casts rays through a chosen camera model, and writes the resulting image out as a `.png` file. Sample renders are included under `2D engine/Output/` (area lights, fisheye, pinhole, thin-lens, mirror reflections, a textured "Earth" sphere, etc.).

## Features

- **Camera models**: Orthographic, Pinhole, Thin-lens, Fisheye (`Camera/`)
- **Geometry**: Sphere, Plane, Cube, Cone, Cylinder, Disk, Triangle (`Figure/`)
- **Materials / BRDFs**: Matte, Phong, Mirror, Isotropic light, Textured matte, with underlying diffuse/specular BRDF implementations (`Materiel/`, `brdf/`)
- **Lighting**: Point lights and area lights, with direct illumination (`Lights/`, `Illumination/`)
- **Textures**: Constant color and image-based textures (`Texture/`)
- **Samplers**: Random and stratified sampling for antialiasing / soft shadows (`Sample/`)
- **Acceleration structure**: A spatial grid with bounding boxes for faster ray-object intersection tests (`Acceleration/`)
- **Custom linear algebra layer**: Points, vectors, normals, rays, matrices, and geometric transforms (`Algebre/`)
- **Ray tracing core**: Ray casting and a Whitted-style recursive tracer (`Trace/`)

## Project structure

```
2D engine/
├── Acceleration/     # Grid acceleration structure, bounding boxes
├── Algebre/           # Vector/point/matrix math, ray, geometric transforms
├── brdf/              # BRDF implementations (diffuse, specular, textured)
├── Camera/            # Orthographic, Pinhole, Thinlens, Fisheye cameras
├── Figure/             # Renderable shapes (sphere, plane, cube, cone, ...)
├── Illumination/      # Color model, direct illumination
├── Lights/             # Point light, area light
├── Materiel/           # Materials (matte, mirror, Phong, isotropic light, ...)
├── Output/             # Sample rendered PNG images
├── Sample/             # Random / stratified samplers
├── Texture/            # Constant and image textures
├── Trace/              # Intersection, ray casting, Whitted tracer
├── Image.cs            # Saves the rendered Bitmap to disk
├── Program.cs          # Entry point — builds the World and renders a scene
├── ViewPlane.cs         # Output image resolution / sampling settings
└── World.cs             # Scene setup: objects, lights, cameras, tracer
```

## Requirements

- **.NET 8 SDK**
- **`System.Drawing.Common`** NuGet package (already referenced in the `.csproj`, used for `Bitmap`/PNG output — this restricts the project to Windows unless a compatible cross-platform GDI+ implementation is available)

## Building & running

```bash
git clone https://github.com/cdoresca/Engine2D.git
cd Engine2D
dotnet build "2D engine.sln"
dotnet run --project "2D engine"
```

Or open `2D engine.sln` in Visual Studio 2022 and run the `2D engine` project directly.

By default, `Program.cs` builds the scene defined in `World.cs` and saves the render as `AreaLightPlan.png` inside the `Output/` folder next to the project.

## Customizing a render

Scenes are currently defined in code, in `World.Build()` / `World.AddObject()`:
- Add or transform shapes (`Sphere`, `Plan`, `Cube`, etc.) and assign a `Material`
- Add `PointLight` or `AreaLight` instances
- Pick a camera (`Orthographic`, `Pinhole`, `Thinlens`, `Fisheye`) and configure its position, look-at, and view-plane distance
- Adjust `ViewPlane` for output resolution and sampling (`plane.width`, `plane.height`, `plane.sampler`)

## Sample output

The `Output/` folder includes renders demonstrating different features: `Pinhole.png`, `Thinlens.png`, `Fisheye.png`, `Mirror.png`, `AreaLight.png`, `AreaLightPlan.png`, `AreaLightSphere.png`, and `Earth.png` (an image-textured sphere).

## Roadmap / possible next steps

- [ ] Parameterize scene definition (e.g. load from a scene file instead of hardcoding in `World.cs`)
- [ ] Add more BRDF/material types (refraction/glass, more complex glossy models)
- [ ] Multithreaded rendering for faster output
- [ ] Command-line options for output resolution, sample count, and file name
